
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Reflection;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbQueryContractDescriptor : AttributeMemberDescriptor, IDbQueryContract, IEntryMemberDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="declaringDescriptor"></param>
        public DbQueryContractDescriptor(Attribute attribute, IEntryDescriptor declaringDescriptor)
            : base(attribute)
        {
            _queryContract          = attribute as IDbQueryContract;
            _declaringDescriptor    = declaringDescriptor;
        }
        #endregion

        #region Fields
        readonly IDbQueryContract   _queryContract          = null;
        readonly IEntryDescriptor   _declaringDescriptor    = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IEntryDescriptor DeclaringDescriptor
        {
            get
            {
                return _declaringDescriptor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string QueryText
        {
            get 
            {
                return _queryContract.QueryText;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryActions QueryAction
        {
            get
            {
                return _queryContract.QueryAction;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryBehaviors QueryBehavior
        {
            get
            {
                return _queryContract.QueryBehavior;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ExecutionTimeout
        {
            get
            {
                return _queryContract.ExecutionTimeout;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool RequiresTransaction
        {
            get
            {
                return _queryContract.RequiresTransaction;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get 
            {
                return _queryContract.Name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Schema
        {
            get
            {
                return _queryContract.Schema;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Abbreviation
        {
            get
            {
                return _queryContract.Abbreviation;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBound
        {
            get
            {
                return !(QueryAction.HasFlag(DbQueryActions.Unknown) 
                    || string.IsNullOrEmpty(QueryText));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool OmitsAbbreviationNaming
        {
            get
            {
                return _queryContract.OmitsAbbreviationNaming;
            }
        }
        #endregion
    }
}
