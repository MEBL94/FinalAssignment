using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FinalAssignment.Data.Interfaces;
using FinalAssignment.Models;
using FinalAssignment.Models.EmailLogic;
using FinalAssignment.Services;
using FinalAssignment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace FinalAssignment.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your card is empty, add some games first");
            }

            if (ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                //_shoppingCart.ClearCart();

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Computer Games Webshop", "computergameswebshop@gmail.com"));

                message.To.Add(new MailboxAddress("Computer Games Webshop", order.Email));

                message.Subject = "Order confirmed";

                message.Body = new TextPart("plain")
                {
                    Text = "Thanks for your order!"
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);

                    client.Authenticate("computergameswebshop@gmail.com", "mebltest");

                    client.Send(message);

                    client.Disconnect(true);
                }

                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            List<ShoppingCartItem> shoppingCartItems = _shoppingCart.GetShoppingCartItems();

            ShoppingCartViewModel svm = new ShoppingCartViewModel();
            svm.ShoppingCart = _shoppingCart;
            svm.ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal();

            _shoppingCart.ShoppingCartItems = shoppingCartItems;
            
            
            ViewBag.CheckoutCompleteMessage = "Thanks for your order! :) ";
            _shoppingCart.ClearCart();

            return View(svm);
        }
    }
}