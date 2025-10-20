using AccountChecker;
using AccountChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AccountChecker.Tests;

/// <summary>
/// Test class for validating AccountQueue thread-safety and concurrent operations
/// </summary>
public class AccountQueueTest
{
    /// <summary>
    /// Tests basic enqueue and dequeue operations
    /// </summary>
    public void TestBasicOperations()
    {
        AccountQueue.Clear();
        
        var account1 = new AccountCombo { Username = "user1", Password = "pass1" };
        var account2 = new AccountCombo { Username = "user2", Password = "pass2" };
        
        AccountQueue.Enqueue(account1);
        AccountQueue.Enqueue(account2);
        
        var count = AccountQueue.Count();
        if (count != 2)
            throw new Exception($"Expected count 2, got {count}");
        
        if (AccountQueue.IsEmpty())
            throw new Exception("Queue should not be empty");
        
        AccountQueue.Dequeue(out var result1);
        if (result1.Username != "user1")
            throw new Exception($"Expected user1, got {result1.Username}");
        
        AccountQueue.Dequeue(out var result2);
        if (result2.Username != "user2")
            throw new Exception($"Expected user2, got {result2.Username}");
        
        if (!AccountQueue.IsEmpty())
            throw new Exception("Queue should be empty");
        
        Console.WriteLine("TestBasicOperations: PASSED");
    }
    
    /// <summary>
    /// Tests concurrent enqueue operations from multiple threads
    /// </summary>
    public async Task TestConcurrentEnqueue()
    {
        AccountQueue.Clear();
        const int ThreadCount = 10;
        const int ItemsPerThread = 100;
        
        var tasks = new List<Task>();
        for (int t = 0; t < ThreadCount; t++)
        {
            int threadId = t;
            tasks.Add(Task.Run(() =>
            {
                for (int i = 0; i < ItemsPerThread; i++)
                {
                    AccountQueue.Enqueue(new AccountCombo
                    {
                        Username = $"user_t{threadId}_i{i}",
                        Password = $"pass_t{threadId}_i{i}"
                    });
                }
            }));
        }
        
        await Task.WhenAll(tasks);
        
        var expectedCount = ThreadCount * ItemsPerThread;
        var actualCount = AccountQueue.Count();
        
        if (actualCount != expectedCount)
            throw new Exception($"Expected count {expectedCount}, got {actualCount}");
        
        Console.WriteLine("TestConcurrentEnqueue: PASSED");
    }
    
    /// <summary>
    /// Tests concurrent dequeue operations from multiple threads
    /// </summary>
    public async Task TestConcurrentDequeue()
    {
        AccountQueue.Clear();
        const int TotalItems = 1000;
        
        // Enqueue items
        for (int i = 0; i < TotalItems; i++)
        {
            AccountQueue.Enqueue(new AccountCombo
            {
                Username = $"user{i}",
                Password = $"pass{i}"
            });
        }
        
        var dequeuedCount = 0;
        var lockObj = new object();
        var tasks = new List<Task>();
        
        for (int t = 0; t < 10; t++)
        {
            tasks.Add(Task.Run(() =>
            {
                while (AccountQueue.Dequeue(out var account))
                {
                    lock (lockObj)
                    {
                        dequeuedCount++;
                    }
                    // Simulate some work
                    Thread.Sleep(1);
                }
                // Queue is empty, wait a bit and try one more time in case of race
                Thread.Sleep(5);
                while (AccountQueue.Dequeue(out var account))
                {
                    lock (lockObj)
                    {
                        dequeuedCount++;
                    }
                }
            }));
        }
        
        await Task.WhenAll(tasks);
        
        if (dequeuedCount != TotalItems)
            throw new Exception($"Expected {TotalItems} dequeued items, got {dequeuedCount}");
        
        if (!AccountQueue.IsEmpty())
            throw new Exception("Queue should be empty after all dequeues");
        
        Console.WriteLine("TestConcurrentDequeue: PASSED");
    }
    
    /// <summary>
    /// Tests mixed concurrent enqueue and dequeue operations
    /// </summary>
    public async Task TestMixedConcurrentOperations()
    {
        AccountQueue.Clear();
        const int EnqueueCount = 500;
        var dequeueSuccessCount = 0;
        var lockObj = new object();
        
        var enqueueTask = Task.Run(() =>
        {
            for (int i = 0; i < EnqueueCount; i++)
            {
                AccountQueue.Enqueue(new AccountCombo
                {
                    Username = $"user{i}",
                    Password = $"pass{i}"
                });
                Thread.Sleep(1); // Small delay to simulate realistic scenario
            }
        });
        
        var dequeueTasks = new List<Task>();
        for (int t = 0; t < 5; t++)
        {
            dequeueTasks.Add(Task.Run(() =>
            {
                // Continue while either queue has items OR enqueue task is still running
                while (true)
                {
                    if (AccountQueue.Dequeue(out var account))
                    {
                        lock (lockObj)
                        {
                            dequeueSuccessCount++;
                        }
                    }
                    else
                    {
                        // Queue is empty - check if we should continue
                        if (enqueueTask.IsCompleted && AccountQueue.IsEmpty())
                        {
                            break; // No more work to do
                        }
                        Thread.Sleep(5); // Wait a bit before retrying
                    }
                }
            }));
        }
        
        await Task.WhenAll(dequeueTasks);
        
        // Drain any remaining items
        while (AccountQueue.Dequeue(out var _))
        {
            lock (lockObj)
            {
                dequeueSuccessCount++;
            }
        }
        
        if (dequeueSuccessCount != EnqueueCount)
            throw new Exception($"Expected {EnqueueCount} successful dequeues, got {dequeueSuccessCount}");
        
        Console.WriteLine("TestMixedConcurrentOperations: PASSED");
    }
    
    /// <summary>
    /// Tests that dequeue returns false when queue is empty
    /// </summary>
    public void TestDequeueOnEmptyQueue()
    {
        AccountQueue.Clear();
        
        if (AccountQueue.Dequeue(out var account))
            throw new Exception("Dequeue should return false on empty queue");
        
        if (account != null)
            throw new Exception("Account should be null when dequeue fails");
        
        Console.WriteLine("TestDequeueOnEmptyQueue: PASSED");
    }
    
    /// <summary>
    /// Runs all tests
    /// </summary>
    public static async Task RunAllTests()
    {
        var test = new AccountQueueTest();
        
        try
        {
            Console.WriteLine("Running AccountQueue Tests...");
            Console.WriteLine("============================");
            
            test.TestBasicOperations();
            await test.TestConcurrentEnqueue();
            await test.TestConcurrentDequeue();
            await test.TestMixedConcurrentOperations();
            test.TestDequeueOnEmptyQueue();
            
            Console.WriteLine("============================");
            Console.WriteLine("All tests PASSED!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("============================");
            Console.WriteLine($"TEST FAILED: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
}
