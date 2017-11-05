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
    [ConfigurationCollection(typeof(SettingsConfigElement), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public sealed class SettingsConfigElementCollection : ConfigElementCollectionBase<SettingsConfigElement>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public SettingsConfigElementCollection()
            : base()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public new SettingsConfigElement this[string key]
        {
            get
            {
                for (int i = 0; i < Count; i++)
                {
                    SettingsConfigElement _element = this[i];

                    if (_element.Key.ToLower().Equals(key.ToLower()))
                    {
                        return _element;
                    }
                }

                return new SettingsConfigElement();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as SettingsConfigElement).Key;
        }
        #endregion
    }
}
