using System;

namespace CrawlerLibrary.External
{
    class Review
    {
        public string UserName { get; set; }
        public float Rating { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public object Details { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public string Id { get; set; }
    }
}
