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
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class IgnoreChildPropertyAttribute : Attribute, IIgnoreChildProperty
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="queryAction"></param>
        public IgnoreChildPropertyAttribute(string propertyName, DbQueryActions queryAction)
            : base()
        {
            _propertyName   = propertyName;
            _queryAction    = queryAction;
        }
        #endregion

        #region Fields
        readonly DbQueryActions     _queryAction        = DbQueryActions.Always;
        readonly string             _propertyName       = null;

        DbQueryPropertyDirections   _propertyDirection  = DbQueryPropertyDirections.InputOutput;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryActions QueryAction
        {
            get
            {
                return _queryAction;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryPropertyDirections PropertyDirection
        {
            get
            {
                return _propertyDirection;
            }

            set
            {
                _propertyDirection = value;
            }
        }
        #endregion
    }
}
