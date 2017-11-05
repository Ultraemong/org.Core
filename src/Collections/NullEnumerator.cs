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
    internal class NullEnumerator : IEnumerator, IDisposable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public NullEnumerator()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public object Current
        {
            get
            {
                return null;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class NullEnumerator<T> : NullEnumerator, IEnumerator<T>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public NullEnumerator()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public new T Current
        {
            get 
            {
                return default(T);
            }
        }
        #endregion
    }
}
