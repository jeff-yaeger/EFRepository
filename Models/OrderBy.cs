namespace Repository.Specifications
{
    using System.Linq.Expressions;

    public class OrderBy<T>where T:class
    {
        public OrderBy(Expression<Func<T, object>> expression)
        {
            Expression = expression;
        }
        
        public OrderBy(Expression<Func<T, object>> expression, OrderByEnum orderByEnum)
        {
            Expression = expression;
            OrderByEnum = orderByEnum;
        }
        
        public OrderByEnum OrderByEnum { get; } = OrderByEnum.Asc;
        public Expression<Func<T, object>> Expression { get; }
    }
}