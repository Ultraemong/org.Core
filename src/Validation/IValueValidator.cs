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
    public interface IValueValidator : IValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreZero"></param>
        /// <returns></returns>
        bool Validate(object value, bool ignoreZero = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        bool Validate(object value, int maxLength);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regularExpression"></param>
        /// <returns></returns>
        bool Validate(object value, string regularExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regularExpression"></param>
        /// <returns></returns>
        bool Validate(object value, Regex regularExpression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Validate(object value, Expression<Func<object, bool>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validator"></param>
        /// <returns></returns>
        bool Validate(object value, IValidator validator);
    }
}
