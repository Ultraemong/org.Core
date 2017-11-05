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
    public abstract class DbQueryOperatingPrinciple
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatingSession"></param>
        public DbQueryOperatingPrinciple(DbQueryOperatingSession operatingSession)
        {
            _operatingSession = operatingSession;
        }
        #endregion

        #region Fields
        readonly DbQueryOperatingSession _operatingSession = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryOperatingSession OperatingSession
        {
            get
            {
                return _operatingSession;
            }
        }
        #endregion
    }
}
