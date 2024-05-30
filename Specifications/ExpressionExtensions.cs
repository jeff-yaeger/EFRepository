namespace Repository.Specifications
{
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return ProcessAnd(expr1, expr2);
        }

        public static Expression<Func<T, bool>> And<T>(this IExpression<T> expr1, IExpression<T> expr2)
        {
            return ProcessAnd(expr1.GetExpression(), expr2.GetExpression());
        }

        public static Expression<Func<T, bool>> And<T>(this IExpression<T> expr1, Expression<Func<T, bool>> expr2)
        {
            return ProcessAnd(expr1.GetExpression(), expr2);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, IExpression<T> expr2)
        {
            return ProcessAnd(expr1, expr2.GetExpression());
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return ProcessOr(expr1, expr2);
        }

        public static Expression<Func<T, bool>> Or<T>(this IExpression<T> expr1, IExpression<T> expr2)
        {
            return ProcessOr(expr1.GetExpression(), expr2.GetExpression());
        }

        public static Expression<Func<T, bool>> Or<T>(this IExpression<T> expr1, Expression<Func<T, bool>> expr2)
        {
            return ProcessOr(expr1.GetExpression(), expr2);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, IExpression<T> expr2)
        {
            return ProcessOr(expr1, expr2.GetExpression());
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> exp)
        {
            return ProcessNot(exp);
        }

        public static Expression<Func<T, bool>> Not<T>(this IExpression<T> exp)
        {
            return ProcessNot(exp.GetExpression());
        }

        private static Expression<Func<T, bool>> ProcessNot<T>(this Expression<Func<T, bool>> exp)
        {
            var parameter = Expression.Parameter(typeof(T));
            var expression = VisitExpression(exp, parameter);
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression), parameter);
        }

        private static Expression<Func<T, bool>> ProcessOr<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var (left, right) = GetExpressions(expr1, expr2, parameter);
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
        }

        private static Expression<Func<T, bool>> ProcessAnd<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));
            var (left, right) = GetExpressions(expr1, expr2, parameter);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        private static (Expression left, Expression right) GetExpressions<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2,
            ParameterExpression parameter)
        {
            var left = VisitExpression(expr1, parameter);
            var right = VisitExpression(expr2, parameter);
            return (left, right);
        }

        private static Expression VisitExpression<T>(Expression<Func<T, bool>> expression, ParameterExpression parameter)
        {
            var visitor = new ReplaceExpressionVisitor(expression.Parameters[0], parameter);
            return visitor.Visit(expression.Body);
        }
    }

    internal class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _newValue;
        private readonly Expression _oldValue;

        internal ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            return node == _oldValue ? _newValue : base.Visit(node);
        }
    }
}