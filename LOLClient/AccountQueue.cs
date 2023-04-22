using AccountChecker.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AccountChecker;

// AccountQueue class represents a static class used for handling a queue of AccountCombo objects.

public static class AccountQueue
{
    // ConcurrentQueue to store AccountCombo objects.
    private static readonly ConcurrentQueue<AccountCombo> _queue = new();
    private static readonly object _lock = new();

    // Enqueues an AccountCombo object.
    public static void Enqueue(AccountCombo account)
    {
        lock (_lock)
        {
            _queue.Enqueue(account);
        }
    }

    // Dequeues an AccountCombo object.
    public static bool Dequeue(out AccountCombo account)
    {
        lock (_lock)
        {
            return _queue.TryDequeue(out account);
        }
    }

    // Checks if the queue is empty.
    public static bool IsEmpty()
    {
        lock (_lock)
        {
            return !_queue.TryPeek(out _);
        }
    }

    // Returns the number of elements in the queue.
    public static int Count()
    {
        lock (_lock)
        {
            return _queue.Count;
        }
    }

    public static void Clear()
    {
        lock (_lock)
        {
            while (_queue.TryDequeue(out _)) ;
        }
    }
}

