using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Data;
using System.Xml;

using org.Core.Collections;
using org.Core.Conversion;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class AdoDotNetDbParameterHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static AdoDotNetDbParameterHelpers()
        {   
        }
        #endregion

        #region Fields
        public const char       PARAMETERNAME_PREFIX    = '@';
        public const string     PARAMETERNAME_FORMAT    = "@{0}";
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string StringToParameterName(string name)
        {
            if (-1 == name.IndexOf(PARAMETERNAME_PREFIX))
                name = string.Format(PARAMETERNAME_FORMAT, name);

            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDirection"></param>
        /// <returns></returns>
        public static ParameterDirection PropertyDirectionToParameterDirection(DbQueryPropertyDirections propertyDirection)
        {
            switch (propertyDirection)
            {
                case DbQueryPropertyDirections.Input:
                    return ParameterDirection.Input;

                case DbQueryPropertyDirections.Output:
                    return ParameterDirection.Output;

                case DbQueryPropertyDirections.InputOutput:
                    return ParameterDirection.InputOutput;
            }

            return ParameterDirection.Input;
        }
        #endregion
    }
}
