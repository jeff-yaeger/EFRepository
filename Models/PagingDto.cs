namespace Repository.EF
{
    public class PagingDto<T> where T : class
    {
        public PagingDto(int totalPages, int pageNumber)
        {
            TotalPages = totalPages;
            PageNumber = pageNumber;
        }
        
        public IList<T> Data { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
    }
}