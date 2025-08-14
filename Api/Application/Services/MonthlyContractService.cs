using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public MonthlyContract AddMonthlyContract(MonthlyContract monthlyContract)
        {
             _context.Add(monthlyContract);

            if (!_context.Vehicles.Any(v => v.Id == monthlyContract.VehicleId))
                throw new Exception("The vehicle you entered does not exist.");

            if (monthlyContract.EntryTime < monthlyContract.ExitTime)
                throw new Exception("The exit time must be after the entry time.");

            if (_context.MonthlyContracts.Any(c => c.ParkingSpotId == monthlyContract.ParkingSpotId
                    && c.ExitTime > DateTime.Now))
                throw new InvalidOperationException("This spot is already occupied by another contract.");

            if (monthlyContract.EndDate > monthlyContract.StartDate.AddYears(1))
                throw new InvalidOperationException("The contract cannot be longer than 1 year");
    
            _context.SaveChanges();
            return monthlyContract;
        }

        public List<MonthlyContract> GetAllMonthlyContracts()
        {
            return _context.MonthlyContracts.ToList();
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

        public MonthlyContract UpdateMonthlyContract(MonthlyContract monthlyContract)
        {
            throw new NotImplementedException();
        }
    }
}