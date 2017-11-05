using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public enum WebRequestMethod
    {
        /// <summary>
        /// Unknown type
        /// </summary>
        Unknown,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        AppendFile,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        DeleteFile,

        /// <summary>
        /// This works properly on both FTP and FILE.
        /// </summary>
        DownloadFile,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        GetDateTimestamp,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        GetFileSize,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        ExistFile,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        ListDirectory,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        ListDirectoryDetails,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        ExistDirectory,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        MakeDirectory,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        PrintWorkingDirectory,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        RemoveDirectory,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        Rename,

        /// <summary>
        /// This works properly on both FTP and FILE.
        /// </summary>
        UploadFile,

        /// <summary>
        /// This works properly on FTP.
        /// </summary>
        UploadFileWithUniqueName,

        /// <summary>
        /// This works properly on HTTP.
        /// </summary>
        Connect,

        /// <summary>
        /// This works properly on HTTP.
        /// </summary>
        Get,

        /// <summary>
        /// This works properly on HTTP.
        /// </summary>
        Head,

        /// <summary>
        /// This works properly on HTTP.
        /// </summary>
        MkCol,

        /// <summary>
        /// This works properly on HTTP.
        /// </summary>
        Post,

        /// <summary>
        /// This works properly on HTTP.
        /// </summary>
        Put,
    }
}
