using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using org.Core.Extensions;

namespace org.Core.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RijndaelCryptoTransform : ICryptoTransformable
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public RijndaelCryptoTransform()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saltKey"></param>
        /// <param name="password"></param>
        public RijndaelCryptoTransform(string saltKey, string password)
        {
            SaltKey     = saltKey;
            Password    = password;
        }
        #endregion

        #region Fields
        Encoding _encoding = Encoding.UTF8;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public Encoding Encoding
        {
            get 
            {
                return _encoding;
            }

            set
            {
                _encoding = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SaltKey
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get;
            set;
        } 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Encrypt(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                var _output = (byte[])null;

                using (var _rijndael = Rijndael.Create())
                {
                    using (var _rfc = new Rfc2898DeriveBytes(Password, SaltKey.ToBytes(Encoding)))
                    {
                        _rijndael.IV    = _rfc.GetBytes(16);
                        _rijndael.Key   = _rfc.GetBytes(32);

                        using (var _ct = _rijndael.CreateEncryptor())
                        {
                            using (var _ms = new MemoryStream())
                            using (var _cs = new CryptoStream(_ms, _ct, CryptoStreamMode.Write))
                            using (var _sw = new StreamWriter(_cs))
                            {
                                _sw.Write(plainText);
                                _sw.Close();

                                _output = _ms.ToArray();

                                _cs.Close();
                                _cs.Dispose();

                                _ms.Close();
                                _ms.Dispose();
                            }
                        }
                    }
                }

                if (_output != null && _output.Length > 0)
                {
                    return Convert.ToBase64String(_output);
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText)
        {
            if (!string.IsNullOrEmpty(cipherText))
            {
                var _output = (string)null;

                using (var _rijndael = Rijndael.Create())
                {
                    using (var _rfc = new Rfc2898DeriveBytes(Password, SaltKey.ToBytes(Encoding)))
                    {
                        _rijndael.IV    = _rfc.GetBytes(16);
                        _rijndael.Key   = _rfc.GetBytes(32);

                        using (ICryptoTransform _ct = _rijndael.CreateDecryptor())
                        {
                            var _bytes = Convert.FromBase64String(cipherText);

                            using (var _ms = new MemoryStream(_bytes))
                            using (var _cs = new CryptoStream(_ms, _ct, CryptoStreamMode.Read))
                            using (var _sr = new StreamReader(_cs, Encoding))
                            {
                                _output = _sr.ReadToEnd();

                                _sr.Close();
                                _sr.Dispose();

                                _cs.Close();
                                _cs.Dispose();

                                _ms.Close();
                                _ms.Dispose();
                            }

                            _ct.Dispose();
                        }
                    }
                }

                return _output;
            }

            return null;
        } 
        #endregion
    }
}
