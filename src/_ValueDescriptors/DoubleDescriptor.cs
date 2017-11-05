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
    public class DoubleDescriptor : IValueDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public DoubleDescriptor()
        {
        }
        #endregion

        #region Fields
        readonly double _minValue       = double.MinValue;
        readonly double _maxValue       = double.MaxValue;
        readonly double _defaultValue   = 0.0D;
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
