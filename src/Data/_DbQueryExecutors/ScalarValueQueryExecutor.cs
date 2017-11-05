
using System;
using System.Data;
using System.Data.SqlClient;

using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class ScalarValueQueryExecutor : IDbQueryExecutor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        public ScalarValueQueryExecutor()
        {
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion
        
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IDbQueryResult Execute(SqlCommand command)
        {
            return new DbQueryResult(command.ExecuteScalar());
        } 
        #endregion
    }
}
