using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReadOnlyListCollection<T> : IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsEmpty { get; }
    }
}
