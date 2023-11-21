using Garage.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Services
{
    public interface IVehicleTypeSelectListService
    {
        List<SelectListItem> GetVehicleTypes();
    }
}
