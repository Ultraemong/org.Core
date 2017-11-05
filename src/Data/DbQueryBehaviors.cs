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
    public enum DbQueryBehaviors
    {
        /// <summary>
        /// 
        /// </summary>
        Default             = 1 << 1,

        /// <summary>
        /// 
        /// </summary>
        SingleRow           = 1 << 2,

        /// <summary>
        /// 
        /// </summary>
        MultipleRows        = 1 << 3,

        /// <summary>
        /// 
        /// </summary>
        ScalarValue         = 1 << 4,

        /// <summary>
        /// 
        /// </summary>
        ReturnValue         = 1 << 5,

        /// <summary>
        /// 
        /// </summary>
        Parameterized       = 1 << 6,

        /// <summary>
        /// 
        /// </summary>
        SchemaOnly          = 1 << 7,
    }
}
