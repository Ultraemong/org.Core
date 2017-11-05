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
    public class NameValueCollection<TKey, TValue> : IDictionary, ICollection, INameValueCollection<TKey, TValue>, IReadOnlyNameValueCollection<TKey, TValue>, IEnumerable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public NameValueCollection()
        {
            _innerRepository = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        public NameValueCollection(int capacity)
        {
            _innerRepository = new Dictionary<TKey, TValue>(capacity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        public NameValueCollection(IEqualityComparer<TKey> comparer)
        {
            _innerRepository = new Dictionary<TKey, TValue>(comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public NameValueCollection(IDictionary<TKey, TValue> dictionary)
        {
            _innerRepository = new Dictionary<TKey, TValue>(dictionary);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        /// <param name="comparer"></param>
        public NameValueCollection(int capacity, IEqualityComparer<TKey> comparer)
        {
            _innerRepository = new Dictionary<TKey, TValue>(capacity, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="comparer"></param>
        public NameValueCollection(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            _innerRepository = new Dictionary<TKey, TValue>(dictionary, comparer);
        }
        #endregion

        #region Fields
        readonly object                     _syncRoot           = new object();
        readonly IDictionary<TKey, TValue>  _innerRepository    = default(IDictionary<TKey, TValue>);
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
                return _innerRepository[key];
            }

            set
            {
                _innerRepository[key] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object IDictionary.this[object key]
        {
            get
            {
                return (_innerRepository as IDictionary)[key];
            }

            set
            {
                (_innerRepository as IDictionary)[key] = value;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public ICollection<TKey> Keys
        {
            get 
            {
                return _innerRepository.Keys;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return _innerRepository.Values;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get 
            {
                return _innerRepository.Count;
            }
        }

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
        public bool IsReadOnly
        {
            get 
            {
                return _innerRepository.IsReadOnly;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFixedSize
        {
            get 
            {
                return (_innerRepository as IDictionary).IsFixedSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return (_innerRepository as IDictionary).IsSynchronized;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return _syncRoot;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ICollection IDictionary.Keys
        {
            get
            {
                return (_innerRepository as IDictionary).Keys;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ICollection IDictionary.Values
        {
            get 
            {
                return (_innerRepository as IDictionary).Values;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get 
            {
                return _innerRepository.Keys;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get 
            {
                return _innerRepository.Values;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            _innerRepository.Add(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return _innerRepository.ContainsKey(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            return _innerRepository.Remove(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _innerRepository.Remove(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _innerRepository.TryGetValue(key, out value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _innerRepository.Add(item);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _innerRepository.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _innerRepository.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _innerRepository.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            (_innerRepository as IDictionary).CopyTo(array, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _innerRepository.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void IDictionary.Add(object key, object value)
        {
            (_innerRepository as IDictionary).Add(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IDictionary.Contains(object key)
        {
            return (_innerRepository as IDictionary).Contains(key);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void IDictionary.Remove(object key)
        {
            (_innerRepository as IDictionary).Remove(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (_innerRepository as IEnumerable).GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
        {
            return (_innerRepository as IDictionary).GetEnumerator();
        }
        #endregion
    }
}
