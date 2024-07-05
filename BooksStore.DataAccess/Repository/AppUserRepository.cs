using BooksStore.DataAccess.Data;
using BooksStore.DataAccess.Repository.IRepository;
using BooksStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.DataAccess.Repository
{
    public class AppUserRepository : Repository<ApplicationUser>, IAppUserRepository
    {
        private readonly ApplicationDbContext _context;
        public AppUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
