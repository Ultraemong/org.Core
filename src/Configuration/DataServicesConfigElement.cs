using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DataServicesConfigElement : ConfigElementBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DataServicesConfigElement()
            : base()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return GetValueOrDefault<string>("name", string.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("service-provider")]
        public string ServiceProvider
        {
            get
            {
                return GetValueOrDefault<string>("service-provider", string.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("service-protocol")]
        public string ServiceProtocol
        {
            get
            {
                return GetValueOrDefault<string>("service-protocol", string.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("service-host")]
        public string ServiceHost
        {
            get
            {
                return GetValueOrDefault<string>("service-host", string.Empty);
            }
        }
        #endregion
    }
}
