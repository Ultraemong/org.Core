using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;
using System.Data.SqlClient;

using org.Core.Utilities;
using org.Core.Diagnostics;
using org.Core.ServiceModel;
using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DbQueryOperation : DbQueryPrinciple, IDbQueryOperationContextProvider
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        protected DbQueryOperation()
            : base()
        {
        }
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool IsExceptionalType(object entity)
        {
            if (ObjectUtils.IsListType(entity))
            {
                var _collection = (entity as IList);

                return (0 < _collection.Count);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatingSession"></param>
        /// <param name="entity"></param>
        /// <param name="memberDescriptor"></param>
        /// <param name="serviceContract"></param>
        /// <param name="queryContract"></param>
        protected virtual void ExecuteQueryImpl(DbQueryOperatingSession operatingSession, object entity, IEntryDescriptor memberDescriptor, IDataServiceContract serviceContract, IDbQueryContract queryContract)
        {
            var _queryContract      = queryContract ?? operatingSession.GetDbQueryContract(memberDescriptor);

            InitializeRequiredProperties(operatingSession.OperationContext, _queryContract);

            var _queryContext       = operatingSession.CreateDbQueryContext(serviceContract);

            InitializeRequiredProperties(_queryContext, _queryContract);

            var _parameterMapper    = operatingSession.CreateDbQueryParameterMapper(memberDescriptor);
            var _operationExecutor  = operatingSession.CreateDbQueryOperationExecutor(memberDescriptor);
            var _propertyValidator  = operatingSession.CreateDbQueryPropertyValidator(memberDescriptor);

            _propertyValidator.Validate(entity);

            _parameterMapper.Map(_queryContext, entity);

            _operationExecutor.Execute(entity, _queryContext);

            var _queryResult = _queryContext.Execute();

            if (null != _queryResult && _queryResult.HasResult)
            {
                var _resultMapper = operatingSession.CreateDbQueryResultMapper(memberDescriptor);

                _resultMapper.Map(entity, _queryResult);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="queryAction"></param>
        /// <param name="serviceContract"></param>
        /// <param name="queryContract"></param>
        /// <returns></returns>
        protected OperationResult<TEntity> ExecuteQuery<TEntity>(TEntity entity, DbQueryActions queryAction, IDataServiceContract serviceContract, IDbQueryContract queryContract)
            where TEntity : class
        {
            using (var _operationContext = new DbQueryOperationContext())
            {
                if (!IsExceptionalType(entity))
                {
                    var _memberDescriptor   = _operationContext.DescriptorManager.GetDescriptor(entity);
                    var _serviceContract    = serviceContract ?? _memberDescriptor.ServiceContract;
                    var _operationSession   = _operationContext.CreateSession(_serviceContract, queryAction);

                    _operationSession.Open();

                    ExecuteQueryImpl(_operationSession, entity, _memberDescriptor, _serviceContract, queryContract);

                    _operationSession.Close();
                }
                else
                {
                    var _entities = entity as IList;

                    for (var i = 0; i < _entities.Count; i++)
                    {
                        if (!_operationContext.HasErrors)
                        {
                            var _memberDescriptor   = _operationContext.DescriptorManager.GetDescriptor(_entities[i]);
                            var _serviceContract    = serviceContract ?? _memberDescriptor.ServiceContract;
                            var _operationSession   = _operationContext.CreateSession(_serviceContract, queryAction);

                            _operationSession.Open();

                            ExecuteQueryImpl(_operationSession, _entities[i], _memberDescriptor, _serviceContract, queryContract);

                            _operationSession.Close();

                            continue;
                        }

                        break;
                    }
                }

                if (_operationContext.HasErrors)
                {
                    return new OperationResult<TEntity>(entity, _operationContext.Errors);
                }
            }

            return new OperationResult<TEntity>(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected OperationResult<TEntity> BaseCreate<TEntity>(TEntity entity)
            where TEntity : class
        {
            var _stackFrame         = StackTraceHelpers.GetStackFrameByIndex(1);
            var _queryContract      = RetrieveDbQueryContract(_stackFrame);
            var _serviceContract    = RetrieveDataServiceContract(_stackFrame);

            return ExecuteQuery<TEntity>(entity, DbQueryActions.Create, _serviceContract, _queryContract);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected OperationResult<TEntity> BaseUpdate<TEntity>(TEntity entity)
            where TEntity : class
        {
            var _stackFrame         = StackTraceHelpers.GetStackFrameByIndex(1);
            var _queryContract      = RetrieveDbQueryContract(_stackFrame);
            var _serviceContract    = RetrieveDataServiceContract(_stackFrame);

            return ExecuteQuery<TEntity>(entity, DbQueryActions.Update, _serviceContract, _queryContract);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected OperationResult<TEntity> BaseDelete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var _stackFrame         = StackTraceHelpers.GetStackFrameByIndex(1);
            var _queryContract      = RetrieveDbQueryContract(_stackFrame);
            var _serviceContract    = RetrieveDataServiceContract(_stackFrame);

            return ExecuteQuery<TEntity>(entity, DbQueryActions.Delete, _serviceContract, _queryContract);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected OperationResult<TEntity> BaseFetch<TEntity>(TEntity entity)
            where TEntity : class
        {
            var _stackFrame         = StackTraceHelpers.GetStackFrameByIndex(1);
            var _queryContract      = RetrieveDbQueryContract(_stackFrame);
            var _serviceContract    = RetrieveDataServiceContract(_stackFrame);
            
            return ExecuteQuery<TEntity>(entity, DbQueryActions.Fetch, _serviceContract, _queryContract);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected OperationResult<IDbQueryResult> ExecuteQuery(Action<IDbQueryParameterizable> predicate = default(Action<IDbQueryParameterizable>))
        {
            var _queryResult        = (IDbQueryResult)null;

            var _stackFrame         = StackTraceHelpers.GetStackFrameByIndex(1);
            var _queryContract      = RetrieveDbQueryContract(_stackFrame);
            var _serviceContract    = RetrieveDataServiceContract(_stackFrame);

            VerifyQueryContract(_queryContract);
            VerifyServiceContract(_serviceContract);

            using (var _operationContext = new DbQueryOperationContext())
            {
                InitializeRequiredProperties(_operationContext, _queryContract);
                
                var _operatingSession   = _operationContext.CreateSession(_serviceContract, _queryContract.QueryAction);
                var _queryContext       = _operatingSession.CreateDbQueryContext(_serviceContract);

                InitializeRequiredProperties(_queryContext, _queryContract);

                _operatingSession.Open();

                if (default(Action<IDbQueryParameterizable>) != predicate)
                {
                    predicate(_queryContext);
                }

                _queryResult = _queryContext.Execute();

                _operatingSession.Close();

                if (_operationContext.HasErrors)
                {
                    return new OperationResult<IDbQueryResult>(_queryResult, _operationContext.Errors);
                }
            }

            return new OperationResult<IDbQueryResult>(_queryResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected OperationResult<TEntity> ExecuteQuery<TEntity>(Action<IDbQueryParameterizable> predicate = default(Action<IDbQueryParameterizable>))
            where TEntity : class, new()
        {
            var _entity             = new TEntity();

            var _stackFrame         = StackTraceHelpers.GetStackFrameByIndex(1);
            var _queryContract      = RetrieveDbQueryContract(_stackFrame);
            var _serviceContract    = RetrieveDataServiceContract(_stackFrame);

            VerifyQueryContract(_queryContract);
            VerifyServiceContract(_serviceContract);

            using (var _operationContext = new DbQueryOperationContext())
            {
                InitializeRequiredProperties(_operationContext, _queryContract);

                var _memberDescriptor   = _operationContext.DescriptorManager.GetDescriptor(_entity);
                var _operatingSession   = _operationContext.CreateSession(_serviceContract, _queryContract.QueryAction);
                var _queryContext       = _operatingSession.CreateDbQueryContext(_memberDescriptor);

                InitializeRequiredProperties(_queryContext, _queryContract);

                _operatingSession.Open();

                if (default(Action<IDbQueryParameterizable>) != predicate)
                {
                    predicate(_queryContext);
                }

                var _queryResult = _queryContext.Execute();

                if (null != _queryResult && _queryResult.HasResult)
                {
                    var _resultMapper = _operatingSession.CreateDbQueryResultMapper(_memberDescriptor);

                    _resultMapper.Map(_entity, _queryResult);
                }
                        
                _operatingSession.Close();

                if (_operationContext.HasErrors)
                {
                    return new OperationResult<TEntity>(_entity, _operationContext.Errors);
                }
            }

            return new OperationResult<TEntity>(_entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stackFrame"></param>
        /// <returns></returns>
        IDbQueryContract RetrieveDbQueryContract(StackFrameWrapper stackFrame)
        {
            if (stackFrame.IsValid)
            {
                var _memberType         = typeof(IDbQueryContract);
                var _memberDescriptor   = stackFrame.MethodDescriptor.GetAttributeDescriptorByAttributeType(_memberType);

                if (null != _memberDescriptor)
                    return _memberDescriptor.Member as IDbQueryContract;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stackFrame"></param>
        /// <returns></returns>
        IDataServiceContract RetrieveDataServiceContract(StackFrameWrapper stackFrame)
        {
            if (stackFrame.IsValid)
            {
                var _memberType         = typeof(IDataServiceContract); 
                var _memberDescriptor   = stackFrame.MethodDescriptor.GetAttributeDescriptorByAttributeType(_memberType);

                if (null != _memberDescriptor)
                    return _memberDescriptor.Member as IDataServiceContract;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        /// <param name="queryContract"></param>
        void InitializeRequiredProperties(DbQueryOperationContext operationContext, IDbQueryContract queryContract)
        {
            if (null == operationContext)
                throw new ArgumentNullException("operationContext");

            if (null == queryContract)
                throw new ArgumentNullException("queryContract");

            operationContext.ExecutionTimeout       = queryContract.ExecutionTimeout;
            operationContext.RequiresTransaction    = queryContract.RequiresTransaction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        /// <param name="queryContract"></param>
        void InitializeRequiredProperties(IDbQueryContext queryContext, IDbQueryContract queryContract)
        {
            if (null == queryContext)
                throw new ArgumentNullException("queryContext");

            if (null == queryContract)
                throw new ArgumentNullException("queryContract");

            queryContext.QueryText      = queryContract.QueryText;
            queryContext.QueryBehavior  = queryContract.QueryBehavior;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContract"></param>
        void VerifyQueryContract(IDbQueryContract queryContract)
        {
            if (null == queryContract)
                throw new ArgumentNullException(string.Format("The attribute '{0}' is not declared in the previous section.", typeof(DbQueryOperationContractAttribute).FullName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceContract"></param>
        void VerifyServiceContract(IDataServiceContract serviceContract)
        {
            if (null == serviceContract)
                throw new ArgumentNullException(string.Format("The attribute '{0}' is not declared in the previous section.", typeof(DataServiceContractAttribute).FullName));
        }
        #endregion
    }
}
