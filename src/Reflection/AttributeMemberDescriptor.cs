using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

using org.Core.Utilities;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public class AttributeMemberDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        public AttributeMemberDescriptor(Attribute attribute)
        {
            _attribute = attribute;
        }
        #endregion

        #region Fields
        Attribute _attribute = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Attribute Member
        {
            get
            {
                return _attribute;
            }
        }
        #endregion

        #region Methods
        #endregion

        #region Internal Classes
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        internal static class Initializer
        {
            #region Constructors
            /// <summary>
            /// 
            /// </summary>
            static Initializer()
            {
            }
            #endregion

            #region Methods
            /// <summary>
            /// 
            /// </summary>
            /// <param name="memberInfo"></param>
            /// <returns></returns>
            public static TDescriptor Initialize<TDescriptor>(Attribute attribute)
                where TDescriptor : AttributeMemberDescriptor
            {
                if (null == attribute)
                    throw new ArgumentNullException("attribute");

                return ObjectUtils.CreateInstanceOf<TDescriptor>(attribute);
            }
            #endregion
        }
        #endregion
    }
}
