using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace LU4_Simple_Demo
{
    public class AgeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //check if there's any input
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Age is required");
            }

            //check if input is a number 
            if (!int.TryParse(value.ToString(), out int age))
            {
                return new ValidationResult(false, "Age must be a number");
            }

            if (age < 18)
            {
                return new ValidationResult(false, "Age must be 18 or older");
            }

            return ValidationResult.ValidResult;//age is 18 or older 
        }
    }
}
