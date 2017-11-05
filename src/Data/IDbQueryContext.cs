using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryContext : IDisposable, IDbQueryParameterizable
    {
        /// <summary>
        /// 
        /// </summary>
        event DbQueryExecutingEventHandler Executing;

        /// <summary>
        /// 
        /// </summary>
        event DbQueryExecutedEventHandler Executed;

        /// <summary>
        /// 
        /// </summary>
        event DbQueryFailedEventHandler Failed;

        /// <summary>
        /// 
        /// </summary>
        string QueryText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryBehaviors? QueryBehavior { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int? ExecutionTimeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDbQueryResult Execute();
    }
}
