using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using org.Core.Reflection;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbQueryOperationDescriptor : MethodMemberDescriptor, IDbQueryOperation, IEntryMemberDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="ownerDescriptor"></param>
        public DbQueryOperationDescriptor(MethodInfo methodInfo, IEntryDescriptor declaringDescriptor)
            : base(methodInfo)
        {
            var _descriptor = AttributeDescriptors.GetDescriptorByAttributeType(typeof(IDbQueryOperation));

            if (null != _descriptor)
                _queryOperation = _descriptor.Member as IDbQueryOperation;

            _declaringDescriptor = declaringDescriptor;
        } 
        #endregion

        #region Fields
        readonly IDbQueryOperation      _queryOperation          = null;
        readonly IEntryDescriptor       _declaringDescriptor     = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IEntryDescriptor DeclaringDescriptor
        {
            get
            {
                return _declaringDescriptor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryActions QueryAction
        {
            get 
            {
                if (null != _queryOperation)
                    return _queryOperation.QueryAction;

                return DbQueryActions.Unknown;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Order
        {
            get
            {
                if (null != _queryOperation)
                    return _queryOperation.Order;

                return 0;
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
