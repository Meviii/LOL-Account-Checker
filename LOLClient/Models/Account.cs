using AccountChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountChecker.Models;

public class Account
{
    private static readonly Dictionary<string, string> regions = new() // Better to use ENUM
    {
        { "NA", "North America" },
        { "EUW", "Europe West" },
        { "RU", "Russia" },
        { "BR", "Brazil" },
        { "TR", "Turkey" },
        { "EUNE", "EU Nordic & East" },
        { "OC1", "Oceania" },
        { "LA2", "Latin America South" },
        { "LA1", "Latin America North" }
    };
    private string _region;

    public string SummonerName { get; set; } = "{NEW ACCOUNT}";
    public string Username { get; set; }
    public string Password { get; set; }
    public string Level { get; set; }
    public string Region
    { 

        get { return _region; }
        set {
            if (regions.ContainsKey(value))
            {
                _region = regions[value];
            }
            else
            {
                _region = value;
            }

        }
    }
    public string BE { get; set; } = "0";
    public string RP { get; set; } = "0";
    public string OE { get; set; } = "0";
    public List<Champion> Champions { get; set; } = new();
    public List<Skin> Skins { get; set; } = new();
    public string LastPlayDate { get; set; } = "Unknown";
    public bool ChatRestricted { get; set; }
    public bool LowPriorityQueue { get; set; }
    public List<Skin> HextechSkins { get; set; } = new();
    public bool IsEmailVerified { get; set; }
    public Rank CurrentRank { get; set; }
    public Rank HighestRank { get; set; }
    public HonorLevel HonorLevel { get; set; } = new();
    public List<Friend> Friends { get; set; } = new();
    public Account() { }

    public override string ToString()
    {
        return $"Summoner Name: {SummonerName}, Region: {Region}, BE: {BE}, RP: {RP}, Champs: {Champions.Count}, Skins: {Skins.Count}, Verified: {IsEmailVerified}";
    }
}
