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
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class DbQueryOperationAttribute : Attribute, IDbQueryOperation
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public DbQueryOperationAttribute(DbQueryActions action)
            : base()
        {
            QueryAction = action;
        }
	    #endregion

        #region Fields
        byte _order = 0;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryActions QueryAction
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Order
        {
            get
            {
                return _order;
            }

            set
            {
                _order = value;
            }
        }
        #endregion
    }
}
