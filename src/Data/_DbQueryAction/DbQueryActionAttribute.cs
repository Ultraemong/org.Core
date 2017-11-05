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
    public sealed class DbQueryActionAttribute : Attribute, IDbQueryAction
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryAction"></param>
        public DbQueryActionAttribute(DbQueryActions queryAction)
            : base()
        {
            QueryAction = queryAction;
        }
        #endregion

        #region Fields
        bool                        _isRequired                     = false;
        bool                        _enableToMergePrefixForInput    = false;
        bool                        _enableToMergePrefixForOutput   = true;

        DbQueryPropertyDirections   _propertyDirection              = DbQueryPropertyDirections.None;
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

        /// <summary>
        /// 
        /// </summary>
        public bool IsRequired
        {
            get
            {
                return _isRequired;
            }

            set
            {
                _isRequired = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnableToMergePrefixForInput
        {
            get 
            {
                return _enableToMergePrefixForInput;
            }

            set
            {
                _enableToMergePrefixForInput = value;
            }
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        public bool EnableToMergePrefixForOutput
        {
            get 
            {
                return _enableToMergePrefixForOutput;
            }

            set
            {
                _enableToMergePrefixForOutput = value;
            }
        }
        #endregion
    }
}
