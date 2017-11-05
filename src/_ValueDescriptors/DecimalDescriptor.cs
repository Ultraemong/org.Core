using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class DecimalDescriptor : IValueDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DecimalDescriptor()
        {
        }
        #endregion

        #region Fields
        readonly decimal _minValue      = decimal.MinValue;
        readonly decimal _maxValue      = decimal.MaxValue;
        readonly decimal _defaultValue  = 0.0M;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public object MininumValue
        {
            get
            {
                return _minValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object MaxinumValue
        {
            get
            {
                return _maxValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object DefaultValue
        {
            get
            {
                return _defaultValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsNumeric
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInteger
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFloat
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBoolean
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}
