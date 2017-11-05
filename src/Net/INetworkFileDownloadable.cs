using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface INetworkFileDownloadable
    {
        /// <summary>
        /// 
        /// </summary>
        event DownloadFailedEventHandler DownloadFailed;

        /// <summary>
        /// 
        /// </summary>
        event DownloadCompletedEventHandler DownloadCompleted;

        /// <summary>
        /// 
        /// </summary>
        event DownloadProgressChangedEventHandler DownloadProgressChanged;

        /// <summary>
        /// 
        /// </summary>
        INetworkFileService FileService { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        IExternalResult CreateDirectory(string directoryPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        bool ExistDirectory(string directoryPath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        bool ExistFile(string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        IExternalResult DownloadFile(string source, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        IExternalResult DownloadFile(string source, FileStream destination);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userToken"></param>
        void DownloadFileAsync(string source, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userToken"></param>
        void DownloadFileAsync(string source, FileStream destination);
    }
}
