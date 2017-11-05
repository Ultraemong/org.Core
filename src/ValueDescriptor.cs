using System;
using System.Linq;

namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class ValueDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static ValueDescriptor()
        {   
        }
        #endregion

        #region Fields
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static IValueDescriptor GetValueDescriptor(Type valueType)
        {
            try
            {
                if (TypeUtils.IsNullable(valueType))
                    valueType = Nullable.GetUnderlyingType(valueType);

                return ValueDescriptors.Descriptors
                    .Where(x => x.Key.Equals(valueType)).Select(x => x.Value).Single();
            }
            catch (InvalidOperationException)
            {
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static object GetMininumValue(Type valueType)
        {
            var _valueDescriptor = GetValueDescriptor(valueType);

            if (null != _valueDescriptor)
                return _valueDescriptor.MininumValue;

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static object GetMaxinumValue(Type valueType)
        {
            var _valueDescriptor = GetValueDescriptor(valueType);

            if (null != _valueDescriptor)
                return _valueDescriptor.MaxinumValue;

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static object GetDefaultValue(Type valueType)
        {
            var _valueDescriptor = GetValueDescriptor(valueType);

            if (null != _valueDescriptor)
                return _valueDescriptor.DefaultValue;

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static bool IsNumeric(Type valueType)
        {
            var _valueDescriptor = GetValueDescriptor(valueType);

            if (null != _valueDescriptor)
                return _valueDescriptor.IsNumeric;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static bool IsInteger(Type valueType)
        {
            var _valueDescriptor = GetValueDescriptor(valueType);

            if (null != _valueDescriptor)
                return _valueDescriptor.IsInteger;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static bool IsFloat(Type valueType)
        {
            var _valueDescriptor = GetValueDescriptor(valueType);

            if (null != _valueDescriptor)
                return _valueDescriptor.IsFloat;

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public static bool IsBoolean(Type valueType)
        {
            var _valueDescriptor = GetValueDescriptor(valueType);

            if (null != _valueDescriptor)
                return _valueDescriptor.IsBoolean;

            return false;
        }
        #endregion
    }
}
