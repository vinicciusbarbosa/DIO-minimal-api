using minimal_api.Api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IMonthlyContractService
    {
        public List<MonthlyContract> ListMonthlyContracts(
            int page = 1,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int? parkingSpotId = null,
            string? vehiclePlate = null);
        MonthlyContract AddMonthlyContract(MonthlyContractDTO contractDTO);
        MonthlyContract UpdateMonthlyContract(UpdateMonthlyContractDTO updatedContract, int id);
        bool RemoveMonthlyContract(int id);
    }
}