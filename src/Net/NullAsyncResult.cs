using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NullAsyncResult : IAsyncResult
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public NullAsyncResult()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public object AsyncState
        {
            get 
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WaitHandle AsyncWaitHandle
        {
            get 
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CompletedSynchronously
        {
            get 
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCompleted
        {
            get 
            {
                return false;    
            }
        } 
        #endregion
    }
}
