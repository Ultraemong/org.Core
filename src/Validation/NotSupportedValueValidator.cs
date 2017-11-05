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
    internal class NotSupportedValueValidator : IValueValidator
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        internal NotSupportedValueValidator()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreZero"></param>
        /// <returns></returns>
        public bool Validate(object value, bool ignoreZero = false)
        {
            return (null != value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public bool Validate(object value, int maxLength)
        {
            return (null != value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regularExpression"></param>
        /// <returns></returns>
        public bool Validate(object value, string regularExpression)
        {
            return (null != value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regularExpression"></param>
        /// <returns></returns>
        public bool Validate(object value, Regex regularExpression)
        {
            return (null != value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Validate(object value, Expression<Func<object, bool>> expression)
        {
            return (null != value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        public bool Validate(object value, IValidator validator)
        {
            return (null != value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Validate(object obj)
        {
            return (null != obj);
        } 
        #endregion
    }
}
