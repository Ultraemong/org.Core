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
    public sealed class DbQueryExecutingEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DbQueryExecutingEventArgs()
            : base()
        {
        }
        #endregion

        #region Fields
        bool _cancel = false;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool Cancel
        {
            get
            {
                return _cancel;
            }

            set
            {
                _cancel = value;
            }
        }
        #endregion
    }
}
