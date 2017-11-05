using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Xml;

using org.Core.Validation;
using org.Core.Collections;

namespace org.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObjectUtils
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static ObjectUtils()
        {
        }
        #endregion

        #region Fields
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsClassType(object obj)
        {
            if (null != obj)
                return ObjectUtils.IsClassType(obj.GetType());

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsClassType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type)
                return type.IsClass;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsPrimitiveType(object obj)
        {
            if (null != obj)
                return ObjectUtils.IsPrimitiveType(obj.GetType());

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsPrimitiveType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type)
                return type.IsPrimitive;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsAttributeType(object obj)
        {
            if (null != obj)
                return ObjectUtils.IsAttributeType(obj.GetType());

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAttributeType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type)
                return typeof(Attribute).IsAssignableFrom(type);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsInterfaceType(object obj)
        {
            if (null != obj)
                return ObjectUtils.IsInterfaceType(obj.GetType());

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsInterfaceType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type)
                return type.IsInterface;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsListType(object obj)
        {
            return (obj is IList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsListType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type)
                return (typeof(IList).IsAssignableFrom(type));

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEnumerableType(object obj)
        {
            return (obj is IEnumerable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerableType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type)
                return (typeof(IEnumerable).IsAssignableFrom(type));

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDictionaryType(object obj)
        {
            return (obj is IDictionary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDictionaryType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type)
                return (typeof(IDictionary).IsAssignableFrom(type));

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsAnonymousType(object obj)
        {
            if (null != obj)
                return ObjectUtils.IsAnonymousType(obj.GetType());

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAnonymousType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type && type.IsGenericType)
            {
                var _definition = type.GetGenericTypeDefinition();

                if (_definition.IsClass && _definition.IsSealed && _definition.Attributes.HasFlag(TypeAttributes.NotPublic))
                {
                    var _attributes = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);

                    if (null != _attributes && 0 < _attributes.Length)
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
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type && type.IsGenericType)
            {
                return (typeof(Nullable<>).Equals(type.GetGenericTypeDefinition()));
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullableType(object obj)
        {
            if (null != obj)
                return ObjectUtils.IsNullableType(obj.GetType());

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsXmlType(object obj)
        {
            if (null != obj)
                return ObjectUtils.IsXmlType(obj.GetType());

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsXmlType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type)
                return type.Equals(typeof(XmlDocument));

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Type GetListItemTypeFrom(object obj)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != obj)
                return ObjectUtils.GetListItemTypeFrom(obj.GetType());

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetListItemTypeFrom(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (ObjectUtils.IsListType(type))
            {
                var _property = type.GetProperty("Item");

                if (null != _property)
                    return _property.PropertyType;
            }

            return null;
        }

        /// <summary>
        /// Gets a value indicating whether the object is null or default.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ignoreZero"></param>
        /// <returns></returns>
        public static bool IsNullOrDefault(object obj, bool ignoreZero = false)
        {
            if (null != obj)
                return !ValueValidator.Validate(obj, ignoreZero);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNumericType(object obj)
        {
            if (null != obj)
                return ObjectUtils.IsNumericType(obj.GetType());

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumericType(Type type)
        {
            //TODO: Change this method to TypeUtils class.
            if (null != type)
            {
                var _type = !IsNullableType(type) ? type : Nullable.GetUnderlyingType(type);

                switch (Type.GetTypeCode(_type))
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Double:
                    case TypeCode.Single:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Decimal:
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance1"></param>
        /// <param name="instance2"></param>
        /// <returns></returns>
        public static bool InheritanceEquals(object instance1, object instance2)
        {
            if (null != instance1 && null != instance2)
                return ObjectUtils.InheritanceEquals(instance1.GetType(), instance2.GetType());

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type1"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        public static bool InheritanceEquals(Type type1, Type type2)
        {
            if (null != type1 && null != type2)
            {
                return (type1.IsAssignableFrom(type2)
                    || type2.IsAssignableFrom(type1));
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object CreateInstanceOf(Type type, params object[] args)
        {
            if (null != type)
            {
                try
                {
                    return Activator.CreateInstance(type, args);
                }
                catch (ArgumentNullException)
                {
                }
                catch (ArgumentException)
                {
                }
                catch (FileNotFoundException)
                {
                }
                catch (FileLoadException)
                {
                }
                catch (MissingMethodException)
                {
                }
                catch (BadImageFormatException)
                {
                }
                catch (NotSupportedException)
                {
                }
                catch (InvalidCastException)
                {
                }
                catch (TypeLoadException)
                {
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static object CreateInstanceOf(Type type, bool nonPublic)
        {
            if (null != type)
            {
                try
                {
                    return Activator.CreateInstance(type, nonPublic);
                }
                catch (ArgumentNullException)
                {
                }
                catch (ArgumentException)
                {
                }
                catch (FileNotFoundException)
                {
                }
                catch (FileLoadException)
                {
                }
                catch (MissingMethodException)
                {
                }
                catch (BadImageFormatException)
                {
                }
                catch (NotSupportedException)
                {
                }
                catch (InvalidCastException)
                {
                }
                catch (TypeLoadException)
                {
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T CreateInstanceOf<T>(Type type)
        {
            return (T)ObjectUtils.CreateInstanceOf(type, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static T CreateInstanceOf<T>(Type type, bool nonPublic)
        {
            return (T)ObjectUtils.CreateInstanceOf(type, nonPublic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T CreateInstanceOf<T>(Type type, params object[] args)
        {
            return (T)ObjectUtils.CreateInstanceOf(type, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstanceOf<T>()
        {
            try
            {
                return Activator.CreateInstance<T>();
            }
            catch (ArgumentNullException)
            {
            }
            catch (ArgumentException)
            {
            }
            catch (FileNotFoundException)
            {
            }
            catch (FileLoadException)
            {
            }
            catch (MissingMethodException)
            {
            }
            catch (BadImageFormatException)
            {
            }
            catch (NotSupportedException)
            {
            }
            catch (InvalidCastException)
            {
            }
            catch (TypeLoadException)
            {
            }

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public static T CreateInstanceOf<T>(bool nonPublic)
        {
            return (T)ObjectUtils.CreateInstanceOf(typeof(T), nonPublic);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CreateInstanceOf<T>(params object[] args)
        {
            return ObjectUtils.CreateInstanceOf<T>(typeof(T), args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static T CreateInstanceOf<T>(string assemblyName, string typeName)
        {
            return ObjectUtils.CreateInstanceOf<T>(assemblyName, typeName, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CreateInstanceOf<T>(string assemblyName, string typeName, object[] args)
        {
            if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
            {
                try
                {
                    var _assembly = Assembly.Load(assemblyName);

                    return ObjectUtils.CreateInstanceOf<T>(_assembly, typeName, args);
                }
                catch (ArgumentNullException)
                {
                }
                catch (ArgumentException)
                {
                }
                catch (FileNotFoundException)
                {
                }
                catch (FileLoadException)
                {
                }
                catch (BadImageFormatException)
                {
                }
            }

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CreateInstanceOf<T>(Assembly assembly, string typeName, object[] args)
        {
            if (null != assembly && !string.IsNullOrEmpty(typeName))
            {
                try
                {
                    return (T)(assembly.CreateInstance(typeName, false, BindingFlags.CreateInstance, null, args, CultureInfo.CurrentCulture, null));
                }
                catch (ArgumentNullException)
                {
                }
                catch (ArgumentException)
                {
                }
                catch (FileNotFoundException)
                {
                }
                catch (FileLoadException)
                {
                }
                catch (BadImageFormatException)
                {
                }
                catch (MissingMethodException)
                {
                }
                catch (NotSupportedException)
                {
                }
                catch (InvalidCastException)
                {
                }
            }

            return default(T);
        }
        #endregion
    }
}
