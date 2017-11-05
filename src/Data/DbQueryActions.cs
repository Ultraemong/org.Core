using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum DbQueryActions
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown     = 1 << 1,

        /// <summary>
        /// 
        /// </summary>
        Create      = 1 << 2,

        /// <summary>
        /// 
        /// </summary>
        Update      = 1 << 3,

        /// <summary>
        /// 
        /// </summary>
        Delete      = 1 << 4,

        /// <summary>
        /// 
        /// </summary>
        Fetch       = 1 << 5,

        /// <summary>
        /// 
        /// </summary>
        Mapping     = 1 << 6,

        /// <summary>
        /// 
        /// </summary>
        Custom      = 1 << 7,

        /// <summary>
        /// 
        /// </summary>
        Always      = Create | Update | Delete | Fetch | Mapping | Custom,
    }
}
