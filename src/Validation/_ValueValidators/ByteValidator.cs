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
    public class ByteValidator : ValueValidatorBase        
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public ByteValidator()
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
        protected override bool ValidateImpl(object value, bool ignoreZero = false)
        {
            if (ignoreZero)
                return !(value.Equals(byte.MinValue) || value.Equals(byte.MaxValue));

            return !(value.Equals((byte)0) || value.Equals(byte.MinValue) || value.Equals(byte.MaxValue));
        }
        #endregion        
    }
}
