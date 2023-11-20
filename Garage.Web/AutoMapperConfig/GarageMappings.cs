using AutoMapper;
using Garage.Domain.Entities;
using Garage3._0.Models.ViewModels;

namespace Garage2._0.AutoMapperConfig
{
    public class GarageMappings : Profile
    {
        public GarageMappings()
        {
            CreateMap<Vehicle, CreateVehicleViewModel>().
                ReverseMap();
        }
    }
}
