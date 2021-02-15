using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NovelDownloader.Domain.Enums
{
    public enum EbookFormatEnum
    {
        [Display(Name = "docx")]
        Docx=1,
        [Display(Name = "epub")]
        Epub
    }
}