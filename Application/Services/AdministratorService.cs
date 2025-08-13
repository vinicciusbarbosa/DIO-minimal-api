using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.DTO;
using minimal_api.Domain.Entities;
using minimal_api.Domain.Interfaces;
using minimal_api.Infra.Db;

namespace minimal_api.Domain.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly AppDbContext _context;
        public AdministratorService(AppDbContext context)
        {
            _context = context;
        }

        public Administrator Create(Administrator administrator)
        {
            _context.Administrators.Add(administrator);
            _context.SaveChanges();
            return administrator;
        }

        public List<Administrator> ListAllAdministrators(int? page)
        {
            var query = _context.Administrators.AsQueryable();

            int itemsPerPage = 10;
            int currentPage = page ?? 1;

            query = query.Skip((currentPage - 1) * itemsPerPage)
                        .Take(itemsPerPage);

            return query.ToList();
        }

        public Administrator? Login(LoginDTO loginDTO)
        {
            var adm = _context.Administrators.Where(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password).FirstOrDefault();
            return adm;
        }

        public Administrator? SearchAdministratorById(int? id)
        {
            return _context.Administrators.Where(v => v.Id == id).FirstOrDefault();
        }
    }
}