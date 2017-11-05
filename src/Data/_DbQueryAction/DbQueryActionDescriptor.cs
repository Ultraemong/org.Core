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
    public sealed class DbQueryActionDescriptor : AttributeMemberDescriptor, IDbQueryAction
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        public DbQueryActionDescriptor(Attribute attribute)
            : base(attribute)
        {
            _queryAction = attribute as IDbQueryAction; 
        }
        #endregion

        #region Fields
        readonly IDbQueryAction _queryAction = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryActions QueryAction
        {
            get 
            {
                return _queryAction.QueryAction;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryPropertyDirections PropertyDirection
        {
            get
            {
                return _queryAction.PropertyDirection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRequired
        {
            get
            {
                return _queryAction.IsRequired;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnableToMergePrefixForInput
        {
            get
            {
                return _queryAction.EnableToMergePrefixForInput;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnableToMergePrefixForOutput
        {
            get
            {
                return _queryAction.EnableToMergePrefixForOutput;
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
