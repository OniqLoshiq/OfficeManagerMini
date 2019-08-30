using OMM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OMM.App.Infrastructure.Attributes
{
    public class IsUniqueUserProperty : ValidationAttribute
    {
        private string property;
        private OmmDbContext context;

        public IsUniqueUserProperty(string propertyToValidate)
        {
            this.property = propertyToValidate;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            this.context = (OmmDbContext)validationContext.GetService(typeof(OmmDbContext));

            var propertyValues = this.context.Users.Select(u => u.GetType()
                                .GetProperty(this.property,
                                             BindingFlags.Instance |
                                             BindingFlags.Public |
                                             BindingFlags.IgnoreCase)
                                .GetValue(u, null))
                                .Select(v => v.ToString())
                                .ToList();

            if (propertyValues.Any(v => v == (string)value))
            {
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}

