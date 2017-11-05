using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using org.Core.Collections;

namespace org.Core.Compression
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CompressionEntryCollection : ICompressionEntryCollection
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public CompressionEntryCollection(IEnumerable<ICompressionEntry> collection)
        {
            _innerRepository = new ListCollection<ICompressionEntry>(collection);
        } 
        #endregion

        #region Fields
        readonly ListCollection<ICompressionEntry> _innerRepository = default(ListCollection<ICompressionEntry>);
        #endregion

        #region Indexes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ICompressionEntry this[int index]
        {
            get 
            {
                return _innerRepository[index];
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
                return _innerRepository.IsEmpty;
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
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ICompressionEntry> GetEnumerator()
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
    }
}
