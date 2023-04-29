using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountChecker.Models;

public class Friend
{

    [JsonProperty("id")]
    public string ChatServiceID { get; set; }
    

}
