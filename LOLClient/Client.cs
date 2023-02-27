using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.Devices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace LOLClient;

public class Client
{
    private readonly List<Process> _processes;
    private readonly ILogger _logger;
    private readonly object _lock = new object();

    public Client(ILogger logger)
    {
        _logger = logger;
        _processes = new();
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

                    Thread.Sleep(2000);

                    return process.Id;
                }
                catch
                {
                    Thread.Sleep(1000);
                }

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
