using System.Collections.Generic;
using NovelDownloader.Domain.Extensions;

namespace NovelDownloader.Domain.Aggregators
{
    public class BookStyle
    {
        public string Name { get; set; }
        public AlignEnum Align { get; set; }
        public string Font { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
        public FontStyleEnum FontStyle { get; set; }
        
        /// <summary>
        /// Margin as CSS style
        /// </summary>
        public List<int> Margin { get; set; }
        
        /// <summary>
        /// Only available for image
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Only available for image
        /// </summary>
        public int Height { get; set; }
    }
}