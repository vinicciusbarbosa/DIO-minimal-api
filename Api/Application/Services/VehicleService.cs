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
using System.Text.RegularExpressions;

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
            if (string.IsNullOrWhiteSpace(plate))
                return null;

            plate.Trim().ToUpper();

            if (!Regex.IsMatch(plate, @"^[A-Z]{3}-\d{4}$"))
                return null;
            
             return _context.Vehicles.SingleOrDefault(v => v.Plate == plate);
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

        public List<Vehicle> ListAllVehiclesPerContractType(ContractType contractType, bool onlyActive = true, int page = 1)
        {
            IQueryable<Vehicle> query;

            if (onlyActive)
            {
                query = _context.Vehicles
                .Where(v => v.Contracts.Any(c => c.ContractType == contractType && c.Active == true));
            }
            else
            {
                query = _context.Vehicles
                .Where(v => v.Contracts.Any(c => c.ContractType == contractType && !c.Active));
            }
            int itemsPerPage = 10;
            query = query.AsNoTracking()
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage);
            return query.ToList();
        }
    }
}