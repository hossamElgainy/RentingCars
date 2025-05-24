namespace Core.Dtos.Logs
{
    public class PagedResult<T>
    {
        public PagedResult(List<T> Items,int TotalCount,int PageIndex,int PageSize)
        {
            this.Items = Items;
            this.TotalCount = TotalCount;
            this.PageNumber = PageIndex;
            this.PageSize = PageSize;
        }

        /// <summary>
        /// The items in the current page
        /// </summary>
        public List<T> Items { get; set; } = new List<T>();

        /// <summary>
        /// Total number of items across all pages
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Current page number (1-based)
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);

        /// <summary>
        /// Whether there's a previous page
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Whether there's a next page
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;
       
    }
}
