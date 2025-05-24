namespace Solution1.Core.Interfaces.Specification.SpecParams
{
    // Pagination model 
    public class BasicPaginationParam
    {
        private const int MaxPageSize = 50;
        private int pageSize = 10;
        private bool print = false;

        public int PageSize
        {
            get { return print ? int.MaxValue : pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;

        public bool Print
        {
            get { return print; }
            set
            {
                print = value;
                // Reset pageSize to default when Print is turned off
                if (!print) pageSize = 10;
            }
        }
    }
}
