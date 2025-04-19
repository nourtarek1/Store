using Domian.Contracts;
using Domian.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationEvalutor
    {
        //Generate Query
        public static IQueryable<TEntity> GetQuery<TEntity,TKey>(
                       IQueryable<TEntity> inputQuery,
                       ISpecifications<TEntity,TKey> spec)
                       where TEntity : BaseEntity<TKey>
       {
            var query = inputQuery;

            #region Where
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria); 
            #endregion

            #region for sorting
            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            else if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);
            #endregion


            #region Pagination
            if (spec.IsPagination)
                query = query.Skip(spec.Skip).Take(spec.Take);
            #endregion

            #region Include

            query = spec.IncludeExpression.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            #endregion

            return query;
        }
    }
}
