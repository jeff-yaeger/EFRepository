namespace Repository.Specifications
{
    using System.Linq.Expressions;
    using EntityModels;

    public class ByIdSpec<TKey, T> : IExpression<T> where T : IEntity<TKey> where TKey: IEquatable<TKey>
    {
        private readonly TKey _id;

        public ByIdSpec(TKey id)
        {
            _id = id;
        }

        public Expression<Func<T, bool>> GetExpression()
        {
            return x => x.Id.Equals(_id);
        }
    }
}