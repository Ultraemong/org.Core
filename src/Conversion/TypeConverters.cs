using System;
using System.Xml;
using System.Data;
using System.Data.SqlTypes;

using org.Core.Collections;

namespace org.Core.Conversion
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TypeConverters : NameValueCollection<Type, ITypeConverter> 
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        TypeConverters()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static TypeConverters()
        {
            s_converters                                                    = new TypeConverters()
            {
                {typeof(string),                                            (new StringConverter())},
                {typeof(bool),                                              (new BooleanConverter())},
                {typeof(byte),                                              (new ByteConverter())},
                {typeof(short),                                             (new Int16Converter())},
                {typeof(int),                                               (new Int32Converter())},
                {typeof(long),                                              (new Int64Converter())},
                {typeof(decimal),                                           (new DecimalConverter())},
                {typeof(double),                                            (new DoubleConverter())},
                {typeof(float),                                             (new FloatConverter())},

                {typeof(DateTime),                                          (new DateTimeConverter())},
                {typeof(TimeSpan),                                          (new TimeSpanConverter())},
                {typeof(DateTimeOffset),                                    (new DateTimeOffsetConverter())},
                {typeof(XmlDocument),                                       (new XmlDocumentConverter())},
                {typeof(Guid),                                              (new GuidConverter())},
                {typeof(Type),                                              (new ClrTypeConverter())},

                {typeof(SqlXml),                                            (new SqlXmlConverter())},
                {typeof(SqlDbType),                                         (new SqlDbTypeConverter())},

                {typeof(IReadOnlyNameValueCollection<string, object>),      (new NameValueCollectionConverter())},
            };
        }
        #endregion

        #region Fields
        static readonly TypeConverters s_converters = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static TypeConverters Converters
        {
            get
            {
                return s_converters;
            }
        }
        #endregion
    }
}
