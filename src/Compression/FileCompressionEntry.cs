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
    public class FileCompressionEntry : CompressionEntry
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="fileName"></param>
        public FileCompressionEntry(string entryName, string fileName)
            : this(entryName, new FileInfo(fileName))
        {   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="fileInfo"></param>
        public FileCompressionEntry(string entryName, FileInfo fileInfo)
        {
            Name            = entryName;
            Extension       = Path.GetExtension(entryName);
            FullName        = fileInfo.FullName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        public FileCompressionEntry(FileInfo fileInfo)
            : this(fileInfo.FullName, fileInfo)
        {   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public FileCompressionEntry(string fileName)
            : this(new FileInfo(fileName))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected FileCompressionEntry()
            : base()
        {
        }
        #endregion
    }
}
