using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Api.Domain.Entities;
using minimal_api.Api.Domain.Enums;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;
using minimal_api.Infra.Db;

namespace minimal_api.Application.Services
{
    public class MonthlyContractService : IMonthlyContractService
    {
        private readonly AppDbContext _context;
        public MonthlyContractService(AppDbContext context)
        {
            _context = context;

        }
        public List<MonthlyContract> GetAllMonthlyContracts()
        {
            return _context.MonthlyContracts.ToList();
        }
        public MonthlyContract AddMonthlyContract(MonthlyContractDTO contractDTO)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(v => v.Plate == contractDTO.Plate);
            if (vehicle == null)
                throw new Exception("Vehicle not found.");

            var contract = new MonthlyContract
            {
                VehicleId = vehicle.Id,
                Vehicle = vehicle,
                ParkingSpotId = contractDTO.ParkingSpotId,
                StartDate = contractDTO.StartDate,
                MonthlyFee = contractDTO.MonthlyFee,
                DiscountPercent = contractDTO.DiscountPercent,
                Active = true
            };

            _context.Add(contract);

            if (_context.MonthlyContracts.Any(c => c.ParkingSpotId == contract.ParkingSpotId && c.ExitTime > DateTime.Now))
                throw new InvalidOperationException("This spot is already occupied by another contract.");

            if (contract.EndDate > contract.StartDate.AddYears(1))
                throw new InvalidOperationException("The contract cannot be longer than 1 year");

            _context.SaveChanges();
            return contract;
        }

        public MonthlyContract UpdateMonthlyContract(UpdateMonthlyContractDTO updatedContract, int id)
        {
            var contract = _context.MonthlyContracts
                .Include(c => c.Vehicle)
                .FirstOrDefault(c => c.Id == id);

            if (contract == null)
                throw new Exception("Contract not found.");

           
            if (updatedContract.EndDate.HasValue)
                contract.EndDate = updatedContract.EndDate.Value;

            if (updatedContract.MonthlyFee.HasValue)
                contract.MonthlyFee = updatedContract.MonthlyFee.Value;

            if (updatedContract.DiscountPercent.HasValue)
                contract.DiscountPercent = updatedContract.DiscountPercent.Value;

            if (updatedContract.Active.HasValue)
                contract.Active = updatedContract.Active.Value;

            
            if (updatedContract.Vehicle != null)
            {
                if (updatedContract.Vehicle.Plate != null)
                    contract.Vehicle.Plate = updatedContract.Vehicle.Plate;

                if (updatedContract.Vehicle.Name != null)
                    contract.Vehicle.Name = updatedContract.Vehicle.Name;

                if (updatedContract.Vehicle.Brand != null)
                    contract.Vehicle.Brand = updatedContract.Vehicle.Brand;

                if (updatedContract.Vehicle.Color != null)
                    contract.Vehicle.Color = updatedContract.Vehicle.Color;

                if (updatedContract.Vehicle.Year.HasValue)
                    contract.Vehicle.Year = updatedContract.Vehicle.Year.Value;
            }

            _context.SaveChanges();
            return contract;
        }


        public MonthlyContract RemoveMonthlyContract(int id)
        {
            var contract = _context.MonthlyContracts.Find(id);
            if (contract == null)
                throw new Exception("Contract not found.");

            _context.MonthlyContracts.Remove(contract);
            _context.SaveChanges();

            return contract;
        }
    }
}