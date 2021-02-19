using System;
using System.ComponentModel;
using System.Linq;

namespace Zamin.Utilities.Extentions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attributes = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)attributes.FirstOrDefault()).Description;
            return description;
        }
    }
}