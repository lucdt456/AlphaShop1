using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaShop1.Models
{
	public class OrderDetail
	{
		public string OrderCode { get; set; }
		[ForeignKey("OrderCode")]
		public OrderModel Order { get; set; }

		public int ProductId { get; set; }
		[ForeignKey("ProductId")]
		public ProductModel Product { get; set; }

        public decimal Price { get; set; }
        public int  Quantity { get; set; }
    }
}
