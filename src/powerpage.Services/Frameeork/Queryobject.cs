using System;
using System.Linq.Expressions;
using LinqKit;

namespace powerpage.Services;

public abstract class QueryObject<TEntity>
{
    private readonly Expression<Func<TEntity, bool>> _query;

    protected QueryObject(Expression<Func<TEntity, bool>> query)
    {
        _query = query;
    }

    public virtual Expression<Func<TEntity, bool>> Query()
    {
        return _query;
    }


}