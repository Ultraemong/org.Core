using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

using org.Core.Collections;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDescriptor"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class PropertyMemberDescriptors<TDescriptor, TCollection> : MemberDescriptors<TDescriptor, TCollection>
        where TDescriptor : PropertyMemberDescriptor
        where TCollection : ReadOnlyListCollection<TDescriptor>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public PropertyMemberDescriptors(IEnumerable<TDescriptor> collection)
            : base(collection)
        {
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class PropertyMemberDescriptors : PropertyMemberDescriptors<PropertyMemberDescriptor, PropertyMemberDescriptors>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public PropertyMemberDescriptors(IEnumerable<PropertyMemberDescriptor> collection)
            : base(collection)
        {
        }
        #endregion

        #region Methods
        #endregion
    }
}
