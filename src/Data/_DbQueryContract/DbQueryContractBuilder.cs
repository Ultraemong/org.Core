using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using org.Core.Design;
using org.Core.Extensions;
using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbQueryContractBuilder
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DbQueryContractBuilder()
        {
            _namingService = NamingServiceFactory.CreateNamingService(CultureInfo.GetCultureInfo("en-US"));
        }
        #endregion

        #region Fields
        bool                        _isSingularOnly             = true;
        readonly INamingService     _namingService              = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Schema
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Namespace
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Abbreviation
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryActions QueryAction
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsElementContainable
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSingularOnly
        {
            get
            {
                return _isSingularOnly;
            }

            set
            {
                _isSingularOnly = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool OmitsAbbreviationNaming
        {
            get;
            set;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetLeftString()
        {
            if (!OmitsAbbreviationNaming)
            {
                if (string.IsNullOrEmpty(Abbreviation))
                {
                    var _split  = _namingService.ExceptReserved(Namespace.Split('.'));
                    var _prefix = _namingService.Abbreviate(_split.GetValueOrDefault((_split.Length - 1), string.Empty));

                    return string.Format("{0}sp", _prefix.ToLower());
                }

                return string.Format("{0}sp", _namingService.Abbreviate(Abbreviation).ToLower());
            }

            return "sp";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCenterString()
        {
            var _output = Name;

            if (!OmitsAbbreviationNaming)
            {
                if (string.IsNullOrEmpty(Abbreviation))
                {
                    var _split      = _namingService.ExceptReserved(Namespace.Split('.'));
                    var _prefix     = _namingService.Abbreviate(_split.GetValueOrDefault((_split.Length - 1), string.Empty));

                    _output = _output.Except(_prefix);

                    if (IsSingularOnly && _namingService.IsPlural(_output))
                        _output = _namingService.Singularize(_output);
                }
                else
                {
                    _output = _output.Except(_namingService.Abbreviate(Abbreviation));

                    if (IsSingularOnly && _namingService.IsPlural(_output))
                        _output = _namingService.Singularize(_output);
                }
            }
            else
            {
                if (IsSingularOnly && _namingService.IsPlural(_output))
                    _output = _namingService.Singularize(_output);
            }

            return _output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetRightString()
        {
            switch(QueryAction)
            {
                case DbQueryActions.Create:
                    return "Create";

                case DbQueryActions.Delete:
                    return "Delete";

                case DbQueryActions.Update:
                    return "Update";

                case DbQueryActions.Fetch:
                    return (IsElementContainable) ? "GetList" : "GetInfo";
            }

            throw new NotSupportedException(string.Format("the '{0}' is not supported.", QueryAction));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbQueryContract GetDbQueryContract()
        {
            var _left           = GetLeftString();
            var _center         = GetCenterString();
            var _right          = GetRightString();
            var _queryText      = string.Format(DbQueryContract.QUERYTEXT_FORMAT, Schema, _left, _center, _right);
            var _queryBehavior  = DbQueryBehaviors.Default;

            if (IsElementContainable)
                _queryBehavior = DbQueryBehaviors.MultipleRows;

            return new DbQueryContract(_queryText, QueryAction, _queryBehavior, Name, Schema, Abbreviation, OmitsAbbreviationNaming);
        }
        #endregion
    }
}
