using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum DbQueryPropertyDirections
    {
        /// <summary>
        /// 
        /// </summary>
        None            = 1 << 1,

        /// <summary>
        /// 
        /// </summary>
        Input           = 1 << 2,

        /// <summary>
        /// 
        /// </summary>
        Output          = 1 << 3,

        /// <summary>
        /// 
        /// </summary>
        InputOutput     = Input | Output,
    }
}
