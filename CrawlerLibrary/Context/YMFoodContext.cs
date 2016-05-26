using CrawlerLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerLibrary.Context
{
    public class YMFoodContext : DbContext
    {
        public YMFoodContext() : base()
        {

        }

        public DbSet<OutputMenu> OutputMenus { get; set; }
    }
}
