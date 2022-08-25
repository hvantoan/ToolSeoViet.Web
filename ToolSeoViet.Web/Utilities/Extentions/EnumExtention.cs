using System;
using System.ComponentModel;

namespace TuanVu.Management.Web.Utilities.Extentions {

    public static class EnumExtention {

        public static string Description(this Enum value) {
            var field = value.GetType().GetField(value.ToString());
            if (field == null) return "Chưa xác định";

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute is DescriptionAttribute ? ((DescriptionAttribute)attribute).Description : value.ToString();
        }
    }
}