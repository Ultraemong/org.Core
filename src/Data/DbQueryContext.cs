using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;

using org.Core.Utilities;
using org.Core.Collections;
using org.Core.Conversion;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbQueryContext : IDbQueryContext
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public DbQueryContext(SqlCommand command)
        {
            _command                = command;
            _connection             = command.Connection;
            _executionOnly          = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="requiresTransaction"></param>
        /// <param name="isolationLevel"></param>
        public DbQueryContext(SqlConnection connection, bool requiresTransaction, IsolationLevel? isolationLevel)
        {
            _connection             = connection;
            _command                = connection.CreateCommand();
            _requiresTransaction    = requiresTransaction;
        }
        #endregion

        #region Events
        public event DbQueryExecutingEventHandler   Executing               = null;
        public event DbQueryExecutedEventHandler    Executed                = null;
        public event DbQueryFailedEventHandler      Failed                  = null;
        #endregion

        #region Fields
        readonly bool                               _requiresTransaction    = false;
        readonly bool                               _executionOnly          = false;

        readonly SqlCommand                         _command                = null;
        readonly SqlConnection                      _connection             = null;
        readonly TypeConverter                      _typeConverter          = new TypeConverter();

        readonly IDbQueryExecutorFactory            _queryExecutorFactory   = new DbQueryExecutorFactory();
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string QueryText
        {
            get
            {
                return _command.CommandText;
            }

            set
            {
                _command.CommandText = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQueryBehaviors? QueryBehavior
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ExecutionTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ExecutionOnly
        {
            get
            {
                return _executionOnly;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void AddParameter(SqlParameter parameter)
        {
            _command.Parameters.Add(parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        public void AddParameter(string name, object value)
        {
            _command.Parameters.AddWithValue(AdoDotNetDbParameterHelpers.StringToParameterName(name), value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        public void AddParameter(string name, SqlDbType type, ParameterDirection direction = ParameterDirection.Output)
        {
            var _parameter              = _command.CreateParameter();

            _parameter.ParameterName    = AdoDotNetDbParameterHelpers.StringToParameterName(name);
            _parameter.SqlDbType        = type;
            _parameter.Direction        = direction;
            
            AddParameter(_parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        public void AddParameter(string name, object value, SqlDbType type, ParameterDirection direction = ParameterDirection.Input)
        {
            var _parameter              = _command.CreateParameter();

            _parameter.ParameterName    = AdoDotNetDbParameterHelpers.StringToParameterName(name);
            _parameter.Value            = value;
            _parameter.SqlDbType        = type;
            _parameter.Direction        = direction;
            
            AddParameter(_parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public void AddParameter(string name, SqlDbType type, int size, ParameterDirection direction = ParameterDirection.Output)
        {
            var _parameter              = _command.CreateParameter();

            _parameter.ParameterName    = AdoDotNetDbParameterHelpers.StringToParameterName(name);
            _parameter.Size             = size;
            _parameter.SqlDbType        = type;
            _parameter.Direction        = direction;
            
            AddParameter(_parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public void AddParameter(string name, object value, SqlDbType type, int size, ParameterDirection direction = ParameterDirection.Input)
        {    
            var _parameter              = _command.CreateParameter();

            _parameter.ParameterName    = AdoDotNetDbParameterHelpers.StringToParameterName(name);
            _parameter.Value            = value;
            _parameter.Size             = size;
            _parameter.SqlDbType        = type;
            _parameter.Direction        = direction;
            
            AddParameter(_parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        public void AddParameter(string name, SqlDbType type, int size, byte precision, byte scale, ParameterDirection direction = ParameterDirection.Output)
        {
            var _parameter              = _command.CreateParameter();

            _parameter.ParameterName    = AdoDotNetDbParameterHelpers.StringToParameterName(name);
            _parameter.Size             = size;
            _parameter.Precision        = precision;
            _parameter.Scale            = scale;
            _parameter.SqlDbType        = type;
            _parameter.Direction        = direction;
            
            AddParameter(_parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="direction"></param>
        public void AddParameter(string name, object value, SqlDbType type, int size, byte precision, byte scale, ParameterDirection direction = ParameterDirection.Input)
        {
            var _parameter              = _command.CreateParameter();

            _parameter.ParameterName    = AdoDotNetDbParameterHelpers.StringToParameterName(name);
            _parameter.Value            = value;
            _parameter.Size             = size;
            _parameter.Precision        = precision;
            _parameter.Scale            = scale;
            _parameter.SqlDbType        = type;
            _parameter.Direction        = direction;
            
            AddParameter(_parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void AddParameters(object parameter)
        {
            var _parameters = _typeConverter.Convert<IReadOnlyNameValueCollection<string, object>>(parameter);

            foreach (var _parameter in _parameters)
            {
                var _name   = AdoDotNetDbParameterHelpers.StringToParameterName(_parameter.Key);
                var _value  = _parameter.Value;

                _command.Parameters.AddWithValue(_name, _value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SqlParameter GetParameter(string name)
        {
            return _command.Parameters[AdoDotNetDbParameterHelpers.StringToParameterName(name)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SqlParameter GetParameter(int index)
        {
            return _command.Parameters[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void RemoveParameter(string name)
        {
            _command.Parameters.RemoveAt(AdoDotNetDbParameterHelpers.StringToParameterName(name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public void RemoveParameter(int index)
        {
            _command.Parameters.RemoveAt(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public void RemoveParameter(SqlParameter parameter)
        {
            _command.Parameters.Remove(parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetParameters()
        {
            _command.Parameters.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool ContainsParameter(SqlParameter parameter)
        {
            return _command.Parameters.Contains(parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsParameter(string name)
        {
            return (-1 < _command.Parameters.IndexOf(AdoDotNetDbParameterHelpers.StringToParameterName(name)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected virtual IDbQueryResult ExecuteImpl(SqlCommand command)
        {
            var _queryBehavior  = QueryBehavior.GetValueOrDefault(DbQueryBehaviors.Default);
            var _queryExecutor  = _queryExecutorFactory.CreateDbQueryExecutor(_queryBehavior);

            if (!_queryBehavior.HasFlag(DbQueryBehaviors.Parameterized))
                command.CommandType = CommandType.StoredProcedure;

            if (ExecutionTimeout.HasValue)
                command.CommandTimeout = ExecutionTimeout.Value;

            return _queryExecutor.Execute(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbQueryResult Execute()
        {
            var _execution      = new DbQueryExecutingEventArgs();
            var _queryResult    = (IDbQueryResult)null;

            if (null != Executing)
                Executing(this, _execution);

            if (!_execution.Cancel)
            { 
                if (!_executionOnly)
                    _connection.Open();

                try
                {
                    _queryResult = ExecuteImpl(_command);

                    if (null != Executed)
                        Executed(this, new DbQueryExecutedEventArgs(_queryResult));
                }
                catch (SqlException ex)
                {
                    if (null != Failed)
                    {
                        var _exception  = new DbQueryException(ex);
                        var _failure    = new DbQueryFailedEventArgs(_exception);

                        Failed(this, _failure);
                    }
                }
                catch (DbQueryException ex)
                {
                    if (null != Failed)
                        Failed(this, new DbQueryFailedEventArgs(ex));
                }

                if (!_executionOnly)
                    _connection.Close();
            }


            return _queryResult;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {   
            if (!_executionOnly)
            {
                if (null != _command)
                    _command.Dispose();

                if (null != _connection)
                    _connection.Dispose();
            }
        }
        #endregion

        #region Internal Classes
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
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
            public static IDbQueryContext Initialize(Type type, SqlCommand command)                
            {
                return ObjectUtils.CreateInstanceOf<IDbQueryContext>(type, command);
            }
            #endregion
        }
        #endregion
    }
}
