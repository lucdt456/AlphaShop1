using Microsoft.AspNetCore.Identity;
namespace AlphaShop1.Models
{
	public class AppUserModel : IdentityUser
	{
		public string Occupation {  get; set; }
	}
}
