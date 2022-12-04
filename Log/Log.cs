using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expand.Log
{
    public class Log
    {


        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        public static void Info(string key,string msg) {
            var logger = LogManager.GetLogger(key);
            logger.Info(msg);
        }



        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        public static void Error(string key,string msg) {
            var logger = LogManager.GetLogger(key);
            logger.Error(msg);
        }
    }
}
