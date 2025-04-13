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
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescinding { get; private set; }
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

    }
}
