using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Utilities;
using org.Core.Diagnostics;
using org.Core.Collections;
using org.Core.ServiceModel;
using org.Core.Reflection;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DbQueryOperationContext : IDisposable, IEntryDescriptorManagerProvider
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static DbQueryOperationContext()
        {
            s_innerCacheRepository = new NameValueCollection<string, DbQueryOperationContext>(StringComparer.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryOperationContext()
        {
            var _currentStackFrame  = StackTraceHelpers.GetStackFrameByIndex(1);
            var _currentCacheKey    = GenerateUniqueCacheKey(_currentStackFrame);

            var _operationContext   = (DbQueryOperationContext)null;

            if (0 < s_innerCacheRepository.Count)
            {
                var _stackFrames = StackTraceHelpers.GetStackFrames();

                for (var i = 0; i < _stackFrames.Length; i++)
                {
                    var _stackFrame = _stackFrames[i];
                    var _cacheKey   = GenerateUniqueCacheKey(_stackFrame);

                    if (!string.IsNullOrEmpty(_cacheKey) && s_innerCacheRepository.ContainsKey(_cacheKey))
                    {
                        _operationContext = s_innerCacheRepository[_cacheKey];
                        break;
                    }
                }
            }

            if (null != _operationContext)
            {
                _isNested                       = true;

                _connectionPool                 = _operationContext._connectionPool;
                _descriptorManager              = _operationContext._descriptorManager;

                _innerSharedRepository          = _operationContext._innerSharedRepository;
                _innerSessionRepository         = _operationContext._innerSessionRepository;
                _innerCacheKeyRepository        = _operationContext._innerCacheKeyRepository;

                _executionTimeout               = _operationContext._executionTimeout;
                _asynchronousProcessing         = _operationContext._asynchronousProcessing;

                _requiresTransaction            = _operationContext._requiresTransaction;
                _requiresEnvironmentVariables   = _operationContext._requiresEnvironmentVariables;

                _innerErrorRepository           = new ListCollection<DbQueryException>(_syncRoot);
            }
            else
            {
                _innerSharedRepository          = new NameValueCollection<string, object>(StringComparer.InvariantCultureIgnoreCase);
                _innerSessionRepository         = new ListCollection<DbQueryOperatingSession>(_syncRoot);
                _innerCacheKeyRepository        = new ListCollection<string>(_syncRoot);

                _connectionPool                 = new DbConnectionPool(this);
                _descriptorManager              = new EntryDescriptorManager(this);

                _innerErrorRepository           = new ListCollection<DbQueryException>(_syncRoot);
            }

            if (!IsDbQueryOperationMember(_currentStackFrame))
            {
                VerifyCacheKey(_currentCacheKey);

                _innerCacheKeyRepository.Add(_currentCacheKey);
                s_innerCacheRepository.Add(_currentCacheKey, this);
            }
        }
        #endregion

        #region Fields
        int?                                                                    _executionTimeout                   = null;
        bool                                                                    _asynchronousProcessing             = false;

        bool                                                                    _requiresTransaction                = false;
        bool                                                                    _requiresEnvironmentVariables       = true;
        
        DbQueryExceptionCollection                                              _cachedErrorCollection              = null;
        DbQueryOperatingSessionCollection                                       _cachedSessionCollection            = null;

        readonly bool                                                           _isNested                           = false;

        readonly object                                                         _syncRoot                           = new object();
        readonly DbConnectionPool                                               _connectionPool                     = null;        
        readonly IEntryDescriptorManager                                        _descriptorManager                  = null;

        readonly NameValueCollection<string, object>                            _innerSharedRepository              = default(NameValueCollection<string, object>);
        readonly ListCollection<DbQueryOperatingSession>                        _innerSessionRepository             = default(ListCollection<DbQueryOperatingSession>);
        readonly ListCollection<DbQueryException>                               _innerErrorRepository               = default(ListCollection<DbQueryException>);
        readonly ListCollection<string>                                         _innerCacheKeyRepository            = default(ListCollection<string>);

        readonly static NameValueCollection<string, DbQueryOperationContext>    s_innerCacheRepository              = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return (0 < _innerErrorRepository.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryExceptionCollection Errors
        {
            get
            {
                if (null == _cachedErrorCollection)
                {
                    _cachedErrorCollection = new DbQueryExceptionCollection(_innerErrorRepository);
                }
                else if (!_innerErrorRepository.Count.Equals(_cachedErrorCollection.Count))
                {
                    _cachedErrorCollection = new DbQueryExceptionCollection(_innerErrorRepository);
                }

                return _cachedErrorCollection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public INameValueCollection<string, object> Items
        {
            get
            {
                return _innerSharedRepository;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryOperatingSessionCollection Session
        {
            get
            {
                if (null == _cachedSessionCollection)
                {
                    _cachedSessionCollection = new DbQueryOperatingSessionCollection(_innerSessionRepository);
                }
                else if (!_innerErrorRepository.Count.Equals(_cachedSessionCollection.Count))
                {
                    _cachedSessionCollection = new DbQueryOperatingSessionCollection(_innerSessionRepository);
                }

                return _cachedSessionCollection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEntryDescriptorManager DescriptorManager
        {
            get
            {
                return _descriptorManager;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ExecutionTimeout
        {
            get
            {
                return _executionTimeout;
            }

            set
            {
                if (!_isNested)
                {
                    _executionTimeout = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AsynchronousProcessing
        {
            get
            {
                return _asynchronousProcessing;
            }

            set
            {
                if (!_isNested)
                {
                    _asynchronousProcessing = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool RequiresTransaction
        {
            get
            {
                return _requiresTransaction;
            }

            set
            {
                if (!_isNested)
                {
                    _requiresTransaction = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool RequiresEnvironmentVariables
        {
            get
            {
                return _requiresEnvironmentVariables;
            }

            set
            {
                if (!_isNested)
                {
                    _requiresEnvironmentVariables = value;
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        [Obsolete("This method will be removed as soon as possible.")]
        public void Attach(DbQueryOperation operation)
        {
            DbQueryPrinciple.OperationContextAssigner.Assign(operation, this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceContractProvider"></param>
        /// <param name="queryAction"></param>
        /// <returns></returns>
        public DbQueryOperatingSession CreateSession(IDataServiceContractProvider serviceContractProvider, DbQueryActions queryAction)
        {
            return CreateSession(serviceContractProvider.ServiceContract, queryAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceContract"></param>
        /// <param name="queryAction"></param>
        /// <returns></returns>
        public DbQueryOperatingSession CreateSession(IDataServiceContract serviceContract, DbQueryActions queryAction)
        {
            var _connection         = _connectionPool.DetachConnection(serviceContract);
            var _operatingSession   = new DbQueryOperatingSession(this, _connection, queryAction);

            _operatingSession.OperationFailed += new DbQueryFailedEventHandler(_OnOperationFailed);
            
            _innerSessionRepository.Add(_operatingSession);

            return _operatingSession;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (!_isNested)
            {
                foreach (var _session in Session)
                {
                    if (_session.State.HasFlag(DbQueryOperatingSessionStates.Failed) 
                        || _session.State.HasFlag(DbQueryOperatingSessionStates.ConnectionFaulted))
                    {
                        _connectionPool.Rollback();
                        break;
                    }
                }

                _connectionPool.Commit();
                _connectionPool.Dispose();

                _innerSharedRepository.Clear();
                _innerSessionRepository.Clear();

                foreach (var _cacheKey in _innerCacheKeyRepository)
                {
                    s_innerCacheRepository.Remove(_cacheKey);
                }

                _innerCacheKeyRepository.Clear();
            }

            _innerErrorRepository.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stackFrame"></param>
        /// <returns></returns>
        string GenerateUniqueCacheKey(StackFrameWrapper stackFrame)
        {
            if (stackFrame.IsValid)
            {
                var _className  = stackFrame.Method.DeclaringType.FullName.ToLower();
                var _methodName = stackFrame.Method.Name.ToLower();

                return string.Format("{0}.{1}", _className, _methodName);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsDbQueryOperationMember(StackFrameWrapper stackFrame)
        {
            if (stackFrame.IsValid)
                return stackFrame.Method.DeclaringType.Equals(typeof(DbQueryOperation));

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        void VerifyCacheKey(string cacheKey)
        {
            if (_innerCacheKeyRepository.Contains(cacheKey) || s_innerCacheRepository.ContainsKey(cacheKey))
            {
                throw new InvalidOperationException(string.Format("The class '{0}' cannot be created more than one instance in the same scope.", GetType().FullName));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _OnOperationFailed(object sender, DbQueryFailedEventArgs e)
        {
            _innerErrorRepository.Add(e.Error);
        }
        #endregion
    }
}
