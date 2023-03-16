using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace AccountChecker;

public class Client
{
    private readonly ConcurrentBag<Process> _processes = new();
    private static readonly object _lock = new();
    public Client()
    {
    }


    public int CreateClient(List<string> processArgs, string path)
    {
        lock (_lock)
        {
            Process process = new();
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

                    process.Start();

                    _processes.Add(process);

                    Thread.Sleep(1000);

                    return process.Id;
                }
                catch
                {
                    CloseClients();
                    throw new Exception("Wrong client path.");
                }
            }
        }
    }

    public void CloseClient(int processId)
    {
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
    public void CloseClients()
    {
        foreach (Process process in _processes)
        {
            process.Kill();
        }
    }


}
