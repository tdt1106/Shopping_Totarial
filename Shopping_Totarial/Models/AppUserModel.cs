using Microsoft.AspNetCore.Identity;

namespace Shopping_Tutarial.Models
{
	public class AppUserModel : IdentityUser
	{
		public string Occupation { get; set; }
		public string RoleId { get; set; }
	}
}
