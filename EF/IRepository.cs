namespace Repository.EF
{
    using System.Linq.Expressions;
    using EntityModels;
    using Microsoft.EntityFrameworkCore;
    using Specifications;

    public interface IRepository<in TKey, T> where T : class, IEntity<TKey> where TKey : IEquatable<TKey>
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        IList<T> Find(ISpecification<T> specification);
        T GetById(TKey id);
        Task<List<T>> FindAsync(ISpecification<T> specification);
        Task<T> GetByIdAsync(TKey id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        Task UpdateAsync(T entity);
        Task<bool> ContainsAsync(ISpecification<T> specification);
        Task<bool> ContainsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(ISpecification<T> specification);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task Complete();
        PagingDto<T> Page(IPagingSpecification<T> specification);
        Task<PagingDto<T>> PageAsync(IPagingSpecification<T> specification);
        void UpdateRange(IEnumerable<T> entities);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        TContext GetContext<TContext>();
        IList<TResult> Find<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> select);
        IList<TResult> Find<TQuery, TResult>(ISpecification<TQuery> specification, Expression<Func<TQuery, TResult>> select) where TQuery : class;
        Task<IList<TResult>> FindAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> select);
        Task<IList<TResult>> FindAsync<TQuery, TResult>(ISpecification<TQuery> specification, Expression<Func<TQuery, TResult>> select) where TQuery : class;
        Task<T?> FindFirstAsync(ISpecification<T> specification);
        Task<TResult?> FindFirstAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> select);
        Task<PagingDto<TResult>> PageAsync<TResult>(IPagingSpecification<T> specification, Expression<Func<T, TResult>> select)where TResult: class;
    }
}