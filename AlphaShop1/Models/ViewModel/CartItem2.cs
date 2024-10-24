namespace AlphaShop1.Models.ViewModel
{
    public class CartItem2
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int SoLuong { get; set; }

        public double Total => SoLuong * Price;
    }
}
