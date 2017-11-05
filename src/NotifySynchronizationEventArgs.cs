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
    public sealed class NotifySynchronizationEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="synchronizationEntry"></param>
        public NotifySynchronizationEventArgs(NotifySynchronizationEntry synchronizationEntry)
            : base()
        {
            _synchronizationEntry = synchronizationEntry;
        }
        #endregion

        #region Fields
        readonly NotifySynchronizationEntry _synchronizationEntry = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public NotifySynchronizationEntry SynchronizationEntry
        {
            get
            {
                return _synchronizationEntry;
            }
        }
        #endregion
    }
}
