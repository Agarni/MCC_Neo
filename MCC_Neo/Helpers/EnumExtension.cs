using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace MCC_Neo.Helpers
{
    public class EnumExtension : MarkupExtension
    {
        private readonly Type _enumType;

        public EnumExtension(Type enumType)
        {
            _enumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return (from object enumValue in Enum.GetValues(_enumType)
                    select new EnumMember { Value = enumValue, Description = ((Enum)enumValue).GetDescription() }).ToArray();
        }

        public class EnumMember
        {
            public string Description { get; set; }
            public object Value { get; set; }
        }
    }

    public static class EnumsExtension
    {
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}
