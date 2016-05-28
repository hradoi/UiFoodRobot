using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerLibrary.External
{
    internal class IncomingYellowJSON
    {
        public Item[] Items { get; set; }
        public string CurrentMenuId { get; set; }
    }
}
