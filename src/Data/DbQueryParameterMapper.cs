using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using System.Data.SqlTypes;

using org.Core.Collections;
using org.Core.Utilities;
using org.Core.Conversion;
using org.Core.Serialization;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbQueryParameterMapper : DbQueryOperatingPrinciple, IDbQueryParameterMapper
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatingSession"></param>
        /// <param name="memberDescriptors"></param>
        public DbQueryParameterMapper(DbQueryOperatingSession operatingSession, DbQueryPropertyDescriptors memberDescriptors)
            : base(operatingSession)
        {
            _propertyDescriptors = memberDescriptors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationSession"></param>
        /// <param name="memberDescriptors"></param>
        /// <param name="isInChild"></param>
        public DbQueryParameterMapper(DbQueryOperatingSession operationSession, DbQueryPropertyDescriptors memberDescriptors, bool isInChild)
            : this(operationSession, memberDescriptors)
        {
            _isInChild = isInChild;
        }
        #endregion

        #region Fields
        readonly bool                           _isInChild              = false;

        readonly TypeConverter                  _typeConverter          = new TypeConverter();
        readonly DbQueryPropertyDescriptors     _propertyDescriptors    = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DbQueryPropertyDescriptors PropertyDescriptors
        {
            get 
            {
                return _propertyDescriptors;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInChild
        {
            get
            {
                return _isInChild;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        public void Map(IDbQueryParameterizable destination, object source)
        {
            Map(destination, source, PropertyDescriptors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <returns></returns>
        protected virtual bool IsExceptionalType(DbQueryPropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.RetrunType.IsPrimitive
                || propertyDescriptor.RetrunType.Equals(typeof(string)))
            {
                return false;
            }
            
            return ObjectUtils.IsClassType(propertyDescriptor.RetrunType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <param name="parameterName"></param>
        /// <param name="propertyDirection"></param>
        /// <returns></returns>
        protected virtual SqlParameter CreateParameter(DbQueryPropertyDescriptor propertyDescriptor, string parameterName, DbQueryPropertyDirections propertyDirection)
        {
            var _parameter              = new SqlParameter();

            _parameter.ParameterName    = AdoDotNetDbParameterHelpers.StringToParameterName(parameterName);
            _parameter.Direction        = AdoDotNetDbParameterHelpers.PropertyDirectionToParameterDirection(propertyDirection);
            _parameter.SqlDbType        = propertyDescriptor.DbType;
            _parameter.Size             = propertyDescriptor.Size;

            if (propertyDescriptor.DbType.Equals(SqlDbType.Decimal))
            {
                if (!ObjectUtils.IsNullOrDefault(propertyDescriptor.Scale))
                {
                    _parameter.Scale = propertyDescriptor.Scale;
                }

                if (!ObjectUtils.IsNullOrDefault(propertyDescriptor.Precision))
                {
                    _parameter.Precision = propertyDescriptor.Precision;
                }
            }

            return _parameter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        protected virtual SqlParameter[] CreateParameters(DbQueryPropertyDescriptor propertyDescriptor)
        {
            var _parameters = new ListCollection<SqlParameter>();

            if (propertyDescriptor.HasInputDirection && propertyDescriptor.HasOutputDirection)
            {
                var _inputName  = propertyDescriptor.GetName(DbQueryPropertyDirections.Input);
                var _outputName = propertyDescriptor.GetName(DbQueryPropertyDirections.Output);

                if (_inputName.Equals(_outputName))
                {
                    _parameters.Add(CreateParameter(propertyDescriptor, _inputName, DbQueryPropertyDirections.InputOutput));
                }
                else
                {
                    _parameters.Add(CreateParameter(propertyDescriptor, _inputName, DbQueryPropertyDirections.Input));
                    _parameters.Add(CreateParameter(propertyDescriptor, _outputName, DbQueryPropertyDirections.Output));
                }
            }
            else if (propertyDescriptor.HasOutputDirection)
            {
                var _direction      = DbQueryPropertyDirections.Output;
                var _parameterName  = propertyDescriptor.GetName(_direction);

                _parameters.Add(CreateParameter(propertyDescriptor, _parameterName, _direction));
            }
            else if (propertyDescriptor.HasInputDirection)
            {
                var _direction      = DbQueryPropertyDirections.Input;
                var _parameterName  = propertyDescriptor.GetName(_direction);

                _parameters.Add(CreateParameter(propertyDescriptor, _parameterName, _direction));
            }

            return _parameters.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="propertyDescriptor"></param>
        protected void MapValueTypeData(IDbQueryParameterizable destination, object source, DbQueryPropertyDescriptor propertyDescriptor)
        {
            var _parameters = CreateParameters(propertyDescriptor);

            foreach (var _parameter in _parameters)
            {
                if (_parameter.Direction.Equals(ParameterDirection.Input)
                    || _parameter.Direction.Equals(ParameterDirection.InputOutput))
                {
                    _parameter.Value = propertyDescriptor.GetValue(source);

                    if (ObjectUtils.IsNullOrDefault(_parameter.Value))
                    {
                        if (!IsInChild)
                        {
                            if (!propertyDescriptor.IsNullable && null != propertyDescriptor.DefaultValue)
                            {
                                _parameter.Value = propertyDescriptor.DefaultValue;

                                destination.AddParameter(_parameter);
                            }
                        }

                        continue;
                    }
                }

                destination.AddParameter(_parameter);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="propertyDescriptor"></param>
        protected void MapReferenceTypeData(IDbQueryParameterizable destination, object source, DbQueryPropertyDescriptor propertyDescriptor)
        {
            var _sourceDescriptor   = OperatingSession.OperationContext.DescriptorManager.GetDescriptor(source);
            var _ignoreProperties   = propertyDescriptor.DeclaringDescriptor.IgnorePropertyDescriptors.GetDescriptors(propertyDescriptor);
            var _sourceProperties   = _sourceDescriptor.PropertyDescriptors.GetDescriptors(propertyDescriptor.QueryAction, _ignoreProperties);

            if (!_sourceProperties.IsEmpty)
            {
                var _parameterMapper = OperatingSession.CreateDbQueryParameterMapper(_sourceProperties, true);

                _parameterMapper.Map(destination, source);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="propertyDescriptor"></param>
        protected void MapXmlTypeData(IDbQueryParameterizable destination, object source, DbQueryPropertyDescriptor propertyDescriptor)
        {
            var _parameters     = CreateParameters(propertyDescriptor);
            var _propertyValue  = propertyDescriptor.GetValue(source);

            foreach (var _parameter in _parameters)
            {
                if (_parameter.Direction.Equals(ParameterDirection.Input) 
                    || _parameter.Direction.Equals(ParameterDirection.InputOutput))
                {
                    if (null != _propertyValue)
                    {
                        var _serializer = new DbQueryXmlSerializer(OperatingSession.OperationContext);
                        var _descriptor = OperatingSession.OperationContext.DescriptorManager.GetDescriptor(_propertyValue);
                        var _document   = _serializer.Serialize(_descriptor.PropertyDescriptors, _propertyValue);

                        if (_document.HasChildNodes)
                        {
                            _parameter.Value = _typeConverter.Convert(typeof(SqlXml), _document);

                            destination.AddParameter(_parameter);
                        }

                        continue;
                    }
                }

                destination.AddParameter(_parameter);
            }

            if (propertyDescriptor.IsNullable)
            {
                var _source = _propertyValue ?? ObjectUtils.CreateInstanceOf(propertyDescriptor.RetrunType);

                MapReferenceTypeData(destination, _source, propertyDescriptor);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="propertyDescriptors"></param>
        protected virtual void Map(IDbQueryParameterizable destination, object source, DbQueryPropertyDescriptors propertyDescriptors)
        {
            foreach (var _propertyDescriptor in propertyDescriptors)
            {
                if (!IsExceptionalType(_propertyDescriptor))
                {
                    MapValueTypeData(destination, source, _propertyDescriptor);
                }
                else
                {
                    if (_propertyDescriptor.DbType.Equals(SqlDbType.Xml))
                    {
                        MapXmlTypeData(destination, source, _propertyDescriptor);
                    }
                    else
                    {
                        var _source = _propertyDescriptor.GetValue(source)
                            ?? ObjectUtils.CreateInstanceOf(_propertyDescriptor.RetrunType);

                        if (_source is IValueSerializable)
                        {
                            var _parameters = CreateParameters(_propertyDescriptor);

                            foreach (var _parameter in _parameters)
                            {
                                if (_parameter.Direction.Equals(ParameterDirection.Input)
                                    || _parameter.Direction.Equals(ParameterDirection.InputOutput))
                                {
                                    var _serializer = (_source as IValueSerializable);
                                    var _serialized = _serializer.Serialize();

                                    if (!ObjectUtils.IsNullOrDefault(_serialized))
                                    {
                                        _parameter.Value = _serialized;

                                        destination.AddParameter(_parameter);
                                    }

                                    continue;
                                }
                                
                                destination.AddParameter(_parameter);
                            }
                        }
                        else
                        {
                            MapReferenceTypeData(destination, _source, _propertyDescriptor);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
