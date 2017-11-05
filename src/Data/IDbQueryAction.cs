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
    public interface IDbQueryAction
    {
        /// <summary>
        /// 
        /// </summary>
        DbQueryActions QueryAction { get; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryPropertyDirections PropertyDirection { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsRequired { get; }

        /// <summary>
        /// 
        /// </summary>
        bool EnableToMergePrefixForInput { get; }

        /// <summary>
        /// 
        /// </summary>
        bool EnableToMergePrefixForOutput { get; }
    }
}
