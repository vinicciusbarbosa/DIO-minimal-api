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
        private readonly AppDbContext _context ;
        public AdministratorService(AppDbContext context)
        {
            _context = context;
        }
        public Administrator? Login(LoginDTO loginDTO)
        {
            var adm = _context.Administrators.Where(a => a.Email == loginDTO.Email && a.Password == loginDTO.Password).FirstOrDefault();
            return adm;
        }
    }
}