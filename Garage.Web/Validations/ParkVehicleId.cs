using Garage2._0.Models.ViewModels;
using Garage2._0.Services;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Validations
{
    public class ParkVehicleId : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var validation = validationContext.GetRequiredService<IValidationService>();

            const string errorMessage = "Failed";

            if (value is int input)
            {
                if (validationContext.ObjectInstance is ParkVehicleViewModel model)
                {
                    input = validation.GetSizeFromId(input);
                    return validation.ParkingSpaceExists(input) ? ValidationResult.Success : new ValidationResult(errorMessage);
                }

            }
            return new ValidationResult(errorMessage);

        }
    }
}
