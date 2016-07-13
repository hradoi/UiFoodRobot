using System;
using CrawlerLibrary;
using CrawlerLibrary.Model;
using System.Collections.Generic;

namespace ConsoleTester
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);

            //AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine("Before");

            YellowFoodConstructor x = new YellowFoodConstructor();
            x.UpdateMenu();

            Console.WriteLine();
            Console.WriteLine("Testing or");

            foreach (var q in x.SearchTodaysMenu(new string[]{ "pui", "carne" }))
                Console.WriteLine(q.Name);

            Console.WriteLine();
            Console.WriteLine("Testing and");

            foreach (var q in x.SearchTodaysMenu(new string[] {"salata", "fattoush" }, or:false))
                Console.WriteLine(q.Name);

            Console.WriteLine();
            Console.WriteLine("Testing select");

            List<OutputMenu> y = x.GetTodaysMenu();
            List<OutputMenu> z = new List<OutputMenu>();
            foreach (var i in y)
                if (i.Name.Contains(" cu "))
                    z.Add(i);

            foreach (var i in y)
                Console.WriteLine(i.Name);
            Console.WriteLine();
            Console.WriteLine("Selected");
            
            foreach (var i in z)
                Console.WriteLine(i.Name);

            Console.WriteLine();
            Console.WriteLine("Testing or");

            foreach (var q in x.SearchFromSource(new string[] { "salata", "fresh" }, source: z))
                Console.WriteLine(q.Name);

            Console.WriteLine();
            Console.WriteLine("Testing and");

            foreach (var q in x.SearchFromSource(new string[] { "salata", "fattoush" }, source: z, or: false))
                Console.WriteLine(q.Name);

            Console.WriteLine("--End--");
            Console.ReadKey();
        }
    }
}