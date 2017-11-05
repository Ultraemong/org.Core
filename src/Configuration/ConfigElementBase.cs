using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ConfigElementBase : ConfigurationElement
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public ConfigElementBase()
            : base()
        {
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
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
            var _value = base[name];

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
