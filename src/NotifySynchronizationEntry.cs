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
    public sealed class NotifySynchronizationEntry
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isDirty"></param>
        /// <param name="isDeleted"></param>
        public NotifySynchronizationEntry(object item, bool isDirty, bool isDeleted)
        {
            _item       = item;
            _isDirty    = isDirty;
            _isDeleted  = isDeleted;
        }
        #endregion

        #region Fields
        readonly object     _item       = null;
        readonly bool       _isDirty    = false;
        readonly bool       _isDeleted  = false;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public object Item
        {
            get
            {
                return _item;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
        }
        #endregion
    }
}
