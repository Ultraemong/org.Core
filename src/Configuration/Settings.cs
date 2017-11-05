using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class Settings
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public Settings(SettingsConfigElementCollection settings)
        {
            _settings = settings;
        }
        #endregion

        #region Fields
        static Settings                             s_settings  = null;
        readonly SettingsConfigElementCollection    _settings   = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static Settings Current
        {
            get
            {
                if (s_settings == null)
                    s_settings = new Settings(Configs.Current.Settings);

                return s_settings;
            }
        }
        #endregion

        #region Mehtods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public string GetValueOrDefault(string key, string defVal)
        {
            if (!string.IsNullOrEmpty(_settings[key].Value))
            {
                return _settings[key].Value;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public byte GetValueOrDefault(string key, byte defVal)
        {
            string _retVal = GetValueOrDefault(key, string.Empty);

            if (!string.IsNullOrEmpty(_retVal))
                return byte.Parse(_retVal);

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public short GetValueOrDefault(string key, short defVal)
        {
            string _retVal = GetValueOrDefault(key, string.Empty);

            if (!string.IsNullOrEmpty(_retVal))
                return short.Parse(_retVal);

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string key, int defVal)
        {
            string _retVal = GetValueOrDefault(key, string.Empty);

            if (!string.IsNullOrEmpty(_retVal))
                return int.Parse(_retVal);

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public long GetValueOrDefault(string key, long defVal)
        {
            string _retVal = GetValueOrDefault(key, string.Empty);

            if (!string.IsNullOrEmpty(_retVal))
                return long.Parse(_retVal);

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defVal"></param>
        /// <param name="digits"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public decimal GetValueOrDefault(string key, decimal defVal, int digits = 2, MidpointRounding mode = MidpointRounding.AwayFromZero)
        {
            string _retVal = GetValueOrDefault(key, string.Empty);

            if (!string.IsNullOrEmpty(_retVal))
                return Math.Round(decimal.Parse(_retVal), digits, mode);

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public bool GetValueOrDefault(string key, bool defVal)
        {
            string _retVal = GetValueOrDefault(key, string.Empty);

            if (!string.IsNullOrEmpty(_retVal))
                return bool.Parse(_retVal);

            return defVal;
        }
        #endregion
    }
}