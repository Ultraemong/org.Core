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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class OidAttribute : Attribute
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oid"></param>
        public OidAttribute(string oid)
        {
            _oid = Guid.Parse(oid);
        }
        #endregion

        #region Fields
        readonly Guid _oid = Guid.Empty;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Guid Oid
        {
            get
            {
                return _oid;
            }
        }
        #endregion
    }
}
