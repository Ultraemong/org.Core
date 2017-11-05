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
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class DbQueryContractAttribute : Attribute, IDbQueryContract
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DbQueryContractAttribute()
        {   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryAction"></param>
        /// <param name="queryText"></param>
        public DbQueryContractAttribute(DbQueryActions queryAction, string queryText)
            : base()
        {
            _queryAction    = queryAction;
            _queryText      = queryText;
        }
        #endregion

        #region Fields
        readonly string             _queryText                  = null;
        readonly DbQueryActions     _queryAction                = DbQueryActions.Unknown;

        string                      _name                       = null;
        string                      _schema                     = DbQueryContract.QUERYTEXT_SCHEMA;
        string                      _abbreviation               = null;

        int                         _executionTimeout           = DbQueryContract.EXECUTION_TIMEOUT;
        bool                        _requiresTransaction        = false;
        bool                        _omitsAbbreviationNaming    = false;
    
        DbQueryBehaviors            _queryBehavior          = DbQueryBehaviors.Default;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string QueryText
        {
            get
            {
                return _queryText;
            }
        }

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
        public DbQueryBehaviors QueryBehavior 
        { 
            get
            {
                return _queryBehavior;
            }

            set
            {
                _queryBehavior = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ExecutionTimeout
        {
            get
            {
                return _executionTimeout;
            }

            set
            {
                _executionTimeout = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool RequiresTransaction
        {
            get
            {
                return _requiresTransaction;
            }

            set
            {
                _requiresTransaction = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Schema
        {
            get
            {
                return _schema;
            }

            set
            {
                _schema = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Abbreviation
        {
            get
            {
                return _abbreviation;
            }

            set
            {
                _abbreviation = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool OmitsAbbreviationNaming
        {
            get
            {
                return _omitsAbbreviationNaming;
            }

            set
            {
                _omitsAbbreviationNaming = value;
            }
        }
        #endregion
    }
}
