using minimal_api.Api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IMonthlyContractService
    {
        List<MonthlyContract> GetAllMonthlyContracts();
        MonthlyContract AddMonthlyContract(MonthlyContractDTO contractDTO);
        MonthlyContract UpdateMonthlyContract(UpdateMonthlyContractDTO updatedContract, int id);
        bool RemoveMonthlyContract(int id);
    }
}