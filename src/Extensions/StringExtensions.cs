using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;

namespace org.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtensions
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static StringExtensions()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string str, string[] values)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim();

                foreach (var _value in values)
                {
                    if (-1 < str.IndexOf(_value, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string Except(this string str, string word, bool ignoreCase = true)
        {
            var _startIndex = (ignoreCase) ? str.ToLower().IndexOf(word.ToLower()) : str.IndexOf(word);
            
            if (-1 < _startIndex)
                return str.Remove(_startIndex, word.Length);

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <param name="ignoreKeywords"></param>
        /// <returns></returns>
        public static string[] Split(this string str, char separator, string[] ignoreKeywords)
        {
            return StringExtensions.Split(str, separator, StringSplitOptions.None, ignoreKeywords);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <param name="options"></param>
        /// <param name="ignoreKeywords"></param>
        /// <returns></returns>
        public static string[] Split(this string str, char separator, StringSplitOptions options, string[] ignoreKeywords)
        {
            var _split = str.Split(new char[] { separator }, options);

            if (0 < _split.Length)
            {
                return _split.Where(x => !StringExtensions.ContainsAny(x, ignoreKeywords)).ToArray();
            }

            return _split;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static byte? ToByte(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                byte _retVal;

                if (byte.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
                    
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static sbyte? ToSByte(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                sbyte _retVal;

                if (sbyte.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static short? ToInt16(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                short _retVal;

                if (short.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static ushort? ToUInt16(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                ushort _retVal;

                if (ushort.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static int? ToInt32(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                int _retVal;

                if (int.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static uint? ToUInt32(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                uint _retVal;

                if (uint.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static long? ToInt64(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                long _retVal;

                if (long.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static ulong? ToUInt64(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                ulong _retVal;

                if (ulong.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static decimal? ToDecimal(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                decimal _retVal;

                if (decimal.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static float? ToSingle(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                float _retVal;

                if (float.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static double? ToDouble(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                double _retVal;

                if (double.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static bool? ToBoolean(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                bool _retVal;

                if (bool.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime _retVal;

                if (DateTime.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static DateTimeOffset? ToDateTimeOffset(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTimeOffset _retVal;

                if (DateTimeOffset.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static char? ToChar(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                char _retVal;

                if (char.TryParse(str, out _retVal))
                {
                    return _retVal;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static byte ToByte(this string str, byte defVal)
        {
            return StringExtensions.ToByte(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static sbyte ToSByte(this string str, sbyte defVal)
        {
            return StringExtensions.ToSByte(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static short ToInt16(this string str, short defVal)
        {
            return StringExtensions.ToInt16(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static ushort ToUInt16(this string str, ushort defVal)
        {
            return StringExtensions.ToUInt16(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static int ToInt32(this string str, int defVal)
        {
            return StringExtensions.ToInt32(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static uint ToUInt32(this string str, uint defVal)
        {
            return StringExtensions.ToUInt32(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static long ToInt64(this string str, long defVal)
        {
            return StringExtensions.ToInt64(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this string str, ulong defVal)
        {
            return StringExtensions.ToUInt64(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defVal)
        {
            return StringExtensions.ToDecimal(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static float ToSingle(this string str, float defVal)
        {
            return StringExtensions.ToSingle(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defVal)
        {
            return StringExtensions.ToDouble(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string str, bool defVal)
        {
            return StringExtensions.ToBoolean(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, DateTime defVal)
        {
            return StringExtensions.ToDateTime(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this string str, DateTimeOffset defVal)
        {
            return StringExtensions.ToDateTimeOffset(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static char ToChar(this string str, char defVal)
        {
            return StringExtensions.ToChar(str).GetValueOrDefault(defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            return StringExtensions.ToBytes(str, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static byte ToByte(this string str, byte defVal, IFormatProvider provider)
        {
            return StringExtensions.ToByte(str, defVal, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static byte ToByte(this string str, byte defVal, NumberStyles style)
        {
            return StringExtensions.ToByte(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static byte ToByte(this string str, byte defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    byte _retVal;

                    if (byte.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static sbyte ToSByte(this string str, sbyte defVal, IFormatProvider provider)
        {
            return StringExtensions.ToSByte(str, defVal, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static sbyte ToSByte(this string str, sbyte defVal, NumberStyles style)
        {
            return StringExtensions.ToSByte(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static sbyte ToSByte(this string str, sbyte defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    sbyte _retVal;

                    if (sbyte.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static short ToInt16(this string str, short defVal, IFormatProvider provider)
        {
            return StringExtensions.ToInt16(str, defVal, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static short ToInt16(this string str, short defVal, NumberStyles style)
        {
            return StringExtensions.ToInt16(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static short ToInt16(this string str, short defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    short _retVal;

                    if (short.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ushort ToUInt16(this string str, ushort defVal, IFormatProvider provider)
        {
            return StringExtensions.ToUInt16(str, defVal, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static ushort ToUInt16(this string str, ushort defVal, NumberStyles style)
        {
            return StringExtensions.ToUInt16(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ushort ToUInt16(this string str, ushort defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    ushort _retVal;

                    if (ushort.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static int ToInt32(this string str, int defVal, IFormatProvider provider)
        {
            return StringExtensions.ToInt32(str, defVal, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int ToInt32(this string str, int defVal, NumberStyles style)
        {
            return StringExtensions.ToInt32(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static int ToInt32(this string str, int defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    int _retVal;

                    if (int.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static uint ToUInt32(this string str, uint defVal, IFormatProvider provider)
        {
            return StringExtensions.ToUInt32(str, defVal, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static uint ToUInt32(this string str, uint defVal, NumberStyles style)
        {
            return StringExtensions.ToUInt32(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static uint ToUInt32(this string str, uint defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    uint _retVal;

                    if (uint.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static long ToInt64(this string str, long defVal, IFormatProvider provider)
        {
            return StringExtensions.ToInt64(str, defVal, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static long ToInt64(this string str, long defVal, NumberStyles style)
        {
            return StringExtensions.ToInt64(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static long ToInt64(this string str, long defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    long _retVal;

                    if (long.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this string str, ulong defVal, IFormatProvider provider)
        {
            return StringExtensions.ToUInt64(str, defVal, NumberStyles.Integer, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this string str, ulong defVal, NumberStyles style)
        {
            return StringExtensions.ToUInt64(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this string str, ulong defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    ulong _retVal;

                    if (ulong.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defVal, IFormatProvider provider)
        {
            return StringExtensions.ToDecimal(str, defVal, NumberStyles.Number, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defVal, NumberStyles style)
        {
            return StringExtensions.ToDecimal(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    decimal _retVal;

                    if (decimal.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static float ToSingle(this string str, float defVal, IFormatProvider provider)
        {
            return StringExtensions.ToSingle(str, defVal, NumberStyles.Float | NumberStyles.AllowThousands, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>    
        /// <returns></returns>
        public static float ToSingle(this string str, float defVal, NumberStyles style)
        {
            return StringExtensions.ToSingle(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static float ToSingle(this string str, float defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    float _retVal;

                    if (float.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defVal, IFormatProvider provider)
        {
            return StringExtensions.ToDouble(str, defVal, NumberStyles.Float | NumberStyles.AllowThousands, provider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defVal, NumberStyles style)
        {
            return StringExtensions.ToDouble(str, defVal, style, NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defVal, NumberStyles style, IFormatProvider provider)
        {
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    double _retVal;

                    if (double.TryParse(str, style, provider, out _retVal))
                    {
                        return _retVal;
                    }
                }
                catch (ArgumentException)
                {
                }
            }

            return defVal;
        }
        #endregion
    }
}
