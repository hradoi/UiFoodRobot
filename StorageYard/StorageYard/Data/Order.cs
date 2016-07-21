using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageYard.Data
{
    public class Order
    {
        public int OrderId { set; get; }
        public string Name { set; get; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
