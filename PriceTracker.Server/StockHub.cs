using Microsoft.AspNetCore.SignalR;

namespace PriceTracker.Server
{
    public class StockHub : Hub
    {
        public async Task SendPrice(List<Stock> stocks)
        {
            await Clients.All.SendAsync("ReceivePrice", stocks);
        }
    }
}
