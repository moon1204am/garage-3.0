using Garage.Web.Models.ViewModels;
using Garage.Web.Services;
using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Validations
{
    public class CheckLicenseNumber : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var validation = validationContext.GetRequiredService<IValidationService>();

            const string errorMessage = "License number already exists";

            if (value is string input)
            {
                if (validationContext.ObjectInstance is CreateVehicleViewModel model)
                {
                    return validation.LicenseNrExists(input) ? new ValidationResult(errorMessage) : ValidationResult.Success;
                }

            }
            return new ValidationResult(errorMessage);

        }
    }
}
