
using System;
using System.Data;
using System.Data.SqlClient;

using org.Core.Collections;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class SchemaOnlyQueryExecutor : IDbQueryExecutor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        public SchemaOnlyQueryExecutor()
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
        /// <param name="command"></param>
        /// <returns></returns>
        public IDbQueryResult Execute(SqlCommand command)
        {
            var _dummy = new ListCollection<IReadOnlyNameValueCollection<string, object>>();

            using (var _reader = command.ExecuteReader(CommandBehavior.SchemaOnly))
            {
                var _schemaTable        = _reader.GetSchemaTable();

                var _columnName         = _schemaTable.Columns["ColumnName"];
                var _columnType         = _schemaTable.Columns["DataTypeName"];

                var _columnSize         = _schemaTable.Columns["ColumnSize"];
                var _columnScale        = _schemaTable.Columns["NumericScale"];
                var _columnPrecision    = _schemaTable.Columns["NumericPrecision"];

                foreach (DataRow _row in _schemaTable.Rows)
                {
                    var _dictionary = new NameValueCollection<string, object>(StringComparer.InvariantCultureIgnoreCase);

                    if (null != _columnName)
                        _dictionary.Add("$Column_Name", Convert.ToString(_row[_columnName]));

                    if (null != _columnSize)
                        _dictionary.Add("$Column_Size", Convert.ToInt32(_row[_columnSize]));

                    if (null != _columnType)
                        _dictionary.Add("$Column_Type", Convert.ToString(_row[_columnType]));

                    if (null != _columnScale)
                    {
                        var _scale = Convert.ToInt32(_row[_columnScale]);

                        _dictionary.Add("$Column_Scale", (255 != _scale) ? _scale : -1);
                    }

                    if (null != _columnPrecision)
                    {
                        var _precision = Convert.ToInt32(_row[_columnPrecision]);

                        _dictionary.Add("$Column_Precision", (255 != _precision) ? _precision : -1);
                    }

                    if (0 < _dictionary.Count)
                        _dummy.Add(_dictionary);
                }
            }

            return new DbQueryResult(_dummy);

        } 
        #endregion
    }
}
