using System;
using System.Xml;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;

using org.Core.Collections;
using org.Core.Conversion;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class AdoDotNetDbParameterRetriever
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public AdoDotNetDbParameterRetriever()
        {
        }
        #endregion

        #region Fields
        readonly TypeConverter _typeConverter = new TypeConverter();
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterCollection"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public IReadOnlyNameValueCollection<string, object> Retrieve(SqlParameterCollection parameterCollection, Expression<Func<SqlParameter, bool>> selector)
        {
            var _dummy          = new NameValueCollection<string, object>(StringComparer.InvariantCultureIgnoreCase);
            var _selector       = selector.Compile();

            foreach (SqlParameter _parameter in parameterCollection)
            {
                var _name = _parameter.ParameterName.TrimStart(AdoDotNetDbParameterHelpers.PARAMETERNAME_PREFIX);
                var _type = _parameter.SqlDbType;

                if (IsValid(_parameter))
                {
                    if (_selector(_parameter))
                    {
                        if (_type.Equals(SqlDbType.Xml))
                        {
                            _dummy.Add(_name, _typeConverter.Convert<XmlDocument>(_parameter.Value));
                            continue;
                        }

                        _dummy.Add(_name, _parameter.Value);
                    }
                }
            }

            return _dummy;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool IsValid(SqlParameter parameter)
        {
            return (null != parameter && DBNull.Value != parameter.Value);
        }
        #endregion
    }
}
