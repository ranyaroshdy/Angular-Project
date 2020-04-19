using AngularProjectAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public class OrderDetailsRepository:IRepository<OrderDetails,int,string>
    {
        ApplicationDbContext Context;
        public OrderDetailsRepository(ApplicationDbContext _Context)
        {
            this.Context = _Context;
        }

        public void Add(OrderDetails orderDetails)
        {
            Context.OrderDetails.Add(orderDetails);
            Context.SaveChanges();
        }
        ///boolcol=>true/false///
        public void Delete(OrderDetails orderDetails)
        {
            orderDetails.IsCanceled = true;
            Context.OrderDetails.Update(orderDetails);
            Context.SaveChanges();
        }

        public IEnumerable<OrderDetails> GetAll()
        {
            return Context.OrderDetails.ToList();
        }

        public OrderDetails GetById(int id)
        {
            throw new Exception("Not Implemented");
        }

        public OrderDetails GetByName(string OrderName)
        {
            throw new Exception("Not Implemented");
        }

        public void Update(OrderDetails orderDetails)
        {
            Context.Entry(orderDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
