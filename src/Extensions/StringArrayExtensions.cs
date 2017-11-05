using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace org.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringArrayExtensions
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static StringArrayExtensions()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static string GetValueOrDefault(this string[] array, int index, string defVal)
        {
            try
            {
                return array[index];
            }
            catch (FormatException)
            {
            }
            catch (InvalidCastException)
            {
            }
            catch (IndexOutOfRangeException)
            {
            }
            catch (ArgumentOutOfRangeException)
            {
            }

            return defVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static byte GetValueOrDefault(this string[] array, int index, byte defVal)
        {
            return StringExtensions.ToByte(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static sbyte GetValueOrDefault(this string[] array, int index, sbyte defVal)
        {
            return StringExtensions.ToSByte(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static short GetValueOrDefault(this string[] array, int index, short defVal)
        {
            return StringExtensions.ToInt16(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static ushort GetValueOrDefault(this string[] array, int index, ushort defVal)
        {
            return StringExtensions.ToUInt16(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static int GetValueOrDefault(this string[] array, int index, int defVal)
        {
            return StringExtensions.ToInt32(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static uint GetValueOrDefault(this string[] array, int index, uint defVal)
        {
            return StringExtensions.ToUInt32(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static long GetValueOrDefault(this string[] array, int index, long defVal)
        {
            return StringExtensions.ToInt64(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static ulong GetValueOrDefault(this string[] array, int index, ulong defVal)
        {
            return StringExtensions.ToUInt64(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static decimal GetValueOrDefault(this string[] array, int index, decimal defVal)
        {
            return StringExtensions.ToDecimal(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static float GetValueOrDefault(this string[] array, int index, float defVal)
        {
            return StringExtensions.ToSingle(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="idx"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static double GetValueOrDefault(this string[] array, int index, double defVal)
        {
            return StringExtensions.ToDouble(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static bool GetValueOrDefault(this string[] array, int index, bool defVal)
        {
            return StringExtensions.ToBoolean(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static DateTime GetValueOrDefault(this string[] array, int index, DateTime defVal)
        {
            return StringExtensions.ToDateTime(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        public static char GetValueOrDefault(this string[] array, int index, char defVal)
        {
            return StringExtensions.ToChar(StringArrayExtensions.GetValueOrDefault(array, index, string.Empty), defVal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join(this string[] array, char separator)
        {
            return string.Join(separator.ToString(), array);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join(this string[] array, string separator)
        {
            return string.Join(separator, array);
        }
        #endregion
    }
}
