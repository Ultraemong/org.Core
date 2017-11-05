using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.Core.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICryptoTransformable
    {
        /// <summary>
        /// 
        /// </summary>
        Encoding Encoding { get; }

        /// <summary>
        /// 
        /// </summary>
        string SaltKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        string Encrypt(string plainText);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        string Decrypt(string cipherText);
    }
}
