using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;

using org.Core.Reflection;
using org.Core.Utilities;
using org.Core.Validation;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbQueryPropertyDescriptor : PropertyMemberDescriptor, IDbQueryProperty, IDbQueryActionProvider, IEntryMemberDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="declaringDescriptor"></param>
        public DbQueryPropertyDescriptor(PropertyInfo propertyInfo, IEntryDescriptor declaringDescriptor)
            : base(propertyInfo)
        {
            var _descriptor = AttributeDescriptors.GetDescriptorByAttributeType(typeof(IDbQueryProperty));
            
            if (null != _descriptor)
                _queryProperty = _descriptor.Member as IDbQueryProperty;

            _declaringDescriptor = declaringDescriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <param name="queryAction"></param>
        internal DbQueryPropertyDescriptor(DbQueryPropertyDescriptor propertyDescriptor, DbQueryActions? queryAction)
            : base(propertyDescriptor.Member)
        {
            _queryProperty          = propertyDescriptor._queryProperty;
            _declaringDescriptor    = propertyDescriptor._declaringDescriptor;

            _queryAction            = queryAction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <param name="queryAction"></param>
        /// <param name="propertyDirection"></param>
        internal DbQueryPropertyDescriptor(DbQueryPropertyDescriptor propertyDescriptor, DbQueryActions? queryAction, DbQueryPropertyDirections? propertyDirection)
            : base(propertyDescriptor.Member)
        {
            _queryProperty          = propertyDescriptor._queryProperty;
            _declaringDescriptor    = propertyDescriptor._declaringDescriptor;

            _queryAction            = queryAction;
            _propertyDirection      = propertyDirection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <param name="propertyDirection"></param>
        internal DbQueryPropertyDescriptor(DbQueryPropertyDescriptor propertyDescriptor, DbQueryPropertyDirections? propertyDirection)
            : base(propertyDescriptor.Member)
        {
            _queryProperty          = propertyDescriptor._queryProperty;
            _declaringDescriptor    = propertyDescriptor._declaringDescriptor;

            _queryAction            = propertyDescriptor._queryAction;
            _propertyDirection      = propertyDirection;
        }
        #endregion

        #region Constants
        public const char                           PROPERTYNAME_DELIMETER          = '_';
        public const string                         PARAMETERNAME_FORMAT            = "{0}_{1}";
        #endregion

        #region Fields
        readonly IDbQueryProperty                   _queryProperty                  = null;
        readonly IEntryDescriptor                   _declaringDescriptor            = null;
        
        DbQueryActions?                             _queryAction                    = null;
        DbQueryPropertyDirections?                  _propertyDirection              = null;

        bool?                                       _hasInputDirection              = null;
        bool?                                       _hasOutputDirection             = null;

        string                                      _cachedInputName                = null;
        string                                      _cachedOutputName               = null;
        
        string                                      _cachedName                     = null;
        string                                      _cachedPrefix                   = null;

        DbQueryActionDescriptors                    _queryActionMembers             = null;
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
        public string Prefix
        {
            get 
            {
                if (string.IsNullOrEmpty(_cachedPrefix))
                {
                    _cachedPrefix = _queryProperty.Prefix;

                    if (string.IsNullOrEmpty(_cachedPrefix))
                        return _cachedPrefix = DeclaringDescriptor.Name;
                }

                return _cachedPrefix;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_cachedName))
                {
                    _cachedName = _queryProperty.Name;

                    if (string.IsNullOrEmpty(_cachedName))
                        return _cachedName = Member.Name;
                }

                return _cachedName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SqlDbType DbType
        {
            get
            {
                return _queryProperty.DbType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get
            {
                return _queryProperty.Size;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Precision
        {
            get
            {
                return _queryProperty.Precision;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte Scale
        {
            get
            {
                return _queryProperty.Scale;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsNullable
        {
            get
            {
                return _queryProperty.IsNullable;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMappable
        {
            get
            {
                return _queryProperty.IsMappable;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPrimaryKey
        {
            get
            {
                return _queryProperty.IsPrimaryKey;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object DefaultValue
        {
            get
            {
                return _queryProperty.DefaultValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Order
        {
            get 
            {
                return _queryProperty.Order;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IgnoreXmlDataMember
        {
            get 
            {
                return (_queryProperty.IgnoreXmlDataMember);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryActionDescriptors ActionDescriptors
        {
            get
            {
                if (null == _queryActionMembers)
                    return _queryActionMembers = DbQueryActionHelpers.RetrieveMemberDescriptors(AttributeDescriptors, _queryAction);

                return _queryActionMembers;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryActions QueryAction
        {
            get
            {
                if (!_queryAction.HasValue)
                {
                    if (!ActionDescriptors.IsEmpty)
                    {
                        foreach (var _memberDescriptor in ActionDescriptors)
                        {
                            if (!_queryAction.HasValue)
                            {
                                _queryAction = _memberDescriptor.QueryAction;
                                continue;
                            }

                            _queryAction |= _memberDescriptor.QueryAction;
                        }

                        return _queryAction.Value;
                    }

                    _queryAction = DbQueryActions.Unknown;
                }

                return _queryAction.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryPropertyDirections PropertyDirection
        {
            get
            {
                if (!_propertyDirection.HasValue)
                {
                    if (!ActionDescriptors.IsEmpty)
                    {
                        foreach (var _memberDescriptor in ActionDescriptors)
                        {
                            if (!_propertyDirection.HasValue)
                            {
                                _propertyDirection = _memberDescriptor.PropertyDirection;
                                continue;
                            }

                            _propertyDirection |= _memberDescriptor.PropertyDirection;
                        }

                        return _propertyDirection.Value;
                    }

                    _propertyDirection = DbQueryPropertyDirections.None;
                }

                return _propertyDirection.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasInputDirection
        {
            get
            {
                if (!_hasInputDirection.HasValue)
                    _hasInputDirection = PropertyDirection.HasFlag(DbQueryPropertyDirections.Input);

                return _hasInputDirection.GetValueOrDefault(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasOutputDirection
        {
            get
            {
                if (!_hasOutputDirection.HasValue)
                    _hasOutputDirection = PropertyDirection.HasFlag(DbQueryPropertyDirections.Output);

                return _hasOutputDirection.GetValueOrDefault(false);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enableToMergePrefix"></param>
        /// <returns></returns>
        public string GetName(bool enableToMergePrefix)
        {
            if (enableToMergePrefix)
                return string.Format(PARAMETERNAME_FORMAT, Prefix, Name);

            return Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDirection"></param>
        /// <returns></returns>
        public string GetName(DbQueryPropertyDirections propertyDirection)
        {
            var _enableToMergePrefix = false;

            if (propertyDirection.Equals(DbQueryPropertyDirections.Input))
            {
                if (string.IsNullOrEmpty(_cachedInputName))
                {
                    _enableToMergePrefix = ActionDescriptors.IsDeclared(x =>
                        x.PropertyDirection.HasFlag(DbQueryPropertyDirections.Input) && x.EnableToMergePrefixForInput);

                    return _cachedInputName = GetName(_enableToMergePrefix);
                }

                return _cachedInputName;
            }
            else if (propertyDirection.Equals(DbQueryPropertyDirections.Output))
            {
                if (string.IsNullOrEmpty(_cachedOutputName))
                {
                    _enableToMergePrefix = ActionDescriptors.IsDeclared(x =>
                        x.PropertyDirection.HasFlag(DbQueryPropertyDirections.Output) && x.EnableToMergePrefixForOutput);

                    return _cachedOutputName = GetName(_enableToMergePrefix);
                }

                return _cachedOutputName;
            }

            return null;
        }
        #endregion
    }
}
