using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface INetworkFileServiceProvider
    {
        /// <summary>
        /// 
        /// </summary>
        INetworkFileService FileService { get; }
    }
}
