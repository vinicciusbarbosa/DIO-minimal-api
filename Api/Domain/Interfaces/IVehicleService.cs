using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using minimal_api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IVehicleService
    {
        List<Vehicle> ListAllVehicles(int page = 1, string? nome = null, string? marca = null);
        Vehicle? SearchById(int id);
        void AddVehicle(Vehicle vehicle);
        void UpdateVehicle(Vehicle vehicle);
        void DeleteVehicle(Vehicle vehicle);
    }
}