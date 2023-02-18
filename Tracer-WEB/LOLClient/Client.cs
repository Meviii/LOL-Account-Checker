using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace LOLClient;

public class Client
{
    private readonly List<Process> _processes;
    private readonly ILogger _logger;

    public Client(ILogger logger)
    {
        _logger = logger;
        _processes = new();
    }

    public void CreateClient(List<string> processArgs, string path)
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

                return;
            }
            catch
            {
                Thread.Sleep(1000);
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
