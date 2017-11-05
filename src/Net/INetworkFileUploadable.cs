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
    public interface INetworkFileUploadable
    {
        /// <summary>
        /// 
        /// </summary>
        event UploadFailedEventHandler UploadFailed;

        /// <summary>
        /// 
        /// </summary>
        event UploadCompletedEventHandler UploadCompleted;

        /// <summary>
        /// 
        /// </summary>
        event UploadProgressChangedEventHandler UploadProgressChanged;

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
        IExternalResult UploadFile(string source, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        IExternalResult UploadFile(FileInfo source, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        IExternalResult UploadFile(FileStream source, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        IExternalResult UploadFile(Stream source, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        void UploadFileAsync(string source, string destination, object userToken = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        void UploadFileAsync(FileInfo source, string destination, object userToken = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        void UploadFileAsync(FileStream source, string destination, object userToken = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        void UploadFileAsync(Stream source, string destination, object userToken = null);
    }
}
