using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbQueryOperatingSessionCollection : ReadOnlyListCollection<DbQueryOperatingSession>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        internal DbQueryOperatingSessionCollection(IEnumerable<DbQueryOperatingSession> collection)
            : base(collection)
        {   
        } 
        #endregion
    }
}
