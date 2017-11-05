using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Compression
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICompressionEntry
    {
        /// <summary>
        /// 
        /// </summary>
        bool HasEntries { get; }

        /// <summary>
        /// 
        /// </summary>
        ICompressionEntryCollection Entries { get; }

        /// <summary>
        /// 
        /// </summary>
        bool CanCompress { get; }

        /// <summary>
        /// 
        /// </summary>
        bool CanDecompress { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// 
        /// </summary>
        string Extension { get; }
    }
}
