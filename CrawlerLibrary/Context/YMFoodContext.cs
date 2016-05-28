using CrawlerLibrary.Model;
using System.Data.Entity;

namespace CrawlerLibrary.Context
{
    public class YMFoodContext : DbContext
    {
        public YMFoodContext(string connStr = "name=Default") : base(connStr) {
            this.Database.CreateIfNotExists();
        }
        public DbSet<OutputMenu> OutputMenus { get; set; }
    }
}
