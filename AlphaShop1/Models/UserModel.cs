using System.ComponentModel.DataAnnotations;

namespace AlphaShop1.Models
{
	public class UserModel
	{
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

		[Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
	}
}
