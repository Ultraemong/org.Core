using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValueSerializable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        object Serialize();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object Deserialize(object value);
    }
}
