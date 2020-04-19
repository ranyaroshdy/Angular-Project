using AngularProjectAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public class CategoryRepository:IRepository<Category,int,string>
    {
        ApplicationDbContext Context;
        public CategoryRepository(ApplicationDbContext _Context)
        {
            this.Context = _Context;
        }

        public void Add(Category category)
        {
            Context.Categories.Add(category);
            Context.SaveChanges();
        }

        public void Delete(Category category)
        {
            category.IsDeleted = true;
            Context.Categories.Update(category);
            Context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return Context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return Context.Categories.Find(id);
        }

        public Category GetByName(string CategoryName)
        {
            return Context.Categories.Where(o => o.Name == CategoryName).FirstOrDefault();
        }

        public void Update(Category category)
        {
            Context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
