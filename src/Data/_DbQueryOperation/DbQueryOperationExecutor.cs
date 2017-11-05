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
    public class DbQueryOperationExecutor : DbQueryOperatingPrinciple, IDbQueryOperationExecutor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatingSession"></param>
        /// <param name="memberDescriptors"></param>
        public DbQueryOperationExecutor(DbQueryOperatingSession operatingSession, DbQueryOperationDescriptors memberDescriptors)
            : base(operatingSession)
        {
            _memberDescriptors = memberDescriptors;
        }
        #endregion

        #region Fields
        readonly DbQueryOperationDescriptors _memberDescriptors = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryOperationDescriptors OperationDescriptors
        {
            get 
            {
                return _memberDescriptors;
            }
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="queryContext"></param>
        public void Execute(object entity, IDbQueryContext queryContext)
        {
            foreach (var _memberDescriptor in OperationDescriptors)
            {
                _memberDescriptor.Invoke(entity, queryContext);
            }
        }
        #endregion
    }
}
