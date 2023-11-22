using Garage.Data.Data;
using Garage.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Garage.Web.Services
{
    public class VehicleTypeSelectListService : IVehicleTypeSelectListService
    {
        private readonly Garage2_0Context db;

        public VehicleTypeSelectListService(Garage2_0Context db)
        {
            this.db = db;
        }
        public List<SelectListItem> GetVehicleTypes()
        {
            return db.VehicleType
                .Select(t => new SelectListItem
                {
                    Text = t.Type.ToString(),
                    Value = t.VehicleTypeId.ToString()
                })
                .ToList();
        }
    }
}
