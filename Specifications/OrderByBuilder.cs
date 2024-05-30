namespace Repository.Specifications
{
    public class OrderByBuilder<T> : IQueryBuilder<T> where T : class
    {
        private readonly IEnumerable<OrderBy<T>> _orderBys;

        public OrderByBuilder(IEnumerable<OrderBy<T>> orderBys)
        {
            if (orderBys == null || !orderBys.Any())
            {
                throw new ArgumentNullException(nameof(orderBys), "What are you doing?....");
            }
            
            _orderBys = orderBys;
        }

        public IQueryable<T> Build(IQueryable<T> query)
        {
            var first = true;
            IOrderedQueryable<T> orderedQuery = null;
            foreach (var orderBy in _orderBys)
            {
                if (first)
                {
                    orderedQuery = orderBy.OrderByEnum == OrderByEnum.Asc ? query.OrderBy(orderBy.Expression) : query.OrderByDescending(orderBy.Expression);
                    first = false;
                }
                else
                {
                    orderedQuery = orderBy.OrderByEnum == OrderByEnum.Asc ? orderedQuery.ThenBy(orderBy.Expression) : orderedQuery.ThenByDescending(orderBy.Expression);
                }
            }
            
            return orderedQuery;
        }
    }
}