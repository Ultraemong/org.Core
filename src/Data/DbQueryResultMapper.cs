using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Xml;

using org.Core.Conversion;
using org.Core.Utilities;
using org.Core.Collections;
using org.Core.Serialization;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbQueryResultMapper : DbQueryOperatingPrinciple, IDbQueryResultMapper
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatingSession"></param>
        /// <param name="memberDescriptors"></param>
        public DbQueryResultMapper(DbQueryOperatingSession operatingSession, DbQueryPropertyDescriptors memberDescriptors)
            : base(operatingSession)
        {
            _propertyDescriptors = memberDescriptors;
        }
        #endregion

        #region Fields
        readonly TypeConverter                _typeConverter        = new TypeConverter();
        readonly DbQueryPropertyDescriptors   _propertyDescriptors  = null;
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
        #endregion

        #region Methods
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
            else if (SqlDbType.Xml.Equals(propertyDescriptor.DbType) 
                && ObjectUtils.IsXmlType(propertyDescriptor.RetrunType))
            {
                return false;
            }

            return ObjectUtils.IsClassType(propertyDescriptor.RetrunType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        protected virtual void HandleReferenceTypeData(object destination, IDbQueryResult source)
        {
            if (null != destination && null != source && source.HasResult)
            {
                var _destiDescriptor    = OperatingSession.OperationContext.DescriptorManager.GetDescriptor(destination);
                var _resultMapper       = OperatingSession.CreateDbQueryResultMapper(_destiDescriptor.PropertyDescriptors);

                _resultMapper.Map(destination, source);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        protected virtual void MapListTypeData(IList destination, IDbQueryResult source)
        {
            if (null != destination && null != source && source.HasResult)
            {
                var _itemType = ObjectUtils.GetListItemTypeFrom(destination);

                if (null != _itemType)
                {
                    foreach (var _result in source)
                    {
                        var _item = ObjectUtils.CreateInstanceOf(_itemType);

                        if (null != _item)
                        {
                            HandleReferenceTypeData(_item, _result);

                            destination.Add(_item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="propertyDescriptor"></param>
        protected virtual void MapValueTypeData(object destination, IDbQueryResult source, DbQueryPropertyDescriptor propertyDescriptor)
        {
            var _propertyName   = propertyDescriptor.GetName(false);
            var _propertyValue  = _typeConverter.Convert(propertyDescriptor.RetrunType, source[propertyDescriptor.Prefix, _propertyName]);

            if (ObjectUtils.IsXmlType(_propertyValue) || !ObjectUtils.IsNullOrDefault(_propertyValue))
            {
                propertyDescriptor.SetValue(destination, _propertyValue);
            }
            else
            {
                if (!propertyDescriptor.IsNullable && !propertyDescriptor.IsReadOnly && null != propertyDescriptor.DefaultValue)
                {
                    _propertyValue = propertyDescriptor.GetValue(destination);

                    if (ObjectUtils.IsNullOrDefault(_propertyValue))
                    {
                        var _defaultValue = _typeConverter.Convert(propertyDescriptor.RetrunType, propertyDescriptor.DefaultValue);

                        propertyDescriptor.SetValue(destination, _defaultValue);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="propertyDescriptor"></param>
        protected void MapXmlTypeData(object destination, IDbQueryResult source, DbQueryPropertyDescriptor propertyDescriptor)
        {
            var _propertyName   = propertyDescriptor.GetName(false);
            var _propertyValue  = source[propertyDescriptor.Prefix, _propertyName];

            if (ObjectUtils.IsXmlType(_propertyValue))
            {
                var _destination = propertyDescriptor.GetValue(destination) 
                    ?? ObjectUtils.CreateInstanceOf(propertyDescriptor.RetrunType);

                var _serializer     = new DbQueryXmlSerializer(OperatingSession.OperationContext);
                var _descriptor     = OperatingSession.OperationContext.DescriptorManager.GetDescriptor(_destination);
                var _deserialized   = _serializer.Deserialize(_propertyValue as XmlDocument, _descriptor.PropertyDescriptors, _destination);

                propertyDescriptor.SetValue(destination, _deserialized);
            }
            else if (propertyDescriptor.IsNullable)
            {
                var _destination = propertyDescriptor.GetValue(destination)
                    ?? ObjectUtils.CreateInstanceOf(propertyDescriptor.RetrunType);

                HandleReferenceTypeData(_destination, source);

                propertyDescriptor.SetValue(destination, _destination);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="propertyDescriptors"></param>
        protected virtual void Map(object destination, IDbQueryResult source, DbQueryPropertyDescriptors propertyDescriptors)
        {
            foreach (var _propertyDescriptor in propertyDescriptors)
            {
                if (_propertyDescriptor.IsMappable && !_propertyDescriptor.IsReadOnly)
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
                            var _destination = _propertyDescriptor.GetValue(destination)
                                ?? ObjectUtils.CreateInstanceOf(_propertyDescriptor.RetrunType);

                            if (_destination is IValueSerializable)
                            {
                                var _originName     = _propertyDescriptor.GetName(false);
                                var _serializer     = (_destination as IValueSerializable);
                                var _deserialized   = _serializer.Deserialize(source[_propertyDescriptor.Prefix, _originName]);

                                if (!ObjectUtils.IsNullOrDefault(_deserialized))
                                    _propertyDescriptor.SetValue(destination, _deserialized);
                            }
                            else
                            {
                                HandleReferenceTypeData(_destination, source);

                                _propertyDescriptor.SetValue(destination, _destination);
                            }
                        }
                    }
                }
            }

            MapListTypeData(destination as IList, source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        public void Map(object destination, IDbQueryResult source)
        {
            Map(destination, source, PropertyDescriptors);
        }
        #endregion
    }
}
