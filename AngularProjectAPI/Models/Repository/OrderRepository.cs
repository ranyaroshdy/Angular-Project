using AngularProjectAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public class OrderRepository:IRepository<Order,int,string>
    {
        ApplicationDbContext Context;
        public OrderRepository(ApplicationDbContext _Context)
        {
            this.Context = _Context;
        }

        public void Add(Order order)
        {
            Context.Orders.Add(order);
            Context.SaveChanges();
        }

        public void Delete(Order order)
        {
            order.IsCanceled = true;
            Context.Orders.Update(order);
            Context.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            return Context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return Context.Orders.Find(id);
        }

        public Order GetByName(string OrderName)
        {
            throw new Exception("Not Implemented");
        }

        public void Update(Order order)
        {
            Context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
