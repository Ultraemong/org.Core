using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Design
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class NamingServiceFactory
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static NamingServiceFactory()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static INamingService CreateNamingService(CultureInfo culture)
        {
            if (null == culture)
                throw new ArgumentNullException("culture");

            if (culture.Name.Equals("en-US", StringComparison.CurrentCultureIgnoreCase))
            {
                return new EnglishUnitedStateNamingService(culture);
            }

            throw new NotSupportedException(string.Format("The '{0}' culture is not supported yet.", culture.Name));
        }
        #endregion
    }
}
