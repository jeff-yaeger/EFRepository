namespace Repository.Specifications
{
    using System.Linq.Expressions;

    public interface ISpecification<T> :IQueryBuilder<T>where T : class
    {
        void AddIncludes(IEnumerable<Expression<Func<T, object>>> includeExpressions);
        void AddOrderBy(IEnumerable<OrderBy<T>> orderBys);
        void AddGroupBy(Expression<Func<T, object>> groupByExpression);
        void AddQueryBuilder(IQueryBuilder<T> queryBuilder);
        IQueryable<T> Build(IQueryable<T> query);
    }
}