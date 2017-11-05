using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicationIdentity
    {
        /// <summary>
        /// 
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        /// 
        /// </summary>
        string AssemblyPath { get; }

        /// <summary>
        /// 
        /// </summary>
        string EntryPoint { get; }
    }
}
