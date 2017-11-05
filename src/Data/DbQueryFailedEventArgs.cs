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
    public sealed class DbQueryFailedEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public DbQueryFailedEventArgs(DbQueryException exception)
            : base()
        {
            _exception = exception;
        }
        #endregion

        #region Fields
        readonly DbQueryException _exception = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryException Error
        {
            get
            {
                return _exception;
            }
        }
        #endregion
    }
}
