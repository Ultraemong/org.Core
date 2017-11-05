using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryResultMapper : IDbQueryPropertyProvider, IDbQueryOperatingSessionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        void Map(object destination, IDbQueryResult source);
    }
}
