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
        /// ִ��SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="dbName"></param>
        void ExecuteSql(string sql, object param, string dbName);



        /// <summary>
        /// SQL��ѯ����List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        List<T> ExecuteSqlReturnList<T>(string sql, object param, string dbName);



        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="pdName">�洢��������</param>
        /// <param name="inParams">����(����)</param>
        /// <param name="outParams">����(����)</param>
        /// <param name="dbName">���ݿ�����</param>
        void ExecureProcdure(string pdName, string[,] inParams, ref string[,] outParams, string dbName);



        /// <summary>
        /// ִ�д洢���̷���DataTable
        /// </summary>
        /// <param name="pdName"></param>
        /// <param name="inParams"></param>
        /// <param name="outParams"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        DataTable ExecureProcdureReturnDataTable(string pdName, string[,] inParams, ref string[,] outParams, string dbName);


        /// <summary>
        /// ִ�д洢���̷���DataSet
        /// </summary>
        /// <param name="pdName"></param>
        /// <param name="inParams"></param>
        /// <param name="outParams"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        DataSet ExecureProcdureReturnDataSet(string pdName, string[,] inParams, ref string[,] outParams, string dbName);
    }
}
