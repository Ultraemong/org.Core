using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Collections
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class ListCollection<T> : IListCollection<T>, IReadOnlyListCollection<T>, IEnumerable, IList, ICollection
    {
        #region Constuctors
        /// <summary>
        /// 
        /// </summary>
        public ListCollection()
        {
            _innerRepository    = new List<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSynchronized"></param>
        public ListCollection(bool isSynchronized)
            : this()
        {
            _isSynchronized     = isSynchronized;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="syncRoot"></param>
        public ListCollection(object syncRoot)
            : this()
        {
            _syncRoot           = syncRoot;
            _isSynchronized     = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        public ListCollection(int capacity)
        {
            _isFixedSize        = true;
            _innerRepository    = new List<T>(capacity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public ListCollection(IEnumerable<T> collection)
        {
            _isReadOnly         = true;
            _innerRepository    = new List<T>(collection);
        }
        #endregion

        #region Fields
        [NonSerialized]
        readonly object     _syncRoot           = new object();

        [DataMember]
        readonly bool       _isReadOnly         = false;

        [DataMember]
        readonly bool       _isFixedSize        = false;

        [DataMember]
        readonly bool       _isSynchronized     = false;

        [DataMember]
        readonly List<T>    _innerRepository    = default(List<T>);
        #endregion

        #region Indexers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return _innerRepository[index];
            }

            set
            {
                _innerRepository[index] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        object IList.this[int index]
        {
            get
            {
                return (_innerRepository as IList)[index];
            }

            set
            {
                (_innerRepository as IList)[index] = value;
            }
        }
        #endregion

        #region Properties
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
                return (0 == _innerRepository.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFixedSize
        {
            get
            {
                return _isFixedSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return _isSynchronized;
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
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _innerRepository.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_innerRepository as IEnumerable).GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            return _innerRepository.IndexOf(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            if (!IsReadOnly)
            {
                if (!IsSynchronized)
                {
                    _innerRepository.Insert(index, item);
                }
                else
                {
                    lock (_syncRoot)
                    {
                        _innerRepository.Insert(index, item);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            if (!IsReadOnly)
            {
                if (!IsSynchronized)
                {
                    _innerRepository.RemoveAt(index);
                }
                else
                {
                    lock (_syncRoot)
                    {
                        _innerRepository.RemoveAt(index);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (!IsReadOnly)
            {
                if (!IsSynchronized)
                {
                    _innerRepository.Add(item);
                }
                else
                {
                    lock (_syncRoot)
                    {
                        _innerRepository.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(IEnumerable<T> collection)
        {
            if (!IsReadOnly)
            {
                if (!IsSynchronized)
                {
                    foreach (var _element in collection)
                    {
                        _innerRepository.Add(_element);
                    }
                }
                else
                {
                    lock (_syncRoot)
                    {
                        foreach (var _element in collection)
                        {
                            _innerRepository.Add(_element);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            if (!IsReadOnly)
            {
                if (!IsSynchronized)
                {
                    _innerRepository.Clear();
                }
                else
                {
                    lock (_syncRoot)
                    {
                        _innerRepository.Clear();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return _innerRepository.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _innerRepository.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            var _remove = false;

            if (!IsReadOnly)
            {
                if (!IsSynchronized)
                {
                    _remove = _innerRepository.Remove(item);
                }
                else
                {
                    lock (_syncRoot)
                    {
                        _remove = _innerRepository.Remove(item);
                    }
                }
            }

            return _remove;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(object value)
        {
            var _index = -1;

            if (!IsReadOnly)
            {
                if (!IsSynchronized)
                {
                    Add((T)value);

                    _index = (Count - 1);
                }
                else
                {
                    lock (_syncRoot)
                    {
                        Add((T)value);

                        _index = (Count - 1);
                    }
                }
            }

            return (Count - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(object value)
        {
            return Contains((T)value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            CopyTo((T[])array, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, object value)
        {
            if (!IsReadOnly)
            {
                if (!IsSynchronized)
                {
                    Insert(index, (T)value);
                }
                else
                {
                    lock (_syncRoot)
                    {
                        Insert(index, (T)value);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Remove(object value)
        {
            if (!IsReadOnly)
            {
                if (!IsSynchronized)
                {
                    Remove((T)value);
                }
                else
                {
                    lock (_syncRoot)
                    {
                        Remove((T)value);
                    }
                }
            }
        }
        #endregion
    }
}
