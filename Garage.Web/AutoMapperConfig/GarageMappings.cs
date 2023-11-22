using AutoMapper;
using Garage.Domain.Entities;
using Garage.Web.Models.ViewModels;

namespace Garage.Web.AutoMapperConfig
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
