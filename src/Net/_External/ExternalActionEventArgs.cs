using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class ExternalActionEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ExternalActionEventArgs(ExternalActionContext context)
        {
            _context = context;
        }
        #endregion

        #region Fields
        readonly ExternalActionContext _context = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public ExternalActionContext Context
        {
            get
            {
                return _context;
            }
        }
        #endregion
    }
}
