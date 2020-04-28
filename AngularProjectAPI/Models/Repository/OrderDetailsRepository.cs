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
            OrderDetails orderDetailsExist= Context.OrderDetails.Where(o => o.OrderID == orderDetails.OrderID && o.ProductID == orderDetails.ProductID).FirstOrDefault();
            if (orderDetailsExist != null)
            {
                orderDetailsExist.Quantity += 1;
                Context.Update(orderDetailsExist);
                Context.SaveChanges();
            }
            else
            {
                Context.OrderDetails.Add(orderDetails);
                Context.SaveChanges();
            }
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

        public int GetProductQuantity(int productID, int orderID)
        {
            OrderDetails orderDetails= Context.OrderDetails.Where(o => o.ProductID == productID && o.OrderID == orderID).FirstOrDefault();
            if (orderDetails != null)
                return orderDetails.Quantity;
            return -1;
        }

        public OrderDetails GetSpesificOrderID(string userid)
        {
            throw new NotImplementedException();
        }

        public int GetTotalQuantity(string UserID)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDetails orderDetails)
        {
            Context.Entry(orderDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
