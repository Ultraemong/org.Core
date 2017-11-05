using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using org.Core.Utilities;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlXmlConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public SqlXmlConverter()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        SqlXml XmlDocumentToSqlXml(XmlDocument document)
        {
            var _sqlXml = (SqlXml)null;

            if (document.HasChildNodes)
            {
                using (var _reader = new XmlNodeReader(document))
                {
                    if (!_reader.IsEmptyElement)
                    {
                        _sqlXml = new SqlXml(_reader);
                    }

                    _reader.Close();
                    _reader.Dispose();
                }
            }

            return _sqlXml;
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
                if (ObjectUtils.IsXmlType(value))
                    return XmlDocumentToSqlXml(value as XmlDocument);
            }

            return (SqlXml)null;
        } 
        #endregion
    }
}
