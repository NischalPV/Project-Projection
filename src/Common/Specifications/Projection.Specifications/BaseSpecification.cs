using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Common.Specifications;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    #region properties
    public Expression<Func<T, bool>> Criteria { get; protected set; }
    public List<Expression<Func<T, object>>> Includes { get; protected set; } = new List<Expression<Func<T, object>>>();
    public List<string> IncludeStrings { get; protected set; } = new List<string>();
    public Expression<Func<T, object>> OrderBy { get; protected set; }
    public Expression<Func<T, object>> OrderByDescending { get; protected set; }
    public int Page { get; protected set; }
    public int PageSize { get; protected set; }
    #endregion

    #region ctor
    protected BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    #endregion

    #region public methods
    public virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    public virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    public virtual void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    public virtual void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    public virtual void ApplyPaging(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
    #endregion
}
