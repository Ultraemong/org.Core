
using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

using org.Core.Conversion;
using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class SingleRowQueryExecutor : IDbQueryExecutor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        public SingleRowQueryExecutor()
        {
        }
        #endregion

        #region Fields
        readonly TypeConverter _typeConverter = new TypeConverter();
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IDbQueryResult Execute(SqlCommand command)
        {
            var _dummy = new ListCollection<IReadOnlyNameValueCollection<string, object>>();

            using (var _reader = command.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (_reader.HasRows)
                {
                    while (_reader.Read())
                    {
                        var _dictionary = new NameValueCollection<string, object>(StringComparer.InvariantCultureIgnoreCase);

                        for (int i = 0; i < _reader.FieldCount; i++)
                        {
                            var _columnName   = _reader.GetName(i);
                            var _columnType   = _typeConverter.Convert<SqlDbType>(_reader.GetDataTypeName(i));
                        
                            if (!IsExceptionalType(_columnType))
                            {
                                if (IsValid(_reader[_columnName]))
                                    _dictionary.Add(_columnName, _reader[_columnName]);
                            }
                            else
                            {
                                if (_columnType.Equals(SqlDbType.Xml))
                                {
                                    var _document = _typeConverter.Convert<XmlDocument>(_reader.GetSqlXml(i));

                                    if (null != _document)
                                        _dictionary.Add(_columnName, _document);
                                }
                            }
                        }

                        _dummy.Add(_dictionary);
                    }
                }
            }

            return new DbQueryResult(_dummy[0]);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsValid(object value)
        {
            return (null != value && DBNull.Value != value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns>
        bool IsExceptionalType(SqlDbType sqlDbType)
        {
            return (SqlDbType.Xml.Equals(sqlDbType)
                || SqlDbType.Binary.Equals(sqlDbType)
                || SqlDbType.Image.Equals(sqlDbType));
        }
        #endregion
    }
}
