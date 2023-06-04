using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountChecker.Models;

public class Rank
{
    private string _tier;
    public string Division { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int LeaguePoints { get; set; }

    public string Tier
    {

        get { return _tier; }
        set
        {
            _tier = NormaliseString(value);
        }
    }

    private string NormaliseString(string str)
    {
        if (str.IsNullOrEmpty())
            return str;

        var sb = new StringBuilder(str.ToLower());
        sb[0] = char.ToUpper(sb[0]);

        return sb.ToString();
    }

    public override string ToString()
    {
        if (Tier == "None" || Tier.IsNullOrEmpty())
            return "Unranked"; 

        if (Wins == 0 && Losses == 0)
            return $"{Tier} {Division}";

        return $"{Tier} {Division}, Wins: {Wins}, Losses: {Losses}";
    }
}
