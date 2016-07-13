using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using CrawlerLibrary.Model;
using CrawlerLibrary.Context;
using CrawlerLibrary.External;
using LinqKit;

namespace CrawlerLibrary
{
    public class YellowFoodConstructor
    {
        DateTime now = DateTime.Today;

        public List<OutputMenu> GetTodaysMenu()
        {
            using (var ctx = new YMFoodContext())
                return ctx.OutputMenus.Where(m => m.CrawlTime.Day == now.Day).ToList();
        }

        public OutputMenu[] SearchFromSource(string[] keywords, List<OutputMenu> source, bool or = true)
        {
            //List<OutputMenu> query = source.Where(m => m.CrawlTime.Day == now.Day).ToList();

            var predicate = or ? PredicateBuilder.False<OutputMenu>() : PredicateBuilder.True<OutputMenu>();

            foreach (string keyword in keywords)
            {
                string temp = keyword;
                predicate = (or ? predicate.Or(p => p.Name.ToLower().Contains(temp)) : predicate.And(p => p.Name.ToLower().Contains(temp)));
            }
            return source.AsQueryable().Where(predicate).ToArray();
        }

        public OutputMenu[] SearchTodaysMenu(string[] keywords, bool or = true)
        {
            using (var ctx = new YMFoodContext())
            {
                IQueryable<OutputMenu> query = ctx.OutputMenus.Where(m => m.CrawlTime.Day == now.Day);
                var predicate = or ? PredicateBuilder.False<OutputMenu>() : PredicateBuilder.True<OutputMenu>();

                foreach (string keyword in keywords)
                {
                    string temp = keyword;
                    predicate = (or ? predicate.Or(p => p.Name.ToLower().Contains(temp)) : predicate.And(p => p.Name.ToLower().Contains(temp)));
                }

                return ctx.OutputMenus.AsExpandable().Where(predicate).ToArray();
            }
        }

        public void UpdateMenu(bool force = false)
        {
            using (var ctx = new YMFoodContext())
            {
                string source = "Yellow";
                OutputMenu testItem = ctx.OutputMenus.OrderByDescending(p => p.CrawlTime).FirstOrDefault();

                if (testItem == null)
                    force = true;
                else
                    Console.WriteLine($"Database is updated for today, with the first item being {testItem.Name}");

                if ((force) || (now.Day != testItem.CrawlTime.Day)) // tested
                {
                    if (force)
                        Console.WriteLine("Forcing update...");

                    WebRequest request = WebRequest.Create("https://www.yellow.menu/yellowserver/api/core/menus/activeMenus?ReturnDetails=false");
                    WebResponse response = request.GetResponse();
                    Console.WriteLine($"Connection status: {((HttpWebResponse)response).StatusDescription}");
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    IncomingYellowJSON IncomingMenu = JsonConvert.DeserializeObject<IncomingYellowJSON>(responseFromServer);

                    foreach (Dish d in IncomingMenu.Items[0].Dishes)
                    {
                        Console.WriteLine($"Starting: {d.Title}");
                        OutputMenu fi = new OutputMenu()
                        {
                            FoodId = d.Id,
                            Name = d.Title,
                            Description = d.Description,
                            Price = d.Price.Value,
                            Img = d.Picture.Sources[0].Uri,
                            Source = source,
                            CrawlTime = now
                        };
                        ctx.OutputMenus.Add(fi);
                        Console.WriteLine($"   - Finished processing {fi.Name}");
                    }
                    ctx.SaveChanges();
                    reader.Close();
                    response.Close();
                }
                else
                {
                    Console.WriteLine("Database is already up to date!");
                }
            }
        }
    }
}