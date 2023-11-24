using Garage.Web.Models.ViewModels;
using Garage.Web.Services;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Validations
{
    public class ParkVehicleId : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var validation = validationContext.GetRequiredService<IValidationService>();

            const string errorMessage = "No parking space";

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
