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
    public sealed class IgnoreChildPropertyDescriptor : AttributeMemberDescriptor, IEntryMemberDescriptor, IIgnoreChildProperty
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeDescriptor"></param>
        /// <param name="declaringDescriptor"></param>
        /// <param name="declaringQueryProperty"></param>
        public IgnoreChildPropertyDescriptor(AttributeMemberDescriptor attributeDescriptor, IEntryDescriptor declaringDescriptor, DbQueryPropertyDescriptor declaringQueryProperty)
            : base(attributeDescriptor.Member)
        {
            _declaringDescriptor    = declaringDescriptor;
            _declaringQueryProperty = declaringQueryProperty;

            _ignoreChildProperty    = attributeDescriptor.Member as IIgnoreChildProperty;

            _queryAction            = _ignoreChildProperty.QueryAction;
            _propertyName           = _ignoreChildProperty.PropertyName;
            _propertyDirection      = _ignoreChildProperty.PropertyDirection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeDescriptor"></param>
        /// <param name="queryAction"></param>
        internal IgnoreChildPropertyDescriptor(IgnoreChildPropertyDescriptor ignorePropertyDescriptor, DbQueryActions queryAction)
            : base(ignorePropertyDescriptor.Member)
        {
            _declaringDescriptor        = ignorePropertyDescriptor._declaringDescriptor;
            _declaringQueryProperty     = ignorePropertyDescriptor._declaringQueryProperty;

            _ignoreChildProperty        = ignorePropertyDescriptor._ignoreChildProperty;

            _queryAction                = queryAction;
            _propertyName               = ignorePropertyDescriptor._propertyName;
            _propertyDirection          = ignorePropertyDescriptor._propertyDirection;
        }
        #endregion

        #region Fields
        readonly DbQueryActions             _queryAction                    = DbQueryActions.Always;
        readonly string                     _propertyName                   = null;
        readonly DbQueryPropertyDirections  _propertyDirection              = DbQueryPropertyDirections.InputOutput;

        readonly IIgnoreChildProperty       _ignoreChildProperty            = null;
        readonly IEntryDescriptor           _declaringDescriptor            = null;
        readonly DbQueryPropertyDescriptor  _declaringQueryProperty         = null;
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
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryPropertyDescriptor DeclaringQueryProperty
        {
            get
            {
                return _declaringQueryProperty;
            }
        }
        #endregion
    }
}
