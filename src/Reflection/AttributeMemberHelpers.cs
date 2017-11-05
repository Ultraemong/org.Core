
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Diagnostics;

using org.Core.Collections;
using org.Core.Utilities;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static class AttributeMemberHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static AttributeMemberHelpers()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="definitionType"></param>
        /// <returns></returns>
        public static bool IsSpecificTypeDefined(Type attributeType, Type definitionType)
        {
            if (null == attributeType)
                throw new ArgumentNullException("attributeType");

            else if (null == definitionType)
                throw new ArgumentNullException("definitionType");

            return ObjectUtils.InheritanceEquals(attributeType, definitionType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="definitionType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static bool IsSpecificTypeDefined(Attribute attribute, Type definitionType)
        {
            if (null == attribute)
                throw new ArgumentNullException("attribute");

            return AttributeMemberHelpers.IsSpecificTypeDefined(attribute.GetType(), definitionType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeProvider"></param>
        /// <param name="definitionType"></param>
        /// <returns></returns>
        public static bool IsSpecificTypeDefined(ICustomAttributeProvider attributeProvider, Type definitionType, bool inherit)
        {
            if (null == attributeProvider)
                throw new ArgumentNullException("attributeProvider");

            if (ObjectUtils.IsAttributeType(definitionType))
            {
                return attributeProvider.IsDefined(definitionType, inherit);
            }
            else if (ObjectUtils.IsInterfaceType(definitionType))
            {
                var _attrs = attributeProvider.GetCustomAttributes(inherit);

                foreach (var _attr in _attrs)
                {
                    if (AttributeMemberHelpers.IsSpecificTypeDefined(_attr as Attribute, definitionType))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeProvider"></param>
        /// <param name="definitionTypes"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static bool IsSpecificTypeDefined(ICustomAttributeProvider attributeProvider, Type[] definitionTypes, bool inherit)
        {
            if (null == definitionTypes)
                throw new ArgumentNullException("definitionTypes");

            foreach (var _definitionType in definitionTypes)
            {
                return AttributeMemberHelpers.IsSpecificTypeDefined(attributeProvider, _definitionType, inherit);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeProvider"></param>
        /// <param name="comparisonType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static Attribute RetrieveMember(ICustomAttributeProvider attributeProvider, Type comparisonType, bool inherit = false)
        {
            var _attrs = AttributeMemberHelpers.RetrieveMembers(attributeProvider, comparisonType, inherit);

            if (null != _attrs && 0 < _attrs.Length)
            {
                return _attrs[0];
            }

            throw new NullReferenceException(string.Format("A '{0}' attribute does not exist.", comparisonType.FullName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="comparisonType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static Attribute RetrieveMember(object instance, Type comparisonType, bool inherit = false)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return AttributeMemberHelpers.RetrieveMember(instance.GetType(), comparisonType, inherit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeProvider"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static Attribute[] RetrieveMembers(ICustomAttributeProvider attributeProvider, bool inherit = false)
        {
            if (null == attributeProvider)
                throw new ArgumentNullException("attributeProvider");

            var _dummy  = new ListCollection<Attribute>();
            var _attrs  = attributeProvider.GetCustomAttributes(inherit);
            
            foreach (var _attr in _attrs)
            {
                _dummy.Add(_attr as Attribute);
            }

            return _dummy.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeProvider"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static Attribute[] RetrieveMembers(ICustomAttributeProvider attributeProvider, Type comparisonType, bool inherit = false)
        {
            if (null == attributeProvider)
                throw new ArgumentNullException("attributeProvider");

            else if (null == comparisonType)
                throw new ArgumentNullException("comparisonType");

            var _dummy = new ListCollection<Attribute>();

            if (ObjectUtils.IsAttributeType(comparisonType))
            {
                var _attrs = attributeProvider.GetCustomAttributes(comparisonType, inherit);

                foreach (var _attr in _attrs)
                {
                    _dummy.Add(_attr as Attribute);
                }

                return _dummy.ToArray();
            }
            else if (ObjectUtils.IsInterfaceType(comparisonType))
            {
                var _attrs = attributeProvider.GetCustomAttributes(inherit);
                
                foreach (var _attr in _attrs)
                {
                    if (AttributeMemberHelpers.IsSpecificTypeDefined(_attr as Attribute, comparisonType))
                    {
                        _dummy.Add(_attr as Attribute);
                    }
                }
            }

            return _dummy.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static Attribute[] RetrieveMembers(object instance, bool inherit = false)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return AttributeMemberHelpers.RetrieveMembers(instance.GetType(), inherit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="comparisonType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static Attribute[] RetrieveMembers(object instance, Type comparisonType, bool inherit = false)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return AttributeMemberHelpers.RetrieveMembers(instance.GetType(), comparisonType, inherit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <param name="instance"></param>
        /// <param name="comparisonType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static TDescriptor RetrieveMemberDescriptor<TDescriptor>(object instance, Type comparisonType, bool inherit = false)
            where TDescriptor : AttributeMemberDescriptor
        {
            return AttributeMemberDescriptor.Initializer.Initialize<TDescriptor>(AttributeMemberHelpers.RetrieveMember(instance, comparisonType, inherit));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="instance"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(object instance, bool inherit = false)
            where TDescriptor : AttributeMemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            var _descriptors = AttributeMemberHelpers.RetrieveMembers(instance, inherit)
                .Select(attr => AttributeMemberDescriptor.Initializer.Initialize<TDescriptor>(attr));

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_descriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="instance"></param>
        /// <param name="comparisonType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(object instance, Type comparisonType, bool inherit = false)
            where TDescriptor : AttributeMemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            var _descriptors = AttributeMemberHelpers.RetrieveMembers(instance, comparisonType, inherit)
                .Select(attr => AttributeMemberDescriptor.Initializer.Initialize<TDescriptor>(attr));

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_descriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="attributeProvider"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(ICustomAttributeProvider attributeProvider, bool inherit = false)
            where TDescriptor : AttributeMemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            var _descriptors = AttributeMemberHelpers.RetrieveMembers(attributeProvider, inherit)
                .Select(attr => AttributeMemberDescriptor.Initializer.Initialize<TDescriptor>(attr));

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_descriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="attributeProvider"></param>
        /// <param name="comparisonType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(ICustomAttributeProvider attributeProvider, Type comparisonType, bool inherit = false)
            where TDescriptor : AttributeMemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            var _descriptors = AttributeMemberHelpers.RetrieveMembers(attributeProvider, comparisonType, inherit)
                .Select(attr => AttributeMemberDescriptor.Initializer.Initialize<TDescriptor>(attr));

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_descriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static AttributeMemberDescriptor RetrieveMemberDescriptor(object instance, Type attributeType, bool inherit = false)
        {
            return AttributeMemberHelpers.RetrieveMemberDescriptor<AttributeMemberDescriptor>(instance, attributeType, inherit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static AttributeMemberDescriptors RetrieveMemberDescriptors(object instance, bool inherit = false)
        {
            return AttributeMemberHelpers.RetrieveMemberDescriptors<AttributeMemberDescriptor, AttributeMemberDescriptors>(instance, inherit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeProvider"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static AttributeMemberDescriptors RetrieveMemberDescriptors(ICustomAttributeProvider attributeProvider, bool inherit = false)
        {
            return AttributeMemberHelpers.RetrieveMemberDescriptors<AttributeMemberDescriptor, AttributeMemberDescriptors>(attributeProvider, inherit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeProvider"></param>
        /// <param name="comparisonType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static AttributeMemberDescriptors RetrieveMemberDescriptors(ICustomAttributeProvider attributeProvider, Type comparisonType, bool inherit = false)
        {
            return AttributeMemberHelpers.RetrieveMemberDescriptors<AttributeMemberDescriptor, AttributeMemberDescriptors>(attributeProvider, comparisonType, inherit);
        }
        #endregion
    }
}
