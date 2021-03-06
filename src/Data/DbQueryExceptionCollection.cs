﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Collections;
using org.Core.ServiceModel;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class DbQueryExceptionCollection : ReadOnlyListCollection<DbQueryException>, IExternalResultCollection
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public DbQueryExceptionCollection(IEnumerable<DbQueryException> collection)
            : base(collection)
        {
            _innerRepository = new ReadOnlyListCollection<IExternalResult>(collection);
        } 
        #endregion

        #region Fields
        readonly ReadOnlyListCollection<IExternalResult> _innerRepository = default(ReadOnlyListCollection<IExternalResult>);
        #endregion

        #region Indexers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IExternalResult IReadOnlyList<IExternalResult>.this[int index]
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
        bool IReadOnlyListCollection<IExternalResult>.IsEmpty
        {
            get 
            {
                return _innerRepository.IsEmpty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        int IReadOnlyCollection<IExternalResult>.Count
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
        public override string ToString()
        {
            var _builder = new StringBuilder();
            
            foreach (var _exception in this)
            {
                _builder.AppendLine(_exception.ErrorText);
            }

            return _builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator<IExternalResult> IEnumerable<IExternalResult>.GetEnumerator()
        {
            return _innerRepository.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (_innerRepository as System.Collections.IEnumerable).GetEnumerator();
        }
        #endregion
    }
}
