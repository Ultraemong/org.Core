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
    public interface IDbQueryOperationExecutor : IDbQueryOperationProvider, IDbQueryOperatingSessionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="queryContext"></param>
        void Execute(object entity, IDbQueryContext queryContext);
    }
}
