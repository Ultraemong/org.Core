using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Reflection;

namespace org.Core.Diagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public static class StackTraceHelpers
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        static StackTraceHelpers()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static StackFrameWrapper[] GetStackFrames()
        {
            var _stackTrace     = new StackTrace(1);
            var _stackFrames    = _stackTrace.GetFrames();

            if (null != _stackFrames && 0 < _stackFrames.Length)
            {
                return _stackFrames.Select(x => new StackFrameWrapper(x)).ToArray();
            }

            return new StackFrameWrapper[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static StackFrameWrapper GetStackFrameByIndex(int index)
        {
            var _stackTrace = new StackTrace(1);
            
            if (_stackTrace.FrameCount >= index)
                return new StackFrameWrapper(_stackTrace.GetFrame(index));

            return new StackFrameWrapper();
        }
        #endregion
    }
}
