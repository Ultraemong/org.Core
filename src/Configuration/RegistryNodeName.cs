using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using org.Core.Collections;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RegistryNodeName
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        public RegistryNodeName(string source)
        {
            _sourceString = source;

            foreach (Match _match in s_templatePattern.Matches(_sourceString))
            {
                var _template   = _match.Groups[0];
                var _matched    = _match.Groups[1];

                _innerRepository.Add(_matched.ToString(), _template.ToString());
            }
        }
        #endregion

        #region Fields
        static readonly Regex                           s_templatePattern   = new Regex(@"\{{([a-zA-Z0-9_]+)\}}");
        
        readonly NameValueCollection<string, string>    _innerRepository    = new NameValueCollection<string,string>(StringComparer.CurrentCultureIgnoreCase);

        string                                          _sourceString       = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(_sourceString);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCompiled
        {
            get
            {
                return (0 == _innerRepository.Count);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool ContainesPropertyName(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");

            return _innerRepository.ContainsKey(propertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public RegistryNodeName Replace(string propertyName, object value)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");

            if (null == value)
                throw new ArgumentNullException("value");

            if (!IsCompiled && ContainesPropertyName(propertyName))
            {
                _sourceString = _sourceString.Replace(_innerRepository[propertyName], value.ToString());

                _innerRepository.Remove(propertyName);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _sourceString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static implicit operator RegistryNodeName(string source)
        {
            return new RegistryNodeName(source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateString"></param>
        /// <returns></returns>
        public static explicit operator string(RegistryNodeName templateString)
        {
            return templateString.ToString();
        }
        #endregion
    }
}
