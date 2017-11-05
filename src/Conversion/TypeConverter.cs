using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Collections;
using System.Data.SqlTypes;

using org.Core.Collections;
using org.Core.Utilities;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public TypeConverter()
        {
        }
        #endregion

        #region Fields
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Convert(Type destinationType, object value)
        {
            try
            {
                var _type = ObjectUtils.IsNullableType(destinationType)
                    ? Nullable.GetUnderlyingType(destinationType) : destinationType;

                return TypeConverters.Converters.Where(x => x.Key.Equals(_type))
                    .Select(x => x.Value).Single().Convert(value);
            }
            catch(InvalidOperationException)
            {   
            }

            return value;//Should be returned unchanged.
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T Convert<T>(object value)
        {
            return ((T)Convert(typeof(T), value));
        }
        #endregion
    }
}
