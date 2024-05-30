namespace Repository.Specifications
{
    using System.Linq.Expressions;

    public class GroupByBuilder<T> : IQueryBuilder<T> where T : class
    {
        private readonly Expression<Func<T, object>> _groupBy;

        public GroupByBuilder(Expression<Func<T, object>> groupBy)
        {
            _groupBy = groupBy;
        }

        public IQueryable<T> Build(IQueryable<T> query)
        {
            return query.GroupBy(_groupBy).SelectMany(x => x);
        }
    }
}