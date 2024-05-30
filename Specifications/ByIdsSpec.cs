namespace Repository.Specifications
{
    using System.Linq.Expressions;
    using EntityModels;

    public class ByIdsSpec<TKey, T> : IExpression<T> where T : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        private readonly List<TKey> _ids;

        public ByIdsSpec(List<TKey> ids)
        {
            _ids = ids;
        }

        public Expression<Func<T, bool>> GetExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}