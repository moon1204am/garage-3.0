using Garage.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage.Web.Services
{
    public interface IVehicleTypeSelectListService
    {
        List<SelectListItem> GetVehicleTypes();
    }
}
