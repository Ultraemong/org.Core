using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.ServiceModel.Description;

using org.Core.Data;
using org.Core.Validation;
using org.Core.Configuration;

namespace org.Core.ServiceModel
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public sealed class DataServiceContractAttribute : Attribute, IDataServiceContract
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DataServiceContractAttribute()
            : base()
        {
        }
        #endregion

        #region Fields
        string              _host               = null;
        Type                _provider           = null;
        string              _protocol           = null;
        string              _configurationName  = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string ConfigurationName
        {
            get
            {
                return _configurationName;
            }

            set
            {
                _configurationName = value;

                if (!string.IsNullOrEmpty(_configurationName))
                {
                    if (string.IsNullOrEmpty(_host))
                        _host = Configs.Current.DataServices[_configurationName].ServiceHost;

                    if (string.IsNullOrEmpty(_protocol))
                        _protocol = Configs.Current.DataServices[_configurationName].ServiceProtocol;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Host
        {
            get
            {
                return _host;
            }

            set
            {
                _host = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Protocol
        {
            get 
            {
                return _protocol;
            }

            set
            {
                _protocol = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type Provider
        {
            get 
            {
                return _provider;
            }

            set
            {
                _provider = value;
            }
        }
        #endregion
    }
}
