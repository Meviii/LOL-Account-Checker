using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace LOLClient;

class Program
{
    public static void Main(string[] args)
    {
        // Pre Update
        //Update update = new();
        //update.UpdateChampions();

        var THREADS_LENGTH = 1;
        Thread[] threads = new Thread[THREADS_LENGTH];

        // create and assign threads
        //for (int i = 0; i < THREADS_LENGTH; i++)
        //{
        threads[0] = new Thread(() => Work("misaluksmurf8", "ssaluk1967", 1));
        threads[0].Start();
        //threads[1] = new Thread(() => Work("mevismurf2", "ssaluk1967", 2));
        //threads[1].Start();
        //threads[2] = new Thread(() => Work("mevioce", "ssaluk1967", 3));
        //threads[2].Start();
        //threads[3] = new Thread(() => Work("misaluksmurf5", "codblackops2", 4));
        //threads[3].Start();
        //}

        // Join threads
        for (int i = 0; i < THREADS_LENGTH; i++)
        {
            threads[i].Join();
        }

    }

    static void Work(string username, string password, int threadNumber)
    {

        // Runner
        Runner runner = new();

        bool didRiotSucceed = runner.RiotClientRunner(username, password, "H:\\Riot Games\\Riot Client\\RiotClientServices.exe");

        if (!didRiotSucceed)
        {
            runner.CleanUp();
            return;
        }

        runner.LeagueClientRunner("H:\\Riot Games\\League of Legends\\LeagueClient.exe");
        runner.CleanUp();
        
        Console.WriteLine($"Thread number {threadNumber} completed.");
    }

}