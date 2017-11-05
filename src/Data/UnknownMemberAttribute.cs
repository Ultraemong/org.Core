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
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class UnknownMemberAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public UnknownMemberAttribute()
            : base()
        {
        }
        #endregion
    }
}
