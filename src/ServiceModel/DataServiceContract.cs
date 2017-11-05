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
    public sealed class DataServiceContract : IDataServiceContract
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationName"></param>
        /// <param name="host"></param>
        /// <param name="protocol"></param>
        /// <param name="provider"></param>
        internal DataServiceContract(string configurationName, string host, string protocol, Type provider)
        {
            ConfigurationName   = configurationName;
            Host                = host;
            Protocol            = protocol;
            Provider            = provider;
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string ConfigurationName
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Host
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Protocol
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Type Provider
        {
            get;
            private set;
        } 
        #endregion
    }
}
