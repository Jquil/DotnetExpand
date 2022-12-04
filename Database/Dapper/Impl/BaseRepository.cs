using Dapper;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using System.Data;
using System.Data.SqlClient;

namespace Expand.Database.Dapper.Impl
{
    public class BaseRepository : IBaseRepository
    {

        #region 属性成员
        /// <summary>
        /// 数据库连接字符串字典
        /// </summary>
        private static Dictionary<string, string> _DBConnStr = new Dictionary<string, string>();
        #endregion


        #region 获取数据库连接字符串
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private string GetDatabaseConnectionString(string dbName)
        {
            if (_DBConnStr.ContainsKey(dbName))
            {
                return _DBConnStr[dbName];
            }
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var key = $"ConnStr_{dbName}";
            var connStr = config[key];
            if (connStr == null)
            {
                throw new Exception($"appsettings.json中找不到key={key}的节点");
            }
            _DBConnStr.Add(dbName, connStr);
            return connStr;
        }
        #endregion


        #region  执行SQL查询，返回List
        public List<T> ExecuteSqlReturnList<T>(string sql, object param, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(GetDatabaseConnectionString(dbName)))
            {
                conn.Open();
                return conn.Query<T>(sql, param).AsList();
            }
        }
        #endregion


        #region 执行存储过程
        public void ExecureProcdure(string pdName, string[,] inParams, ref string[,] outParams, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(GetDatabaseConnectionString(dbName)))
            {
                conn.Open();

                DynamicParameters paras = new DynamicParameters();
                if (inParams != null && inParams.Length > 0)
                {
                    for (var i = 0; i < inParams.Length / 2; i++)
                    {
                        paras.Add(inParams[i, 0], inParams[i, 1]);
                    }
                }
                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        paras.Add(outParams[i, 0], outParams[i, 1], DbType.String, ParameterDirection.Output);
                    }
                }

                conn.Execute(pdName, paras, commandType: CommandType.StoredProcedure);

                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        outParams[i, 1] = paras.Get<string>(outParams[i, 0]);
                    }
                }
            }
        }
        #endregion


        #region 执行SQL
        public void ExecuteSql(string sql, object param, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(GetDatabaseConnectionString(dbName)))
            {
                conn.Open();
                conn.Execute(sql, param);
            }
        }
        #endregion


        #region 执行存储过程返回DataTable
        public DataTable ExecureProcdureReturnDataTable(string pdName, string[,] inParams, ref string[,] outParams, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(GetDatabaseConnectionString(dbName)))
            {
                DataTable dt = new DataTable();
                conn.Open();

                DynamicParameters paras = new DynamicParameters();
                if (inParams != null && inParams.Length > 0)
                {
                    for (var i = 0; i < inParams.Length / 2; i++)
                    {
                        paras.Add(inParams[i, 0], inParams[i, 1]);
                    }
                }
                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        paras.Add(outParams[i, 0], outParams[i, 1], DbType.String, ParameterDirection.Output);
                    }
                }

                var reader = conn.ExecuteReader(pdName, paras, null, null, CommandType.StoredProcedure);
                dt.Load(reader);
                reader.Close();
                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        outParams[i, 1] = paras.Get<string>(outParams[i, 0]);
                    }
                }
                return dt;
            }
        }
        #endregion


        #region 执行存储过程返回DataSet
        public DataSet ExecureProcdureReturnDataSet(string pdName, string[,] inParams, ref string[,] outParams, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(GetDatabaseConnectionString(dbName)))
            {
                DataSet ds = new DataSet();
                conn.Open();

                DynamicParameters paras = new DynamicParameters();
                if (inParams != null && inParams.Length > 0)
                {
                    for (var i = 0; i < inParams.Length / 2; i++)
                    {
                        paras.Add(inParams[i, 0], inParams[i, 1]);
                    }
                }
                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        paras.Add(outParams[i, 0], outParams[i, 1], DbType.String, ParameterDirection.Output);
                    }
                }
                var reader = conn.ExecuteReader(pdName, paras, null, null, CommandType.StoredProcedure);
                var dtIndex = 0;
                while (!reader.IsClosed)
                {
                    ds.Tables.Add("Table" + (dtIndex + 1));
                    ds.EnforceConstraints = false;
                    ds.Tables[dtIndex].Load(reader);
                    dtIndex++;
                }

                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        outParams[i, 1] = paras.Get<string>(outParams[i, 0]);
                    }
                }

                return ds;
            }
        }
        #endregion
    }
}using Dapper;
using Microsoft.Extensions.Configuration;
using Repository.Interface;
using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    public class BaseRepository : IBaseRepository
    {

        #region 属性成员
        /// <summary>
        /// 数据库连接字符串字典
        /// </summary>
        private static Dictionary<string, string> _DBConnStr = new Dictionary<string, string>();
        #endregion


        #region 获取数据库连接字符串
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private string GetDatabaseConnectionString(string dbName)
        {
            if (_DBConnStr.ContainsKey(dbName))
            {
                return _DBConnStr[dbName];
            }
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var key = $"ConnStr_{dbName}";
            var connStr = config[key];
            if (connStr == null)
            {
                throw new Exception($"appsettings.json中找不到key={key}的节点");
            }
            _DBConnStr.Add(dbName, connStr);
            return connStr;
        }
        #endregion


        #region  执行SQL查询，返回List
        public List<T> ExecuteSqlReturnList<T>(string sql, object param, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(GetDatabaseConnectionString(dbName)))
            {
                conn.Open();
                return conn.Query<T>(sql, param).AsList();
            }
        }
        #endregion


        #region 执行存储过程
        public void ExecureProcdure(string pdName, string[,] inParams, ref string[,] outParams, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(GetDatabaseConnectionString(dbName)))
            {
                conn.Open();

                DynamicParameters paras = new DynamicParameters();
                if (inParams != null && inParams.Length > 0)
                {
                    for (var i = 0; i < inParams.Length / 2; i++)
                    {
                        paras.Add(inParams[i, 0], inParams[i, 1]);
                    }
                }
                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        paras.Add(outParams[i, 0], outParams[i, 1], DbType.String, ParameterDirection.Output);
                    }
                }

                conn.Execute(pdName, paras, commandType: CommandType.StoredProcedure);

                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        outParams[i, 1] = paras.Get<string>(outParams[i, 0]);
                    }
                }
            }
        }
        #endregion


        #region 执行SQL
        public void ExecuteSql(string sql, object param, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(GetDatabaseConnectionString(dbName)))
            {
                conn.Open();
                conn.Execute(sql, param);
            }
        }
        #endregion


        #region 执行存储过程返回DataTable
        public DataTable ExecureProcdureReturnDataTable(string pdName, string[,] inParams, ref string[,] outParams, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(GetDatabaseConnectionString(dbName)))
            {
                DataTable dt = new DataTable();
                conn.Open();

                DynamicParameters paras = new DynamicParameters();
                if (inParams != null && inParams.Length > 0)
                {
                    for (var i = 0; i < inParams.Length / 2; i++)
                    {
                        paras.Add(inParams[i, 0], inParams[i, 1]);
                    }
                }
                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        paras.Add(outParams[i, 0], outParams[i, 1], DbType.String, ParameterDirection.Output);
                    }
                }

                var reader = conn.ExecuteReader(pdName, paras, null, null, CommandType.StoredProcedure);
                dt.Load(reader);
                reader.Close();
                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        outParams[i, 1] = paras.Get<string>(outParams[i, 0]);
                    }
                }
                return dt;
            }
        }
        #endregion


        #region 执行存储过程返回DataSet
        public DataSet ExecureProcdureReturnDataSet(string pdName, string[,] inParams, ref string[,] outParams, string dbName)
        {
            using (IDbConnection conn = new SqlConnection(GetDatabaseConnectionString(dbName)))
            {
                DataSet ds = new DataSet();
                conn.Open();

                DynamicParameters paras = new DynamicParameters();
                if (inParams != null && inParams.Length > 0)
                {
                    for (var i = 0; i < inParams.Length / 2; i++)
                    {
                        paras.Add(inParams[i, 0], inParams[i, 1]);
                    }
                }
                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        paras.Add(outParams[i, 0], outParams[i, 1], DbType.String, ParameterDirection.Output);
                    }
                }
                var reader = conn.ExecuteReader(pdName, paras, null, null, CommandType.StoredProcedure);
                var dtIndex = 0;
                while (!reader.IsClosed)
                {
                    ds.Tables.Add("Table" + (dtIndex + 1));
                    ds.EnforceConstraints = false;
                    ds.Tables[dtIndex].Load(reader);
                    dtIndex++;
                }

                if (outParams != null && outParams.Length > 0)
                {
                    for (var i = 0; i < outParams.Length / 2; i++)
                    {
                        outParams[i, 1] = paras.Get<string>(outParams[i, 0]);
                    }
                }

                return ds;
            }
        }
        #endregion
    }
}