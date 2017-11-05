using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using org.Core.Collections;
using org.Core.Extensions;

namespace org.Core.Design
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EnglishUnitedStateNamingService : INamingService
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        internal EnglishUnitedStateNamingService(CultureInfo culture)
        {
            _reservedKeywords       = new string[] 
            { 
                "Entities",         
                "Entity", 

                "Models",           
                "Model",

                "ServiceModels",    
                "ServiceModel",

                "Handlers",         
                "Handler", 

                "Services",         
                "Service",  
              
                "Proxies",          
                "Proxy",

                "Libraries",        
                "Library",

                "Classes",          
                "Class",

                "Configuration",
            };

            _abbreviationMapEntry   = new NameValueCollection<string, string>(StringComparer.CurrentCultureIgnoreCase)
            {
                { "Common",                         "Co" },

                { "InternationalDigitalSystems",    "IDS" },
                { "InternationalDigitalSystem",     "IDS" },
                { "iDigitalSystems",                "IDS" },
                { "iDigitalSystem",                 "IDS" },
                { "iDigitalSys",                    "IDS" },
            };

            _cultureInfo            = culture;
            _pluralizationService   = PluralizationService.CreateService(culture);
        }
        #endregion

        #region Fields
        readonly CultureInfo                            _cultureInfo            = null;

        readonly string[]                               _reservedKeywords       = null;
        readonly PluralizationService                   _pluralizationService   = null;        
        readonly NameValueCollection<string, string>    _abbreviationMapEntry   = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public CultureInfo Culture
        {
            get
            {
                return _cultureInfo;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool IsReserved(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                return (word.ContainsAny(_reservedKeywords));
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public string ExceptReserved(string word)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public string[] ExceptReserved(string[] words)
        {
            if (0 < words.Length)
            {
                return words.Where(x => !x.ContainsAny(_reservedKeywords)).ToArray();
            }

            return new string[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public string Abbreviate(string word)
        {
            if (!string.IsNullOrEmpty(word) && _abbreviationMapEntry.ContainsKey(word))
            {
                return _abbreviationMapEntry[word];
            }

            return word;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool IsPlural(string word)
        {
            return _pluralizationService.IsPlural(word);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool IsSingular(string word)
        {
            return _pluralizationService.IsSingular(word);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public string Pluralize(string word)
        {
            return _pluralizationService.Pluralize(word);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public string Singularize(string word)
        {
            return _pluralizationService.Singularize(word);
        }
        #endregion
    }
}
