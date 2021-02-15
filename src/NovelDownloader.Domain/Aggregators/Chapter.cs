using System.Collections.Generic;

namespace NovelDownloader.Domain.Aggregators
{
    public class Chapter
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<string> Paragraphs { get; set; }
    }
}