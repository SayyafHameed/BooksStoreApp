using BooksStore.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.DataAccess.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category {  get; private set; }
        public IProductRepository Product { get; private set; }
        public IAppUserRepository AppUser { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
            AppUser = new AppUserRepository(_context);
            ShoppingCart = new ShoppingCartRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
