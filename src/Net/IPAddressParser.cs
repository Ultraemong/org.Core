using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace org.Core.Net
{
    /// <summary>
    /// 
    /// </summary>
    public static class IPAddressParser
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static IPAddressParser()
        {
            _ipAddrMapEntry = new Dictionary<string, IPAddress>()
            {
                {"Any",             IPAddress.Any},
                {"Broadcast",       IPAddress.Broadcast},
                {"IPv6Any",         IPAddress.IPv6Any},
                {"IPv6Loopback",    IPAddress.IPv6Loopback},
                {"IPv6None",        IPAddress.IPv6None},
                {"Loopback",        IPAddress.Loopback},
                {"None",            IPAddress.None}
            };
        }
        #endregion

        #region Fields
        static readonly Dictionary<string, IPAddress> _ipAddrMapEntry = default(Dictionary<string, IPAddress>);
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endPointAddress"></param>
        /// <returns></returns>
        public static IPAddress Parse(string endPointAddress)
        {
            IPAddress _retVal = null;

            if (!string.IsNullOrEmpty(endPointAddress))
            {
                try
                {
                    _retVal = _ipAddrMapEntry.Where(x => x.Key.ToLower().Equals(endPointAddress.ToLower())).Select(x => x.Value).Single();
                }
                catch (InvalidOperationException)
                {
                    if (!endPointAddress.IndexOf('.').Equals(0))
                        _retVal = IPAddress.Parse(endPointAddress);
                }
            }

            return _retVal;
        }
        #endregion
    }
}
