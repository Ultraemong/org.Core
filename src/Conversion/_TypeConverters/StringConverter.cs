using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class StringConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public StringConverter()
        {
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Convert(object value)
        {
            if (null != value)
            {
                return value.ToString();
            }

            return null;
        }
        #endregion
    }
}