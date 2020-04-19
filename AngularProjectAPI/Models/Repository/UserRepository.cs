using AngularProjectAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public class UserRepository:IRepository<User,string,string>
    {
        ApplicationDbContext Context;
        public UserRepository(ApplicationDbContext _Context)
        {
            this.Context = _Context;
        }

        public void Add(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public void Delete(User user)
        {
            Context.Users.Remove(user);
            Context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return Context.Users.ToList();
        }

        public User GetById(string id)
        {
            return Context.Users.Find(id);
        }

        public User GetByName(string UserName)
        {
            return Context.Users.Where(o => o.UserName == UserName).FirstOrDefault();
        }

        public void Update(User user)
        {
            Context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
