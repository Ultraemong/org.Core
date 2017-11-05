using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Compression
{
    /// <summary>
    /// 
    /// </summary>
    public class FileCompressor : ICompressible
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public FileCompressor()
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressionEntry"></param>
        /// <returns></returns>
        public virtual FileInfo Compress(ICompressionEntry compressionEntry)
        {
            if (compressionEntry.CanCompress)
            {
                var _outputFullName = GetOutputFullNameToCompress(compressionEntry);

                using (var _stream = new FileStream(_outputFullName, FileMode.CreateNew))
                using (var _destination = new ZipArchive(_stream, ZipArchiveMode.Create, false, Encoding.UTF8))
                {
                    Compress(_destination, compressionEntry);
                }

                return new FileInfo(_outputFullName);
            }

            throw new InvalidOperationException("The compression operation cannot be performed.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="compressionEntry"></param>
        protected void Compress(ZipArchive destination, ICompressionEntry compressionEntry)
        {
            if (null == destination)
                throw new ArgumentNullException("destination");

            if (null == compressionEntry)
                throw new ArgumentNullException("compressionEntry");

            if (compressionEntry.HasEntries)
            { 
                foreach (var _source in compressionEntry.Entries)
                {
                    if (_source is FileCompressionEntry)
                    {
                        Compress(destination, _source.Name, _source.FullName);
                    }
                    else if (_source is DirectoryCompressionEntry)
                    {
                        destination.CreateEntry(_source.Name);

                        if (_source.HasEntries)
                            Compress(destination, _source);
                    }
                }
            }
            else
            {
                Compress(destination, Path.GetFileName(compressionEntry.FullName), compressionEntry.FullName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="entryName"></param>
        /// <param name="fullName"></param>
        protected void Compress(ZipArchive destination, string entryName, string fullName)
        {
            if (null == destination)
                throw new ArgumentNullException("destination");

            if (string.IsNullOrEmpty(entryName))
                throw new ArgumentNullException("entryName");

            if (string.IsNullOrEmpty(fullName))
                throw new ArgumentNullException("fullName");

            var _entry          = destination.CreateEntry(entryName, CompressionLevel.Optimal);
            var _lastWriteTime  = File.GetLastWriteTime(fullName);

            if (_lastWriteTime.Year < 1980
                || _lastWriteTime.Year > 2107)
            {
                _lastWriteTime = new DateTime(1980, 1, 1, 0, 0, 0);
            }

            _entry.LastWriteTime = _lastWriteTime;

            using (var _entryStream = _entry.Open())
            using (var _sourceStream = new FileStream(fullName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var _bytesRead      = 0;
                var _byteBuffer     = new byte[1024 * 2];
                
                while (0 < (_bytesRead = _sourceStream.Read(_byteBuffer, 0, _byteBuffer.Length)))
                {
                    _entryStream.Write(_byteBuffer, 0, _bytesRead);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressionEntry"></param>
        /// <returns></returns>
        public virtual DirectoryInfo Decompress(ICompressionEntry compressionEntry)
        {
            if (compressionEntry.CanDecompress)
            {
                var _outputFullName     = GetOutputFullNameToDecompress(compressionEntry);
                var _outputDirectory    = Directory.CreateDirectory(_outputFullName);

                using (var _stream = new FileStream(compressionEntry.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var _source = new ZipArchive(_stream, ZipArchiveMode.Read, false, Encoding.UTF8))
                {
                    foreach (var _entry in _source.Entries)
                    {
                        var _fullPath = Path.GetFullPath(Path.Combine(_outputDirectory.FullName, _entry.FullName));

                        if (!Path.GetFileName(_fullPath).Length.Equals(0))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(_fullPath)); 
                            
                            using (var _destination = new FileStream(_fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                            using (var _entryStream = _entry.Open())
                            {
                                var _bytesRead  = 0;
                                var _byteBuffer = new byte[1024 * 2];

                                while (0 < (_bytesRead = _entryStream.Read(_byteBuffer, 0, _byteBuffer.Length)))
                                {
                                    _destination.Write(_byteBuffer, 0, _bytesRead);
                                }
                            }

                            File.SetLastWriteTime(_fullPath, _entry.LastWriteTime.DateTime);

                            continue;
                        }

                        Directory.CreateDirectory(_fullPath);
                    }
                }

                return _outputDirectory;
            }

            throw new InvalidOperationException("The decompression operation cannot be performed.");
        }
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressionEntry"></param>
        /// <returns></returns>
        protected string GetOutputFullNameToCompress(ICompressionEntry compressionEntry)
        {
            var _outputName         = Path.GetFileNameWithoutExtension(compressionEntry.Name);
            var _outputDirectory    = Path.GetDirectoryName(compressionEntry.Name);
            var _outputFullName     = Path.GetFullPath(Path.Combine(_outputDirectory, string.Format("{0}.{1}", _outputName, compressionEntry.Extension)));
            
            var _tryCount           = 0;
            var _limitTryCount      = 10;

            do
            {
                _tryCount++;

                if (File.Exists(_outputFullName))
                {
                    _outputFullName = Path.GetFullPath(Path.Combine(_outputDirectory, string.Format("{0}({1}).{2}", _outputName, _tryCount, compressionEntry.Extension)));
                    continue;
                }
                
                break;
            }
            while (_tryCount != _limitTryCount);

            return _outputFullName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressionEntry"></param>
        /// <returns></returns>
        protected string GetOutputFullNameToDecompress(ICompressionEntry compressionEntry)
        {
            var _outputName         = Path.GetFileNameWithoutExtension(compressionEntry.Name);
            var _outputDirectory    = Path.GetDirectoryName(compressionEntry.Name);
            var _outputFullName     = Path.GetFullPath(Path.Combine(_outputDirectory, _outputName));
            
            var _tryCount           = 0;
            var _limitTryCount      = 10;

            do
            {
                _tryCount++;

                if (Directory.Exists(_outputFullName))
                {
                    _outputFullName = Path.GetFullPath(Path.Combine(_outputDirectory, string.Format("{0}({1})", _outputName, _tryCount)));
                    continue;
                }
                
                break;
            }
            while (_tryCount != _limitTryCount);

            return _outputFullName;
        }
        #endregion
    }
}
