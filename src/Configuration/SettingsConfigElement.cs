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
    public sealed class SettingsConfigElement : ConfigElementBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public SettingsConfigElement()
            : base()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true, IsKey = true)]
        public string Key
        {
            get
            {
                return GetValueOrDefault<string>("key", string.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return GetValueOrDefault<string>("value", string.Empty);
            }
        }
        #endregion
    }
}
