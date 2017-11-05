using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using org.Core.Collections;
using org.Core.Extensions;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DbQueryResult : IDbQueryResult, IConvertible
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scalarValue"></param>
        public DbQueryResult(object scalarValue)
        {
            if (null != scalarValue)
            {
                _scalarValue = scalarValue;
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public DbQueryResult(IReadOnlyNameValueCollection<string, object> dictionary)
        {
            if (default(IReadOnlyNameValueCollection<string, object>) != dictionary && !dictionary.IsEmpty)
            {
                _dictionary = dictionary;
            }
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public DbQueryResult(IReadOnlyListCollection<IReadOnlyNameValueCollection<string, object>> collection)
        {
            if (default(IReadOnlyListCollection<IReadOnlyNameValueCollection<string, object>>) != collection && !collection.IsEmpty)
            {
                _collection = new ListCollection<IDbQueryResult>(collection.Select(x => new DbQueryResult(x)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="collection"></param>
        public DbQueryResult(IReadOnlyNameValueCollection<string, object> dictionary, IReadOnlyListCollection<IReadOnlyNameValueCollection<string, object>> collection)
            : this(dictionary)
        {
            if (default(IReadOnlyListCollection<IReadOnlyNameValueCollection<string, object>>) != collection && !collection.IsEmpty)
            {
                _collection = new ListCollection<IDbQueryResult>(collection.Select(x => new DbQueryResult(x)));
            }
        }
        #endregion

        #region Fields
        readonly object                                             _scalarValue    = null;
        
        readonly IReadOnlyNameValueCollection<string, object>       _dictionary     = default(IReadOnlyNameValueCollection<string, object>);
        readonly IReadOnlyListCollection<IDbQueryResult>            _collection     = default(IReadOnlyListCollection<IDbQueryResult>);
        #endregion

        #region Indexers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IDbQueryResult this[int index]
        {
            get
            {
                if (default(IReadOnlyListCollection<IDbQueryResult>) != _collection)
                {
                    return _collection[index];
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                if (default(IReadOnlyNameValueCollection<string, object>) != _dictionary)
                {
                    if (null != name && _dictionary.ContainsKey(name))
                    {
                        return _dictionary[name];
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public object this[string prefix, string name]
        {
            get
            {
                var _value = this[GetName(prefix, name)];

                if (null == _value)
                    _value = this[name];

                return _value;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool HasResult
        {
            get
            {
                if (null != _scalarValue
                    || (default(IReadOnlyNameValueCollection<string, object>) != _dictionary && !_dictionary.IsEmpty)
                    || (default(IReadOnlyListCollection<IDbQueryResult>) != _collection && !_collection.IsEmpty))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ErrorCode
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorText
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                if (default(IReadOnlyListCollection<IDbQueryResult>) != _collection)
                {
                    return _collection.Count;
                }
                else if (default(IReadOnlyNameValueCollection<string, object>) != _dictionary)
                {
                    return _dictionary.Count;
                }

                return 0;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetName(string prefix, string name)
        {
            if (!string.IsNullOrEmpty(prefix) && !string.IsNullOrEmpty(name))
            {
                return string.Format(DbQueryPropertyDescriptor.PARAMETERNAME_FORMAT, prefix, name);
            }
            else if (string.IsNullOrEmpty(prefix) && !string.IsNullOrEmpty(name))
            {
                return name;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlDocument GetXml(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public XmlDocument GetXml(string prefix, string name)
        {
            return GetXml(GetName(prefix, name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public byte[] GetBinary(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public byte[] GetBinary(string prefix, string name)
        {
            return GetBinary(GetName(prefix, name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public bool GetValueOrDefault(string name, bool defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToBoolean(_value.ToString(), defVal);
            }
                
            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public bool? GetValueOrDefault(string name, bool? defVal)
        {
            var _value  = this[name];
            
            if (null != _value)
            {
                var _retVal = StringExtensions.ToBoolean(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }
            
            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public bool GetValueOrDefault(string prefix, string name, bool defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public bool? GetValueOrDefault(string prefix, string name, bool? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public DateTime GetValueOrDefault(string name, DateTime defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToDateTime(_value.ToString(), defVal);
            }
            
            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public DateTime? GetValueOrDefault(string name, DateTime? defVal)
        {
            var _value  = this[name];
            
            if (null != _value)
            {
                var _retVal = StringExtensions.ToDateTime(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public DateTime GetValueOrDefault(string prefix, string name, DateTime defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public DateTime? GetValueOrDefault(string prefix, string name, DateTime? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public DateTimeOffset GetValueOrDefault(string name, DateTimeOffset defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToDateTimeOffset(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public DateTimeOffset? GetValueOrDefault(string name, DateTimeOffset? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToDateTimeOffset(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public DateTimeOffset GetValueOrDefault(string prefix, string name, DateTimeOffset defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public DateTimeOffset? GetValueOrDefault(string prefix, string name, DateTimeOffset? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public string GetValueOrDefault(string name, string defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return _value.ToString();
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public string GetValueOrDefault(string prefix, string name, string defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string name, int defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt32(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public int? GetValueOrDefault(string name, int? defVal)
        {
            var _value = this[name];

            if(null != _value)
            {
                var _retVal = StringExtensions.ToInt32(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string name, int defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt32(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string name, int defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt32(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string name, int defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt32(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string prefix, string name, int defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public int? GetValueOrDefault(string prefix, string name, int? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string prefix, string name, int defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string prefix, string name, int defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public int GetValueOrDefault(string prefix, string name, int defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public uint GetValueOrDefault(string name, uint defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt32(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public uint? GetValueOrDefault(string name, uint? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToUInt32(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public uint GetValueOrDefault(string name, uint defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt32(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public uint GetValueOrDefault(string name, uint defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt32(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public uint GetValueOrDefault(string name, uint defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt32(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public uint GetValueOrDefault(string prefix, string name, uint defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public uint? GetValueOrDefault(string prefix, string name, uint? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public uint GetValueOrDefault(string prefix, string name, uint defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public uint GetValueOrDefault(string prefix, string name, uint defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public uint GetValueOrDefault(string prefix, string name, uint defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public byte GetValueOrDefault(string name, byte defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToByte(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public byte? GetValueOrDefault(string name, byte? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToByte(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public byte GetValueOrDefault(string name, byte defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToByte(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public byte GetValueOrDefault(string name, byte defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToByte(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public byte GetValueOrDefault(string name, byte defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if(null != _value)
            {
                return StringExtensions.ToByte(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public byte GetValueOrDefault(string prefix, string name, byte defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public byte? GetValueOrDefault(string prefix, string name, byte? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public byte GetValueOrDefault(string prefix, string name, byte defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public byte GetValueOrDefault(string prefix, string name, byte defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public byte GetValueOrDefault(string prefix, string name, byte defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public sbyte GetValueOrDefault(string name, sbyte defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToSByte(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public sbyte? GetValueOrDefault(string name, sbyte? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToSByte(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public sbyte GetValueOrDefault(string name, sbyte defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToSByte(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public sbyte GetValueOrDefault(string name, sbyte defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToSByte(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public sbyte GetValueOrDefault(string name, sbyte defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToSByte(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public sbyte GetValueOrDefault(string prefix, string name, sbyte defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public sbyte? GetValueOrDefault(string prefix, string name, sbyte? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public sbyte GetValueOrDefault(string prefix, string name, sbyte defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public sbyte GetValueOrDefault(string prefix, string name, sbyte defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public sbyte GetValueOrDefault(string prefix, string name, sbyte defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public decimal GetValueOrDefault(string name, decimal defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToDecimal(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public decimal? GetValueOrDefault(string name, decimal? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToDecimal(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public decimal GetValueOrDefault(string name, decimal defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToDecimal(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public decimal GetValueOrDefault(string name, decimal defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToDecimal(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public decimal GetValueOrDefault(string name, decimal defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToDecimal(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public decimal GetValueOrDefault(string prefix, string name, decimal defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public decimal? GetValueOrDefault(string prefix, string name, decimal? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public decimal GetValueOrDefault(string prefix, string name, decimal defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public decimal GetValueOrDefault(string prefix, string name, decimal defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public decimal GetValueOrDefault(string prefix, string name, decimal defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public double GetValueOrDefault(string name, double defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToDouble(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public double? GetValueOrDefault(string name, double? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToDouble(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public double GetValueOrDefault(string name, double defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToDouble(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public double GetValueOrDefault(string name, double defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToDouble(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public double GetValueOrDefault(string name, double defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToDouble(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public double GetValueOrDefault(string prefix, string name, double defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public double? GetValueOrDefault(string prefix, string name, double? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public double GetValueOrDefault(string prefix, string name, double defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public double GetValueOrDefault(string prefix, string name, double defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public double GetValueOrDefault(string prefix, string name, double defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public short GetValueOrDefault(string name, short defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt16(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public short? GetValueOrDefault(string name, short? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToInt16(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public short GetValueOrDefault(string name, short defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt16(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public short GetValueOrDefault(string name, short defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt16(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public short GetValueOrDefault(string name, short defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt16(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public short GetValueOrDefault(string prefix, string name, short defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public short? GetValueOrDefault(string prefix, string name, short? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public short GetValueOrDefault(string prefix, string name, short defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public short GetValueOrDefault(string prefix, string name, short defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public short GetValueOrDefault(string prefix, string name, short defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public ushort GetValueOrDefault(string name, ushort defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt16(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public ushort? GetValueOrDefault(string name, ushort? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToUInt16(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ushort GetValueOrDefault(string name, ushort defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt16(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public ushort GetValueOrDefault(string name, ushort defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt16(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ushort GetValueOrDefault(string name, ushort defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt16(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public ushort GetValueOrDefault(string prefix, string name, ushort defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public ushort? GetValueOrDefault(string prefix, string name, ushort? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ushort GetValueOrDefault(string prefix, string name, ushort defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public ushort GetValueOrDefault(string prefix, string name, ushort defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ushort GetValueOrDefault(string prefix, string name, ushort defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public long GetValueOrDefault(string name, long defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt64(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public long? GetValueOrDefault(string name, long? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToInt64(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public long GetValueOrDefault(string name, long defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt64(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public long GetValueOrDefault(string name, long defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt64(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public long GetValueOrDefault(string name, long defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToInt64(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public long GetValueOrDefault(string prefix, string name, long defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public long? GetValueOrDefault(string prefix, string name, long? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public long GetValueOrDefault(string prefix, string name, long defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public long GetValueOrDefault(string prefix, string name, long defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public long GetValueOrDefault(string prefix, string name, long defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public ulong GetValueOrDefault(string name, ulong defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt64(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public ulong? GetValueOrDefault(string name, ulong? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToUInt64(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ulong GetValueOrDefault(string name, ulong defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt64(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public ulong GetValueOrDefault(string name, ulong defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt64(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ulong GetValueOrDefault(string name, ulong defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToUInt64(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public ulong GetValueOrDefault(string prefix, string name, ulong defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public ulong? GetValueOrDefault(string prefix, string name, ulong? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ulong GetValueOrDefault(string prefix, string name, ulong defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public ulong GetValueOrDefault(string prefix, string name, ulong defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public ulong GetValueOrDefault(string prefix, string name, ulong defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public float GetValueOrDefault(string name, float defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToSingle(_value.ToString(), defVal);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public float? GetValueOrDefault(string name, float? defVal)
        {
            var _value = this[name];

            if (null != _value)
            {
                var _retVal = StringExtensions.ToSingle(_value.ToString());

                return _retVal.HasValue ? _retVal : defVal;
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public float GetValueOrDefault(string name, float defVal, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToSingle(_value.ToString(), defVal, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public float GetValueOrDefault(string name, float defVal, NumberStyles style)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToSingle(_value.ToString(), defVal, style);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public float GetValueOrDefault(string name, float defVal, NumberStyles style, IFormatProvider provider)
        {
            var _value = this[name];

            if (null != _value)
            {
                return StringExtensions.ToSingle(_value.ToString(), defVal, style, provider);
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public float GetValueOrDefault(string prefix, string name, float defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public float? GetValueOrDefault(string prefix, string name, float? defVal)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public float GetValueOrDefault(string prefix, string name, float defVal, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public float GetValueOrDefault(string prefix, string name, float defVal, NumberStyles style)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public float GetValueOrDefault(string prefix, string name, float defVal, NumberStyles style, IFormatProvider provider)
        {
            return GetValueOrDefault(GetName(prefix, name), defVal, style, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            if (default(IReadOnlyListCollection<IDbQueryResult>) != _collection)
            {
                return (_collection as IEnumerable).GetEnumerator();
            }
            else if (default(IReadOnlyNameValueCollection<string, object>) != _dictionary)
            {
                return (_dictionary as IEnumerable).GetEnumerator();
            }

            return new NullEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IDbQueryResult> GetEnumerator()
        {
            if (default(IReadOnlyListCollection<IDbQueryResult>) != _collection)
            {
                return _collection.GetEnumerator();
            }

            return new NullEnumerator<IDbQueryResult>();
        }

        #region IConvertiable Members
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(_scalarValue, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return Convert.ToString(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionType"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return (null == provider) ? Convert.ChangeType(_scalarValue, conversionType) : Convert.ChangeType(_scalarValue, conversionType, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_scalarValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_scalarValue);
        } 
        #endregion
        #endregion
    }
}
