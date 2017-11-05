using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Utilities;
using System.Collections;

namespace org.Core.Collections
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ReadOnlyListCollection<TElement> : IReadOnlyListCollection<TElement>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public ReadOnlyListCollection(IEnumerable<TElement> collection)
        {
            _innerRepository = new ListCollection<TElement>(collection);
        }
        #endregion

        #region Fields
        readonly IListCollection<TElement> _innerRepository = default(IListCollection<TElement>);
        #endregion

        #region Indexes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TElement this[int index]
        {
            get
            {
                return (_innerRepository as IReadOnlyList<TElement>)[index];
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
                return (_innerRepository as IReadOnlyList<TElement>).Count;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TElement> GetEnumerator()
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
        #endregion

        #region Internal Classes
        /// <summary>
        /// 
        /// </summary>
        internal static class Initializer
        {
            #region Constructors
            /// <summary>
            /// 
            /// </summary>
            static Initializer()
            {
            }
            #endregion

            #region Methods
            /// <summary>
            /// 
            /// </summary>
            /// <param name="collection"></param>
            /// <returns></returns>
            public static TCollection Initialize<TCollection>(IEnumerable<TElement> collection)
                where TCollection : ReadOnlyListCollection<TElement>
            {
                if (default(IEnumerable<TElement>) == collection)
                    throw new ArgumentNullException("collection");

                return ObjectUtils.CreateInstanceOf<TCollection>(collection);
            }
            #endregion
        }
        #endregion
    }
}
