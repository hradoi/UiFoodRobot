using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Data.Entity;
using System.Linq;
using CrawlerLibrary.Model;
using CrawlerLibrary.Context;

namespace ConsoleApplication1
{
    public class YellowConstructor
    {
        public List<OutputMenu> Menu { get; set; }
        public string Json { get; set; }
        
        // new database context
        
        //public class Person
        //{
        //    public int PersonId { get; set; }
        //    public string Name { get; set; }
        //    List<FoodItem> Order { get; set; }
        //}
        public void GetMenu()
        {
            WebRequest request = WebRequest.Create("https://www.yellow.menu/yellowserver/api/core/menus/activeMenus?ReturnDetails=false");
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            Menu = new List<OutputMenu>();
            IncomingFoodObject IncomingMenu = JsonConvert.DeserializeObject<IncomingFoodObject>(responseFromServer);
            using (var ctx = new YMFoodContext())
            {
                Console.WriteLine("In context");
                foreach (IncomingFoodObject.Dish d in IncomingMenu.Items[0].Dishes)
                {
                    Console.WriteLine($"Processing: {d.Title}");
                    OutputMenu fi = new OutputMenu()
                    {
                        FoodId = d.Id,
                        Name = d.Title,
                        Description = d.Description,
                        Price = d.Price.Value,
                        Img = d.Picture.Sources[0].Uri,
                        Source = "Yellow"
                    };
                    Menu.Add(fi);
                    ctx.OutputMenus.Add(fi);
                    ctx.SaveChanges();


                    Console.WriteLine($"    Finished processing {fi.Name}");
                }
            }
            reader.Close();
            response.Close();

            Json = JsonConvert.SerializeObject(Menu.ToArray());

            File.WriteAllText("../../path.txt", Json);


            using (var ctx = new YMFoodContext())
            {
                var supe = ctx.OutputMenus.Where(m => m.Name.Contains("supa")).ToList();
                foreach (var sup in supe)
                    Console.WriteLine($"{sup.Name} will cost {sup.Price}");
            }
        }

        

    }
}


