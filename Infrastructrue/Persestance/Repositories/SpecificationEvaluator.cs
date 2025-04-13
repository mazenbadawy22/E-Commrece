using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Persistence.Repositories
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>
            (
            IQueryable<T>InputQuery,
            Specfications<T> specfications
            ) where T : class
        {
            var query = InputQuery;
            if(specfications.Criteria is not null)
                query = query.Where(specfications.Criteria);
            //foreach(var item in specfications.IncludeExpressions)
            //{
            //    query=query.Include(item);
            //}

            query = specfications.IncludeExpressions.Aggregate
                (query, (CurrentQuery, includeExpression) => CurrentQuery.Include(includeExpression));
            if (specfications.OrderBy is not null)
                query = query.OrderBy(specfications.OrderBy);
            else if (specfications.OrderByDescinding is not null)
                query = query.OrderByDescending(specfications.OrderByDescinding);
            return query;
        }
    }
}
