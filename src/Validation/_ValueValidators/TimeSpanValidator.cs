using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeSpanValidator : ValueValidatorBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public TimeSpanValidator()
            : base ()
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
        protected override bool ValidateImpl(object value, bool ignoreZero = false)
        {
            if (ignoreZero)
                return !(value.Equals(TimeSpan.MinValue) || value.Equals(TimeSpan.MaxValue));

            return !(value.Equals(TimeSpan.Zero) || value.Equals(TimeSpan.MinValue) || value.Equals(TimeSpan.MaxValue));
        }
        #endregion   
    }
}
