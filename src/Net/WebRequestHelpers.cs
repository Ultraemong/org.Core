using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using org.Core.Extensions;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebRequestHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static WebRequestHelpers()
        {
        }
        #endregion

        #region Constants
        public const char PathDelimiter  = '\\';
        public const char UriDelimiter   = '/';
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <returns></returns>
        public static string ReplacePathDelimiterToUriDelimiter(string resourcePath)
        {
            if (!string.IsNullOrEmpty(resourcePath))
                return resourcePath.Split(PathDelimiter).Join(UriDelimiter);

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestMethod"></param>
        /// <returns></returns>
        public static string WebRequestMethodToFtpMethodString(WebRequestMethod requestMethod)
        {
            switch (requestMethod)
            {
                case WebRequestMethod.AppendFile:
                    return WebRequestMethods.Ftp.AppendFile;

                case WebRequestMethod.DeleteFile:
                    return WebRequestMethods.Ftp.DeleteFile;

                case WebRequestMethod.DownloadFile:
                    return WebRequestMethods.Ftp.DownloadFile;

                case WebRequestMethod.GetDateTimestamp:
                    return WebRequestMethods.Ftp.GetDateTimestamp;

                case WebRequestMethod.ExistFile:
                case WebRequestMethod.GetFileSize:
                    return WebRequestMethods.Ftp.GetFileSize;

                case WebRequestMethod.ListDirectory:
                case WebRequestMethod.ExistDirectory:
                    return WebRequestMethods.Ftp.ListDirectory;

                case WebRequestMethod.ListDirectoryDetails:
                    return WebRequestMethods.Ftp.ListDirectoryDetails;

                case WebRequestMethod.MakeDirectory:
                    return WebRequestMethods.Ftp.MakeDirectory;

                case WebRequestMethod.PrintWorkingDirectory:
                    return WebRequestMethods.Ftp.PrintWorkingDirectory;

                case WebRequestMethod.RemoveDirectory:
                    return WebRequestMethods.Ftp.RemoveDirectory;

                case WebRequestMethod.Rename:
                    return WebRequestMethods.Ftp.Rename;

                case WebRequestMethod.UploadFile:
                    return WebRequestMethods.Ftp.UploadFile;

                case WebRequestMethod.UploadFileWithUniqueName:
                    return WebRequestMethods.Ftp.UploadFileWithUniqueName;
            }

            return null;
        }
        #endregion
    }
}
