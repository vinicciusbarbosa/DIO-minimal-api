using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IMonthlyContractService
    {
        List<MonthlyContract> GetAllMonthlyContracts();
        MonthlyContract AddMonthlyContract(MonthlyContract monthlyContract);
        MonthlyContract UpdateMonthlyContract(MonthlyContract monthlyContract);
        MonthlyContract RemoveMonthlyContract(int id);
    }
}