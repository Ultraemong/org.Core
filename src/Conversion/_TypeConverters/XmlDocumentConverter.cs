using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data.SqlTypes;

using org.Core.Conversion;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    internal class XmlDocumentConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public XmlDocumentConverter()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        XmlDocument StringToXmlDocument(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                var _document = new XmlDocument();

                _document.LoadXml(source);

                return _document;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        XmlDocument SqlXmlToXmlDocument(SqlXml source)
        {
            var _document = (XmlDocument)null;

            if (null != source && !source.IsNull)
            {
                using (var _reader = source.CreateReader())
                {
                    if (_reader.Read())
                    {
                        _document = new XmlDocument();

                        _document.Load(_reader);
                    }

                    _reader.Close();
                    _reader.Dispose();
                }
            }

            return _document;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Convert(object value)
        {
            if (null != value)
            {
                var _unboxed = (value as XmlDocument);

                if (null == _unboxed)
                {
                    if (value is string)
                        return StringToXmlDocument(value.ToString());

                    else if (value is SqlXml)
                        return SqlXmlToXmlDocument(value as SqlXml);
                }
                
                return _unboxed;
            }

            return null;
        }
        #endregion
    }
}