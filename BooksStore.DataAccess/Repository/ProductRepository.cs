using BooksStore.DataAccess.Data;
using BooksStore.DataAccess.Repository.IRepository;
using BooksStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productObject = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productObject != null)
            {
                productObject.Title = product.Title;
                productObject.ISBN = product.ISBN;
                productObject.Price = product.Price;
                productObject.Price50 = product.Price50;
                productObject.ListPrice = product.ListPrice;
                productObject.Price100 = product.Price100;
                productObject.Description = product.Description;
                productObject.CategoryId = product.CategoryId;
                productObject.Author = product.Author;
				if (product.ImageUrl != null)
				{
					productObject.ImageUrl = product.ImageUrl;
				}
			}
        }
    }
}
