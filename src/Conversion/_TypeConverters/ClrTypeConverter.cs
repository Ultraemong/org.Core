using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Collections;
using System.Data;
using System.Xml;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    public class ClrTypeConverter : ITypeConverter
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public ClrTypeConverter()
        {
            _typeMapEntry = new NameValueCollection<SqlDbType, Type>()
            {
                {SqlDbType.BigInt,              typeof(long)},
                {SqlDbType.Binary,              typeof(byte[])},
                {SqlDbType.Bit,                 typeof(bool)},
                {SqlDbType.Char,                typeof(string)},
                {SqlDbType.Date,                typeof(DateTime)},
                {SqlDbType.DateTime,            typeof(DateTime)},
                {SqlDbType.DateTime2,           typeof(DateTime)},
                {SqlDbType.DateTimeOffset,      typeof(DateTimeOffset)},
                {SqlDbType.Decimal,             typeof(decimal)},
                {SqlDbType.Float,               typeof(double)},
                {SqlDbType.Image,               typeof(byte[])},
                {SqlDbType.Int,                 typeof(int)},
                {SqlDbType.Money,               typeof(decimal)},
                {SqlDbType.NChar,               typeof(string)},
                {SqlDbType.NText,               typeof(string)},
                {SqlDbType.NVarChar,            typeof(string)},
                {SqlDbType.Real,                typeof(float)},
                {SqlDbType.SmallDateTime,       typeof(DateTime)},
                {SqlDbType.SmallInt,            typeof(short)},
                {SqlDbType.SmallMoney,          typeof(decimal)},
                {SqlDbType.Text,                typeof(string)},
                {SqlDbType.Time,                typeof(TimeSpan)},
                {SqlDbType.Timestamp,           typeof(byte[])},
                {SqlDbType.TinyInt,             typeof(byte)},
                {SqlDbType.UniqueIdentifier,    typeof(Guid)},
                {SqlDbType.VarBinary,           typeof(byte[])},
                {SqlDbType.VarChar,             typeof(string)},
                {SqlDbType.Xml,                 typeof(XmlDocument)},
            };
        }
        #endregion

        #region Methods
        readonly IReadOnlyNameValueCollection<SqlDbType, Type> _typeMapEntry = default(IReadOnlyNameValueCollection<SqlDbType, Type>);
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
                if (value is SqlDbType)
                {
                    var _unboxed = (SqlDbType)value;

                    return _typeMapEntry.Where(x => x.Key.Equals(_unboxed)).Select(x => x.Value).Single();
                }
            }

            return null;
        } 
        #endregion
    }
}
