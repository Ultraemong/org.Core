using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryContract
    {
        /// <summary>
        /// 
        /// </summary>
        string QueryText { get; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryActions QueryAction { get; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryBehaviors QueryBehavior { get; }

        /// <summary>
        /// 
        /// </summary>
        int ExecutionTimeout { get; }

        /// <summary>
        /// 
        /// </summary>
        bool RequiresTransaction { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string Schema { get; }

        /// <summary>
        /// 
        /// </summary>
        string Abbreviation { get; }

        /// <summary>
        /// 
        /// </summary>
        bool OmitsAbbreviationNaming { get; }
    }
}
