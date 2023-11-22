using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage.Web.Services
{
    public interface IPersonVehiclesSelectListService
    {
        List<SelectListItem> GetPersonVehicles();
    }
}