﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOLClient.Models;

public class Account
{
    public string SummonerName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Level { get; set; }
    public string Region { get; set; } // better to be enum
    public string BE { get; set; }
    public string RP { get; set; }
    public string OE { get; set; }
    public bool ChatRestricted { get; set; }
    public bool LowPriorityQueue { get; set; }
    public List<Skin> HextechSkins { get; set; } = new List<Skin>();
    public bool IsEmailVerified { get; set; }
    public List<Champion> Champions { get; set; } = new List<Champion>();
    public List<Skin> Skins { get; set; } = new List<Skin>();

    public Account() { }

}
