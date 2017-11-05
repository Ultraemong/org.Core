using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryExecutorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        IDbQueryExecutor CreateDbQueryExecutor(DbQueryBehaviors queryBehavior);
    }
}
