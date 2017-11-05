using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRegistryPathInfo
    {
        /// <summary>
        /// 
        /// </summary>
        RegistryRootType RootType { get; }

        /// <summary>
        /// 
        /// </summary>
        string NodeName { get; }
    }
}
