using System;

using org.Core.Collections;

namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ValueDescriptors : NameValueCollection<Type, IValueDescriptor> 
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        ValueDescriptors()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static ValueDescriptors()
        {
            s_descriptors               = new ValueDescriptors()
            {
                {typeof(byte),          (new ByteDescriptor())},
                {typeof(DateTime),      (new DateTimeDescriptor())},
                {typeof(decimal),       (new DecimalDescriptor())},
                {typeof(double),        (new DoubleDescriptor())},
                {typeof(short),         (new Int16Descriptor())},
                {typeof(int),           (new Int32Descriptor())},
                {typeof(long),          (new Int64Descriptor())},
                {typeof(sbyte),         (new SByteDescriptor())},
                {typeof(float),         (new FloatDescriptor())},
                {typeof(string),        (new StringDescriptor())},
                {typeof(TimeSpan),      (new TimeSpanDescriptor())},
                {typeof(uint),          (new UInt32Descriptor())},
                {typeof(ulong),         (new UInt64Descriptor())},
                {typeof(ushort),        (new UInt16Descriptor())},
                {typeof(Guid),          (new GuidDescriptor())},
            };
        }
        #endregion

        #region Fields
        static readonly ValueDescriptors s_descriptors = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static ValueDescriptors Descriptors
        {
            get
            {
                return s_descriptors;
            }
        }
        #endregion
    }
}
