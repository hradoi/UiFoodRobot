using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerLibrary.Model
{
    internal class IncomingFoodObject
    {
        public Item[] Items { get; set; }
        public string CurrentMenuId { get; set; }

        public class Item
        {
            public DateTime Day { get; set; }
            public int MenuType { get; set; }
            public Dish[] Dishes { get; set; }
            public bool IsActive { get; set; }
            public Intervalcapacity[] IntervalCapacities { get; set; }
            public string Id { get; set; }
        }
        public class Dish
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
        public class Picture
        {
            public Source[] Sources { get; set; }
            public string Id { get; set; }
        }
        public class Source
        {
            public string Uri { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
            public string MimeType { get; set; }
            public string Extension { get; set; }
            public int TargetEnv { get; set; }
        }
        public class Chef
        {
            public string FullName { get; set; }
            public Picture Picture { get; set; }
            public object Details { get; set; }
            public string Id { get; set; }
        }
        public class Price
        {
            public float Value { get; set; }
            public int Currency { get; set; }
            public string Text { get; set; }
        }
        public class Details
        {
            public string[] Ingredients { get; set; }
            public string Nutrition { get; set; }
            public float Rating { get; set; }
            public Review[] Reviews { get; set; }
            public object IngredientTypes { get; set; }
            public string Service { get; set; }
            public int RatingsCount { get; set; }
        }
        public class Review
        {
            public string UserName { get; set; }
            public float Rating { get; set; }
            public DateTime Timestamp { get; set; }
            public string Description { get; set; }
            public object Details { get; set; }
            public bool IsActive { get; set; }
            public bool IsApproved { get; set; }
            public string Id { get; set; }
        }
        public class Dishtag
        {
            public string Name { get; set; }
            public Picture Picture { get; set; }
            public string Id { get; set; }
        }
        public class Intervalcapacity
        {
            public int IntervalIndex { get; set; }
            public string Name { get; set; }
            public int Capacity { get; set; }
        }
    }
}
