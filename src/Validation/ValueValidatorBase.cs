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
    public abstract class ValueValidatorBase : IValueValidator, IValidator
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public ValueValidatorBase()
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected bool IsValid(object obj)
        {
            return (null != obj && DBNull.Value != obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreZero"></param>
        /// <returns></returns>
        public bool Validate(object value, bool ignoreZero = false)
        {
            if (IsValid(value))
                return ValidateImpl(value, ignoreZero);
            
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public bool Validate(object value, int maxLength)
        {
            if (IsValid(value))
                return ValidateImpl(value, maxLength);

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regularExpression"></param>
        /// <returns></returns>
        public bool Validate(object value, string regularExpression)
        {   
            if(!string.IsNullOrEmpty(regularExpression))
                return Validate(value, new Regex(regularExpression));
            
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regularExpression"></param>
        /// <returns></returns>
        public bool Validate(object value, Regex regularExpression)
        {
            if (IsValid(value))
                return regularExpression.IsMatch(value.ToString());

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Validate(object value, Expression<Func<object, bool>> expression)
        {
            var _expression = expression.Compile();

            return _expression(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        public bool Validate(object value, IValidator validator)
        {
            return validator.Validate(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreZero"></param>
        /// <returns></returns>
        protected abstract bool ValidateImpl(object value, bool ignoreZero = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        protected virtual bool ValidateImpl(object value, int maxLength)
        {
            return (value.ToString().Length <= maxLength);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool IValidator.Validate(object obj)
        {
            return Validate(obj);
        }
        #endregion
    }
}
