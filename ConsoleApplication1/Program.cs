using System;
using CrawlerLibrary;

namespace ConsoleTester
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);

            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine("Before");

            YellowFoodConstructor x = new YellowFoodConstructor();
            x.UpdateMenu(true);

            Console.WriteLine();
            Console.WriteLine("Testing query");

            foreach (var q in x.SearchTodaysMenu(new string[]{ "porc", "pui" }))
                Console.WriteLine(q.Name);

            Console.WriteLine("--End--");
            Console.ReadKey();
        }
    }
}