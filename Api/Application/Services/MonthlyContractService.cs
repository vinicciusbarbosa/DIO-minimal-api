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
        public MonthlyContract AddMonthlyContract(MonthlyContractDTO dto)
        {
            var parkingSpot = _context.ParkingSpots.FirstOrDefault(s => s.Id == dto.ParkingSpotId);
    
            if (parkingSpot == null)
                throw new Exception("Parking spot not found.");

            if (parkingSpot.IsOccupied)
                throw new Exception($"Parking spot {parkingSpot.SpotNumber} is already occupied.");

            var vehicle = new Vehicle
            {
                Plate = dto.VehiclePlate,
                Brand = dto.VehicleBrand,
                Color = dto.VehicleColor,
                Year = dto.VehicleYear
            };

            var contract = new MonthlyContract
            {
                ParkingSpotId = parkingSpot.Id,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                MonthlyFee = dto.MonthlyFee,
                Vehicle = vehicle
            };

            parkingSpot.IsOccupied = true;
            parkingSpot.CurrentVehicle = vehicle;

            _context.MonthlyContracts.Add(contract);
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