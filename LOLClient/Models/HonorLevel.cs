using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountChecker.Models;

public class HonorLevel
{

    public string Level { get; set; } = "0";
    public bool RewardsLocked { get; set; } = true;
    public string Checkpoint { get; set; }

    public override string ToString()
    {
        return $"Level: {Level}  Checkpoint: {Checkpoint}  RewardsLocked: {RewardsLocked}";
    }
}
