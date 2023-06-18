using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using AccountChecker.Connections;
using System.Linq;

namespace AccountChecker;

// A class that manages processes started by a client

public class Client
{
    // A thread-safe bag that stores the processes managed by the client
    private readonly ConcurrentBag<Process> _processes = new();

    // A static object used to lock the critical section
    private static readonly object _lock = new();

    // Constructor for the client
    public Client() { }

    // Starts a new process and adds it to the list of managed processes
    public int CreateClient(List<string> processArgs, string path)
    {
        // Lock the critical section to ensure thread safety
        lock (_lock)
        {
            // Create a new process object
            Process process = new();

            // Attempt to start the process and add it to the list of managed processes
            while (true)
            {
                try
                {
                    ProcessStartInfo processInfo = new()
                    {
                        FileName = path,
                        Arguments = string.Join(" ", processArgs),
                        UseShellExecute = false
                    };

                    process.StartInfo = processInfo;

                    Thread.Sleep(1000);

                    process.Start();

                    _processes.Add(process);

                    // Wait for the process to start before returning the process ID
                    Thread.Sleep(1000);

                    return process.Id;
                }
                // If there was an error starting the process, close all managed processes and throw an exception
                catch
                {
                    CloseClients();
                    throw new Exception("Wrong client path.");
                }
            }
        }
    }

    // Kills a specific managed process by process ID
    public void CloseClient(int processId)
    {
        // Lock the critical section to ensure thread safety
        lock (_lock)
        {
            foreach (Process process in _processes)
            {
                if (process.Id == processId)
                {
                    process.Kill();
                }
            }
        }
    }

    // Kills all managed processes
    public void CloseClients()
    {
        foreach (Process process in _processes)
        {
            process.Kill();
        }
    }
}
