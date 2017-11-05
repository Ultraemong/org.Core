using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryProperty
    {
        /// <summary>
        /// 
        /// </summary>
        string Prefix { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        SqlDbType DbType { get; }

        /// <summary>
        /// 
        /// </summary>
        int Size { get; }

        /// <summary>
        /// 
        /// </summary>
        byte Precision { get; }

        /// <summary>
        /// 
        /// </summary>
        byte Scale { get; }

        /// <summary>
        /// 
        /// </summary>
        object DefaultValue { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsNullable { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsMappable { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsPrimaryKey { get; }

        /// <summary>
        /// 
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IgnoreXmlDataMember { get; }
    }
}
