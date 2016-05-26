using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Data.Entity;

/******************************************************************************
 * Helper function to download and parse the menu from Yellow.menu
 * 
 * Usage:
 *  x = new YellowConstructor()
 *  x.menu()
 * 
 ******************************************************************************/


namespace UiFoodRobot
{
    internal class YellowConstructor
    {
        public List<OutgoingFoodObject.FoodItem> Menu { get; set; }
        public string Json { get; set; }
        private class IncomingFoodObject
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
        // new database context
        internal class OutgoingFoodObject
        {
            public class FoodItem
            {
                public string FoodId { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
                public float Price { get; set; }
                public string Img { get; set; }
                public int Category { get; set; }
                public int Restriction { get; set; }
                public string Source { get; set; }

            }
            public class Person
            {
                public int PersonId { get; set; }
                public string Name { get; set; }
                List<FoodItem> Order { get; set; }
            }
        }
        public string GetMenu()
        {
            string returnMessage = "";

            WebRequest request = WebRequest.Create("https://www.yellow.menu/yellowserver/api/core/menus/activeMenus?ReturnDetails=false");
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            Menu = new List<OutgoingFoodObject.FoodItem>();
            IncomingFoodObject YellowMenu = JsonConvert.DeserializeObject<IncomingFoodObject>(responseFromServer);

            foreach (IncomingFoodObject.Dish d in YellowMenu.Items[0].Dishes)
            {
                returnMessage += $"Processing: {d.Title}\n";
                OutgoingFoodObject.FoodItem fi = new OutgoingFoodObject.FoodItem()
                {
                    FoodId = d.Id,
                    Name = d.Title,
                    Description = d.Description,
                    Price = d.Price.Value,
                    Img = d.Picture.Sources[0].Uri,
                    Source = "Yellow"
                };
                Menu.Add(fi);
                returnMessage += $"    Finished processing {fi.Name}\n";
            }

            reader.Close();
            response.Close();

            Json = JsonConvert.SerializeObject(Menu.ToArray());

            //File.WriteAllText("./path.txt", Json);
            return returnMessage;
        }
    }
}

