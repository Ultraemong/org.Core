using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Utilities;

namespace org.Core.Validation
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ValueValidateAttribute : Attribute, IValueValidatorFactoryProvider
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueValidatorFactoryType"></param>
        public ValueValidateAttribute(Type valueValidatorFactoryType)
            : base()
        {
            _valueValidatorFactory = valueValidatorFactoryType;
        }
        #endregion

        #region Fields
        Type _valueValidatorFactory = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public IValueValidatorFactory ValueValidatorFactory
        {
            get 
            {
                if (null != _valueValidatorFactory)
                    return ObjectUtils.CreateInstanceOf<IValueValidatorFactory>(_valueValidatorFactory);

                return ValueValidatorFactoryManager.CreateValueValidatorFactory();
            }
        } 
        #endregion
    }
}
