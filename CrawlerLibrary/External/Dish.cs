using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerLibrary.External
{
    class Dish
    {
        public bool IsVegetarian { get; set; }
        public bool IsServedCold { get; set; }
        public bool IsForAbstention { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Picture Picture { get; set; }
        public int DishType { get; set; }
        public object Tags { get; set; }
        public Dishtag[] DishTags { get; set; }
        public Chef Chef { get; set; }
        public int ItemsLeft { get; set; }
        public int Reserved { get; set; }
        public Price Price { get; set; }
        public string Weight { get; set; }
        public Details Details { get; set; }
        public string MenuId { get; set; }
        public int MenuIndex { get; set; }
        public bool IsTwoTiled { get; set; }
        public string Id { get; set; }
    }
}
