
namespace org.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValueDescriptor
    {
        /// <summary>
        /// 
        /// </summary>
        object MininumValue { get; }

        /// <summary>
        /// 
        /// </summary>
        object MaxinumValue { get; }

        /// <summary>
        /// 
        /// </summary>
        object DefaultValue { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsNumeric { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsInteger { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsFloat { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsBoolean { get; }
    }
}
