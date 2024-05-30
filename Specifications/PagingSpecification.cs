namespace Repository.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using EF;

    public class PagingSpecification<T> :Specification<T>, IPagingSpecification<T> where T:class
    {
        private readonly int _take;
        private readonly int _skip;
        
        public PagingSpecification(int pageNumber, int quantity)
        {
            (_skip, _take) = GetSkipAndTake(pageNumber, quantity);
        }
        
        public PagingSpecification(int pageNumber, int quantity, Expression<Func<T, bool>> criteria):base(criteria)
        {
            (_skip, _take) = GetSkipAndTake(pageNumber, quantity);
        }
        
        public PagingSpecification(int pageNumber, int quantity, IExpression<T> criteria):base(criteria)
        {
            (_skip, _take) = GetSkipAndTake(pageNumber, quantity);
        }

        public PagingDto<TResult> GetPagingDto<TResult>(int total) where TResult:class
        {
            return new PagingDto<TResult>( (int)Math.Ceiling(total /(double)_take), _skip <= 0 ? 1 : (_skip/_take)+1);
        }
        
        public IQueryable<T> AddPaging(IQueryable<T> query)
        {
            return query.Skip(_skip).Take(_take);
        }
        
        private static (int skip, int take) GetSkipAndTake(int pageNumber, int quantity)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var skip = quantity * (pageNumber - 1);
            var take = quantity;
            return (skip, take);
        }
    }
}
