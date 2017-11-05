using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using org.Core.Collections;
using org.Core.Utilities;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class NameValueCollectionConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public NameValueCollectionConverter()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        IReadOnlyNameValueCollection<string, object> AnonymousObjectToNameValueCollection(object obj)
        {
            var _collection = new NameValueCollection<string, object>(StringComparer.InvariantCultureIgnoreCase);

            foreach (System.ComponentModel.PropertyDescriptor _descriptor in System.ComponentModel.TypeDescriptor.GetProperties(obj))
            {
                _collection.Add(_descriptor.Name.Replace('_', '-'), _descriptor.GetValue(obj));
            }

            return _collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Convert(object value)
        {
            if (null != value)
            {
                if (ObjectUtils.IsAnonymousType(value))
                    return AnonymousObjectToNameValueCollection(value);
            }

            return (IReadOnlyNameValueCollection<string, object>)null;
        } 
        #endregion
    }
}
