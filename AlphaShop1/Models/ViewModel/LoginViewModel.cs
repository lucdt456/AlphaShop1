using System.ComponentModel.DataAnnotations;

namespace AlphaShop1.Models.ViewModel
{
	public class LoginViewModel
	{
		public int Id { get; set; }

		[Required (ErrorMessage ="Nhập UserName")]
		public string UserName { get; set; }

		[Required(ErrorMessage ="Nhập Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
