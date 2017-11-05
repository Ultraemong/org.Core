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
    public class SbyteValidator : ValueValidatorBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public SbyteValidator()
            : base()
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
                return !(value.Equals(sbyte.MinValue) || value.Equals(sbyte.MaxValue));

            return !(value.Equals((sbyte)0) || value.Equals(sbyte.MinValue) || value.Equals(sbyte.MaxValue));
        }
        #endregion   
    }
}
