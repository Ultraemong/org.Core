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
    public class FloatDescriptor : IValueDescriptor
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public FloatDescriptor()
        {
        }
        #endregion

        #region Fields
        readonly float _minValue        = float.MinValue;
        readonly float _maxValue        = float.MaxValue;
        readonly float _defaultValue    = 0.0F;
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
