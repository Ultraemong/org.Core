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
    public sealed class WebRequestState : IDisposable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public WebRequestState(Stream stream)
        {
            _sourceStream = stream;
        }
        #endregion

        #region Fields
        Stream                          _sourceStream       = null;

        int                             _bytesHandled       = 0;
        long                            _totalBytesHandled  = 0L;

        byte[]                          _byteBuffer         = null;

        bool                            _isDisposed         = false;
        bool                            _isInitialized      = false;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Stream SourceStream
        {
            get
            {
                return _sourceStream;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] ByteBuffer
        {
            get
            {
                return _byteBuffer;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int BytesHandled
        {
            get
            {
                return _bytesHandled;
            }

            set
            {
                _bytesHandled = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public long TotalBytesHandled
        {
            get
            {
                return _totalBytesHandled;
            }

            set
            {
                _totalBytesHandled = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public void Initialize(WebRequestContext requestContext)
        {
            if (!_isInitialized)
            {
                _bytesHandled       = 0;
                _totalBytesHandled  = 0L;

                _byteBuffer         = new byte[requestContext.BufferSize];

                _isInitialized      = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _sourceStream.Flush();
                _sourceStream.Close();

                _bytesHandled       = 0;
                _totalBytesHandled  = 0L;

                _byteBuffer         = new byte[0];

                _isDisposed         = true;
            }
        }
        #endregion
    }
}
