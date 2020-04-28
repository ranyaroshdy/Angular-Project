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
        public Order GetSpesificOrderID(string userid)
        {
            Order order = Context.Orders.Where(o => o.OrderOwnerID == userid && o.checkout == false).FirstOrDefault();
            if (order != null)
                return order;
            return null;
        }

        public int GetTotalQuantity(string UserID)
        {
            Order order = GetSpesificOrderID(UserID);
            if (order != null)
            {
                int sum=Context.OrderDetails.Where(o => o.OrderID == order.OrderID).Sum(o => o.Quantity);
                return sum;
            }
            return 0;
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

        public int GetProductQuantity(int ID1, int ID2)
        {
            throw new NotImplementedException();
        }

        public int GetTotalQuantity(int UserID)
        {
            throw new NotImplementedException();
        }
    }
}
