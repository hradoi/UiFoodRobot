using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UiFoodRobot
{
    public class OrderOrderMenuItem
    {
        public OrderOrderMenuItem(){}
        public int Id { get; set; }
        public string FoodId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Source { get; set; }
    }
    public class OrderContext : DbContext
    {
        public OrderContext(string connStr = "name=Default") : base(connStr)
        {
            this.Database.CreateIfNotExists();
        }
        public DbSet<OrderOrderMenuItem> OrderOrderMenuItems { get; set; }
    }
}
