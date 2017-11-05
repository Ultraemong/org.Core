using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.ServiceModel;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbConnection : DbQueryPrinciple, IDisposable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        /// <param name="serviceContract"></param>
        public DbConnection(DbQueryOperationContext operationContext, IDataServiceContract serviceContract)
            : base(operationContext)
        {
            _contract       = serviceContract;
            _connection     = new SqlConnection(_contract.Host);
        } 
        #endregion

        #region Fields
        readonly SqlConnection          _connection         = null;
        readonly IDataServiceContract   _contract           = null;

        SqlTransaction                  _transaction        = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool IsAlive
        {
            get
            {
                //I have no idea, but it will be implemented for asynchronous programming.
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInTransaction
        {
            get
            {
                return (null != _transaction);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanOpen
        {
            get
            {
                return (IsAlive && !_connection.State.HasFlag(ConnectionState.Open));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CanClose
        {
            get
            {
                return (IsAlive && !IsInTransaction && _connection.State.HasFlag(ConnectionState.Open));
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            if (CanOpen)
            {
                _connection.Open();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            if (CanClose)
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SqlCommand CreateDbCommand()
        {
            var _command = _connection.CreateCommand();

            if (OperationContext.RequiresTransaction)
                _command.Transaction = CreateDbTransaction();

            if (OperationContext.ExecutionTimeout.HasValue)
                _command.CommandTimeout = OperationContext.ExecutionTimeout.Value;

            return _command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SqlTransaction CreateDbTransaction()
        {
            if (null == _transaction)
            {
                Open();

                return _transaction = _connection.BeginTransaction();
            }

            return _transaction;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            if (null != _transaction)
            {
                _transaction.Commit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Rollback()
        {
            if (null != _transaction)
            {
                _transaction.Rollback();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (null != _transaction)
            {
                _transaction.Dispose();
            }

            if (null != _connection)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        #endregion
    }
}
