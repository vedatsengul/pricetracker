using Microsoft.AspNetCore.SignalR;

namespace PriceTracker.Server
{
    public class StockPriceGenerator
    {
        public List<Stock> stocks;

        private readonly IHubContext<StockHub> _stockHub;
        private Timer _timer;

        public StockPriceGenerator(IHubContext<StockHub> stockHub)
        {
            _stockHub = stockHub;
            stocks = new List<Stock>();
            var lastUpdated = DateTime.UtcNow;
            stocks.AddRange(new List<Stock>() {
                new Stock() { Id = 1, Name = "Stock 1", Price = 105, LastUpdated = lastUpdated },
                new Stock() { Id = 2, Name = "Stock 2", Price = 115, LastUpdated = lastUpdated },
                new Stock() { Id = 3, Name = "Stock 3", Price = 125, LastUpdated = lastUpdated },
                new Stock() { Id = 4, Name = "Stock 4", Price = 135, LastUpdated = lastUpdated },
                new Stock() { Id = 5, Name = "Stock 5", Price = 145, LastUpdated = lastUpdated },
                new Stock() { Id = 6, Name = "Stock 6", Price = 155, LastUpdated = lastUpdated },
                new Stock() { Id = 7, Name = "Stock 7", Price = 165, LastUpdated = lastUpdated },
                new Stock() { Id = 8, Name = "Stock 8", Price = 175, LastUpdated = lastUpdated },
                new Stock() { Id = 9, Name = "Stock 9", Price = 185, LastUpdated = lastUpdated },
                new Stock() { Id = 10, Name = "Stock 10", Price = 195, LastUpdated = lastUpdated }});
        }

        public void Start()
        {
            _timer = new Timer(UpdateStockPrice, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        private void UpdateStockPrice(object state)
        {
            var lastUpdated = DateTime.UtcNow;

            foreach (var item in stocks)
            {
                var oldPrice = item.Price;
                var newPrice = new Random().Next(101, 199);

                if (oldPrice == newPrice)
                {
                    item.PriceChange = PriceChangeStatus.Unchanged;
                }
                else if (oldPrice > newPrice)
                {
                    item.PriceChange = PriceChangeStatus.Decreased;
                }
                else
                {
                    item.PriceChange = PriceChangeStatus.Increased;
                }

                item.Price = newPrice;

                item.LastUpdated = lastUpdated;
            }

            _stockHub.Clients.All.SendAsync("ReceivePrice", stocks);
        }
    }
}
