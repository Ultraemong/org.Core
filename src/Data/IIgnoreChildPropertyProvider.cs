﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIgnoreChildPropertyProvider
    {
        /// <summary>
        /// 
        /// </summary>
        IgnoreChildPropertyDescriptors IgnorePropertyDescriptors { get; } 
    }
}
