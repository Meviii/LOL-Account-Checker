
using AccountChecker.Connections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccountChecker.Tasks;

public class EventData
{
    private readonly LeagueConnection _leagueConnection;

    public EventData(LeagueConnection leagueConnection)
    {
        _leagueConnection = leagueConnection;
    }

    public async Task ClaimEventRewardsAsync()
    {
        var response = await _leagueConnection.RequestAsync(HttpMethod.Post, "/lol-event-shop/v1/lazy-load-data", null);

        if (!response.IsSuccessStatusCode)
            return;

        await _leagueConnection.RequestAsync(HttpMethod.Post, "/lol-event-shop/v1/claim-select-all", null);

    }

    public async Task BuyOffer(string offerId)
    {
        await _leagueConnection.RequestAsync(HttpMethod.Post,
                                            "/lol-event-shop/v1/purchase-offer",
                                            new Dictionary<string, object> { { "offerId", offerId } });

    }
}
