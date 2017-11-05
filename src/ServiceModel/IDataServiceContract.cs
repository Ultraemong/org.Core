using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.ServiceModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataServiceContract
    {
        /// <summary>
        /// 
        /// </summary>
        string ConfigurationName { get; }

        /// <summary>
        /// 
        /// </summary>
        string Host { get; }

        /// <summary>
        /// 
        /// </summary>
        string Protocol { get; }

        /// <summary>
        /// 
        /// </summary>
        Type Provider { get; }
    }
}
