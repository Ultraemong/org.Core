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
    public class UInt32Validator : ValueValidatorBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public UInt32Validator()
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
                return !(value.Equals(uint.MinValue) || value.Equals(uint.MaxValue));

            return !(value.Equals((uint)0) || value.Equals(uint.MinValue) || value.Equals(uint.MaxValue));
        }
        #endregion
    }
}
