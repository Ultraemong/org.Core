using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;

using org.Core.Utilities;
using org.Core.Collections;
using org.Core.Conversion;
using org.Core.Extensions;
using org.Core.Serialization;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbQueryXmlSerializer : DbQueryPrinciple
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public DbQueryXmlSerializer(DbQueryOperationContext operationContext)
            : base(operationContext)
        {   
        }
        #endregion

        #region Fields
        const string                ITEMWRAPPER_NODENAME    = "Items";

        readonly TypeConverter      _typeConverter          = new TypeConverter();
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <returns></returns>
        protected virtual bool IsSupportedType(DbQueryPropertyDescriptor propertyDescriptor)
        {
            return 
            (
                propertyDescriptor.RetrunType.Equals(typeof(bool))
                || propertyDescriptor.RetrunType.Equals(typeof(decimal))
                || propertyDescriptor.RetrunType.Equals(typeof(double))
                || propertyDescriptor.RetrunType.Equals(typeof(string))
                || propertyDescriptor.RetrunType.Equals(typeof(int))
                || propertyDescriptor.RetrunType.Equals(typeof(long))
                || propertyDescriptor.RetrunType.Equals(typeof(short))
                || propertyDescriptor.RetrunType.Equals(typeof(byte))

                || propertyDescriptor.RetrunType.Equals(typeof(DateTime))
                || propertyDescriptor.RetrunType.Equals(typeof(TimeSpan))

                || propertyDescriptor.RetrunType.Equals(typeof(sbyte))
                || propertyDescriptor.RetrunType.Equals(typeof(uint))
                || propertyDescriptor.RetrunType.Equals(typeof(ushort))
                || propertyDescriptor.RetrunType.Equals(typeof(ulong))

                || propertyDescriptor.RetrunType.Equals(typeof(byte[]))
                || propertyDescriptor.RetrunType.Equals(typeof(string[]))

                || propertyDescriptor.RetrunType.Equals(typeof(Uri))
                || propertyDescriptor.RetrunType.Equals(typeof(Single))                
            );
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

        #region Serialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWrite"></param>
        /// <param name="propertyDescriptors"></param>
        /// <param name="obj"></param>
        public void Serialize(XmlTextWriter writer, DbQueryPropertyDescriptors propertyDescriptors, object obj)
        {
            if (null != obj)
            {
                if (ObjectUtils.IsListType(obj))
                {
                    SerializeListTypeData(writer, propertyDescriptors, obj as IList);
                    return;
                }

                SerializeValueTypeData(writer, propertyDescriptors, obj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptors"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public XmlDocument Serialize(DbQueryPropertyDescriptors propertyDescriptors, object obj)
        {
            var _document = new XmlDocument();

            if (null != obj)
            {
                using (var _stringWriter = new StringWriter())
                using (var _xmlTextWriter = new XmlTextWriter(_stringWriter))
                {
                    _xmlTextWriter.WriteStartElement(propertyDescriptors.DeclaringDescriptor.Name);
                    
                    Serialize(_xmlTextWriter, propertyDescriptors, obj);

                    _xmlTextWriter.WriteEndElement();

                    _document.LoadXml(_stringWriter.ToString());

                    _xmlTextWriter.Flush();
                    _xmlTextWriter.Close();
                    _xmlTextWriter.Dispose();

                    _stringWriter.Flush();
                    _stringWriter.Close();
                    _stringWriter.Dispose();
                }
            }

            return _document;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public XmlDocument Serialize(object obj)
        {
            var _descriptor = OperationContext.DescriptorManager.GetDescriptor(obj);

            return Serialize(_descriptor.PropertyDescriptors, obj);
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="propertyDescriptors"></param>
        /// <param name="obj"></param>
        protected void SerializeValueTypeData(XmlTextWriter writer, DbQueryPropertyDescriptors propertyDescriptors, object obj)
        {
            foreach (var _propertyDescriptor in propertyDescriptors)
            {
                if (!_propertyDescriptor.IgnoreXmlDataMember)
                {
                    if (IsExceptionalType(_propertyDescriptor))
                    {
                        var _propertyValue = _propertyDescriptor.GetValue(obj);

                        if (null != _propertyValue)
                        {
                            if (_propertyValue is IValueSerializable)
                            {
                                var _serializer = (_propertyValue as IValueSerializable);
                                var _serialized = _serializer.Serialize();

                                if (!ObjectUtils.IsNullOrDefault(_serialized))
                                {
                                    writer.WriteStartElement(_propertyDescriptor.GetName(true));
                                    writer.WriteValue(_serialized);
                                    writer.WriteEndElement();
                                }
                            }
                            else
                            {
                                var _valueDescriptor = OperationContext.DescriptorManager.GetDescriptor(_propertyValue);

                                writer.WriteStartElement(_propertyDescriptor.GetName(true));

                                Serialize(writer, _valueDescriptor.PropertyDescriptors, _propertyValue);

                                writer.WriteEndElement();
                            }
                        }
                    }
                    else
                    {
                        var _propertyName   = _propertyDescriptor.GetName(true);
                        var _propertyValue  = _propertyDescriptor.GetValue(obj);

                        if (!ObjectUtils.IsNullOrDefault(_propertyValue))
                        {
                            if (!IsSupportedType(_propertyDescriptor))
                                _propertyValue = _propertyValue.ToString();

                            writer.WriteStartElement(_propertyName);
                            writer.WriteValue(_propertyValue);
                            writer.WriteEndElement();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="propertyDescriptors"></param>
        /// <param name="obj"></param>
        protected void SerializeListTypeData(XmlTextWriter writer, DbQueryPropertyDescriptors propertyDescriptors, IList obj)
        {
            if (null != obj)
            {
                SerializeValueTypeData(writer, propertyDescriptors, obj);

                writer.WriteStartElement(ITEMWRAPPER_NODENAME);

                foreach (var _item in obj)
                {
                    var _itemDescriptor = OperationContext.DescriptorManager.GetDescriptor(_item);

                    writer.WriteStartElement(_itemDescriptor.Name);
                    
                    Serialize(writer, _itemDescriptor.PropertyDescriptors, _item);

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }
        #endregion

        #region Deserialization Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="propertyDescriptors"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public void Deserialize(XmlNodeReader reader, DbQueryPropertyDescriptors propertyDescriptors, object destination)
        {
            while (reader.Read())
            {
                if (CanDeserialize(reader))
                {
                    if (!reader.Name.Equals(ITEMWRAPPER_NODENAME, StringComparison.CurrentCultureIgnoreCase))
                    {
                        DeserializeValueTypeData(reader, propertyDescriptors, destination);
                        continue;
                    }
                    
                    DeserializeListItemTypeData(reader, destination as IList);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyDescriptors"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public object Deserialize(XmlDocument source, DbQueryPropertyDescriptors propertyDescriptors, object destination)
        {
            if (null != source && null != destination)
            {
                using (var _reader = new XmlNodeReader(source.FirstChild))
                {
                    //Fisrt node should be skipped.
                    _reader.Skip();

                    Deserialize(_reader, propertyDescriptors, destination);
                    
                    _reader.Close();
                    _reader.Dispose();
                }

                return destination;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeReader"></param>
        /// <returns></returns>
        protected virtual bool CanDeserialize(XmlNodeReader nodeReader)
        {
            return (null != nodeReader && nodeReader.IsStartElement() && !nodeReader.IsEmptyElement);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDescriptor"></param>
        /// <returns></returns>
        protected virtual bool CanDeserialize(DbQueryPropertyDescriptor propertyDescriptor)
        {
            return (null != propertyDescriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="propertyDescriptor"></param>
        /// <param name="destination"></param>
        protected void DeserializeValueTypeData(XmlNodeReader reader, DbQueryPropertyDescriptor propertyDescriptor, object destination)
        {
            if (CanDeserialize(reader) && CanDeserialize(propertyDescriptor))
            {
                reader.Read();

                if (reader.NodeType.Equals(XmlNodeType.Text) && reader.HasValue)
                {
                    var _value = _typeConverter.Convert(propertyDescriptor.RetrunType, reader.Value);

                    if (!ObjectUtils.IsNullOrDefault(_value))
                    {
                        propertyDescriptor.SetValue(destination, _value);
                    }
                }

                reader.Read();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="propertyDescriptors"></param>
        /// <param name="destination"></param>
        protected void DeserializeValueTypeData(XmlNodeReader reader, DbQueryPropertyDescriptors propertyDescriptors, object destination)
        {
            var _propertyDescriptor = propertyDescriptors.GetDescriptorByPropertyName(reader.Name);

            if (CanDeserialize(reader) && CanDeserialize(_propertyDescriptor))
            {
                if (IsExceptionalType(_propertyDescriptor))
                {
                    if (typeof(IValueSerializable).IsAssignableFrom(_propertyDescriptor.RetrunType))
                    {
                        reader.Read();

                        if (reader.NodeType.Equals(XmlNodeType.Text) && reader.HasValue)
                        {
                            var _destination    = _propertyDescriptor.GetValue(destination) ?? ObjectUtils.CreateInstanceOf(_propertyDescriptor.RetrunType);
                            var _serializer     = (_destination as IValueSerializable);
                            var _deserialized   = _serializer.Deserialize(reader.Value);

                            if (!ObjectUtils.IsNullOrDefault(_deserialized))
                            {
                                _propertyDescriptor.SetValue(destination, _deserialized);
                            }
                        }

                        reader.Read();
                    }
                    else
                    {
                        DeserializeReferenceTypeData(reader, _propertyDescriptor, destination);
                    }

                    return;
                }

                DeserializeValueTypeData(reader, _propertyDescriptor, destination);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="propertyDescriptor"></param>
        /// <param name="destination"></param>
        protected void DeserializeReferenceTypeData(XmlNodeReader reader, DbQueryPropertyDescriptor propertyDescriptor, object destination)
        {
            if (CanDeserialize(reader) && CanDeserialize(propertyDescriptor))
            { 
                if (ObjectUtils.IsListType(propertyDescriptor.RetrunType))
                {
                    var _destination        = (propertyDescriptor.GetValue(destination) as IList) ?? ObjectUtils.CreateInstanceOf<IList>(propertyDescriptor.RetrunType);
                    var _destiDescriptor    = OperationContext.DescriptorManager.GetDescriptor(_destination);

                    Deserialize(reader, _destiDescriptor.PropertyDescriptors, _destination);

                    propertyDescriptor.SetValue(destination, _destination);
                }
                else
                {
                    var _parentName         = reader.Name;
                    var _destination        = propertyDescriptor.GetValue(destination) ?? ObjectUtils.CreateInstanceOf(propertyDescriptor.RetrunType);
                    var _destiDescriptor    = OperationContext.DescriptorManager.GetDescriptor(_destination);

                    while (reader.Read())
                    {
                        if (!(reader.NodeType.Equals(XmlNodeType.EndElement) 
                            && _parentName.Equals(reader.Name, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            DeserializeValueTypeData(reader, _destiDescriptor.PropertyDescriptors, _destination);
                            continue;
                        }

                        propertyDescriptor.SetValue(destination, _destination);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="memberDescriptor"></param>
        /// <param name="destination"></param>
        protected void DeserializeListItemTypeData(XmlNodeReader reader, IList destination)
        {
            if (CanDeserialize(reader) && null != destination)
            {
                var _wrapperName    = reader.Name;
                var _parentName     = (string)null;
                
                var _itemType       = ObjectUtils.GetListItemTypeFrom(destination);
                var _listItem       = (object)null;

                var _descriptor     = (IEntryDescriptor)null;

                while(reader.Read())
                {
                    if (!(reader.NodeType.Equals(XmlNodeType.EndElement) 
                        && _wrapperName.Equals(reader.Name, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        if (!reader.IsEmptyElement)
                        {
                            if (reader.NodeType.Equals(XmlNodeType.Element)
                                && string.IsNullOrEmpty(_parentName))
                            {
                                _listItem       = ObjectUtils.CreateInstanceOf(_itemType);
                                _descriptor     = OperationContext.DescriptorManager.GetDescriptor(_listItem);

                                _parentName     = reader.Name;
                                continue;
                            }
                            else if (reader.NodeType.Equals(XmlNodeType.EndElement) 
                                && _parentName.Equals(reader.Name, StringComparison.CurrentCultureIgnoreCase))
                            {
                                destination.Add(_listItem);

                                _listItem       = null;
                                _descriptor     = null;

                                _parentName     = null;
                                continue;
                            }

                            DeserializeValueTypeData(reader, _descriptor.PropertyDescriptors, _listItem);
                            continue;
                        }
                    }

                    break;
                }
            }
        }
        #endregion
        #endregion
    }
}
