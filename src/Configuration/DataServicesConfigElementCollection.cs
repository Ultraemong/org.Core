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
    [ConfigurationCollection(typeof(DataServicesConfigElement), AddItemName = "data-service", ClearItemsName = "clear", RemoveItemName = "remove", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public sealed class DataServicesConfigElementCollection : ConfigElementCollectionBase<DataServicesConfigElement>
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DataServicesConfigElementCollection()
            : base()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new DataServicesConfigElement this[string name]
        {
            get
            {
                for (int i = 0; i < Count; i++)
                {
                    DataServicesConfigElement _element = this[i];

                    if (_element.Name.ToLower().Equals(name.ToLower()))
                    {
                        return _element;
                    }
                }

                return new DataServicesConfigElement();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("default-data-service")]
        public string DefaultDataService
        {
            get
            {
                return GetValueOrDefault<string>("default-data-service", string.Empty);
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
            return (element as DataServicesConfigElement).Name;
        }
        #endregion
    }
}
