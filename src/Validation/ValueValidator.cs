using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace org.Core.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public static class ValueValidator
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static ValueValidator()
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
        /// <param name="value"></param>
        /// <returns></returns>
        static IValueValidator GetValueValidator(object value)
        {
            var _factory = ValueValidatorFactoryManager.CreateValueValidatorFactory();

            return _factory.CreateValueValidator(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreZero"></param>
        /// <returns></returns>
        public static bool Validate(object value, bool ignoreZero = false)
        {
            var _validator = GetValueValidator(value);

            if (null != _validator)
                return _validator.Validate(value, ignoreZero);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static bool Validate(object value, int maxLength)
        {
            var _validator = GetValueValidator(value);

            if (null != _validator)
                return _validator.Validate(value, maxLength);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regularExpression"></param>
        /// <returns></returns>
        public static bool Validate(object value, string regularExpression)
        {
            var _validator = GetValueValidator(value);

            if (null != _validator)
                return _validator.Validate(value, regularExpression);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regularExpression"></param>
        /// <returns></returns>
        public static bool Validate(object value, Regex regularExpression)
        {
            var _validator = GetValueValidator(value);

            if (null != _validator)
                return _validator.Validate(value, regularExpression);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool Validate(object value, Expression<Func<object, bool>> expression)
        {
            var _validator = GetValueValidator(value);

            if (null != _validator)
                return _validator.Validate(value, expression);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        public static bool Validate(object value, IValidator validator)
        {
            var _validator = GetValueValidator(value);

            if (null != _validator)
                return _validator.Validate(value, validator);

            return false;
        }
        #endregion
    }
}
