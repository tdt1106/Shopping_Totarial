﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Shopping_Tutarial.Models;
using Shopping_Tutarial.Repository;
using System.Security.Claims;

namespace Shopping_Tutarial.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly DataContext _dataContext;
        private readonly IEmailSender _emailSender;

        public CheckoutController(IEmailSender emailSender, DataContext context)
		{
			_dataContext = context;
			_emailSender = emailSender;
		}

		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if(userEmail == null)
			{
				return RedirectToAction("Login","Account");
			}
			else
			{
				var ordercode = Guid.NewGuid().ToString();
				var orderItem = new OrderModel();
				orderItem.OrderCode = ordercode;
				orderItem.UserName = userEmail;
				orderItem.Status = 1;
				orderItem.CreatedDate = DateTime.Now;
				_dataContext.Add(orderItem);
				_dataContext.SaveChanges();

				List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				foreach( var cart in cartItems)
				{
					var orderdetails = new OrderDetails();
					orderdetails.UserName = userEmail;
					orderdetails.OrderCode = ordercode;
					orderdetails.ProductId = cart.ProductId;
					orderdetails.Price = cart.Price;
					orderdetails.Quantity = cart.Quantity;
					_dataContext.Add(orderdetails);
					_dataContext.SaveChanges();
				}
				HttpContext.Session.Remove("Cart");
				// Send mail order success
				var receiver = userEmail;
				var subject = "Đặt hàng thành công";
				var message = "Đặt hàng thành công, trải nghiệm dịch vụ nhé";

				await _emailSender.SendEmailAsync(receiver,subject, message);
				
				TempData["success"] = "Đơn hàng đã được tạo, vui lòng chờ duyệt đơn hàng nhé.";
				return RedirectToAction("Index", "Cart");
			}
			return View();
		}
	}
}
