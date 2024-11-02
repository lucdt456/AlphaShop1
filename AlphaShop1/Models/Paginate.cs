namespace AlphaShop1.Models
{
	public class Paginate
	{
		public int TotalItems { get; private set; } //Tổng số items
		public int PageSize { get; private set; } //Tổng số item/trang
		public int CurrentPage { get; private set; } // Trang hiện tại
		public int TotalPages { get; private set; } // Tổng trang
		public int StartPage { get; private set; }
		public int MaxPagesToShow { get; set; }
		public int EndPage { get; private set; }


		public Paginate()
		{

		}

		public Paginate(int totalItems, int currentPage, int pageSize)
		{
			//Số trang
			int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

			int maxPagesToShow = 4;
			int halfPagesToShow = (int)(maxPagesToShow / 2);
			int startPage = currentPage - halfPagesToShow;
			int endPage = currentPage + halfPagesToShow;

			if (startPage < 1)
			{
				startPage = 1;
				endPage = startPage + maxPagesToShow - 1;

				if (endPage > totalPages)
				{
					endPage = totalPages;
				}
			}

			if (endPage > totalPages)
			{
				endPage = totalPages;
				startPage = endPage - maxPagesToShow + 1;
				if (startPage < 1)
				{
					startPage = 1;
				}
			}

			TotalItems = totalItems;
			CurrentPage = currentPage;
			PageSize = pageSize;
			TotalPages = totalPages;
			StartPage = startPage;
			EndPage = endPage;
			MaxPagesToShow = maxPagesToShow;

		}
	}
}
