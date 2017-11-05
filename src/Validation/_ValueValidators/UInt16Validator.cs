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
    public class UInt16Validator : ValueValidatorBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public UInt16Validator()
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
                return !(value.Equals(ushort.MinValue) || value.Equals(ushort.MaxValue));

            return !(value.Equals((ushort)0) || value.Equals(ushort.MinValue) || value.Equals(ushort.MaxValue));
        }
        #endregion
    }
}
