using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using org.Core.Collections;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDescriptor"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class MethodMemberDescriptors<TDescriptor, TCollection> : MemberDescriptors<TDescriptor, TCollection>
        where TDescriptor : MethodMemberDescriptor
        where TCollection : ReadOnlyListCollection<TDescriptor>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public MethodMemberDescriptors(IEnumerable<TDescriptor> collection)
            : base(collection)
        {
        }
        #endregion

        #region Methods
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class MethodMemberDescriptors : MethodMemberDescriptors<MethodMemberDescriptor, MethodMemberDescriptors>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public MethodMemberDescriptors(IEnumerable<MethodMemberDescriptor> collection)
            : base(collection)
        {
        }
        #endregion
    }
}
