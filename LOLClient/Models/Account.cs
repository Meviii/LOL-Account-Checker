using AccountChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOLClient.Models;

public class Account
{
    private static readonly Dictionary<string, string> regions = new()
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

    public string SummonerName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Level { get; set; }
    public string Region // better to be enum
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
    public string BE { get; set; }
    public string RP { get; set; }
    public string OE { get; set; }
    public List<Champion> Champions { get; set; } = new List<Champion>();
    public List<Skin> Skins { get; set; } = new List<Skin>();
    public bool ChatRestricted { get; set; }
    public bool LowPriorityQueue { get; set; }
    public List<Skin> HextechSkins { get; set; } = new List<Skin>();
    public bool IsEmailVerified { get; set; }
    public Rank CurrentRank { get; set; }
    public Rank HighestRank { get; set; }
    public Account() { }
}
