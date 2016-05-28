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

            //Console.WriteLine("Testing Crawler");
            YellowFoodConstructor x = new YellowFoodConstructor();
            x.UpdateMenu();

            Console.WriteLine();
            Console.WriteLine("Testing query");

            foreach (var q in x.Query("porc"))
                Console.WriteLine(q.Name);

            Console.WriteLine("--End--");
            Console.ReadKey();
        }
    }
}