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
    public abstract class DbQueryPrinciple : IDbQueryOperationContextProvider
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DbQueryPrinciple()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public DbQueryPrinciple(DbQueryOperationContext operationContext)
        {
            _operationContext = operationContext;
        } 
        #endregion

        #region Fields
        DbQueryOperationContext _operationContext = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryOperationContext OperationContext
        {
            get
            {
                return _operationContext;
            }
        }
        #endregion

        #region Methods
        #endregion

        #region Internal Classes
        /// <summary>
        /// 
        /// </summary>
        internal static class OperationContextAssigner
        {
            #region Constructors
            /// <summary>
            /// 
            /// </summary>
            static OperationContextAssigner()
            {
            }
            #endregion

            #region Methods
            /// <summary>
            /// 
            /// </summary>
            /// <param name="queryPrinciple"></param>
            /// <param name="operationContext"></param>
            public static void Assign(DbQueryPrinciple queryPrinciple, DbQueryOperationContext operationContext)
            {
                if (null != queryPrinciple && null == queryPrinciple._operationContext)
                {
                    queryPrinciple._operationContext = operationContext;
                }
            }
            #endregion
        } 
        #endregion
    }
}