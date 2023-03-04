using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System;

namespace LOLClient;

public class Client
{
    private readonly List<Process> _processes;

    public Client()
    {
        _processes = new();
    }

    public int CreateClient(List<string> processArgs, string path)
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

                Thread.Sleep(2000);

                return process.Id;
            }
            catch
            {
                CloseClients();
                throw new Exception("Wrong client path.");
            }
        }
    }

    public void CloseClient(int processId)
    {
        foreach (Process process in _processes)
        {
            if (process.Id == processId)
                process.Kill();
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
