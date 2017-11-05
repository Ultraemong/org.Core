using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.ServiceModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public class DataServiceProxy<TService> : IDisposable, ICommunicationObject, IDataServiceContractProvider
        where TService : class
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        protected DataServiceProxy()
        {
            _serviceContract = DataServiceContractHelpers.RetrieveDataServiceContract(this);

            if (null == _serviceContract)
                throw new InvalidOperationException("The service contract is not declared.");

            var _binding            = DataServiceContractHelpers.GetDataServiceBinding(_serviceContract);
            var _endPoint           = DataServiceContractHelpers.GetDataServiceEndpoint(_serviceContract);

            _innerChannelFactory    = new ChannelFactory<TService>(_binding, _endPoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceContract"></param>
        protected DataServiceProxy(IDataServiceContract serviceContract)
        {
            _serviceContract = serviceContract;
            
            if (null == _serviceContract)
                throw new InvalidOperationException("The service contract is not declared.");

            var _binding            = DataServiceContractHelpers.GetDataServiceBinding(serviceContract);
            var _endPoint           = DataServiceContractHelpers.GetDataServiceEndpoint(serviceContract);

            _innerChannelFactory    = new ChannelFactory<TService>(_binding, _endPoint);
        }
        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Closed
        {
            add
            {
                _innerChannelFactory.Closed += value;
            }

            remove
            {
                _innerChannelFactory.Closed -= value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Closing
        {
            add
            {
                _innerChannelFactory.Closing += value;
            }

            remove
            {
                _innerChannelFactory.Closing -= value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Faulted
        {
            add
            {
                _innerChannelFactory.Faulted += value;
            }

            remove
            {
                _innerChannelFactory.Faulted -= value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Opened
        {
            add
            {
                _innerChannelFactory.Opened += value;
            }

            remove
            {
                _innerChannelFactory.Opened -= value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Opening
        {
            add
            {
                _innerChannelFactory.Opening += value;
            }

            remove
            {
                _innerChannelFactory.Opening -= value;
            }
        }
        #endregion

        #region Fields
        bool                                _isOpened               = false;

        readonly IDataServiceContract       _serviceContract        = null;
        readonly ChannelFactory<TService>   _innerChannelFactory    = default(ChannelFactory<TService>);
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public CommunicationState State
        {
            get 
            {
                return _innerChannelFactory.State;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDataServiceContract ServiceContract
        {
            get
            {
                return _serviceContract;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected TService Service
        {
            get
            {
                Open();

                var _service = _innerChannelFactory.CreateChannel();
                
                if (_service is IClientChannel)
                {
                    var _channel        = _service as IClientChannel;

                    _channel.Opened     += new EventHandler(_OnChannelOpened);
                    _channel.Closed     += new EventHandler(_OnChannelClosed);
                    _channel.Faulted    += new EventHandler(_OnChannelFaulted);
                    
                    _channel.Open();
                }

                return _service;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            if (!_isOpened)
            {
                _innerChannelFactory.Open();

                _isOpened = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        public void Open(TimeSpan timeout)
        {
            if (!_isOpened)
            {
                _innerChannelFactory.Open(timeout);

                _isOpened = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            if (_isOpened)
            {
                _innerChannelFactory.Close();

                _isOpened = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        public void Close(TimeSpan timeout)
        {
            if (_isOpened)
            {
                _innerChannelFactory.Close(timeout);

                _isOpened = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return _innerChannelFactory.BeginClose(timeout, callback, state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            return _innerChannelFactory.BeginClose(callback, state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            return _innerChannelFactory.BeginOpen(timeout, callback, state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginOpen(AsyncCallback callback, object state)
        {
            return _innerChannelFactory.BeginOpen(callback, state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public void EndClose(IAsyncResult result)
        {
            _innerChannelFactory.EndClose(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public void EndOpen(IAsyncResult result)
        {
            _innerChannelFactory.EndOpen(result);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Abort()
        {
            _innerChannelFactory.Abort();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            try
            {
                Close();
            }
            catch (Exception)
            {
                Abort();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _OnChannelClosed(object sender, EventArgs e)
        {
            var _channel = sender as IClientChannel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _OnChannelOpened(object sender, EventArgs e)
        {
            var _channel = sender as IClientChannel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _OnChannelFaulted(object sender, EventArgs e)
        {
            var _channel = sender as IClientChannel;
        }
        #endregion
    }
}
