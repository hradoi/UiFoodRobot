using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerLibrary.External
{
    class Details
    {
        public string[] Ingredients { get; set; }
        public string Nutrition { get; set; }
        public float Rating { get; set; }
        public Review[] Reviews { get; set; }
        public object IngredientTypes { get; set; }
        public string Service { get; set; }
        public int RatingsCount { get; set; }
    }
}
