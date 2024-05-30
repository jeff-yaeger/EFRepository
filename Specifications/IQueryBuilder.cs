namespace Repository.Specifications
{
    public interface IQueryBuilder<T>
    {
        public IQueryable<T> Build(IQueryable<T> query);
    }
}