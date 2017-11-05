using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using org.Core.Collections;
using System.Linq.Expressions;

namespace org.Core.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MemberDescriptors<TDescriptor, TCollection> : ReadOnlyListCollection<TDescriptor>
        where TDescriptor : MemberDescriptor
        where TCollection : ReadOnlyListCollection<TDescriptor>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public MemberDescriptors(IEnumerable<TDescriptor> collection)
            : base(collection)
        {   
        } 
        #endregion

        #region Fields
        #endregion

        #region Indexers
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TDescriptor GetDescriptorByName(string name)
        {
            foreach (var _descriptor in this)
            {
                var _originName = _descriptor.Member.Name.ToLower();

                if (_originName.Equals(name.ToLower()))
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
        public TDescriptor GetDescriptorByAttributeType(Type attributeType, bool inherit = false)
        {
            foreach (var _descriptor in this)
            {
                var _originType = _descriptor.Member.GetType();

                if (!inherit)
                {
                    if (attributeType.Equals(_originType))
                    {
                        return _descriptor;
                    }
                }
                else
                {
                    if (attributeType.IsAssignableFrom(_originType))
                    {
                        return _descriptor;
                    }
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
        public TCollection GetDescriptorsByAttributeType(Type attributeType, bool inherit = false)
        {
            var _dummy = new ListCollection<TDescriptor>();

            foreach (var _descriptor in this)
            {
                foreach (var _attribute in _descriptor.AttributeDescriptors)
                {
                    var _originType = _attribute.Member.GetType();

                    if (!inherit)
                    {
                        if (attributeType.Equals(_originType))
                        {
                            _dummy.Add(_descriptor);
                            break;
                        }
                    }
                    else
                    {
                        if (attributeType.IsAssignableFrom(_originType))
                        {
                            _dummy.Add(_descriptor);
                            break;
                        }
                    }
                }
            }

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_dummy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public TCollection GetDescriptorsByAttribute<TAttribute>(Expression<Func<TAttribute, bool>> selector, bool inherit = false)
            where TAttribute : Attribute
        {
            var _type       = typeof(TAttribute);
            var _dummy      = new ListCollection<TDescriptor>();
            var _selector   = selector.Compile();

            foreach (var _descriptor in this)
            {
                foreach (var _attribute in _descriptor.AttributeDescriptors)
                {
                    var _origin         = _attribute.Member;
                    var _originType     = _origin.GetType();

                    if (!inherit)
                    {
                        if (_type.Equals(_originType) && _selector((TAttribute)_origin))
                        {
                            _dummy.Add(_descriptor);
                        }
                    }
                    else
                    {
                        if (_type.IsAssignableFrom(_originType) && _selector((TAttribute)_origin))
                        {
                            _dummy.Add(_descriptor);
                        }
                    }
                }
            }

            return ReadOnlyListCollection<TDescriptor>.Initializer.Initialize<TCollection>(_dummy);
        }
        #endregion
    }
}
