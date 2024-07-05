using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        IProductRepository Product { get; }

        IAppUserRepository AppUser { get; }

        IShoppingCartRepository ShoppingCart { get; }

        void Save();
    }
}
