
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using org.Core.Reflection;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbQueryOperationHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static DbQueryOperationHelpers()
        {
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_declaringType"></param>
        /// <param name="declaringDescriptor"></param>
        /// <returns></returns>
        public static DbQueryOperationDescriptors RetrieveMemberDescriptors(Type _declaringType, IEntryDescriptor declaringDescriptor)
        {
            var _descriptors = MethodMemberHelpers.RetrieveMembers(_declaringType, true, typeof(IDbQueryOperation), true)
                .Select(meth => new DbQueryOperationDescriptor(meth, declaringDescriptor)).OrderBy(meth => meth.Order);

            return new DbQueryOperationDescriptors(_descriptors, declaringDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="declaringDescriptor"></param>
        /// <returns></returns>
        public static DbQueryOperationDescriptors RetrieveMemberDescriptors(object instance, IEntryDescriptor declaringDescriptor)
        {
            return DbQueryOperationHelpers.RetrieveMemberDescriptors(instance.GetType(), declaringDescriptor);
        }
        #endregion
    }
}
