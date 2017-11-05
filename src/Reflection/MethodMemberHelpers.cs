using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;

using org.Core.Collections;
using org.Core.Utilities;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static class MethodMemberHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static MethodMemberHelpers()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="methodName"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static MethodInfo RetrieveMember(Type declaringType, string methodName, bool nonPublic)
        {
            if (null == declaringType)
                throw new ArgumentNullException("declaringType");

            else if (string.IsNullOrEmpty(methodName))
                throw new ArgumentNullException("methodName");

            var _flag   = (!nonPublic) ? BindingFlags.Public : BindingFlags.NonPublic;
            var _method = declaringType.GetMethod(methodName, _flag | BindingFlags.Instance);

            if (null == _method)
                throw new NullReferenceException(string.Format("The '{0}' method was not found in {1}", methodName, declaringType.FullName));

            return _method;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static MethodInfo RetrieveMember(object instance, string methodName, bool nonPublic)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMember(instance.GetType(), methodName, nonPublic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="methodName"></param>
        /// <param name="nonPublic"></param>
        /// <param name="binder"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public static MethodInfo RetrieveMember(Type declaringType, string methodName, bool nonPublic, Binder binder, Type[] types, ParameterModifier[] modifiers)
        {
            if (null == declaringType)
                throw new ArgumentNullException("declaringType");

            else if (string.IsNullOrEmpty(methodName))
                throw new ArgumentNullException("methodName");

            var _flag   = (!nonPublic) ? BindingFlags.Public : BindingFlags.NonPublic;
            var _method = declaringType.GetMethod(methodName, _flag | BindingFlags.Instance, binder ?? Type.DefaultBinder, types, modifiers);

            if (null != _method)
                throw new NullReferenceException(string.Format("The '{0}' method does not exist in {1}", methodName, declaringType.FullName));

            return _method;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="nonPublic"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public static MethodInfo RetrieveMember(object instance, string methodName, bool nonPublic, Binder binder, Type[] types, ParameterModifier[] modifiers)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMember(instance.GetType(), methodName, nonPublic, binder, types, modifiers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static MethodInfo[] RetrieveMembers(Type declaringType, bool nonPublic)
        {
            if (null == declaringType)
                throw new ArgumentNullException("declaringType");

            var _flag       = (!nonPublic) ? BindingFlags.Public : BindingFlags.NonPublic;
            var _methods    = declaringType.GetMethods(_flag | BindingFlags.Instance);

            if (null != _methods && 0 < _methods.Length)
            {
                return _methods;
            }

            return new MethodInfo[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="inherit"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static MethodInfo[] RetrieveMembers(object instance, bool nonPublic)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMembers(instance.GetType(), nonPublic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static MethodInfo[] RetrieveMembers(Type declaringType, bool nonPublic, Type attributeType, bool inherit)
        {
            if (null == declaringType)
                throw new ArgumentNullException("declaringType");

            else if (null == attributeType)
                throw new ArgumentNullException("attributeType");

            var _flag = (!nonPublic) ? BindingFlags.Public : BindingFlags.NonPublic;

            if (ObjectUtils.IsAttributeType(attributeType))
            {
                var _methods = declaringType.GetMethods(_flag | BindingFlags.Instance);

                if (null != _methods && 0 < _methods.Length)
                {
                    return _methods.Where(x => x.IsDefined(attributeType, inherit)).ToArray();
                }
            }
            else if (ObjectUtils.IsClassType(attributeType) || ObjectUtils.IsInterfaceType(attributeType))
            {
                var _dummy      = new ListCollection<MethodInfo>();
                var _methods    = declaringType.GetMethods(_flag | BindingFlags.Instance);

                foreach (var _method in _methods)
                {
                    if (AttributeMemberHelpers.IsSpecificTypeDefined(_method, attributeType, inherit))
                    {
                        _dummy.Add(_method);
                    }
                }

                return _dummy.ToArray();
            }

            return new MethodInfo[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="inherit"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static MethodInfo[] RetrieveMembers(object instance, bool nonPublic, Type attributeType, bool inherit)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMembers(instance.GetType(), nonPublic, attributeType, inherit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <param name="declaringType"></param>
        /// <param name="methodName"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static TDescriptor RetrieveMemberDescriptor<TDescriptor>(Type declaringType, string methodName, bool nonPublic)
            where TDescriptor : MemberDescriptor
        {
            return MemberDescriptor.Initializer.Initialize<TDescriptor>(MethodMemberHelpers.RetrieveMember(declaringType, methodName, nonPublic));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static TDescriptor RetrieveMemberDescriptor<TDescriptor>(object instance, string methodName, bool nonPublic)
            where TDescriptor : MemberDescriptor
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMemberDescriptor<TDescriptor>(instance.GetType(), methodName, nonPublic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <param name="declaringType"></param>
        /// <param name="methodName"></param>
        /// <param name="nonPublic"></param>
        /// <param name="binder"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public static TDescriptor RetrieveMemberDescriptor<TDescriptor>(Type declaringType, string methodName, bool nonPublic, Binder binder, Type[] types, ParameterModifier[] modifiers)
            where TDescriptor : MemberDescriptor
        {
            return MemberDescriptor.Initializer.Initialize<TDescriptor>(MethodMemberHelpers.RetrieveMember(declaringType, methodName, nonPublic, binder, types, modifiers));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="types"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static TDescriptor RetrieveMemberDescriptor<TDescriptor>(object instance, string methodName, bool nonPublic, Binder binder, Type[] types, ParameterModifier[] modifiers)
            where TDescriptor : MemberDescriptor
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMemberDescriptor<TDescriptor>(instance.GetType(), methodName, nonPublic, binder, types, modifiers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="declaringType"></param>
        /// <param name="inherit"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(Type declaringType, bool nonPublic)
            where TDescriptor : MemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            var _descriptors = MethodMemberHelpers.RetrieveMembers(declaringType, nonPublic)
                .Select(meth => MemberDescriptor.Initializer.Initialize<TDescriptor>(meth));

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_descriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="instance"></param>
        /// <param name="inherit"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(object instance, bool nonPublic)
            where TDescriptor : MemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMemberDescriptors<TDescriptor, TCollection>(instance.GetType(), nonPublic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="declaringType"></param>
        /// <param name="inherit"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(Type declaringType, bool inherit, bool nonPublic, Type attributeType)
            where TDescriptor : MemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            var _descriptors = MethodMemberHelpers.RetrieveMembers(declaringType, nonPublic, attributeType, inherit)
                .Select(meth => MemberDescriptor.Initializer.Initialize<TDescriptor>(meth));

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_descriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="instance"></param>
        /// <param name="inherit"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static TCollection RetrieveMemberDescriptors<TDescriptor, TCollection>(object instance, bool inherit, bool nonPublic, Type attributeType)
            where TDescriptor : MemberDescriptor
            where TCollection : ReadOnlyListCollection<TDescriptor>
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMemberDescriptors<TDescriptor, TCollection>(instance.GetType(), inherit, nonPublic, attributeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="inherit"></param>
        /// <param name="nonPublic"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static MethodMemberDescriptors RetrieveMemberDescriptors(Type declaringType, bool inherit, bool nonPublic, Type attributeType)
        {
            return MethodMemberHelpers.RetrieveMemberDescriptors<MethodMemberDescriptor, MethodMemberDescriptors>(declaringType, inherit, nonPublic, attributeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static MethodMemberDescriptors RetrieveMemberDescriptors(object instance, bool inherit, bool nonPublic, Type attributeType)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMemberDescriptors(instance.GetType(), inherit, nonPublic, attributeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="methodName"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static MethodMemberDescriptor RetrieveMemberDescriptor(Type declaringType, string methodName, bool nonPublic)
        {
            return MethodMemberHelpers.RetrieveMemberDescriptor<MethodMemberDescriptor>(declaringType, methodName, nonPublic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static MethodMemberDescriptor RetrieveMemberDescriptor(object instance, string methodName, bool nonPublic)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMemberDescriptor(instance.GetType(), methodName, nonPublic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="methodName"></param>
        /// <param name="nonPublic"></param>
        /// <param name="binder"></param>
        /// <param name="types"></param>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        public static MethodMemberDescriptor RetrieveMemberDescriptor(Type declaringType, string methodName, bool nonPublic, Binder binder, Type[] types, ParameterModifier[] modifiers)
        {
            return MethodMemberHelpers.RetrieveMemberDescriptor<MethodMemberDescriptor>(declaringType, methodName, nonPublic, binder, types, modifiers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        /// <param name="types"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static MethodMemberDescriptor RetrieveMemberDescriptor(object instance, string methodName, bool nonPublic, Binder binder, Type[] types, ParameterModifier[] modifiers)
        {
            if (null == instance)
                throw new ArgumentNullException("instance");

            return MethodMemberHelpers.RetrieveMemberDescriptor(instance.GetType(), methodName, nonPublic, binder, types, modifiers);
        }
        #endregion
    }
}
