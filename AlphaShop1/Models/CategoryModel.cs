using System.ComponentModel.DataAnnotations;

namespace AlphaShop1.Models
{
	public class CategoryModel
	{
		[Key]
		public int Id { get; set; }

		[Required, MinLength(4,ErrorMessage ="Yêu cầu nhập tên Danh Mục")]
		public string Name { get; set; }

        [Required, MinLength(4, ErrorMessage = "Yêu cầu mô tả Danh Mục")]
        public string  Description { get; set; }

		[Required]
        public string Slug { get; set; }

		public int Status { get; set; }
	}
}
