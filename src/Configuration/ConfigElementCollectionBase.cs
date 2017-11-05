using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ConfigElementCollectionBase<TElement> : ConfigurationElementCollection
        where TElement : ConfigElementBase, new()
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public ConfigElementCollectionBase()
            : base()
        {
        } 
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TElement this[int index]
        {
            get
            {
                var _element = BaseGet(index);

                if (null != _element)
                    return ((TElement)_element);

                return new TElement();
            }

            set
            {
                if (!IsReadOnly())
                {
                    if (index <= (Count - 1))
                    {
                        BaseAdd(index, value);
                        return;
                    }
                    else if ((index - (Count - 1)).Equals(1))
                    {
                        BaseAdd(value);
                        return;
                    }

                    throw new IndexOutOfRangeException();
                }
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new TElement();
        }
        #endregion
    }
}
