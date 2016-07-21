using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageYard.FContext;
using StorageYard.Data;
using StorageYard.Repo;

namespace StorageYard.Manager
{
    public class OrderManager : IDisposable
    {
        private static volatile OrderManager instance;
        private static object syncRoot = new Object();

        private GRepo<Context> repo;
        private OrderManager() {
            repo = new GRepo<Context>();
        }

        public void Dispose()
        {
            repo.Dispose();
        }

        public static void Free()
        {
            if(instance != null)
            {
                instance.Dispose();
            }
        }

        public static OrderManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new OrderManager();
                    }
                }

                return instance;
            }
        }

        public Menu InsertMenu(Menu menu, bool save = true)
        {
            return repo.Insert<Menu>(menu, save);
        }
        public Item InsertItem(Item item, bool save = true)
        {
            return repo.Insert<Item>(item, save);
        }
        public Order InsertOrder(Order order, bool save = true)
        {
            return repo.Insert<Order>(order, save);
        }


        public Menu UpdateMenu(Menu menu, bool save = true)
        {
            return repo.Update<Menu>(menu, save);
        }
        public Item UpdateItem(Item item, bool save = true)
        {
            return repo.Update<Item>(item, save);
        }
        public Order UpdateOrder(Order order, bool save = true)
        {
            return repo.Update<Order>(order, save);
        }


        public Menu DeleteMenu(Menu menu, bool save = true)
        {
            return repo.Delete<Menu>(menu, save);
        }
        public Item DeleteItem(Item item, bool save = true)
        {
            return repo.Delete<Item>(item, save);
        }
        public Order DeleteOrder(Order order, bool save = true)
        {
            return repo.Delete<Order>(order, save);
        }

        public IQueryable<Menu> SelectMenu()
        {
            return repo.Select<Menu>();
        }
        public IQueryable<Item> SelectItem()
        {
            return repo.Select<Item>();
        }
        public IQueryable<Order> SelectOrder()
        {
            return repo.Select<Order>();
        }

        public void EmptyDB()
        {
            foreach(Menu m in SelectMenu())
            {
                DeleteMenu(m,false);
            }
            foreach(Item i in SelectItem())
            {
                DeleteItem(i,false);
            }
            foreach(Order o in SelectOrder())
            {
                DeleteOrder(o,false);
            }
            SaveDB();
        }


        public int SaveDB()
        {
            return repo.Save();
        }
    }
}
