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
    public enum DbQueryOperatingSessionStates
    {
        /// <summary>
        /// 
        /// </summary>
        Ready               = 1 << 1,

        /// <summary>
        /// 
        /// </summary>
        Opened              = 1 << 2,

        /// <summary>
        /// 
        /// </summary>
        Closed              = 1 << 3,

        /// <summary>
        /// 
        /// </summary>
        Validated           = 1 << 4,

        /// <summary>
        /// 
        /// </summary>
        Executed            = 1 << 5,

        /// <summary>
        /// 
        /// </summary>
        Failed              = 1 << 11,

        /// <summary>
        /// 
        /// </summary>
        ConnectionFaulted   = 1 << 12,
    }
}
