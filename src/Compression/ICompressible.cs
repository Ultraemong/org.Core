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
    public interface ICompressible
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressionEntry"></param>
        /// <returns></returns>
        FileInfo Compress(ICompressionEntry compressionEntry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressionEntry"></param>
        /// <returns></returns>
        DirectoryInfo Decompress(ICompressionEntry compressionEntry);
    }
}
