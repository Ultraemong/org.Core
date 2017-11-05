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
    public class Int16Validator : ValueValidatorBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public Int16Validator()
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
                return !(value.Equals(short.MinValue) || value.Equals(short.MaxValue));

            return !(value.Equals((short)0) || value.Equals(short.MinValue) || value.Equals(short.MaxValue));
        }
        #endregion   
    }
}
