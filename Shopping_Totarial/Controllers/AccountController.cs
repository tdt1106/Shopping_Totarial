using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Shopping_Tutarial.Models;
using Shopping_Tutarial.Models.ViewModels;

namespace Shopping_Tutarial.Controllers
{   
	public class AccountController : Controller
	{
		private UserManager<AppUserModel> _userManage;
		private SignInManager<AppUserModel> _signInManager;
		private readonly IEmailSender _emailSender;
		public AccountController(IEmailSender emailSender, SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManage)
		{
			_signInManager = signInManager;
			_userManage = userManage;
			_emailSender = emailSender;
		}
		[HttpGet]   
		public IActionResult Login(string returnUrl)
		{

			return View(new LoginViewModel {  ReturnUrl = returnUrl});
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if(ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.Username, loginVM.Password, false, false);
				if(result.Succeeded)
				{
					return Redirect(loginVM.ReturnUrl ?? "/");
				}
				ModelState.AddModelError("", "Username hoặc Password bị sai");
			}	

			return View(loginVM);
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(UserModel user)
		{
			if(ModelState.IsValid)
			{
				AppUserModel newUser = new AppUserModel { UserName = user.Username, Email = user.Email };
				IdentityResult result = await _userManage.CreateAsync(newUser, user.Password); ;
				if(result.Succeeded)
				{
					TempData["success"] = "Tạo user thành công.";
					return Redirect("/account/login");
				}
				foreach(IdentityError error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}	
			}	
			return View(user);
		}
		public async Task<IActionResult> Logout(string returnUrl = "/")
		{
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}
	}
}
