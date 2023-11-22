using System.ComponentModel.DataAnnotations;

namespace Garage.Web.Validations
{
    //public class ParkVehicleType : ValidationAttribute
    //{
    //    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    //    {
    //        var validation = validationContext.GetRequiredService<IValidation>();
    //        const string errorMessage = "Failed";

    //        if (value is int input)
    //        {
    //            if (validationContext.ObjectInstance is CreateVehicleViewModel model)
    //            {
    //                input = validation.GetSizeFromType(input);
    //                return validation.ParkingSpaceExists(input) ? ValidationResult.Success : new ValidationResult(errorMessage);
    //            }
    //        }
    //        return new ValidationResult(errorMessage);

    //    }
    //}
}
