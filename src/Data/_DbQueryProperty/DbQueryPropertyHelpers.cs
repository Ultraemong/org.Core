
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
    public static class DbQueryPropertyHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static DbQueryPropertyHelpers()
        {
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="declaringDescriptor"></param>
        /// <returns></returns>
        public static DbQueryPropertyDescriptors RetrieveMemberDescriptors(Type declaringType, IEntryDescriptor declaringDescriptor)
        {
            var _descriptors = PropertyMemberHelpers.RetrieveMembers(declaringType, false, typeof(IDbQueryProperty), true, true, true)
                .Select(prop => new DbQueryPropertyDescriptor(prop, declaringDescriptor)).OrderBy(prop => prop.Order);

            return new DbQueryPropertyDescriptors(_descriptors, declaringDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="declaringDescriptor"></param>
        /// <returns></returns>
        public static DbQueryPropertyDescriptors RetrieveMemberDescriptors(object instance, IEntryDescriptor declaringDescriptor)
        {
            return DbQueryPropertyHelpers.RetrieveMemberDescriptors(instance.GetType(), declaringDescriptor);
        }
        #endregion
    }
}
