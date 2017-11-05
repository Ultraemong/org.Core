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
    public class ValueValidatorFactory : IValueValidatorFactory
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public ValueValidatorFactory()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IValueValidator CreateValueValidator(object value)
        {
            if (null != value)
            {
                try
                {
                    var _type = value.GetType();

                    if (ObjectUtils.IsNullableType(value))
                        _type = Nullable.GetUnderlyingType(_type);

                    return ValueValidators.Validators.Where(x => x.Key.Equals(_type)).Select(x => x.Value).Single();
                }
                catch (InvalidOperationException)
                {
                    return new NotSupportedValueValidator();
                }
            }

            return new NotSupportedValueValidator();
        }

        #endregion
    }
}
