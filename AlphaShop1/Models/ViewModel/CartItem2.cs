namespace AlphaShop1.Models.ViewModel
{
    public class CartItem2
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int SoLuong { get; set; }

        public decimal Total => SoLuong * Price;
    }
}
