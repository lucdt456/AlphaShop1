namespace AlphaShop1.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string OrderCode { get; set; }
		public string UserEmail { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public int Status { get; set; }
	}
}
