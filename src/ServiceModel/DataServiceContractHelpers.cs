using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Xml;

using org.Core.Reflection;

namespace org.Core.ServiceModel
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataServiceContractHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static DataServiceContractHelpers()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IDataServiceContract RetrieveDataServiceContract(object instance)
        {
            var _descriptor = AttributeMemberHelpers.RetrieveMemberDescriptor(instance, typeof(IDataServiceContract), true);

            if (null != _descriptor)
                return _descriptor.Member as IDataServiceContract;

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberDescriptors"></param>
        /// <returns></returns>
        public static IDataServiceContract RetrieveDataServiceContract(AttributeMemberDescriptors memberDescriptors)
        {
            var _descriptor = memberDescriptors.GetDescriptorByAttributeType(typeof(IDataServiceContract));

            if (null != _descriptor)
                return _descriptor.Member as IDataServiceContract;

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceContract"></param>
        /// <returns></returns>
        public static Binding GetDataServiceBinding(IDataServiceContract serviceContract)
        {
            if (!string.IsNullOrEmpty(serviceContract.Protocol))
            {
                switch (serviceContract.Protocol.ToLower())
                {
                    case "http":
                    case "basichttp":

                        return new BasicHttpBinding()
                        {
                            MaxBufferSize               = 2147483647,
                            MaxReceivedMessageSize      = 2147483647,

                            CloseTimeout                = new TimeSpan(1, 50, 0),
                            OpenTimeout                 = new TimeSpan(1, 50, 0),
                            SendTimeout                 = new TimeSpan(1, 50, 0),
                            ReceiveTimeout              = new TimeSpan(1, 50, 0),

                            ReaderQuotas                = new XmlDictionaryReaderQuotas()
                            {
                                MaxDepth                = 128,
                                MaxArrayLength          = 2147483646,
                                MaxBytesPerRead         = 4096,
                                MaxNameTableCharCount   = 16384,
                                MaxStringContentLength  = 8388608,
                            },
                        };

                    case "https":
                    case "basichttps":
                        return new BasicHttpsBinding();

                    case "nethttp":
                        return new NetHttpBinding();

                    case "nethttps":
                        return new NetHttpsBinding();
                    
                    case "namedpipe":
                    case "netnamedpipe":
                        return new NetNamedPipeBinding();
                    
                    case "tcp":
                    case "nettcp":
                        return new NetTcpBinding();

                    case "udp":
                        return new UdpBinding();
                    
                    case "web":
                    case "webhttp":
                        return new WebHttpBinding();

                    case "wsdual":
                    case "wsdualhttp":
                        return new WSDualHttpBinding();
                    
                    case "wsfederation":
                    case "wsfederationhttp":
                        return new WSFederationHttpBinding();

                    case "ws2007federation":
                    case "ws2007federationhttp":
                        return new WS2007FederationHttpBinding();

                    case "ws":
                    case "wshttp":
                        return new WSHttpBinding();

                    case "ws2007":
                    case "ws2007http":
                        return new WS2007HttpBinding();
                }

                throw new NotSupportedException(string.Format("The '{0}' protocol is not supported.", serviceContract.Protocol));
            }

            throw new ArgumentNullException("serviceContract.Protocol");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceContract"></param>
        /// <returns></returns>
        public static EndpointAddress GetDataServiceEndpoint(IDataServiceContract serviceContract)
        {
            if (string.IsNullOrEmpty(serviceContract.Host))
                throw new ArgumentNullException("serviceContract.Host");

            return new EndpointAddress(serviceContract.Host);
        }
        #endregion
    }
}
