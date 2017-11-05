using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Validate(object obj);
    }
}
