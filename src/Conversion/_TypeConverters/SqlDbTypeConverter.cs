using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;

using org.Core.Collections;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlDbTypeConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public SqlDbTypeConverter()
        {
            _typeMapEntry = new NameValueCollection<string, SqlDbType>()
            {
                {"bigint",                      SqlDbType.BigInt},              
                {"binary",                      SqlDbType.Binary},              
                {"bit",                         SqlDbType.Bit},                 
                {"char",                        SqlDbType.Char},                
                {"date",                        SqlDbType.Date},                
                {"datetime",                    SqlDbType.DateTime},            
                {"datetime2",                   SqlDbType.DateTime2},           
                {"datetimeoffset",              SqlDbType.DateTimeOffset},   
                {"decimal",                     SqlDbType.Decimal},             
                {"float",                       SqlDbType.Float},               
                {"image",                       SqlDbType.Image},               
                {"int",                         SqlDbType.Int},                 
                {"money",                       SqlDbType.Money},               
                {"nchar",                       SqlDbType.NChar},               
                {"ntext",                       SqlDbType.NText},               
                {"nvarchar",                    SqlDbType.NVarChar},            
                {"real",                        SqlDbType.Real},                
                {"smalldatetime",               SqlDbType.SmallDateTime},       
                {"smallint",                    SqlDbType.SmallInt},            
                {"smallmoney",                  SqlDbType.SmallMoney},          
                {"text",                        SqlDbType.Text},                
                {"time",                        SqlDbType.Time},                
                {"timestamp",                   SqlDbType.Timestamp},           
                {"tinyint",                     SqlDbType.TinyInt},             
                {"uniqueidentifier",            SqlDbType.UniqueIdentifier},    
                {"varbinary",                   SqlDbType.VarBinary},           
                {"varchar",                     SqlDbType.VarChar},             
                {"xml",                         SqlDbType.Xml},                 
            };
        }
        #endregion

        #region Fields
        readonly IReadOnlyNameValueCollection<string, SqlDbType> _typeMapEntry = default(IReadOnlyNameValueCollection<string, SqlDbType>);
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Convert(object value)
        {
            if (null != value)
            {
                if (value is string)
                {
                    var _unboxed = value.ToString();

                    return _typeMapEntry.Where(x => x.Key.Equals(_unboxed.ToLower())).Select(x => x.Value).Single();
                }
            }

            return null;
        } 
        #endregion
    }
}
