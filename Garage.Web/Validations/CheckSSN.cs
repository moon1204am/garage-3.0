using Garage2._0.Models.ViewModels;
using Garage2._0.Services;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Validations
{
    public class CheckSSN : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var validation = validationContext.GetRequiredService<IValidationService>();

            const string errorMessage = "SSN does not exist";

            if (value is string input)
            {
                if (validationContext.ObjectInstance is CreateVehicleViewModel or VehiclesOverviewViewModel)
                {
                    bool exists = validation.SSNExists(input);
                    return exists ? ValidationResult.Success : new ValidationResult(errorMessage);
                }

            }
            return new ValidationResult(errorMessage);
        }
    }
}
