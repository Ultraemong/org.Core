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
    public class DirectoryCompressionEntry : CompressionEntry
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="directoryInfo"></param>
        public DirectoryCompressionEntry(string entryName, DirectoryInfo directoryInfo)
            : base()
        {
            Name        = entryName;
            FullName    = directoryInfo.FullName;

            foreach (var _fileSysInfo in directoryInfo.EnumerateFileSystemInfos("*", SearchOption.AllDirectories))
            {
                var _length     = _fileSysInfo.FullName.Length - Name.Length;
                var _entryName  = _fileSysInfo.FullName.Substring(Name.Length, _length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                if (_fileSysInfo is FileInfo)
                {
                    BaseAddEntry(new FileCompressionEntry(_entryName, _fileSysInfo as FileInfo));
                    continue;
                }

                BaseAddEntry(new DirectoryCompressionEntry()
                {
                    Name        = string.Format("{0}{1}", _entryName, Path.DirectorySeparatorChar),
                    FullName    = null,
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryInfo"></param>
        public DirectoryCompressionEntry(DirectoryInfo directoryInfo)
            : this(directoryInfo.FullName, directoryInfo)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryName"></param>
        public DirectoryCompressionEntry(string directoryName)
            : this(directoryName, new DirectoryInfo(directoryName))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected DirectoryCompressionEntry()
            : base()
        {
        }
        #endregion
    }
}
