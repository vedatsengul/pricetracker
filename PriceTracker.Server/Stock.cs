namespace PriceTracker.Server
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime LastUpdated { get; set; }
        public PriceChangeStatus PriceChange { get; set; }
    }

    public enum PriceChangeStatus
    {
        Increased = 0,
        Unchanged = 1,
        Decreased = 2
    }
}
