using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;
using minimal_api.Infra.Db;
using minimal_api.Domain.Enums;
using minimal_api.Api.Domain.Enums;

namespace minimal_api.Domain.Services
{   public class VehicleService : IVehicleService
    {
        private readonly AppDbContext _context;
        public VehicleService(AppDbContext context)
        {
            _context = context;

        }

        public Vehicle? SearchById(int id)
        {
            return _context.Vehicles.Where(v => v.Id == id).FirstOrDefault();
        }

        public Vehicle? SearchByPlate(string plate)
        {
            return _context.Vehicles.Where(v => v.Plate == plate).FirstOrDefault();
        }

        public List<Vehicle> ListAllVehicles(int page = 1, string? name = null, string? brand = null)
        {
            var query = _context.Vehicles.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(v => EF.Functions.Like(v.Name.ToLower(), $"%{name.ToLower()}%"));

            }
            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(v => EF.Functions.Like(v.Brand.ToLower(), $"%{brand.ToLower()}%"));
            }

            int itemsPerPage = 10;

            query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            return query.ToList();
        }

        public List<Vehicle> ListAllVehiclesPerContractType(ContractType contractType, bool onlyActive = true)
        {
            throw new NotImplementedException();
        }
    }
}