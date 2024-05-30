namespace Repository.Specifications
{
    using System;
    using System.Linq.Expressions;

    public interface IExpression<T>
    {
        Expression<Func<T, bool>> GetExpression();
    }
}