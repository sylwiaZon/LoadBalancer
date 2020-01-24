using System;
using NHibernate;

namespace Sebalance
{
    public class NHSQLInterceptor : EmptyInterceptor, IInterceptor
    {
        NHibernate.SqlCommand.SqlString
            IInterceptor.OnPrepareStatement
                (NHibernate.SqlCommand.SqlString sql)
        {
            NHSQL.NHibernateSQL = sql.ToString();
            return sql;
        }
    }
}
