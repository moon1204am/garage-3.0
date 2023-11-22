using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage2._0.Services
{
    public interface IPersonVehiclesSelectListService
    {
        List<SelectListItem> GetPersonVehicles();
    }
}