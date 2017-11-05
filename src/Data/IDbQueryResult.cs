using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using org.Core.ServiceModel;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryResult : IEnumerable, IConvertible, IEnumerable<IDbQueryResult>, IExternalResult
    {
        /// <summary>
        /// 
        /// </summary>
        bool HasResult { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IDbQueryResult this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object this[string name] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        object this[string prefix, string name] { get; }

        /// <summary>
        /// 
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        XmlDocument GetXml(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        XmlDocument GetXml(string prefix, string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        byte[] GetBinary(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        byte[] GetBinary(string prefix, string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        bool GetValueOrDefault(string name, bool defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        bool? GetValueOrDefault(string name, bool? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        bool GetValueOrDefault(string prefix, string name, bool defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        bool? GetValueOrDefault(string prefix, string name, bool? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        DateTime GetValueOrDefault(string name, DateTime defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        DateTime? GetValueOrDefault(string name, DateTime? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        DateTime GetValueOrDefault(string prefix, string name, DateTime defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        DateTime? GetValueOrDefault(string prefix, string name, DateTime? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        DateTimeOffset GetValueOrDefault(string name, DateTimeOffset defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        DateTimeOffset? GetValueOrDefault(string name, DateTimeOffset? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        DateTimeOffset GetValueOrDefault(string prefix, string name, DateTimeOffset defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        DateTimeOffset? GetValueOrDefault(string prefix, string name, DateTimeOffset? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        string GetValueOrDefault(string name, string defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        string GetValueOrDefault(string prefix, string name, string defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        int GetValueOrDefault(string name, int defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        int? GetValueOrDefault(string name, int? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        int GetValueOrDefault(string name, int defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        int GetValueOrDefault(string name, int defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        int GetValueOrDefault(string name, int defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        int GetValueOrDefault(string prefix, string name, int defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        int? GetValueOrDefault(string prefix, string name, int? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        int GetValueOrDefault(string prefix, string name, int defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        int GetValueOrDefault(string prefix, string name, int defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        int GetValueOrDefault(string prefix, string name, int defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        uint GetValueOrDefault(string name, uint defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        uint? GetValueOrDefault(string name, uint? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        uint GetValueOrDefault(string name, uint defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        uint GetValueOrDefault(string name, uint defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        uint GetValueOrDefault(string name, uint defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        uint GetValueOrDefault(string prefix, string name, uint defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        uint? GetValueOrDefault(string prefix, string name, uint? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        uint GetValueOrDefault(string prefix, string name, uint defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        uint GetValueOrDefault(string prefix, string name, uint defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        uint GetValueOrDefault(string prefix, string name, uint defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        byte GetValueOrDefault(string name, byte defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        byte? GetValueOrDefault(string name, byte? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        byte GetValueOrDefault(string name, byte defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        byte GetValueOrDefault(string name, byte defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        byte GetValueOrDefault(string name, byte defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        byte GetValueOrDefault(string prefix, string name, byte defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        byte? GetValueOrDefault(string prefix, string name, byte? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        byte GetValueOrDefault(string prefix, string name, byte defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        byte GetValueOrDefault(string prefix, string name, byte defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        byte GetValueOrDefault(string prefix, string name, byte defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        sbyte GetValueOrDefault(string name, sbyte defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        sbyte? GetValueOrDefault(string name, sbyte? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        sbyte GetValueOrDefault(string name, sbyte defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        sbyte GetValueOrDefault(string name, sbyte defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        sbyte GetValueOrDefault(string name, sbyte defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        sbyte GetValueOrDefault(string prefix, string name, sbyte defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        sbyte? GetValueOrDefault(string prefix, string name, sbyte? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        sbyte GetValueOrDefault(string prefix, string name, sbyte defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        sbyte GetValueOrDefault(string prefix, string name, sbyte defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        sbyte GetValueOrDefault(string prefix, string name, sbyte defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        decimal GetValueOrDefault(string name, decimal defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        decimal? GetValueOrDefault(string name, decimal? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        decimal GetValueOrDefault(string name, decimal defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        decimal GetValueOrDefault(string name, decimal defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        decimal GetValueOrDefault(string name, decimal defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        decimal GetValueOrDefault(string prefix, string name, decimal defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        decimal? GetValueOrDefault(string prefix, string name, decimal? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        decimal GetValueOrDefault(string prefix, string name, decimal defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        decimal GetValueOrDefault(string prefix, string name, decimal defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        decimal GetValueOrDefault(string prefix, string name, decimal defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        double GetValueOrDefault(string name, double defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        double? GetValueOrDefault(string name, double? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        double GetValueOrDefault(string name, double defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        double GetValueOrDefault(string name, double defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        double GetValueOrDefault(string name, double defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        double GetValueOrDefault(string prefix, string name, double defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        double? GetValueOrDefault(string prefix, string name, double? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        double GetValueOrDefault(string prefix, string name, double defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        double GetValueOrDefault(string prefix, string name, double defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        double GetValueOrDefault(string prefix, string name, double defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        short GetValueOrDefault(string name, short defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        short? GetValueOrDefault(string name, short? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        short GetValueOrDefault(string name, short defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        short GetValueOrDefault(string name, short defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        short GetValueOrDefault(string name, short defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        short GetValueOrDefault(string prefix, string name, short defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        short? GetValueOrDefault(string prefix, string name, short? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        short GetValueOrDefault(string prefix, string name, short defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        short GetValueOrDefault(string prefix, string name, short defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        short GetValueOrDefault(string prefix, string name, short defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        ushort GetValueOrDefault(string name, ushort defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        ushort? GetValueOrDefault(string name, ushort? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        ushort GetValueOrDefault(string name, ushort defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        ushort GetValueOrDefault(string name, ushort defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        ushort GetValueOrDefault(string name, ushort defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        ushort GetValueOrDefault(string prefix, string name, ushort defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        ushort? GetValueOrDefault(string prefix, string name, ushort? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        ushort GetValueOrDefault(string prefix, string name, ushort defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        ushort GetValueOrDefault(string prefix, string name, ushort defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        ushort GetValueOrDefault(string prefix, string name, ushort defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        long GetValueOrDefault(string name, long defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        long? GetValueOrDefault(string name, long? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        long GetValueOrDefault(string name, long defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        long GetValueOrDefault(string name, long defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        long GetValueOrDefault(string name, long defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        long GetValueOrDefault(string prefix, string name, long defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        long? GetValueOrDefault(string prefix, string name, long? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        long GetValueOrDefault(string prefix, string name, long defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        long GetValueOrDefault(string prefix, string name, long defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        long GetValueOrDefault(string prefix, string name, long defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        ulong GetValueOrDefault(string name, ulong defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        ulong? GetValueOrDefault(string name, ulong? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        ulong GetValueOrDefault(string name, ulong defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        ulong GetValueOrDefault(string name, ulong defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        ulong GetValueOrDefault(string name, ulong defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        ulong GetValueOrDefault(string prefix, string name, ulong defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        ulong? GetValueOrDefault(string prefix, string name, ulong? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        ulong GetValueOrDefault(string prefix, string name, ulong defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        ulong GetValueOrDefault(string prefix, string name, ulong defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        ulong GetValueOrDefault(string prefix, string name, ulong defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        float GetValueOrDefault(string name, float defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        float? GetValueOrDefault(string name, float? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        float GetValueOrDefault(string name, float defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        float GetValueOrDefault(string name, float defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        float GetValueOrDefault(string name, float defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        float GetValueOrDefault(string prefix, string name, float defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <returns></returns>
        float? GetValueOrDefault(string prefix, string name, float? defVal);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        float GetValueOrDefault(string prefix, string name, float defVal, System.IFormatProvider provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        float GetValueOrDefault(string prefix, string name, float defVal, System.Globalization.NumberStyles style);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="name"></param>
        /// <param name="defVal"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        float GetValueOrDefault(string prefix, string name, float defVal, System.Globalization.NumberStyles style, System.IFormatProvider provider);
    }
}
