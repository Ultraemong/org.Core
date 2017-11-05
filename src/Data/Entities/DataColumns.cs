using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Data;
using org.Core.Collections;

namespace org.Core.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DataColumns : ListCollection<DataColumn>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DataColumns()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public DataColumns(IEnumerable<DataColumn> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        public DataColumns(int capacity)
            : base(capacity)
        {
        }
        #endregion
    }
}
