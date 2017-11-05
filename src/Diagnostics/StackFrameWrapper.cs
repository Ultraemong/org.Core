using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using org.Core.Reflection;
using System.Reflection;

namespace org.Core.Diagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class StackFrameWrapper
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        internal StackFrameWrapper()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stackFrame"></param>
        internal StackFrameWrapper(StackFrame stackFrame)
        {
            _stackFrame = stackFrame;
        }
        #endregion

        #region Fields
        readonly StackFrame     _stackFrame         = null;

        MethodInfo              _methodInfo         = null;
        MethodMemberDescriptor  _methodDescriptor   = null;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public bool IsValid
        {
            get
            {
                return (null != _stackFrame && null != Method);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                if (null == _methodInfo)
                {
                    return _methodInfo = _stackFrame.GetMethod() as MethodInfo;
                }

                return _methodInfo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MethodMemberDescriptor MethodDescriptor
        {
            get
            {
                if (null == _methodDescriptor && IsValid)
                {
                    return _methodDescriptor = new MethodMemberDescriptor(Method);
                }

                return _methodDescriptor;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetFileColumnNumber()
        {
            if (IsValid)
                return _stackFrame.GetFileColumnNumber();

            return int.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetFileLineNumber()
        {
            if (IsValid)
                return _stackFrame.GetFileLineNumber();

            return int.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            if (IsValid)
                return _stackFrame.GetFileName();

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetILOffset()
        {
            if (IsValid)
                return _stackFrame.GetILOffset();

            return int.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetNativeOffset()
        {
            if (IsValid)
                return _stackFrame.GetNativeOffset();

            return int.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stackFrame"></param>
        /// <returns></returns>
        public static implicit operator StackFrameWrapper(StackFrame stackFrame)
        {
            return new StackFrameWrapper(stackFrame);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameWrapper"></param>
        /// <returns></returns>
        public static explicit operator StackFrame(StackFrameWrapper frameWrapper)
        {
            return frameWrapper._stackFrame;
        }
        #endregion
    }
}
