using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class WebSocketClientCollection : IList<WebSocketClient>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public WebSocketClientCollection()
        {
            _collection = new List<WebSocketClient>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        public WebSocketClientCollection(int capacity)
        {
            _collection = new List<WebSocketClient>(capacity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public WebSocketClientCollection(IEnumerable<WebSocketClient> collection)
        {
            _collection = new List<WebSocketClient>(collection);
        } 
        #endregion

        #region Fields
        IList<WebSocketClient> _collection = default(IList<WebSocketClient>);
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public WebSocketClient this[int index]
        {
            get
            {
                return _collection[index];
            }

            set
            {
                _collection[index] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get 
            {
                return _collection.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get 
            {
                return _collection.IsReadOnly;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(WebSocketClient item)
        {
            return _collection.IndexOf(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, WebSocketClient item)
        {
            _collection.Insert(index, item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            _collection.RemoveAt(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(WebSocketClient item)
        {
            if (!WebSocketStatus.Connected.Equals(item.Status))
            {
                WebSocketClient.StartListen(item);

                _collection.Add(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _collection.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(WebSocketClient item)
        {
            return _collection.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(WebSocketClient[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(WebSocketClient item)
        {
            return _collection.Remove(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<WebSocketClient> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)(_collection)).GetEnumerator();
        } 
        #endregion
    }
}
