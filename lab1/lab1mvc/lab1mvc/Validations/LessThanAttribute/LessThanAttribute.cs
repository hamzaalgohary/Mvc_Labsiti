using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using lab1mvc.context;
using lab1mvc.Models;

namespace lab1mvc.Validations.LessThanAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]

    public class LessThanAttribute : ValidationAttribute
    {


        private readonly string _otherPropertyName;

        public LessThanAttribute(string otherPropertyName)
        {
            _otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = Convert.ToInt32(value);
            var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherProperty == null)
                return new ValidationResult($"Unknown property: {_otherPropertyName}");

            var otherValue = Convert.ToInt32(otherProperty.GetValue(validationContext.ObjectInstance));

            if (currentValue >= otherValue)
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be less than {_otherPropertyName}");

            return ValidationResult.Success;
        }

    }
}
