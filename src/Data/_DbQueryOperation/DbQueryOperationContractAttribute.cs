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
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class DbQueryOperationContractAttribute : DbQueryContractAttribute
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        public DbQueryOperationContractAttribute(string queryText)
            : base(DbQueryActions.Custom, queryText)
        {
        }
        #endregion
    }
}
