using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryPropertyProvider
    {
        /// <summary>
        /// 
        /// </summary>
        DbQueryPropertyDescriptors PropertyDescriptors { get; }
    }
}
