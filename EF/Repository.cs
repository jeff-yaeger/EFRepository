namespace Repository.EF
{
    using System.Linq.Expressions;
    using EntityModels;
    using global::EntityDefaults;
    using Microsoft.EntityFrameworkCore;
    using Specifications;

    public class Repository<TKey, T> : IRepository<TKey, T> where T : class, IEntity<TKey> where TKey : IEquatable<TKey>
    {
        private readonly DbContext _context;
        private readonly IEntitySetter _setter;

        protected Repository(DbContext context, IEntitySetter setter)
        {
            _context = context;
            _setter = setter;
        }

        public void Add(T entity)
        {
            _setter.Set(new EntityTransaction { Entity = entity, State = TransactionState.Add });
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _setter.SetRange(entities.Select(x => new EntityTransaction { Entity = x, State = TransactionState.Add }));
            _context.Set<T>().AddRange(entities);
        }

        public async Task AddAsync(T entity)
        {
            Add(entity);
            await Complete();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            AddRange(entities);
            await Complete();
        }

        public IList<T> Find(ISpecification<T> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return specification.Build(_context.Set<T>()).ToList();
        }

        public IList<TResult> Find<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> select)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (select == null)
            {
                throw new ArgumentNullException(nameof(select));
            }

            return specification.Build(_context.Set<T>()).Select(select).ToList();
        }

        public IList<TResult> Find<TQuery, TResult>(ISpecification<TQuery> specification, Expression<Func<TQuery, TResult>> select) where TQuery : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (select == null)
            {
                throw new ArgumentNullException(nameof(select));
            }

            return specification.Build(_context.Set<TQuery>()).Select(select).ToList();
        }

        public async Task<T?> FindFirstAsync(ISpecification<T> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return await specification.Build(_context.Set<T>()).FirstOrDefaultAsync();
        }
        
        public async Task<TResult?> FindFirstAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> select)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (select == null)
            {
                throw new ArgumentNullException(nameof(select));
            }

            return await specification.Build(_context.Set<T>()).Select(select).FirstOrDefaultAsync();
        }
        
        public async Task<List<T>> FindAsync(ISpecification<T> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return await specification.Build(_context.Set<T>()).ToListAsync();
        }

        public async Task<IList<TResult>> FindAsync<TResult>(ISpecification<T> specification, Expression<Func<T, TResult>> select)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (select == null)
            {
                throw new ArgumentNullException(nameof(select));
            }

            return await specification.Build(_context.Set<T>()).Select(select).ToListAsync();
        }

        public async Task<IList<TResult>> FindAsync<TQuery, TResult>(ISpecification<TQuery> specification, Expression<Func<TQuery, TResult>> select) where TQuery : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (select == null)
            {
                throw new ArgumentNullException(nameof(select));
            }

            return await specification.Build(_context.Set<TQuery>()).Select(select).ToListAsync();
        }

        public PagingDto<T> Page(IPagingSpecification<T> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var query = specification.Build(_context.Set<T>());
            var pagingDto = specification.GetPagingDto<T>(query.Count());
            pagingDto.Data = specification.AddPaging(query).ToList();

            return pagingDto;
        }

        public async Task<PagingDto<T>> PageAsync(IPagingSpecification<T> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            var query = specification.Build(_context.Set<T>());
            var pagingDto = specification.GetPagingDto<T>(await query.CountAsync());
            pagingDto.Data = await specification.AddPaging(query).ToListAsync();

            return pagingDto;
        }

        public async Task<PagingDto<TResult>> PageAsync<TResult>(IPagingSpecification<T> specification, Expression<Func<T, TResult>> select)where TResult: class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }
            
            if (select == null)
            {
                throw new ArgumentNullException(nameof(select));
            }

            var query = specification.Build(_context.Set<T>());
            var pagingDto = specification.GetPagingDto<TResult>(await query.CountAsync());
            pagingDto.Data = await specification.AddPaging(query).Select(select).ToListAsync();

            return pagingDto;
        }

        public T GetById(TKey id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task RemoveAsync(T entity)
        {
            Remove(entity);
            await Complete();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            RemoveRange(entities);
            await Complete();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        public void Update(T entity)
        {
            _setter.Set(new EntityTransaction { Entity = entity, State = TransactionState.Update });
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            UpdateRange(entities);
            await Complete();
        }

        public async Task UpdateAsync(T entity)
        {
            Update(entity);
            await Complete();
        }

        public async Task<bool> ContainsAsync(ISpecification<T> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return await specification.Build(_context.Set<T>()).AnyAsync();
        }

        public async Task<bool> ContainsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).AnyAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            return await specification.Build(_context.Set<T>()).CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).CountAsync();
        }

        public TContext GetContext<TContext>() 
        {
            if (_context is TContext tContext)
            {
                return tContext;
            }

            return default;
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}