using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Data.Entity;
using System.Linq;
using CrawlerLibrary.Model;
using CrawlerLibrary.Context;
using CrawlerLibrary.External;

namespace CrawlerLibrary
{
    public class YellowFoodConstructor
    {
        DateTime now = DateTime.Today;
        //public List<OutputMenu> Menu { get; set; }
        public List<OutputMenu> Query(string query)
        {
            using (var ctx = new YMFoodContext())
            {
                List<OutputMenu> today = ctx.OutputMenus.Where(m => m.CrawlTime.Day == now.Day).ToList<OutputMenu>();
                if (query == "")
                    return today;
                else
                    return today.Where(x => x.Name.ToLower().Contains(query.ToLower())).ToList<OutputMenu>();
            }
        }

        public void UpdateMenu(bool force = false)
        {
            using (var ctx = new YMFoodContext())
            {
                string source = "Yellow";

                OutputMenu test = ctx.OutputMenus.FirstOrDefault(p => true);  // Should change this though
                if (test == null)
                    force = true;
                else
                    Console.WriteLine(test.Name);
                if ((force) || (now.Day != test.CrawlTime.Day))
                {
                    if (force)
                        Console.WriteLine("Forcing update");
                    WebRequest request = WebRequest.Create("https://www.yellow.menu/yellowserver/api/core/menus/activeMenus?ReturnDetails=false");
                    WebResponse response = request.GetResponse();
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    //Menu = new List<OutputMenu>();
                    IncomingYellowJSON IncomingMenu = JsonConvert.DeserializeObject<IncomingYellowJSON>(responseFromServer);

                    foreach (Dish d in IncomingMenu.Items[0].Dishes)
                    {
                        Console.WriteLine($"Processing: {d.Title}");
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
                        //Menu.Add(fi);
                        ctx.OutputMenus.Add(fi);
                        Console.WriteLine($"    Finished processing {fi.Name}");
                    }
                    ctx.SaveChanges();
                    reader.Close();
                    response.Close();
                }
                else
                {
                    Console.WriteLine("DB already up to date!");
                }
            }
        }
    }
}


