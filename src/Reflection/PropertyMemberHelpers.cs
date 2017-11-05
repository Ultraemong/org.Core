using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using org.Core.Collections;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static class PropertyMemberHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static PropertyMemberHelpers()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static bool CheckAccessibility(PropertyInfo property, bool? canWrite, bool? canRead)
        {
            if (null == property)
                throw new ArgumentNullException("property");

            var _result = true;

            if (canWrite.HasValue)
                _result = (canWrite.Value) ? property.CanWrite : !property.CanWrite;

            if (canRead.HasValue)
                _result = (canRead.Value) ? property.CanRead : !property.CanRead;

            return _result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="propertyName"></param>
        /// <param name="nonPublic"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo RetrieveMember(Type declaringType, string propertyName, bool nonPublic, bool? canWrite, bool? canRead)
        {
            if (null == declaringType)
                throw new ArgumentNullException("declaringType");

            else if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");

            var _flag           = (!nonPublic) ? BindingFlags.Public : BindingFlags.NonPublic;
            var _property       = declaringType.GetProperty(propertyName, _flag | BindingFlags.Instance);
            
            if (null == _property)
                throw new NullReferenceException(string.Format("The '{0}' property does not exist in {1}", propertyName, declaringType.FullName));

            if (!PropertyMemberHelpers.CheckAccessibility(_property, canWrite, canRead))
                throw new InvalidOperationException(string.Format("The '{0}' property cannot be reached", propertyName));

            return _property;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="nonPublic"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo RetrieveMember(object instance, string propertyName, bool nonPublic, bool? canWrite, bool? canRead)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            else if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");

            return PropertyMemberHelpers.RetrieveMember(instance.GetType(), propertyName, nonPublic, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo[] RetrieveMembers(Type declaringType, bool nonPublic, bool? canWrite, bool? canRead)
        {
            if (null == declaringType)
                throw new ArgumentNullException("declaringType");

            var _flag           = (!nonPublic) ? BindingFlags.Public : BindingFlags.NonPublic;
            var _dummy          = new ListCollection<PropertyInfo>();
            var _properties     = declaringType.GetProperties(_flag | BindingFlags.Instance);
            
            foreach (var _property in _properties)
            {
                if (PropertyMemberHelpers.CheckAccessibility(_property, canWrite, canRead))
                {
                    _dummy.Add(_property);
                }
            }

            return _dummy.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="nonPublic"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo[] RetrieveMembers(object instance, bool nonPublic, bool? canWrite, bool? canRead)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMembers(instance.GetType(), nonPublic, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="returnType"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo[] RetrieveMembers(Type declaringType, bool nonPublic, Type returnType, bool? canWrite, bool? canRead)
        {
            if (null == returnType)
                throw new ArgumentNullException("returnType");

            var _dummy          = new ListCollection<PropertyInfo>();
            var _properties     = PropertyMemberHelpers.RetrieveMembers(declaringType, nonPublic, canWrite, canRead);

            foreach (var _property in _properties)
            {
                var _returnType = _property.PropertyType;

                if (returnType.Equals(_returnType) || returnType.IsAssignableFrom(_returnType))
                {
                    _dummy.Add(_property);
                }
            }

            return _dummy.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="nonPublic"></param>
        /// <param name="returnType"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo[] RetrieveMembers(object instance, bool nonPublic, Type returnType, bool? canWrite, bool? canRead)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMembers(instance.GetType(), nonPublic, returnType, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo[] RetrieveMembers(Type declaringType, bool nonPublic, Type attributeType, bool inherit, bool? canWrite, bool? canRead)
        {
            if (null == attributeType)
                throw new ArgumentNullException("attributeType");

            var _dummy      = new ListCollection<PropertyInfo>();
            var _properties = PropertyMemberHelpers.RetrieveMembers(declaringType, nonPublic, canWrite, canRead);

            foreach (var _property in _properties)
            {
                if (AttributeMemberHelpers.IsSpecificTypeDefined(_property, attributeType, inherit))
                {
                    _dummy.Add(_property);
                }
            }

            return _dummy.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo[] RetrieveMembers(object instance, bool nonPublic, Type attributeType, bool inherit, bool? canWrite, bool? canRead)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMembers(instance.GetType(), nonPublic, attributeType, inherit, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeTypes"></param>
        /// <param name="inherit"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo[] RetrieveMembers(Type declaringType, bool nonPublic, Type[] attributeTypes, bool inherit, bool? canWrite, bool? canRead)
        {
            if (null == attributeTypes)
                throw new ArgumentNullException("attributeTypes");

            var _dummy      = new ListCollection<PropertyInfo>();
            var _properties = PropertyMemberHelpers.RetrieveMembers(declaringType, nonPublic, canWrite, canRead);

            foreach (var _property in _properties)
            {
                if (AttributeMemberHelpers.IsSpecificTypeDefined(_property, attributeTypes, inherit))
                {
                    _dummy.Add(_property);
                }
            }

            return _dummy.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeTypes"></param>
        /// <param name="inherit"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo[] RetrieveMembers(object instance, bool nonPublic, Type[] attributeTypes, bool inherit, bool? canWrite, bool? canRead)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMembers(instance.GetType(), nonPublic, attributeTypes, inherit, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo[] RetrieveMembers(Type type, bool? canWrite, bool? canRead)
        {
            if (null == type)
                throw new ArgumentNullException("type");

            var _flag       = BindingFlags.Public | BindingFlags.NonPublic;
            var _dummy      = new ListCollection<PropertyInfo>();
            var _properties = type.GetProperties(_flag | BindingFlags.Instance);

            foreach (var _property in _properties)
            {
                if (PropertyMemberHelpers.CheckAccessibility(_property, canWrite, canRead))
                {
                    _dummy.Add(_property);
                }
            }

            return _dummy.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyInfo[] RetrieveMembers(object instance, bool? canWrite, bool? canRead)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMembers(instance.GetType(), canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <param name="declaringType"></param>
        /// <param name="propertyName"></param>
        /// <param name="nonPublic"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static TDescriptor RetrieveMemberDescriptor<TDescriptor>(Type declaringType, string propertyName, bool nonPublic, bool? canWrite, bool? canRead)
            where TDescriptor : MemberDescriptor
        {
            return MemberDescriptor.Initializer.Initialize<TDescriptor>
            (
                PropertyMemberHelpers.RetrieveMember(declaringType, propertyName, nonPublic, canWrite, canRead)
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="nonPublic"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static TDescriptor RetrieveMemberDescriptor<TDescriptor>(object instance, string propertyName, bool nonPublic, bool? canWrite, bool? canRead)
            where TDescriptor : MemberDescriptor
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMemberDescriptor<TDescriptor>(instance.GetType(), propertyName, nonPublic, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static TDescriptor RetrieveMemberDescriptor<TDescriptor>(Type declaringType, bool nonPublic, Type attributeType, bool inherit, bool? canWrite, bool? canRead)
            where TDescriptor : MemberDescriptor
        {
            var _properties = PropertyMemberHelpers.RetrieveMembers(declaringType, nonPublic, attributeType, inherit, canWrite, canRead);

            if (0 < _properties.Length)
                return MemberDescriptor.Initializer.Initialize<TDescriptor>(_properties[0]);

            throw new NullReferenceException(string.Format("The '{0}' attribute does not exist in {1}", attributeType.Name, declaringType.FullName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <param name="instance"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static TDescriptor RetrieveMemberDescriptor<TDescriptor>(object instance, bool nonPublic, Type attributeType, bool inherit, bool? canWrite, bool? canRead)
            where TDescriptor : MemberDescriptor
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMemberDescriptor<TDescriptor>(instance.GetType(), nonPublic, attributeType, inherit, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="returnType"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(Type declaringType, bool nonPublic, Type returnType, bool? canWrite, bool? canRead)
            where TDescriptor : MemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            var _descriptors = PropertyMemberHelpers.RetrieveMembers(declaringType, nonPublic, returnType, canWrite, canRead)
                .Select(prop => MemberDescriptor.Initializer.Initialize<TDescriptor>(prop));

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_descriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="instance"></param>
        /// <param name="nonPublic"></param>
        /// <param name="returnType"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(object instance, bool nonPublic, Type returnType, bool? canWrite, bool? canRead)
            where TDescriptor : MemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMemberDescriptors<TDescriptor, TCollection>(instance.GetType(), nonPublic, returnType, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(Type declaringType, bool nonPublic, Type attributeType, bool inherit, bool? canWrite, bool? canRead)
            where TDescriptor : MemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            var _descriptors = PropertyMemberHelpers.RetrieveMembers(declaringType, nonPublic, attributeType, inherit, canWrite, canRead)
                .Select(prop => MemberDescriptor.Initializer.Initialize<TDescriptor>(prop));

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_descriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="instance"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <param name="nonPublic"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(object instance, bool nonPublic, Type attributeType, bool inherit, bool? canWrite, bool? canRead)
            where TDescriptor : MemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMemberDescriptors<TDescriptor, TCollection>(instance.GetType(), nonPublic, attributeType, inherit, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="propertyName"></param>
        /// <param name="nonPublic"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyMemberDescriptor RetrieveMemberDescriptor(Type declaringType, string propertyName, bool nonPublic, bool? canWrite, bool? canRead)
        {
            return PropertyMemberHelpers.RetrieveMemberDescriptor<PropertyMemberDescriptor>(declaringType, propertyName, nonPublic, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="nonPublic"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyMemberDescriptor RetrieveMemberDescriptor(object instance, string propertyName, bool nonPublic, bool? canWrite, bool? canRead)
        {
            return PropertyMemberHelpers.RetrieveMemberDescriptor<PropertyMemberDescriptor>(instance, propertyName, nonPublic, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyMemberDescriptor RetrieveMemberDescriptor(Type declaringType, bool nonPublic, Type attributeType, bool inherit, bool? canWrite, bool? canRead)
        {
            return PropertyMemberHelpers.RetrieveMemberDescriptor<PropertyMemberDescriptor>(declaringType, nonPublic, attributeType, inherit, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <param name="canWrite"></param>
        /// <param name="canRead"></param>
        /// <returns></returns>
        public static PropertyMemberDescriptor RetrieveMemberDescriptor(object instance, bool nonPublic, Type attributeType, bool inherit, bool? canWrite, bool? canRead)
        {
            return PropertyMemberHelpers.RetrieveMemberDescriptor<PropertyMemberDescriptor>(instance, nonPublic, attributeType, inherit, canWrite, canRead);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        public static PropertyMemberDescriptors RetrieveMemberDescriptors(Type declaringType, bool nonPublic, Type returnType)
        {
            return PropertyMemberHelpers.RetrieveMemberDescriptors<PropertyMemberDescriptor, PropertyMemberDescriptors>(declaringType, nonPublic, returnType, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="returnType"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static PropertyMemberDescriptors RetrieveMemberDescriptors(object instance, bool nonPublic, Type returnType)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMemberDescriptors(instance.GetType(), nonPublic, returnType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static PropertyMemberDescriptors RetrieveMemberDescriptors(Type declaringType, bool nonPublic, Type attributeType, bool inherit)
        {
            return PropertyMemberHelpers.RetrieveMemberDescriptors<PropertyMemberDescriptor, PropertyMemberDescriptors>(declaringType, nonPublic, attributeType, inherit, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="returnType"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static PropertyMemberDescriptors RetrieveMemberDescriptors(object instance, bool nonPublic, Type attributeType, bool inherit)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return PropertyMemberHelpers.RetrieveMemberDescriptors(instance.GetType(), nonPublic, attributeType, inherit);
        }
        #endregion
    }
}
