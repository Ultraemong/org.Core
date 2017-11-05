using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using org.Core.ServiceModel;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExternalActionManager : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        event ExternalUploadFailedEventHandler UploadFailed;

        /// <summary>
        /// 
        /// </summary>
        event ExternalUploadCompletedEventHandler UploadCompleted;

        /// <summary>
        /// 
        /// </summary>
        event ExternalUploadProgressChangedEventHandler UploadProgressChanged;

        /// <summary>
        /// 
        /// </summary>
        event ExternalDownloadFailedEventHandler DownloadFailed;

        /// <summary>
        /// 
        /// </summary>
        event ExternalDownloadCompletedEventHandler DownloadCompleted;

        /// <summary>
        /// 
        /// </summary>
        event ExternalDownloadProgressChangedEventHandler DownloadProgressChanged;

        /// <summary>
        /// 
        /// </summary>
        string Host { get; }

        /// <summary>
        /// 
        /// </summary>
        NetworkCredential Credential { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool ExistFile(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool ExistDirectory(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IExternalResult CreateDirectory(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destFileName"></param>
        /// <returns></returns>
        IExternalResult UploadFile(FileInfo source, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <returns></returns>
        IExternalResult UploadFile(string source, string destination);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        void UploadFileAsync(string source, string destination, object userToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        void UploadFileAsync(FileInfo source, string destination, object userToken);

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
        void DownloadFileAsync(string source, string destination, object userToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userToken"></param>
        void DownloadFileAsync(string source, FileStream destination, object userToken);
    }
}
