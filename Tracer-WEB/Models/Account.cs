using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer_WEB.Models;

public class Account
{
    public string SummonerName { get; set; }

    public string Level { get; set; }
    public string Region { get; set; } // better to be enum
    public string BE { get; set; }
    public string RP { get; set; }
    public bool IsEmailVerified { get; set; }
    public List<Champion> Champions { get; set; }
    public List<Skin> Skins { get; set; }

    public Account() { }

}
