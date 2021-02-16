using System.Collections.Generic;
using NovelDownloader.Domain.Enums;

namespace NovelDownloader.Domain.Aggregators
{
    public class BookMetadata
    {
        public string Provider { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public List<string> Categories { get; set; }
        public byte[] Cover { get; set; }
        public string CoverName { get; set; }
        
        public int TotalChapter { get; set; }
        public BookTypeEnum BookType { get; set; }
    }
}