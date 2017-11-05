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
    public sealed class DbQueryOperatingSessionStateChangedEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="addedState"></param>
        public DbQueryOperatingSessionStateChangedEventArgs(DbQueryOperatingSessionStates currentState, DbQueryOperatingSessionStates addedState)
            : base()
        {
            CurrentState    = currentState;
            AddedState      = addedState;
        } 
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryOperatingSessionStates CurrentState
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryOperatingSessionStates AddedState
        {
            get;
            private set;
        }
        #endregion
    }
}
