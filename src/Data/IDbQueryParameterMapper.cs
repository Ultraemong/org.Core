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
    public interface IDbQueryParameterMapper : IDbQueryPropertyProvider, IDbQueryOperatingSessionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsInChild { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        void Map(IDbQueryParameterizable destination, object source);
    }
}
