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
    public interface IIgnoreChildProperty
    {
        /// <summary>
        /// 
        /// </summary>
        DbQueryActions QueryAction { get; }

        /// <summary>
        /// 
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryPropertyDirections PropertyDirection { get; }
    }
}
