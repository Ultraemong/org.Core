using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Collections;

namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExternalResultCollection : IReadOnlyListCollection<IExternalResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
