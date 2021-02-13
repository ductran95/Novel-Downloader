using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NovelDownloader.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName<T>(this T enumValue) where T: Enum
        {
            var displayNameAtt = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayNameAttribute>();

            return displayNameAtt?.DisplayName ?? enumValue.ToString();
        }
    }
}