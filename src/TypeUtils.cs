using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeUtils
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static TypeUtils()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsClass(Type type)
        {
            if (null != type)
                return type.IsClass;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsPrimitive(Type type)
        {
            if (null != type)
                return type.IsPrimitive;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAttribute(Type type)
        {
            if (null != type)
                return typeof(Attribute).IsAssignableFrom(type);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsInterface(Type type)
        {
            if (null != type)
                return type.IsInterface;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsList(Type type)
        {
            if (null != type)
                return (typeof(IList).IsAssignableFrom(type));

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerable(Type type)
        {
            if (null != type)
                return (typeof(IEnumerable).IsAssignableFrom(type));

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDictionary(Type type)
        {
            if (null != type)
                return (typeof(IDictionary).IsAssignableFrom(type));

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAnonymous(Type type)
        {
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
        public static bool IsNullable(Type type)
        {
            if (null != type && type.IsGenericType)
            {
                return (typeof(Nullable<>).Equals(type.GetGenericTypeDefinition()));
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsXml(Type type)
        {
            if (null != type)
                return type.Equals(typeof(XmlDocument));

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumeric(Type type)
        {
            if (null != type)
            {
                if (IsNullable(type))
                    type = Nullable.GetUnderlyingType(type);

                switch (Type.GetTypeCode(type))
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
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetListItemTypeFrom(Type type)
        {
            if (IsList(type))
            {
                var _property = type.GetProperty("Item");

                if (null != _property)
                    return _property.PropertyType;
            }

            return null;
        }
        #endregion
    }
}
