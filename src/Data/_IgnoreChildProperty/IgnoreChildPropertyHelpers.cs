using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Reflection;
using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IgnoreChildPropertyHelpers
    {
        #region Constructors
        /// <summary>
        ///
        /// </summary>
        static IgnoreChildPropertyHelpers()
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
        public static IgnoreChildPropertyDescriptors RetrieveMemberDescriptors(Type declaringType, IEntryDescriptor declaringDescriptor)
        {
            var _ignorePropertyList     = new ListCollection<IgnoreChildPropertyDescriptor>();
            var _propertyDescriptors    = DbQueryPropertyHelpers.RetrieveMemberDescriptors(declaringType, declaringDescriptor);

            foreach (var _propertyDescriptor in _propertyDescriptors)
            {
                var _ignoreChildProperties = IgnoreChildPropertyHelpers.RetrieveMemberDescriptors(_propertyDescriptor, declaringDescriptor);

                if (!_ignoreChildProperties.IsEmpty)
                {
                    _ignorePropertyList.AddRange(_ignoreChildProperties);
                }
            }

            return new IgnoreChildPropertyDescriptors(_ignorePropertyList, declaringDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <param name="declaringDescriptor"></param>
        /// <returns></returns>
        public static IgnoreChildPropertyDescriptors RetrieveMemberDescriptors(DbQueryPropertyDescriptor propertyDescriptor, IEntryDescriptor declaringDescriptor)
        {
            var _ignoreChildProperties  = new ListCollection<IgnoreChildPropertyDescriptor>();
            var _attributeDescriptors   = propertyDescriptor.GetAttributeDescriptorsByAttributeType(typeof(IIgnoreChildProperty));

            foreach (var _attributeDescriptor in _attributeDescriptors)
            {
                _ignoreChildProperties.Add(new IgnoreChildPropertyDescriptor(_attributeDescriptor, declaringDescriptor, propertyDescriptor));
            }

            return new IgnoreChildPropertyDescriptors(_ignoreChildProperties, declaringDescriptor);
        }
        #endregion
    }
}
