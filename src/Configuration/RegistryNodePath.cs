using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Extensions;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class RegistryNodePath : IRegistryPathInfo
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootType"></param>
        /// <param name="nodeName"></param>
        protected RegistryNodePath(RegistryRootType rootType, string nodeName)
        {
            _rootType   = rootType;
            _nodeName   = nodeName;
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

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static IRegistryPathInfo Parse(string nodeName)
        {
            if (string.IsNullOrEmpty(nodeName))
                throw new ArgumentNullException("nodeName");

            var _split      = nodeName.Split('\\');
            var _rootNode   = _split.GetValueOrDefault(0, string.Empty);

            if (string.IsNullOrEmpty(_rootNode))
                throw new ArgumentNullException("A root node cannot be empty.");

            var _childNode  = nodeName.Substring(_rootNode.Length, nodeName.Length - _rootNode.Length).TrimStart('\\');

            if (_rootNode.ToUpper().Equals(RegistryNodeHelpers.GetRegistryNodeName(RegistryRootType.CLASSES_ROOT)))
                return new RegistryNodePath(RegistryRootType.CLASSES_ROOT, _childNode);

            else if (_rootNode.ToUpper().Equals(RegistryNodeHelpers.GetRegistryNodeName(RegistryRootType.CURRENT_USER)))
                return new RegistryNodePath(RegistryRootType.CURRENT_USER, _childNode);

            else if (_rootNode.ToUpper().Equals(RegistryNodeHelpers.GetRegistryNodeName(RegistryRootType.LOCAL_MACHINE)))
                return new RegistryNodePath(RegistryRootType.LOCAL_MACHINE, _childNode);

            else if (_rootNode.ToUpper().Equals(RegistryNodeHelpers.GetRegistryNodeName(RegistryRootType.USERS)))
                return new RegistryNodePath(RegistryRootType.USERS, _childNode);

            else if (_rootNode.ToUpper().Equals(RegistryNodeHelpers.GetRegistryNodeName(RegistryRootType.CURRENT_CONFIG)))
                return new RegistryNodePath(RegistryRootType.CURRENT_CONFIG, _childNode);

            throw new InvalidOperationException(string.Format("The '{0}' node name is invalid.", nodeName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootType"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static IRegistryPathInfo Parse(RegistryRootType rootType, RegistryNodeName nodeName)
        {
            if (null == nodeName || nodeName.IsEmpty)
                throw new ArgumentNullException("nodeName");

            if (nodeName.IsCompiled)
                return new RegistryNodePath(rootType, nodeName.ToString());

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathInfo"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static IRegistryPathInfo Combine(IRegistryPathInfo pathInfo, RegistryNodeName nodeName)
        {
            if (null == pathInfo)
                throw new ArgumentNullException("pathProvider");

            if (null == nodeName || nodeName.IsEmpty)
                throw new ArgumentNullException("nodeName");

            return new RegistryNodePath(pathInfo.RootType, string.Format(@"{0}\{1}", pathInfo.NodeName, nodeName));
        }
        #endregion
    }
}
