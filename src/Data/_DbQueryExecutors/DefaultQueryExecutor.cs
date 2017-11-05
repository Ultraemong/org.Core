
using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

using org.Core.Conversion;
using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultQueryExecutor : IDbQueryExecutor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        public DefaultQueryExecutor()
        {
        }
        #endregion

        #region Fields
        readonly TypeConverter                  _typeConverter      = new TypeConverter();
        readonly AdoDotNetDbParameterRetriever  _parameterRetriever = new AdoDotNetDbParameterRetriever();
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
            command.ExecuteNonQuery();

            Expression<Func<SqlParameter, bool>> _selector = x => 
                x.Direction.Equals(ParameterDirection.Output) || x.Direction.Equals(ParameterDirection.InputOutput);

            var _params = _parameterRetriever.Retrieve(command.Parameters, _selector);

            return new DbQueryResult(_params);

        }
        #endregion
    }
}
