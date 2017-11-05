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
    public sealed class DbQueryContract : IDbQueryContract
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="queryAction"></param>
        /// <param name="queryBehavior"></param>
        /// <param name="name"></param>
        /// <param name="schema"></param>
        /// <param name="abbreviation"></param>
        internal DbQueryContract(string queryText, DbQueryActions queryAction, DbQueryBehaviors queryBehavior, string name, string schema, string abbreviation)
        {
            _queryText      = queryText;
            _queryAction    = queryAction;
            _queryBehavior  = queryBehavior;

            _name           = name;
            _schema         = schema;
            _abbreviation   = abbreviation;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="queryAction"></param>
        /// <param name="queryBehavior"></param>
        /// <param name="name"></param>
        /// <param name="schema"></param>
        /// <param name="abbreviation"></param>
        /// <param name="omitsAbbreviationNaming"></param>
        internal DbQueryContract(string queryText, DbQueryActions queryAction, DbQueryBehaviors queryBehavior, string name, string schema, string abbreviation, bool omitsAbbreviationNaming)
            : this (queryText, queryAction, queryBehavior, name, schema, abbreviation)
        {
            _omitsAbbreviationNaming = omitsAbbreviationNaming;
        } 
        #endregion

        #region Fields
        readonly string                 _name                       = null;
        readonly string                 _schema                     = QUERYTEXT_SCHEMA;
        readonly string                 _abbreviation               = null;

        readonly string                 _queryText                  = null;

        readonly bool                   _omitsAbbreviationNaming    = false;

        readonly DbQueryActions         _queryAction                = DbQueryActions.Create;
        readonly DbQueryBehaviors       _queryBehavior              = DbQueryBehaviors.Default;

        public const int                EXECUTION_TIMEOUT           = 30;

        public const char               QUERYTEXT_DELIMETER         = '_';
        public const string             QUERYTEXT_SCHEMA            = "dbo";
        public const string             QUERYTEXT_FORMAT            = "[{0}].[{1}_{2}_{3}]";
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
        }

        /// <summary>
        /// 
        /// </summary>
        public int ExecutionTimeout
        {
            get 
            {
                return EXECUTION_TIMEOUT;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool RequiresTransaction
        {
            get 
            {
                return false;
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
        }
        #endregion
    }
}
