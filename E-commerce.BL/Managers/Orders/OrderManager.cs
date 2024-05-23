using E_commerce.BL.Dtos.Orders;
using E_commerce.BL.Dtos.Products;
using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Unit_of_work;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Managers.Orders
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool PlaceOrder(string userId, List<OrderRequestDto> orderRequest)
        {
            // Try to find Cart

            var cart =  _unitOfWork.CartRepository.GetCartByUserId(userId);

            //If cart not found return false

            if (cart == null)
            {
                return false;
            }

            //create list of order Items

            var orderItems = new List<OrderItem>();


            //do for loop for each item in request body

            foreach (var item in orderRequest)
            {
                //fetch cartitem of product
                var cartItem = _unitOfWork.CartItemRepository.GetCartItemByCartIdAndProductId(cart.Id, item.ProductId);

                //if cart item exist create order item
                if (cartItem != null && cartItem.Quantity >= item.Quantity)
                {
                  
                    orderItems.Add(new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice =cartItem.UnitPrice,
                        Price = cartItem.TotalPrice
                    });

                    //if quantity in cart bigger than in order request decrease quantity

                    cartItem.Quantity -= item.Quantity;

                    // if quantity of cart item be 0  remove cart item
                    if (cartItem.Quantity == 0)
                    {
                        _unitOfWork.CartItemRepository.Delete(cartItem);
                    }

                }
                else
                {
                    return false;
                }
            }


               var order = new Order
               {
                  UserId = userId,
                  OrderItems = orderItems,
                  TotalOrderPrice = orderItems.Sum(oi => oi.UnitPrice*oi.Quantity)
               };

                _unitOfWork.OrderRepository.Add(order);
                _unitOfWork.SaveChanges();
                return true;

        }


        public IEnumerable<OrderHistoryDto> GetOrderHistory(string userId)
        {
            var ordersFromDB = _unitOfWork.OrderRepository.GetOrdersByUserId(userId);
            return ordersFromDB.Select(o => new OrderHistoryDto
            {
             Id = o.Id,
             TotalOrderPrice = o.TotalOrderPrice,
             OrderDate = o.OrderDate
       
         });

        }
    }
}
