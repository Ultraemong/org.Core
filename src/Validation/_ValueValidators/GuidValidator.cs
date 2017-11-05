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
    public class GuidValidator : ValueValidatorBase        
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public GuidValidator()
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
            return !value.Equals(Guid.Empty);
        }
        #endregion        
    }
}
