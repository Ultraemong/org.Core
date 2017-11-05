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
    public class DoubleValidator : ValueValidatorBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DoubleValidator()
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
                return !(value.Equals(double.MinValue) || value.Equals(double.MaxValue));

            return !(value.Equals(0.0d) || value.Equals(double.MinValue) || value.Equals(double.MaxValue));
        }
        #endregion   
    }
}
