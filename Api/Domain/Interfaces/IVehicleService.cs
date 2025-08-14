using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using minimal_api.Api.Domain.Enums;
using minimal_api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IVehicleService
    {
        List<Vehicle> ListAllVehicles(int page = 1, string? nome = null, string? marca = null);
        List<Vehicle> ListAllVehiclesPerContractType(ContractType contractType, bool onlyActive = true, int page = 1);
        Vehicle? SearchById(int id);
        Vehicle? SearchByPlate(string plate);
    }
}