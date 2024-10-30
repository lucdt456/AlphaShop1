namespace AlphaShop1.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		public string Order_Code { get; set; }
		public string UserName { get; set; }
		public DateTime CreateDate { get; set; } = DateTime.Now;
		public int Status { get; set; }
	}
}
