using LOLClient.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LOLClient.Tasks;

public class HextechData
{
    private readonly Connection _connection;
    public HextechData(Connection connection)
    {
        _connection = connection;
    }

    private async Task PostRecipe(string recipeName, int repeat = 1)
    {
        await _connection.RequestAsync(HttpMethod.Post,
                                       $"/lol-loot/v1/recipes/{recipeName}/craft?repeat={repeat},",
                                       null);
    }

    public void OpenChests()
    {

    }

    public void OpenLoot()
    {

    }

    public void DisenchantEternalShards()
    {

    }

}
