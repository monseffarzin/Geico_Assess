using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GeicoData.Models
{

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
        {
            public string Field { get; set; }
            public string Values { get; set; }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                var rule = new ModelClientValidationRule
                {
                    ErrorMessage = ErrorMessage,
                    ValidationType = "requiredif"
                };
                rule.ValidationParameters["field"] = Field;
                rule.ValidationParameters["values"] = Values;
                yield return rule;
            }
            private const string DefaultErrorMessageFormatString = "The {0} field is required.";

            private bool IsValueRequired(string checkValue, Object currentValue)
            {
                if (checkValue.Equals("!null", StringComparison.InvariantCultureIgnoreCase))
                {
                    return currentValue != null;
                }
                return checkValue.Equals(currentValue.ToString());
            }

            protected override ValidationResult IsValid(Object value, ValidationContext context)
            {
                Object instance = context.ObjectInstance;
                System.Type type = instance.GetType();
                bool valueRequired = false;
                var ValuesArray = Values.Split('|').ToList();
                Object propertyValue = type.GetProperty(Field).GetValue(instance, null);
                for (int i = 0; i < ValuesArray.Count; i++)
                {
                    valueRequired = IsValueRequired(ValuesArray[i], propertyValue);
                    if (valueRequired) break;
                }

                if (valueRequired)
                {
                    return !string.IsNullOrEmpty(Convert.ToString(value))
                        ? ValidationResult.Success
                        : new ValidationResult(ErrorMessage != "" ? ErrorMessage
                                    : string.Format(DefaultErrorMessageFormatString, context.DisplayName));
                }
                return ValidationResult.Success;
            }
    }


}