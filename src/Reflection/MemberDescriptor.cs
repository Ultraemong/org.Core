using org.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MemberDescriptor : IAttributeDescriptorProvider
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        public MemberDescriptor(MemberInfo memberInfo)
        {
            _memberInfo = memberInfo;
        }
        #endregion

        #region Fields
        readonly MemberInfo         _memberInfo                 = null;
        AttributeMemberDescriptors  _attributes                 = null;

        bool?                       _hasAttributeMembers        = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public MemberInfo Member
        {
            get
            {
                return _memberInfo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MemberTypes MemberType
        {
            get
            {
                return _memberInfo.MemberType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasAttributeMembers
        {
            get
            {
                if (!_hasAttributeMembers.HasValue)
                {
                    foreach(var _attribute in _memberInfo.CustomAttributes)
                    {
                        _hasAttributeMembers = true;

                        break;
                    }
                }

                return _hasAttributeMembers.GetValueOrDefault(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public AttributeMemberDescriptors AttributeDescriptors
        {
            get
            {
                if (null == _attributes)
                    return _attributes = AttributeMemberHelpers.RetrieveMemberDescriptors(Member);

                return _attributes;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public AttributeMemberDescriptor GetAttributeDescriptorByAttributeType(Type attributeType)
        {
            return AttributeDescriptors.GetDescriptorByAttributeType(attributeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public AttributeMemberDescriptors GetAttributeDescriptorsByAttributeType(Type attributeType)
        {
            return AttributeDescriptors.GetDescriptorsByAttributeType(attributeType);
        }
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
            public static TDescriptor Initialize<TDescriptor>(MemberInfo memberInfo)
                where TDescriptor : MemberDescriptor
            {
                if (null == memberInfo)
                    throw new ArgumentNullException("memberInfo");

                return ObjectUtils.CreateInstanceOf<TDescriptor>(memberInfo);
            }
            #endregion
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    public abstract class MemberDescriptor<TMember> : MemberDescriptor
        where TMember : MemberInfo
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        public MemberDescriptor(TMember memberInfo)
            : base(memberInfo)
        {
            _memberInfo = memberInfo;
        }
        #endregion

        #region Fields
        readonly TMember _memberInfo = default(TMember);
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public new TMember Member
        {
            get
            {
                return _memberInfo;
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
