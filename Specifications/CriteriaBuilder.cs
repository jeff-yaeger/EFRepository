namespace Repository.Specifications
{
    using System.Linq.Expressions;

    public class CriteriaBuilder<T> : IQueryBuilder<T> where T : class
    {
        private readonly Expression<Func<T, bool>> _criteria;

        public CriteriaBuilder(Expression<Func<T, bool>> criteria)
        {
            _criteria = criteria;
        }

        public IQueryable<T> Build(IQueryable<T> query)
        {
            return query.Where(_criteria);
        }
    }
}