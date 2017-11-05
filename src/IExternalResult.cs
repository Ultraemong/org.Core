using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExternalResult
    {
        /// <summary>
        /// 
        /// </summary>
        int ErrorCode { get; }

        /// <summary>
        /// 
        /// </summary>
        string ErrorText { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IExternalResult<T> : IExternalResult
        where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        T Result { get; }
    }
}
