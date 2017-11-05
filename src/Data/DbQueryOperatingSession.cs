using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using org.Core.Collections;
using org.Core.Utilities;
using org.Core.Conversion;
using org.Core.ServiceModel;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbQueryOperatingSession : DbQueryPrinciple, IDisposable, IDbQueryOperationContextProvider
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        /// <param name="connection"></param>
        /// <param name="queryAction"></param>
        public DbQueryOperatingSession(DbQueryOperationContext operationContext, DbConnection connection, DbQueryActions queryAction) 
            : base(operationContext)
        {
            _connection   = connection;
            _queryAction  = queryAction;
        }
        #endregion

        #region Events
        public event DbQueryFailedEventHandler                          OperationFailed                     = null;
        public event DbQueryOperatingSessionStateChangedEventHandler    StateChanged                        = null;
        #endregion

        #region Fields
        const string                                                    ERRORCODE_PARAMETERNAME             = "__ErrorCode";
        const int                                                       ERRORCODE_PARAMETERSIZE             = 4;

        const string                                                    ERRORTEXT_PARAMETERNAME             = "__ErrorText";
        const int                                                       ERRORTEXT_PARAMETERSIZE             = 200;

        readonly DbConnection                                           _connection                         = null;
        readonly DbQueryActions                                         _queryAction                        = DbQueryActions.Unknown;
        
        DbQueryOperatingSessionStates                                   _state                              = DbQueryOperatingSessionStates.Ready;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryActions QueryAction
        {
            get
            {
                return _queryAction;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryOperatingSessionStates State
        {
            get 
            {
                return _state;
            }

            private set
            {
                _state |= value;

                if (null != StateChanged)
                {
                    var _arguments = new DbQueryOperatingSessionStateChangedEventArgs(_state, value);

                    StateChanged(this, _arguments);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbConnection Connection
        {
            get
            {
                return _connection;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgument"></param>
        protected void RaiseOperationFailedEvent(DbQueryFailedEventArgs eventArgument)
        {
            if (null != OperationFailed)
            {
                OperationFailed(this, eventArgument);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            try
            {
                if (!Connection.IsAlive)
                {
                    State = DbQueryOperatingSessionStates.ConnectionFaulted;

                    throw new DbQueryException("The connection is unavailable");
                }

                Connection.Open();

                State = DbQueryOperatingSessionStates.Opened;
            }
            catch (SqlException ex)
            {
                var _exception  = new DbQueryException(ex);
                var _arguments  = new DbQueryFailedEventArgs(_exception);

                RaiseOperationFailedEvent(_arguments);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            try
            {
                Connection.Close();

                State = DbQueryOperatingSessionStates.Closed;
            }
            catch (SqlException ex)
            {
                var _exception  = new DbQueryException(ex);
                var _arguments  = new DbQueryFailedEventArgs(_exception);

                RaiseOperationFailedEvent(_arguments);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractMemberProvider"></param>
        /// <returns></returns>
        public IDbQueryContract GetDbQueryContract(IDbQueryContractProvider contractMemberProvider)
        {
            return GetDbQueryContract(contractMemberProvider.ContractDescriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractMemberDescriptors"></param>
        /// <returns></returns>
        public IDbQueryContract GetDbQueryContract(DbQueryContractDescriptors contractMemberDescriptors)
        {
            var _contractDescriptor = contractMemberDescriptors.GetDescriptorByAction(QueryAction) 
                ?? contractMemberDescriptors.GetDescriptorByAction(DbQueryActions.Unknown);

            if (null == _contractDescriptor)
            {
                return CreateDbQueryContract(contractMemberDescriptors.DeclaringDescriptor);
            }
            else if (!_contractDescriptor.IsBound)
            {
                return CreateDbQueryContract(contractMemberDescriptors.DeclaringDescriptor, _contractDescriptor);
            }

            return _contractDescriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public IDbQueryContract CreateDbQueryContract(IEntryDescriptor descriptor)
        {
            var _contractBuilder                    = new DbQueryContractBuilder();

            _contractBuilder.Name                   = descriptor.Name;
            _contractBuilder.Schema                 = DbQueryContract.QUERYTEXT_SCHEMA;
            _contractBuilder.Namespace              = descriptor.Namespace;
            _contractBuilder.QueryAction            = QueryAction;
            _contractBuilder.IsElementContainable   = descriptor.IsListType;

            return _contractBuilder.GetDbQueryContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="queryContract"></param>
        /// <returns></returns>
        public IDbQueryContract CreateDbQueryContract(IEntryDescriptor descriptor, IDbQueryContract queryContract)
        {
            var _contractBuilder                        = new DbQueryContractBuilder();

            _contractBuilder.Name                       = string.IsNullOrEmpty(queryContract.Name) ? descriptor.Name : queryContract.Name;
            _contractBuilder.Schema                     = string.IsNullOrEmpty(queryContract.Schema) ? DbQueryContract.QUERYTEXT_SCHEMA : queryContract.Schema;
            _contractBuilder.Namespace                  = descriptor.Namespace;
            _contractBuilder.Abbreviation               = queryContract.Abbreviation;
            _contractBuilder.QueryAction                = QueryAction;
            _contractBuilder.IsElementContainable       = descriptor.IsListType;
            _contractBuilder.OmitsAbbreviationNaming    = queryContract.OmitsAbbreviationNaming;

            return _contractBuilder.GetDbQueryContract();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceContract"></param>
        /// <returns></returns>
        public IDbQueryContext CreateDbQueryContext(IDataServiceContractProvider serviceContractProvider)
        {
            return CreateDbQueryContext(serviceContractProvider.ServiceContract);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceContract"></param>
        /// <returns></returns>
        public IDbQueryContext CreateDbQueryContext(IDataServiceContract serviceContract)
        {
            var _command        = Connection.CreateDbCommand();
            var _provider       = serviceContract.Provider ?? typeof(DbQueryContext);
            var _queryContext   = DbQueryContext.Initializer.Initialize(_provider, _command);

            if (OperationContext.ExecutionTimeout.HasValue)
                _queryContext.ExecutionTimeout = OperationContext.ExecutionTimeout;

            _queryContext.Failed     += new DbQueryFailedEventHandler(_OnFailed);
            _queryContext.Executed   += new DbQueryExecutedEventHandler(_OnExecuted);
            _queryContext.Executing  += new DbQueryExecutingEventHandler(_OnExecuting);

            return _queryContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyMemberProvider"></param>
        /// <returns></returns>
        public IDbQueryPropertyValidator CreateDbQueryPropertyValidator(IDbQueryPropertyProvider propertyMemberProvider)
        {
            return CreateDbQueryPropertyValidator(propertyMemberProvider.PropertyDescriptors.GetDescriptorsByQueryAction(QueryAction));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyMemberDescriptors"></param>
        /// <returns></returns>
        public IDbQueryPropertyValidator CreateDbQueryPropertyValidator(DbQueryPropertyDescriptors propertyMemberDescriptors)
        {
            var _propertyValidator          = new DbQueryPropertyValidator(this, propertyMemberDescriptors);

            _propertyValidator.Failed       += new DbQueryFailedEventHandler(_OnFailed);
            _propertyValidator.Validated    += new DbQueryValidatedEventHandler(_OnValidated);

            return _propertyValidator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationMemberProvider"></param>
        /// <returns></returns>
        public IDbQueryOperationExecutor CreateDbQueryOperationExecutor(IDbQueryOperationProvider operationMemberProvider)
        {
            return CreateDbQueryOperationExecutor(operationMemberProvider.OperationDescriptors.GetDescriptorsByQueryAction(QueryAction));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationMemberDescriptors"></param>
        /// <returns></returns>
        public IDbQueryOperationExecutor CreateDbQueryOperationExecutor(DbQueryOperationDescriptors operationMemberDescriptors)
        {
            return new DbQueryOperationExecutor(this, operationMemberDescriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyMemberProvider"></param>
        /// <returns></returns>
        public IDbQueryParameterMapper CreateDbQueryParameterMapper(IDbQueryPropertyProvider propertyMemberProvider)
        {
            return CreateDbQueryParameterMapper(propertyMemberProvider.PropertyDescriptors.GetDescriptorsByQueryAction(QueryAction));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyMemberProvider"></param>
        /// <param name="isInChild"></param>
        /// <returns></returns>
        public IDbQueryParameterMapper CreateDbQueryParameterMapper(IDbQueryPropertyProvider propertyMemberProvider, bool isInChild)
        {
            return CreateDbQueryParameterMapper(propertyMemberProvider.PropertyDescriptors.GetDescriptorsByQueryAction(QueryAction), isInChild);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyMemberDescriptors"></param>
        /// <returns></returns>
        public IDbQueryParameterMapper CreateDbQueryParameterMapper(DbQueryPropertyDescriptors propertyMemberDescriptors)
        {
            return new DbQueryParameterMapper(this, propertyMemberDescriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyMemberDescriptors"></param>
        /// <param name="isInChild"></param>
        /// <returns></returns>
        public IDbQueryParameterMapper CreateDbQueryParameterMapper(DbQueryPropertyDescriptors propertyMemberDescriptors, bool isInChild)
        {
            return new DbQueryParameterMapper(this, propertyMemberDescriptors, isInChild);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyMemberProvider"></param>
        /// <returns></returns>
        public IDbQueryResultMapper CreateDbQueryResultMapper(IDbQueryPropertyProvider propertyMemberProvider)
        {
            return CreateDbQueryResultMapper(propertyMemberProvider.PropertyDescriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyMemberDescriptors"></param>
        /// <returns></returns>
        public IDbQueryResultMapper CreateDbQueryResultMapper(DbQueryPropertyDescriptors propertyMemberDescriptors)
        {
            return new DbQueryResultMapper(this, propertyMemberDescriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _OnFailed(object sender, DbQueryFailedEventArgs e)
        {
            RaiseOperationFailedEvent(e);

            State = DbQueryOperatingSessionStates.Failed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _OnExecuted(object sender, DbQueryExecutedEventArgs e)
        {
            State = DbQueryOperatingSessionStates.Executed;

            if (OperationContext.RequiresEnvironmentVariables)
            {
                var _errorCode = e.QueryResult.GetValueOrDefault(ERRORCODE_PARAMETERNAME, 0);
                var _errorText = e.QueryResult.GetValueOrDefault(ERRORTEXT_PARAMETERNAME, string.Empty);

                if (0 != _errorCode)
                {
                    throw new DbQueryException(_errorText, _errorCode);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _OnExecuting(object sender, DbQueryExecutingEventArgs e)
        {
            e.Cancel = (State.HasFlag(DbQueryOperatingSessionStates.ConnectionFaulted) 
                || State.HasFlag(DbQueryOperatingSessionStates.Failed));

            if (!e.Cancel)
            {
                var _sender = sender as IDbQueryContext;

                if (OperationContext.RequiresEnvironmentVariables)
                {
                    if (!_sender.ContainsParameter(ERRORCODE_PARAMETERNAME))
                        _sender.AddParameter(ERRORCODE_PARAMETERNAME, SqlDbType.Int, ERRORCODE_PARAMETERSIZE, ParameterDirection.Output);

                    if (!_sender.ContainsParameter(ERRORTEXT_PARAMETERNAME))
                        _sender.AddParameter(ERRORTEXT_PARAMETERNAME, SqlDbType.VarChar, ERRORTEXT_PARAMETERSIZE, ParameterDirection.Output);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _OnValidated(object sender, EventArgs e)
        {
            State = DbQueryOperatingSessionStates.Validated;
        }
        #endregion
    }
}
