using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC_Neo.Core.Helpers
{
    public static class ValidarModel
    {
        public static IEnumerable<ErrorInfo> GetErrors(object instance)
        {
            var metadataAttrib =
                instance.GetType().GetCustomAttributes(typeof(MetadataTypeAttribute), true).OfType
                    <MetadataTypeAttribute>().FirstOrDefault();
            var buddyClassOrModelClass = metadataAttrib != null ? metadataAttrib.MetadataClassType : instance.GetType();
            var buddyClassProperties = TypeDescriptor.GetProperties(buddyClassOrModelClass).Cast<PropertyDescriptor>();
            var modelClassProperties = TypeDescriptor.GetProperties(instance.GetType()).Cast<PropertyDescriptor>();

            return from buddyProp in buddyClassProperties
                   join modelProp in modelClassProperties on buddyProp.Name equals modelProp.Name
                   from attribute in buddyProp.Attributes.OfType<ValidationAttribute>()
                   where !attribute.IsValid(modelProp.GetValue(instance))
                   select new ErrorInfo(buddyProp.Name, attribute.FormatErrorMessage(string.Empty), instance);
        }

    }

    public class ErrorInfo
    {
        public string Propriedade { get; set; }
        public string MensagemErro { get; set; }
        public object Objeto { get; set; }

        public ErrorInfo(string propriedade, string mensagemErro, object objeto)
        {
            Propriedade = propriedade;
            MensagemErro = mensagemErro;
            Objeto = objeto;
        }
    }
}