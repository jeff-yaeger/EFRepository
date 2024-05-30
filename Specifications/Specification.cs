namespace Repository.Specifications
{
    using System.Linq.Expressions;

    public class Specification<T> : ISpecification<T> where T : class
    {
        private readonly List<IQueryBuilder<T>> _builders = new();

        public Specification()
        {
        }

        public Specification(Expression<Func<T, bool>> criteria)
        {
            _builders.Add(new CriteriaBuilder<T>(criteria));
        }

        public Specification(IExpression<T> expression)
        {
            _builders.Add(new CriteriaBuilder<T>(expression.GetExpression()));
        }

        public void AddIncludes(IEnumerable<Expression<Func<T, object>>> includeExpressions)
        {
            _builders.Add(new IncludesBuilder<T>(includeExpressions));
        }

        public void AddOrderBy(IEnumerable<OrderBy<T>> orderBys)
        {
            _builders.Add(new OrderByBuilder<T>(orderBys));
        }

        public void AddGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            _builders.Add(new GroupByBuilder<T>(groupByExpression));
        }

        public void AddQueryBuilder(IQueryBuilder<T> queryBuilder)
        {
            _builders.Add(queryBuilder);
        }

        public IQueryable<T> Build(IQueryable<T> query)
        {
            return _builders.Aggregate(query, (current, builder) => builder.Build(current));
        }
    }
}