using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using org.Core.Utilities;
using org.Core.Reflection;
using org.Core.Extensions;

namespace org.Core.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    public static class CryptoTransformHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static CryptoTransformHelpers()
        {
        }
        #endregion

        #region Fields
        /// <summary>
        /// 
        /// </summary>
        static readonly string[] s_encryptionSaltKeys = new string[] {
             
                "BABUDOMS",   "BABUISMS",   "BABUSHKA"
            ,   "BABYDOLL",   "BABYFOOD",   "BABYHOOD"
            ,   "BABYSITS",   "BACALAOS",   "BACCARAS"
            ,   "BACCARAT",   "BACCATED",   "BACCHANT"
            ,   "BACCHIAC",   "BACCHIAN",   "BACCHIUS"
            ,   "BACHCHAS",   "BACHELOR",   "BACILLAR"
            ,   "BACILLUS",   "BACKACHE",   "BACKBAND"
            ,   "BACKBEAT",   "BACKBEND",   "BACKBITE"
            ,   "BACKBOND",   "BACKBONE",   "BACKBURN"
            ,   "BACKCAST",   "BACKCHAT",   "BACKCOMB"
            ,   "BACKDATE",   "BACKDOOR",   "BACKDOWN"
            ,   "BACKDROP",   "BACKFALL",   "BACKFILE"
            ,   "BACKFILL",   "BACKFIRE",   "BACKFITS"
            ,   "BACKFLIP",   "BACKFLOW",   "BACKHAND"
            ,   "BACKHAUL",   "BACKHOED",   "BACKHOES"
            ,   "BACKINGS",   "BACKLAND",   "BACKLASH"
            ,   "BACKLESS",   "BACKLIFT",   "BACKLIST"
            ,   "BACKLOAD",   "BACKLOGS",   "BACKLOTS"
            ,   "BACKMOST",   "BACKOUTS",   "BACKPACK"
            ,   "BACKPAYS",   "BACKREST",   "BACKROOM"
            ,   "BACKRUSH",   "BACKSAWS",   "BACKSEAT"
            ,   "BACKSETS",   "BACKSEYS",   "BACKSIDE"
            ,   "BACKSLAP",   "BACKSLID",   "BACKSPIN"
            ,   "BACKSTAB",   "BACKSTAY",   "BACKSTOP"
                            
        };                  
                            
        /// <summary>
        /// 
        /// </summary>
        static readonly string[] s_encryptionPasswords = new string[] {

                "AARDVARK",   "AARDWOLF",   "AASVOGEL"
            ,   "ABACTORS",   "ABACUSES",   "ABALONES"
            ,   "ABAMPERE",   "ABANDING",   "ABANDONS"
            ,   "ABAPICAL",   "ABASEDLY",   "ABASHING"
            ,   "ABATABLE",   "ABATISES",   "ABATTOIR"
            ,   "ABATURES",   "ABBACIES",   "ABBATIAL"
            ,   "ABBESSES",   "ABDICANT",   "ABDICATE"
            ,   "ABDOMENS",   "ABDOMINA",   "ABDUCENS"
            ,   "ABDUCENT",   "ABDUCING",   "ABDUCTED"
            ,   "ABDUCTEE",   "ABDUCTOR",   "ABEARING"
            ,   "ABEGGING",   "ABELMOSK",   "ABERRANT"
            ,   "ABERRATE",   "ABESSIVE",   "ABETMENT"
            ,   "ABETTALS",   "ABETTERS",   "ABETTING"
            ,   "ABETTORS",   "ABEYANCE",   "ABEYANCY"
            ,   "ABFARADS",   "ABHENRYS",   "ABHORRED"
            ,   "ABHORRER",   "ABIDANCE",   "ABIDINGS"
            ,   "ABIGAILS",   "ABJECTED",   "ABJECTLY"
            ,   "ABJOINTS",   "ABJURERS",   "ABJURING"
            ,   "ABLATING",   "ABLATION",   "ABLATIVE"
            ,   "ABLATORS",   "ABLEGATE",   "ABLEISMS"
            ,   "ABLEISTS",   "ABLUENTS",   "ABLUTION"
            ,   "ABNEGATE",   "ABNORMAL",   "ABOIDEAU"
            ,   "ABOITEAU",   "ABOMASAL",   "ABOMASUM"
            ,   "ABOMASUS",   "ABORALLY",   "ABORDING"

        }; 
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ICryptoTransformable RetrieveCryptoTransform(object instance)
        {
            if (ObjectUtils.IsClassType(instance))
            {
                var _encryptable        = AttributeMemberHelpers.RetrieveMember(instance, typeof(IEncryptable), true) as IEncryptable;
                var _cryptoTransform    = ObjectUtils.CreateInstanceOf<ICryptoTransformable>(_encryptable.CryptoTransformProvider);

                if (null != _cryptoTransform)
                {
                    _cryptoTransform.SaltKey    = _encryptable.SaltKey;
                    _cryptoTransform.Password   = _encryptable.Password;

                    return _cryptoTransform;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomSaltKeyString()
        {
            var _random = new Random();
            var _index  = _random.Next(0, (s_encryptionSaltKeys.Length - 1));

            return s_encryptionSaltKeys[_index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateRandomSaltKeyBytes()
        {
            return CryptoTransformHelpers.GenerateRandomSaltKeyBytes(Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] GenerateRandomSaltKeyBytes(Encoding encoding)
        {
            return CryptoTransformHelpers.GenerateRandomSaltKeyString()
                .ToBytes(encoding);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomPasswordString()
        {
            var _random = new Random();
            var _index  = _random.Next(0, (s_encryptionPasswords.Length - 1));

            return s_encryptionPasswords[_index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateRandomPasswordBytes()
        {
            return CryptoTransformHelpers.GenerateRandomPasswordBytes(Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] GenerateRandomPasswordBytes(Encoding encoding)
        {
            return CryptoTransformHelpers.GenerateRandomPasswordString()
                .ToBytes(encoding);
        }
        #endregion
    }
}
