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
    public class DecimalValidator : ValueValidatorBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DecimalValidator()
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
                return !(value.Equals(decimal.MinValue) || value.Equals(decimal.MaxValue));

            return !(value.Equals(0.0m) || value.Equals(decimal.MinValue) || value.Equals(decimal.MaxValue));
        }
        #endregion
    }
}
