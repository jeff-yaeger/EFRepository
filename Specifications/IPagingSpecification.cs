namespace Repository.Specifications
{
    using EF;

    public interface IPagingSpecification<T>:ISpecification<T> where T : class
    {
        IQueryable<T> AddPaging(IQueryable<T> query);
        PagingDto<TResult> GetPagingDto<TResult>(int total) where TResult : class;
    }
}