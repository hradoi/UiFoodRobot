using System;

namespace CrawlerLibrary.Model
{
    public class OutputMenu
    {
        public OutputMenu()
        {

        }
        public int Id { get; set; }
        public string FoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Img { get; set; }
        public int Category { get; set; }
        public int Restriction { get; set; }
        public string Source { get; set; }
        public DateTime CrawlTime { get; set; }
    }
}
