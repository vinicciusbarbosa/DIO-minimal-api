using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;
using minimal_api.Infra.Db;

namespace minimal_api.Domain.Services
{   public class VehicleService : IVehicleService
    {
        private readonly AppDbContext _context;
        public VehicleService(AppDbContext context)
        {
            _context = context;
        }
        public void AddVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();
        }

        public Vehicle? SearchById(int id)
        {
            return _context.Vehicles.Where(v => v.Id == id).FirstOrDefault();
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

        public void UpdateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }
    }
}