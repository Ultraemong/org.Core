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
    public sealed class IgnoreChildPropertyDescriptors : AttributeMemberDescriptors<IgnoreChildPropertyDescriptor, IgnoreChildPropertyDescriptors>, IEntryMemberDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="declaringDescriptor"></param>
        public IgnoreChildPropertyDescriptors(IEnumerable<IgnoreChildPropertyDescriptor> collection, IEntryDescriptor declaringDescriptor)
            : base(collection)
        {
            _declaringDescriptor = declaringDescriptor;
        }
        #endregion

        #region Fields
        readonly IEntryDescriptor _declaringDescriptor = null;
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
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <returns></returns>
        public IgnoreChildPropertyDescriptor GetDescriptor(DbQueryPropertyDescriptor propertyDescriptor)
        {
            foreach (var _property in this)
            {
                if (_property.PropertyName.Equals(propertyDescriptor.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    return _property;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryAction"></param>
        /// <returns></returns>
        public IgnoreChildPropertyDescriptors GetDescriptorsByQueryAction(DbQueryActions queryAction)
        {
            var _ignoreChildProperties = this.Where(prop => prop.QueryAction.HasFlag(queryAction))
                .Select(prop => new IgnoreChildPropertyDescriptor(prop, queryAction));

            return new IgnoreChildPropertyDescriptors(_ignoreChildProperties, DeclaringDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringPropertyDescriptor"></param>
        /// <returns></returns>
        public IgnoreChildPropertyDescriptors GetDescriptors(DbQueryPropertyDescriptor declaringPropertyDescriptor)
        {
            var _ignoreChildProperties = GetDescriptorsByQueryAction(declaringPropertyDescriptor.QueryAction)
                .Where(prop => prop.DeclaringQueryProperty.Name.Equals(declaringPropertyDescriptor.Name, StringComparison.CurrentCultureIgnoreCase));

            return new IgnoreChildPropertyDescriptors(_ignoreChildProperties, DeclaringDescriptor);
        }
        #endregion
    }
}
