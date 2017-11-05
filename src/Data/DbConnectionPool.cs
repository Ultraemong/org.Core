using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using org.Core.Collections;
using org.Core.ServiceModel;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbConnectionPool : DbQueryPrinciple, IDisposable, IDbQueryOperationContextProvider
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public DbConnectionPool(DbQueryOperationContext operationContext)
            : base(operationContext)
        {
            if (!operationContext.Items.ContainsKey(CACHEKEY))
            {
                operationContext.Items.Add(CACHEKEY, new NameValueCollection<string, DbConnection>());
            }

            _innerCacheRepository = (operationContext.Items[CACHEKEY] as INameValueCollection<string, DbConnection>);
        }
        #endregion

        #region Fields
        const string                                            CACHEKEY                = "_org_core_data_dbconnectionpool";

        bool                                                    _isCommitted            = false;
        bool                                                    _isRolledback           = false;

        readonly object                                         _syncRoot               = new object();
        readonly INameValueCollection<string, DbConnection>     _innerCacheRepository   = default(INameValueCollection<string, DbConnection>);
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DbConnection DetachConnection(object entity)
        {
            return DetachConnection(DataServiceContractHelpers.RetrieveDataServiceContract(entity));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceContract"></param>
        /// <returns></returns>
        public DbConnection DetachConnection(IDataServiceContract serviceContract)
        {
            if (null != serviceContract)
            {
                if (!_innerCacheRepository.ContainsKey(serviceContract.Host))
                {
                    lock (_syncRoot)
                    {
                        _innerCacheRepository.Add(serviceContract.Host, new DbConnection(OperationContext, serviceContract));
                    }
                }

                return _innerCacheRepository[serviceContract.Host];
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            if (!_isCommitted && !_isRolledback)
            {
                foreach (var _connection in _innerCacheRepository.Values)
                {
                    _connection.Commit();
                }

                _isCommitted = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Rollback()
        {
            if (!_isCommitted && !_isRolledback)
            {
                foreach (var _connection in _innerCacheRepository.Values)
                {
                    _connection.Rollback();
                }

                _isRolledback = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            foreach (var _connection in _innerCacheRepository.Values)
            {
                if (null != _connection)
                {
                    _connection.Dispose();
                }
            }

            lock (_syncRoot)
            {
                _innerCacheRepository.Clear();
            }
        }
        #endregion
    }
}
