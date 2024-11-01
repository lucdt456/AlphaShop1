namespace AlphaShop1.Models
{
	public class Paginate
	{
		public int TotalItems { get; private set; } //Tổng số items
		public int PageSize { get; private set; } //Tổng số item/trang
		public int CurrentPage { get; private set; } // Trang hiện tại
		public int TotalPages { get; private set; } // Tổng trang
		public int StartPage { get; private set; }
		public int EndPage { get; private set; }


		public Paginate()
		{

		}

		public Paginate(int totalItems, int currentPage, int pageSize = 9)
		{
			//Số trang
			int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

			int startPage = currentPage - 5;
			int endPage = startPage + 4;

			if (startPage < 0)
			{
				endPage = endPage - (startPage - 1);
				startPage = 1;
			}

			if(endPage >totalPages)
			{
				startPage = endPage - 9;
			}

			TotalItems = totalItems;
			CurrentPage = currentPage;
			PageSize = pageSize;
			TotalPages = totalPages;
			StartPage = startPage;
			EndPage = endPage;

		}
	}
}
