using System;

namespace CrawlerLibrary.External
{
    class Item
    {
        public DateTime Day { get; set; }
        public int MenuType { get; set; }
        public Dish[] Dishes { get; set; }
        public bool IsActive { get; set; }
        public Intervalcapacity[] IntervalCapacities { get; set; }
        public string Id { get; set; }
    }
}
