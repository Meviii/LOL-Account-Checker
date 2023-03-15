using AccountChecker;
using AccountChecker.Utility;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountChecker.Tests;

public class MainTest
{ 
    public async Task TestRequest()
    {
        var runner = new Runner();
        var utility = new CoreUtility();
        //await runner.Job_ExecuteTasksWithoutAccountFetching("",
        //                                             "",
        //                                             utility.LoadFromSettingsFile());
        runner.CleanUp();

    }

}
