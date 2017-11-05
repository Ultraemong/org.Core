using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class OperationResourceAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public OperationResourceAttribute()
            : base()
        {
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion
    }
}
