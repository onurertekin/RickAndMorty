namespace Contract.Request
{
    public class PaginatedRequest
    {
        protected PaginatedRequest()
        {
            pageNumber = 1;
            pageSize = 10;
            sortBy = "Id";
            sortDirection = "desc";
        }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string sortBy { get; set; }
        public string sortDirection { get; set; }
    }
}
