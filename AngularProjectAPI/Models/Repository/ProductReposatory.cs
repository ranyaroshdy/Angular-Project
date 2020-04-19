using AngularProjectAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public class ProductReposatory : IRepository<Product, int,string>
    {
        ApplicationDbContext Context;
        public ProductReposatory(ApplicationDbContext _Context)
        {
            this.Context = _Context;
        }

        public void Add(Product product)
        {
            Context.Products.Add(product);
            Context.SaveChanges();
        }

        public void Delete(Product product)
        {
            product.IsDeleted = true;
            Context.Products.Update(product);
            Context.SaveChanges();            
        }

        public IEnumerable<Product> GetAll()
        {
            return Context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return Context.Products.Find(id);
        }

        public Product GetByName(string title)
        {
            return Context.Products.Where(o => o.Title == title).FirstOrDefault();
        }

        public void Update(Product Object)
        {
            Context.Entry(Object).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
