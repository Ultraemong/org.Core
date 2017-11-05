using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class RegistryNodeAttribute : Attribute, IRegistryPathInfo
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootType"></param>
        /// <param name="nodeName"></param>
        public RegistryNodeAttribute(RegistryRootType rootType, string nodeName)
        {
            _rootType  = rootType;
            _nodeName  = nodeName;
        }
        #endregion

        #region Fields
        readonly RegistryRootType   _rootType   = RegistryRootType.LOCAL_MACHINE;
        readonly string             _nodeName   = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public RegistryRootType RootType
        {
            get 
            {
                return _rootType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string NodeName
        {
            get 
            {
                return _nodeName;
            }
        }
        #endregion
    }
}
