using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularProjectAPI.Models;
using AngularProjectAPI.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<Order, int, string> OrderRepository;
        private readonly UserManager<User> UserManagerr;

        public OrdersController(IRepository<Order, int, string> _OrderRepository, UserManager<User> _UserManager)
        {
            OrderRepository = _OrderRepository;
            UserManagerr = _UserManager;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            IEnumerable<Order> orders = OrderRepository.GetAll();
            if (orders.Count() > 0)
                return orders.ToList();
            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = OrderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
        
        [Route("GetCurrentOrder/{Userid}")]
        [HttpGet]
        public ActionResult<int> GetCurrentOrder(string Userid)
        {
            var order = OrderRepository.GetSpesificOrderID(Userid);
            if (order == null)
            {
                return 0;
            }
            return order.OrderID;
        }
        
        [Route("GetTotalQuantity/{Userid}")]
        [HttpGet]
        public async Task<ActionResult<int>> GetTotalQuantity(string Userid)
        {
            var user= await UserManagerr.GetUserAsync(HttpContext.User);
            //var userIdd = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var sum = OrderRepository.GetTotalQuantity(Userid);
            return sum;
        }

        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }
            if (!OrderExists(id))
            {
                return NotFound();
            }
            OrderRepository.Update(order);
            return NoContent();
        }

        
        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            OrderRepository.Add(order);
            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }

        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteOrder(int id)
        {
            var order = OrderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            OrderRepository.Delete(order);
            return order;
        }

        private bool OrderExists(int id)
        {
            if (OrderRepository.GetById(id) == null)
                return false;
            return true;
        }
    }
}