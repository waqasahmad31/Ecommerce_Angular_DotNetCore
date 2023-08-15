using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        // *** condition and includes statements *** //
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T,object>>> Includes { get; }

        // *** For Sorting *** //
        Expression<Func<T,object>> OrderBy { get; }
        Expression<Func<T,object>> OrderByDescending { get; }

        // *** for pagination **//
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
