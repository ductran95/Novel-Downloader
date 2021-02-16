using System;

namespace NovelDownloader.Domain.Extensions
{
    [Flags]
    public enum FontStyleEnum
    {
        Bold = 0,
        Italic = 1,
        Underline = 2,
        Strikethrough = 4
    }
}