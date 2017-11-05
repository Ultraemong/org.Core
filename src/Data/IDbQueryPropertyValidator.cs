using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Validation;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryPropertyValidator : IDbQueryPropertyProvider, IValidator, IDbQueryOperatingSessionProvider
    {
        /// <summary>
        /// 
        /// </summary>
        event DbQueryFailedEventHandler Failed;

        /// <summary>
        /// 
        /// </summary>
        event DbQueryValidatedEventHandler Validated;
    }
}
