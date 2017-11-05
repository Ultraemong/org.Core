using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace org.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryParameterizable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        void AddParameter(SqlParameter parameter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        void AddParameter(string name, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        void AddParameter(string name, SqlDbType type, ParameterDirection direction = ParameterDirection.Output);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        void AddParameter(string name, object value, SqlDbType type, ParameterDirection direction = ParameterDirection.Input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        void AddParameter(string name, SqlDbType type, int size, ParameterDirection direction = ParameterDirection.Output);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        void AddParameter(string name, object value, SqlDbType type, int size, ParameterDirection direction = ParameterDirection.Input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="direction"></param>
        void AddParameter(string name, SqlDbType type, int size, byte precision, byte scale, ParameterDirection direction = ParameterDirection.Output);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="direction"></param>
        void AddParameter(string name, object value, SqlDbType type, int size, byte precision, byte scale, ParameterDirection direction = ParameterDirection.Input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        void AddParameters(object parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SqlParameter GetParameter(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        SqlParameter GetParameter(int index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        void RemoveParameter(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        void RemoveParameter(int index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        void RemoveParameter(SqlParameter parameter);

        /// <summary>
        /// 
        /// </summary>
        void ResetParameters();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool ContainsParameter(SqlParameter parameter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool ContainsParameter(string name);
    }
}
