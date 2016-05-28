using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerLibrary.External
{
    class Source
    {
        public string Uri { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public string MimeType { get; set; }
        public string Extension { get; set; }
        public int TargetEnv { get; set; }
    }
}
