using LOLClient;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tracer_WEB.ViewModels;

namespace Tracer_WEB.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly List<Thread> _threads;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("/{userName}")]
    public IActionResult RunSearch(string userName, string password)
    {
        _logger.LogInformation($"{userName}:{password}");
        if (userName == null || password == null)
        {
            return View();
        }

        Run(userName, password);

        return View(nameof(Index));
    }

    private void Run(string userName, string password)
    {
        // Runner
        Runner runner = new();

        bool didRiotSucceed = runner.RiotClientRunner(userName, password, "H:\\Riot Games\\Riot Client\\RiotClientServices.exe");

        if (!didRiotSucceed)
        {
            runner.CleanUp();
            return;
        }

        runner.LeagueClientRunner("H:\\Riot Games\\League of Legends\\LeagueClient.exe");
        runner.CleanUp();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}