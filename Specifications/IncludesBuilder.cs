namespace Repository.Specifications
{
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

    public class IncludesBuilder<T> : IQueryBuilder<T> where T : class
    {
        private readonly IEnumerable<Expression<Func<T, object>>> _includes;

        public IncludesBuilder(IEnumerable<Expression<Func<T, object>>> includes)
        {
            _includes = includes;
        }

        public IQueryable<T> Build(IQueryable<T> query)
        {
            return _includes.Aggregate(query,
                (current, include) => current.Include(include));
        }
    }
}