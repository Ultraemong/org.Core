using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEncryptable
    {
        /// <summary>
        /// 
        /// </summary>
        string SaltKey { get;}

        /// <summary>
        /// 
        /// </summary>
        string Password { get; }

        /// <summary>
        /// 
        /// </summary>
        Type CryptoTransformProvider { get; }
    }
}
