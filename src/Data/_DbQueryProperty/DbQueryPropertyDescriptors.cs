using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;

using org.Core.Reflection;
using org.Core.Utilities;
using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbQueryPropertyDescriptors : PropertyMemberDescriptors<DbQueryPropertyDescriptor, DbQueryPropertyDescriptors>, IEntryMemberDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="declaringDescriptor"></param>
        public DbQueryPropertyDescriptors(IEnumerable<DbQueryPropertyDescriptor> collection, IEntryDescriptor declaringDescriptor)
            : base(collection)
        {
            _declaringDescriptor    = declaringDescriptor;
            _innerLinkedRepository  = new NameValueCollection<string, int>(StringComparer.InvariantCultureIgnoreCase);

            for (var _index = 0; _index < Count; _index++)
                _innerLinkedRepository.Add(this[_index].GetName(true), _index);
        }
        #endregion

        #region Fields
        readonly IEntryDescriptor                   _declaringDescriptor    = null;
        readonly NameValueCollection<string, int>   _innerLinkedRepository  = default(NameValueCollection<string, int>);
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
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public DbQueryPropertyDescriptor GetDescriptorByPropertyName(string propertyName)
        {
            if (_innerLinkedRepository.ContainsKey(propertyName))
            {
                return this[_innerLinkedRepository[propertyName]];
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryAction"></param>
        /// <param name="ignorePropertyDescriptors"></param>
        /// <returns></returns>
        public DbQueryPropertyDescriptors GetDescriptorsByQueryAction(DbQueryActions queryAction)
        {
            var _descriptors = this.Where(prop => prop.ActionDescriptors.IsDeclared(queryAction))
                .Select(prop => new DbQueryPropertyDescriptor(prop, queryAction));

            return new DbQueryPropertyDescriptors(_descriptors, DeclaringDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public DbQueryPropertyDescriptors GetDescriptorsByQueryProperty(Expression<Func<IDbQueryProperty, bool>> selector)
        {
            var _selector = selector.Compile();

            return new DbQueryPropertyDescriptors(this.Where(x => _selector(x)), _declaringDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryAction"></param>
        /// <param name="ignorePropertyDescriptors"></param>
        /// <returns></returns>
        public DbQueryPropertyDescriptors GetDescriptors(DbQueryActions queryAction, IgnoreChildPropertyDescriptors ignorePropertyDescriptors)
        {
            var _filteredProperties = GetDescriptorsByQueryAction(queryAction);

            if (!ignorePropertyDescriptors.IsEmpty)
            {
                var _propertyCollection = new ListCollection<DbQueryPropertyDescriptor>();

                foreach (var _filteredProperty in _filteredProperties)
                {
                    var _ignoreDescriptor = ignorePropertyDescriptors.GetDescriptor(_filteredProperty);

                    if (null != _ignoreDescriptor)
                    {
                        if (!_filteredProperty.PropertyDirection.Equals(DbQueryPropertyDirections.None))
                        {
                            var _propertyDirection = _filteredProperty.PropertyDirection & ~_ignoreDescriptor.PropertyDirection;

                            if (!((int)_propertyDirection).Equals(0))
                            {
                                _propertyCollection.Add(new DbQueryPropertyDescriptor(_filteredProperty, _propertyDirection));
                            }
                        }

                        continue;
                    }

                    _propertyCollection.Add(_filteredProperty);
                }

                return new DbQueryPropertyDescriptors(_propertyCollection, _declaringDescriptor);
            }

            return _filteredProperties;
        }
        #endregion
    }
}
