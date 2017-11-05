using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq.Expressions;

using org.Core.Collections;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDescriptor"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public abstract class AttributeMemberDescriptors<TDescriptor, TCollection> : org.Core.Collections.ReadOnlyListCollection<TDescriptor>
        where TDescriptor : AttributeMemberDescriptor
        where TCollection : ReadOnlyListCollection<TDescriptor>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public AttributeMemberDescriptors(IEnumerable<TDescriptor> collection)
            : base(collection)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public TDescriptor GetDescriptorFor<T>(Expression<Func<T, bool>> selector)
        {
            var _selector = selector.Compile();

            foreach (var _descriptor in this)
            {
                var _origin = _descriptor.Member as object;

                if (_selector((T)_origin))
                {
                    return _descriptor;
                }
            }

            return default(TDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public TCollection GetDescriptorsFor<T>(Expression<Func<T, bool>> selector)
        {
            var _dummy      = new List<TDescriptor>();
            var _selector   = selector.Compile();

            foreach (var _descriptor in this)
            {
                var _origin = _descriptor.Member as object;

                if (_selector((T)_origin))
                {
                    _dummy.Add(_descriptor);
                }
            }

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_dummy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public TDescriptor GetDescriptorByAttributeType(Type attributeType)
        {
            foreach (var _descriptor in this)
            {
                var _originType = _descriptor.Member.GetType();

                if (AttributeMemberHelpers.IsSpecificTypeDefined(_originType, attributeType))
                {
                    return _descriptor;
                }
            }

            return default(TDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public TCollection GetDescriptorsByAttributeType(Type attributeType)
        {
            var _dummy = new List<TDescriptor>();

            foreach (var _descriptor in this)
            {
                var _originType = _descriptor.Member.GetType();

                if (AttributeMemberHelpers.IsSpecificTypeDefined(_originType, attributeType))
                {
                    _dummy.Add(_descriptor);
                }
            }

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_dummy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="selector"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public TCollection GetDescriptorsByAttribute<TAttribute>(Expression<Func<TAttribute, bool>> selector, bool inherit = false)
            where TAttribute : Attribute
        {
            var _type       = typeof(TAttribute);
            var _dummy      = new List<TDescriptor>();
            var _selector   = selector.Compile();

            foreach (var _descriptor in this)
            {
                var _origin     = _descriptor.Member;
                var _originType = _origin.GetType();

                if (AttributeMemberHelpers.IsSpecificTypeDefined(_originType, _type) && _selector((TAttribute)_origin))
                {
                    _dummy.Add(_descriptor);
                }
            }

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_dummy);
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class AttributeMemberDescriptors : AttributeMemberDescriptors<AttributeMemberDescriptor, AttributeMemberDescriptors>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public AttributeMemberDescriptors(IEnumerable<AttributeMemberDescriptor> collection)
            : base(collection)
        {
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        #endregion
    }
}
