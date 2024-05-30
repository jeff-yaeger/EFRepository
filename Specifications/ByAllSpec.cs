namespace Repository.Specifications
{
    using System.Linq.Expressions;
    using EntityModels;

    public class ByAllSpec<T> : IExpression<T> where T : IEntity<int>
    {

        public ByAllSpec()
        {
        }

        public Expression<Func<T, bool>> GetExpression()
        {
            return x => x.Id > 0;
        }
    }
}