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
    public interface IValueValidatorFactoryProvider
    {
        /// <summary>
        /// 
        /// </summary>
        IValueValidatorFactory ValueValidatorFactory { get; }
    }
}
