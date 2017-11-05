using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.ServiceModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataServiceContractProvider
    {
        /// <summary>
        /// 
        /// </summary>
        IDataServiceContract ServiceContract { get; }
    }
}
