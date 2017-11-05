using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

using org.Core.Reflection;
using org.Core.Utilities;
using org.Core.Collections;
using org.Core.Diagnostics;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class RegistryEntry
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public RegistryEntry()
        {
            _nodePathInfo   = RegistryNodeHelpers.ExtractRegistryPathProvider(this);

            if (null == _nodePathInfo)
                throw new ArgumentNullException("The definition of registry node could not be found.");

            _childNodeName  = new RegistryNodeName(_nodePathInfo.NodeName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodePathInfo"></param>
        public RegistryEntry(IRegistryPathInfo nodePathInfo)
        {
            if (null == nodePathInfo)
                throw new ArgumentNullException("pathInfo");

            _nodePathInfo   = nodePathInfo;
            _childNodeName  = new RegistryNodeName(nodePathInfo.NodeName);
        }
        #endregion

        #region Fields
        readonly NameValueCollection<string, RegistryNameValuePair>     _innerRepository    = new NameValueCollection<string, RegistryNameValuePair>(StringComparer.CurrentCultureIgnoreCase);

        bool                                                            _isDirty            = false;
        bool                                                            _isDeleted          = false;
        
        RegistryNodeName                                                _childNodeName      = null;
        IRegistryPathInfo                                               _nodePathInfo       = null;
        IRegistryPathInfo[]                                             _childNodePaths     = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool CanRead
        {
            get
            {
                return (!IsDeleted && _childNodeName.IsCompiled);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanWrite
        {
            get
            {
                return (!IsDeleted && _childNodeName.IsCompiled);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                return _isDeleted;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public virtual void Delete()
        {
            if (CanWrite)
            {
                using (var _registryKey = RegistryNodeHelpers.GetRegistryKey(_nodePathInfo.RootType))
                {
                    _registryKey.DeleteSubKeyTree(_nodePathInfo.NodeName, false);
                }

                SetDeleted();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual internal void SetDirty()
        {
            _isDirty    = true;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual internal void SetDeleted()
        {
            _isDirty    = true;
            _isDeleted  = true;
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal IRegistryPathInfo[] GetChildNodePaths()
        {
            if (CanRead)
            {
                if (null == _childNodePaths || IsDirty)
                {
                    using (var _registryKey = RegistryNodeHelpers.GetRegistryKey(_nodePathInfo))
                    {
                        _childNodePaths = _registryKey.GetSubKeyNames().Select(n => RegistryNodePath.Combine(_nodePathInfo, n)).ToArray();
                    }

                    _isDirty = false;
                }

                return _childNodePaths;
            }

            return new IRegistryPathInfo[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="valueType"></param>
        protected void SetValue(string name, object value, RegistryValueKind valueType = RegistryValueKind.String)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (!IsDeleted && !_childNodeName.IsCompiled)
            {
                var _stackFrame     = StackTraceHelpers.GetStackFrameByIndex(1);
                var _propertyName   = _stackFrame.Method.Name.Replace("get_", string.Empty).Replace("set_", string.Empty);

                _childNodeName.Replace(_propertyName, value);

                if (!_childNodeName.IsCompiled)
                {
                    _innerRepository.Add(name, new RegistryNameValuePair(name, value, valueType));
                    return;
                }
                else
                {
                    _nodePathInfo = RegistryNodePath.Parse(_nodePathInfo.RootType, _childNodeName);

                    using (var _registryKey = RegistryNodeHelpers.GetRegistryKey(_nodePathInfo))
                    {
                        if (0 < _innerRepository.Count)
                        {
                            foreach (var _value in _innerRepository.Values)
                            {
                                if (!_value.IsDeleted)
                                {
                                    _registryKey.SetValue(_value.Name, _value.Value, _value.ValueType);
                                    continue;
                                }

                                _registryKey.DeleteValue(_value.Name, false);
                            }

                            _innerRepository.Clear();
                        }
                    }
                }
            }

            if (CanWrite)
            {
                using (var _registryKey = RegistryNodeHelpers.GetRegistryKey(_nodePathInfo))
                {
                    if (null == value)
                    {
                        _registryKey.DeleteValue(name, false);
                        return;
                    }

                    _registryKey.SetValue(name, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected object GetValue(string name)
        {
            return GetValue(name, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        protected object GetValue(string name, object defaultValue)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (CanRead)
            {
                using (var _registryKey = RegistryNodeHelpers.GetRegistryKey(_nodePathInfo))
                {
                    return _registryKey.GetValue(name, defaultValue);
                }
            }
            else if (!IsDeleted)
            {
                return _innerRepository.ContainsKey(name)
                    ? _innerRepository[name].Value : defaultValue;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        protected void Delete(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            if (CanWrite)
            {
                using (var _registryKey = RegistryNodeHelpers.GetRegistryKey(_nodePathInfo))
                {
                    _registryKey.DeleteValue(name, false);
                }
            }
            else if (!IsDeleted)
            {
                if (!_innerRepository.ContainsKey(name))
                {
                    _innerRepository[name].SetDeleted();
                    return;
                }

                _innerRepository.Add(name, new RegistryNameValuePair(name, null));
            }
        }
        #endregion

        #region Internal Classes
        /// <summary>
        /// 
        /// </summary>
        internal static class Initializer
        {
            #region Constructors
            /// <summary>
            /// 
            /// </summary>
            static Initializer()
            {
            }
            #endregion

            #region Methods
            /// <summary>
            /// 
            /// </summary>
            /// <param name="type"></param>
            /// <param name="command"></param>
            /// <returns></returns>
            public static RegistryEntry Initialize(Type type, IRegistryPathInfo pathInfo)
            {
                var _registryEntry              = ObjectUtils.CreateInstanceOf<RegistryEntry>(type);

                _registryEntry._nodePathInfo    = pathInfo;
                _registryEntry._childNodeName   = new RegistryNodeName(pathInfo.NodeName);

                return _registryEntry;
            }
            #endregion
        }
        #endregion
    }
}
