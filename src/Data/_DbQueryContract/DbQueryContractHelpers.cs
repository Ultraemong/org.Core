
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using org.Core.Reflection;
using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbQueryContractHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static DbQueryContractHelpers()
        {
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberDescriptors"></param>
        /// <param name="declaringDescriptor"></param>
        /// <returns></returns>
        public static DbQueryContractDescriptors RetrieveMemberDescriptors(AttributeMemberDescriptors memberDescriptors, IEntryDescriptor declaringDescriptor)
        {
            var _descriptors = memberDescriptors.GetDescriptorsByAttributeType(typeof(IDbQueryContract))
                .Select(attr => new DbQueryContractDescriptor(attr.Member, declaringDescriptor));

            return new DbQueryContractDescriptors(_descriptors, declaringDescriptor);
        }
        #endregion
    }
}
