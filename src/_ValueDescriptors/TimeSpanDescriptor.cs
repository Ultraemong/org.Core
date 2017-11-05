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
    public class TimeSpanDescriptor : IValueDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public TimeSpanDescriptor()
        {
        }
        #endregion

        #region Fields
        readonly TimeSpan _minValue         = TimeSpan.MinValue;
        readonly TimeSpan _maxValue         = TimeSpan.MaxValue;
        readonly TimeSpan _defaultValue     = TimeSpan.Zero;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public object MininumValue
        {
            get
            {
                return _defaultValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object MaxinumValue
        {
            get
            {
                return _defaultValue;
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
                return false;
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
                return false;
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
