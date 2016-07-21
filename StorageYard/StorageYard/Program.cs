using StorageYard.Data;
using StorageYard.FContext;
using StorageYard.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageYard
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderManager.Instance.EmptyDB();

            Menu menu = new Menu();
            menu.Name = "Test1";
            OrderManager.Instance.InsertMenu(menu);
            menu = new Menu();
            menu.Name = "Test2";
            OrderManager.Instance.InsertMenu(menu);
            menu = new Menu();
            menu.Name = "Test3";
            OrderManager.Instance.InsertMenu(menu);
            menu = new Menu();
            menu.Name = "Test4";
            OrderManager.Instance.InsertMenu(menu);

            foreach(Menu m in OrderManager.Instance.SelectMenu())
            {
                Console.WriteLine(m.Name);
            }

            OrderManager.Free();
            Console.ReadKey();
        }
    }
}
