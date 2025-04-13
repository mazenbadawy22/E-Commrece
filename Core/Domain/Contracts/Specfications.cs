using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public abstract class Specfications<T> where T : class
    {
        public Expression<Func<T,bool>>? Criteria { get; }
        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();
        #region FilterAndSort
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescinding { get; private set; }
        #endregion
        #region Pagination
        public int Skip { get;  set; }
        public int Take { get; set; }
        public bool IsPaginated { get; set; }
        
        #endregion
        protected Specfications(Expression<Func<T,bool>>?  criteria)
        {
            Criteria = criteria;
        }
        protected void AddInclude(Expression<Func<T,object>> expression)
        
           => IncludeExpressions.Add(expression);
        protected void SetOrderBy(Expression<Func<T, object>> expression)

          => OrderBy = expression;
        protected void SetOrderByDescinding(Expression<Func<T, object>> expression)

          => OrderByDescinding = expression;
        protected void ApplyPagination(int PageIndex , int PageSize)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }
    }
}
