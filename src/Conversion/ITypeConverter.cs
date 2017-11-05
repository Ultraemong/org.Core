using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITypeConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        object Convert(object value);
    }
}
