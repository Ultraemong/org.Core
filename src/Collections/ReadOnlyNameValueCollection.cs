using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class ReadOnlyNameValueCollection<TKey, TValue> : IReadOnlyNameValueCollection<TKey, TValue>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public ReadOnlyNameValueCollection(IDictionary<TKey, TValue> dictionary)
        {
            _repository = new NameValueCollection<TKey, TValue>(dictionary);
        }
        #endregion

        #region Fields
        INameValueCollection<TKey, TValue> _repository = default(INameValueCollection<TKey, TValue>);
        #endregion

        #region Indexers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get 
            {
                return (_repository as IReadOnlyDictionary<TKey, TValue>)[key];
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (0 == Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get 
            {
                return (_repository as IReadOnlyDictionary<TKey, TValue>).Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get 
            {
                return (_repository as IReadOnlyDictionary<TKey, TValue>).Keys;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get 
            {
                return (_repository as IReadOnlyDictionary<TKey, TValue>).Values;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return (_repository as IReadOnlyDictionary<TKey, TValue>).ContainsKey(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return (_repository as IReadOnlyDictionary<TKey, TValue>).TryGetValue(key, out value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return (_repository as IReadOnlyDictionary<TKey, TValue>).GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_repository as IEnumerable).GetEnumerator();
        }
        #endregion
    }
}
