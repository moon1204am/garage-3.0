using AutoMapper.Configuration;
using Garage.Data.Data;
using Garage3._0.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0.Validations
{
    public class ParkVehicleType : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            var db = validationContext.GetRequiredService<Garage2_0Context>();

            const string errorMessage = "Failed";

            if (value is int input)
            {
                if (validationContext.ObjectInstance is CreateVehicleViewModel model)
                {
                    //service(input)
                    switch(input)
                    {
                        case 1: 
                            AddAirplane(db);
                            break;
                         case 2: 
                            AddBoat(db); 
                            break;
                         case 3: 
                            AddBus(db); 
                            break;
                          case 4 : 
                            AddCar(db); 
                            break;
                          case 5 : 
                            AddMotorcycle(db); 
                            break;
                    }
                    return ValidationResult.Success;
                }

            }
            return new ValidationResult(errorMessage);

        }

        private void AddMotorcycle(Garage2_0Context db)
        {
            throw new NotImplementedException();
        }

        private void AddCar(Garage2_0Context db)
        {
            throw new NotImplementedException();
        }

        private void AddBus(Garage2_0Context db)
        {
            throw new NotImplementedException();
        }

        private void AddBoat(Garage2_0Context db)
        {
            throw new NotImplementedException();
        }

        private void AddAirplane(Garage2_0Context db)
        {
            throw new NotImplementedException();
        }
    }
}
