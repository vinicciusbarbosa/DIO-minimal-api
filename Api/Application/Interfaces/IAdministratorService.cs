using minimal_api.Domain.DTO;
using minimal_api.Domain.Entities;

namespace minimal_api.Domain.Interfaces
{
    public interface IAdministratorService
    {
        Administrator? Login(LoginDTO loginDTO);
        Administrator Create(Administrator administrator);
        Administrator? SearchAdministratorById(int? id);
        List<Administrator> ListAllAdministrators(int? page);
    }
}