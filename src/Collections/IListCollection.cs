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
    public interface IListCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        void AddRange(IEnumerable<T> collection);
    }
}
