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
    public class Configs : ConfigurationSection
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Configs()
            : base()
        {
        } 
        #endregion

        #region Fields
        static Configs          s_configs       = null;
        static readonly string  s_sectionName   = "org.global.configuration";
	    #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static Configs Current
        {
            get
            {
                if (null == s_configs)
                    return s_configs = ConfigurationManager.GetSection(s_sectionName) as Configs;

                return s_configs;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("settings")]
        public SettingsConfigElementCollection Settings
        {
            get
            {
                return GetValueOrDefault<SettingsConfigElementCollection>("settings", null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("data-services")]
        public DataServicesConfigElementCollection DataServices
        {
            get
            {
                return GetValueOrDefault<DataServicesConfigElementCollection>("data-services", null);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        protected TValue GetValueOrDefault<TValue>(string name, TValue defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                try
                {
                    return ((TValue)_value);
                }
                catch (InvalidCastException)
                {
                }
                catch (InvalidOperationException)
                {
                }
            }

            return defVal;
        }
        #endregion
    }
}
