using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expand.Print.Interface
{
    public interface IBaseZebraPrinterHelper
    {

        /// <summary>
        /// 连接打印机
        /// </summary>
        /// <param name="resource">资源名称</param>
        /// <param name="printerId">打印机Id</param>
        void Connect(string resource,string printerId = "");


        /// <summary>
        /// 断开连接打印机
        /// </summary>
        /// <param name="printerId">打印机Id</param>
        /// <param name="isDisconnectAll">是否断开所有打印机连接</param>
        void Disconnect(string printerId="",bool isDisconnectAll = false);


        /// <summary>
        /// 获取打印机列表
        /// </summary>
        /// <returns></returns>
        List<string> GetPrinterList();


        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="cmd">打印指令</param>
        /// <param name="printerId">打印机Id</param>
        /// <param name="retry">失败重试</param>
        /// <param name="maxRetry">失败重试最大次数</param>
        void Print(string cmd,string printerId = "",int retry = 0,int maxRetry = 3);
    }
}
