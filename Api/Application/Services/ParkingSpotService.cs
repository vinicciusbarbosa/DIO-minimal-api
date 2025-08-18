using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using minimal_api.Domain.DTO;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;
using minimal_api.Infra.Db;

namespace minimal_api.Application.Services
{
    public class ParkingSpotService : IParkingSpotService
    {
        private AppDbContext _context;
        public ParkingSpotService(AppDbContext context)
        {
            _context = context;
        }

        public List<ParkingSpot> ListAllAvaliableParkingSpots()
        {
           var avaliableSpots = _context.ParkingSpots.Where(p => !p.IsOccupied).ToList();

            if (!avaliableSpots.Any())
                throw new Exception("No parking spots available.");

            return avaliableSpots;
        }

        public ParkingSpot AddParkingSpot(ParkingSpotDTO parkingSpotDTO)
        {
            var parkingSpot = new ParkingSpot
            {
                SpotNumber = parkingSpotDTO.SpotNumber,
                ContractType = parkingSpotDTO.ContractType,
                IsOccupied = false,
            };

            _context.ParkingSpots.Add(parkingSpot);
            _context.SaveChanges();

            return parkingSpot;
        }


        public ParkingSpot? UpdateParkingSpot(UpdateParkingSpotDTO updateParkingSpotDTO, int id)
        {
            var parkingSpot = _context.ParkingSpots.FirstOrDefault(p => p.Id == id);

            if (parkingSpot == null)
                return null;

            if (updateParkingSpotDTO.CurrentVehicleId.HasValue)
                parkingSpot.CurrentVehicleId = updateParkingSpotDTO.CurrentVehicleId;

            if (updateParkingSpotDTO.IsOccupied.HasValue)
                parkingSpot.IsOccupied = updateParkingSpotDTO.IsOccupied.Value;

            if (updateParkingSpotDTO.ContractType.HasValue)
                parkingSpot.ContractType = updateParkingSpotDTO.ContractType.Value;

            if (!string.IsNullOrEmpty(updateParkingSpotDTO.SpotNumber))
                parkingSpot.SpotNumber = updateParkingSpotDTO.SpotNumber;

            _context.SaveChanges();
            return parkingSpot;
        }

        public ParkingSpot? RemoveParkingSpot(int id)
        {
            var parkingSpot = _context.ParkingSpots.Find(id);

            if (parkingSpot == null)
                throw new InvalidOperationException("Spot not found.");

            _context.ParkingSpots.Remove(parkingSpot);
            _context.SaveChanges();
            return parkingSpot;
        }
    }
}