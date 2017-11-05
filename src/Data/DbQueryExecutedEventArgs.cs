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
    public sealed class DbQueryExecutedEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryResult"></param>
        public DbQueryExecutedEventArgs(IDbQueryResult queryResult)
            : base()
        {
            _queryResult = queryResult;
        }
        #endregion

        #region Fields
        readonly IDbQueryResult _queryResult = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IDbQueryResult QueryResult
        {
            get
            {
                return _queryResult;
            }
        }
        #endregion
    }
}
