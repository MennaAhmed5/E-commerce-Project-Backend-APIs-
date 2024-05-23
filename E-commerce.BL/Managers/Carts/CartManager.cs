using E_commerce.BL.Dtos.CartItems;
using E_commerce.BL.Dtos.Carts;
using E_commerce.BL.Dtos.Categories;
using E_commerce.BL.Dtos.Products;
using E_commerce.DAL.Data.Models;
using E_commerce.DAL.Unit_of_work;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.BL.Managers.Carts
{
    public class CartManager : ICartManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Cart AddCart(string userId)
        {
            Cart cart = new Cart
            {
                ApplicationUserId = userId,
                
            };
            _unitOfWork.CartRepository.Add(cart);
            _unitOfWork.SaveChanges();

            return cart;
        }

        public bool DeleteCart(string UserId)
        {
            var cart = _unitOfWork.CartRepository.GetCartByUserId(UserId);
            if (cart == null)
            {
                return false;
            }

            _unitOfWork.CartRepository.Delete(cart);
            _unitOfWork.SaveChanges();
            return true;
        }

        public CartWithItemsDto? GetUserCartWithItems(string userId)

        {
            Cart? cartFromDb = _unitOfWork.CartRepository.GetUserCartWithItems(userId);
            if (cartFromDb == null)
            {
                return null;
            }
            return new CartWithItemsDto
            {
                Id = cartFromDb.Id,
                ApplicationUserId = cartFromDb.ApplicationUserId,

                CartItems = cartFromDb.CartItems.Select(ci => new CartItemChildReadDto
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.TotalPrice,
                }),
 
            };
        }


        public CartWithItemsDto? AddToCart(AddCartItemDto addCartItemDto, string userId)
        {
            // Find cart by user id
            var cart = _unitOfWork.CartRepository.GetUserCartWithItems(userId);

            // If the cart does not exist, create a new one
            if (cart == null)
            {
                cart = AddCart(userId);
            }

            // Check if the product exists
            var product = _unitOfWork.ProductRepository.GetById(addCartItemDto.ProductId);
            if (product == null || addCartItemDto.Quantity==0)
            {
                return null;
            }

            // Check if the product already exists in the cart
            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == addCartItemDto.ProductId);

            if (existingCartItem != null)
            {
                // If the product exists in the cart, increase the quantity
                existingCartItem.Quantity = existingCartItem.Quantity + addCartItemDto.Quantity;
                _unitOfWork.CartItemRepository.Update(existingCartItem);
            }
            else
            {
                // If the product does not exist in the cart, create a new cart item
                var cartItem = new CartItem
                {
                    ProductId = addCartItemDto.ProductId,
                    Quantity = addCartItemDto.Quantity,
                    CartId = cart.Id,
                    UnitPrice = product.Price,
                };

                _unitOfWork.CartItemRepository.Add(cartItem);
            }

            // Save changes to the database
            _unitOfWork.SaveChanges();



            // Return the cart with its items
            return new CartWithItemsDto
            {
                Id = cart.Id,
                ApplicationUserId = cart.ApplicationUserId,
                CartItems = cart.CartItems.Select(ci => new CartItemChildReadDto
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.TotalPrice,
                })
            };
        }

            public bool RemoveItemFromCart(int productId , string userId)
            {
            var cart = _unitOfWork.CartRepository.GetCartByUserId(userId);
            if (cart == null)
            {
                return false;
            }

            // if cart exist try to find cart item by cartid  and productid

            var cartItem = _unitOfWork.CartItemRepository.GetCartItemByCartIdAndProductId(cart.Id, productId);
            //if cart item exist remove it
            if (cartItem == null)
            {
                return false;
            }

            _unitOfWork.CartItemRepository.Delete(cartItem);
            _unitOfWork.SaveChanges();

            return true;
        }

        

       public bool EditItemQuantityInCart(EditCartItemDto editCartItemDto , string userId)
        {
            var cart = _unitOfWork.CartRepository.GetCartByUserId(userId);

            if (cart == null)
            {
                return false;
            }
            //if cart exist try to find cart item
            var cartItem = _unitOfWork.CartItemRepository.GetCartItemByCartIdAndProductId(cart.Id, editCartItemDto.ProductId);

            //if cartitem not exist return false
            if (cartItem == null)
            {
                return false;
            }

            cartItem.Quantity = editCartItemDto.Quantity;
             _unitOfWork.SaveChanges();

            return true;
        }


    }

}
         
    

