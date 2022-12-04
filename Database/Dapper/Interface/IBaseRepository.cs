using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expand.Database.Dapper.Interface
{
    public interface IBaseRepository
    {


        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="dbName"></param>
        void ExecuteSql(string sql, object param, string dbName);



        /// <summary>
        /// SQL查询返回List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        List<T> ExecuteSqlReturnList<T>(string sql, object param, string dbName);



        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="pdName">存储过程名称</param>
        /// <param name="inParams">参数(输入)</param>
        /// <param name="outParams">参数(返回)</param>
        /// <param name="dbName">数据库名称</param>
        void ExecureProcdure(string pdName, string[,] inParams, ref string[,] outParams, string dbName);



        /// <summary>
        /// 执行存储过程返回DataTable
        /// </summary>
        /// <param name="pdName"></param>
        /// <param name="inParams"></param>
        /// <param name="outParams"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        DataTable ExecureProcdureReturnDataTable(string pdName, string[,] inParams, ref string[,] outParams, string dbName);


        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="pdName"></param>
        /// <param name="inParams"></param>
        /// <param name="outParams"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        DataSet ExecureProcdureReturnDataSet(string pdName, string[,] inParams, ref string[,] outParams, string dbName);
    }
}
