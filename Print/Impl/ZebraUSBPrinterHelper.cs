using Expand.Print.Interface;
using Expand.Print.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;

namespace Expand.Print.Impl
{
    public class ZebraUSBPrinterHelper : IBaseZebraPrinterHelper
    {

        #region 属性成员
        private static Dictionary<string, ZebraPrinterSession> _SessionPairs = new Dictionary<string, ZebraPrinterSession>();

        private readonly string _DefaultPrintId = "default";
        #endregion


        #region 连接打印机
        public void Connect(string resource, string printerId = "")
        {
            if (string.IsNullOrEmpty(printerId))
            {
                printerId = _DefaultPrintId;
            }
            if (_SessionPairs.ContainsKey(printerId))
            {
                return;
            }
            var session = new ZebraPrinterSession()
            {
                mUsbConnection = new UsbConnection(resource)
            };
            session.mUsbConnection.Open();
            session.mZebraPrinter = ZebraPrinterFactory.GetInstance(session.mUsbConnection);
            _SessionPairs.Add(printerId, session);
        }
        #endregion


        #region 断开连接
        public void Disconnect(string printerId = "", bool isDisconnectAll = false)
        {
            if (isDisconnectAll)
            {
                foreach(var kvp in _SessionPairs)
                {
                    kvp.Value.mUsbConnection.Close();
                    kvp.Value.mZebraPrinter.Connection.Close();
                }
                _SessionPairs.Clear();
            }
            else
            {
                if (string.IsNullOrEmpty(printerId))
                {
                    printerId = _DefaultPrintId;
                }

                if (!_SessionPairs.ContainsKey(printerId))
                {
                    return;
                }
                var value = _SessionPairs[printerId];
                value.mUsbConnection.Close();
                value.mZebraPrinter.Connection.Close();
                value.mUsbConnection = null;
                value.mZebraPrinter = null;
                _SessionPairs.Remove(printerId);
            }
        }
        #endregion


        #region 获取斑马打印机列表
        public List<string> GetPrinterList()
        {
            List<string> data = new List<string>();
            foreach(DiscoveredUsbPrinter printer in UsbDiscoverer.GetZebraUsbPrinters(new ZebraPrinterFilter()))
            {
                data.Add(printer.ToString());
            }
            return data;
        }
        #endregion


        #region 打印[实现断开重连机制]
        public void Print(string cmd, string printerId = "", int retry = 0, int maxRetry = 3)
        {
            if (string.IsNullOrEmpty(printerId))
            {
                printerId = _DefaultPrintId;
            }

            if (!_SessionPairs.ContainsKey(printerId))
            {
                var list = GetPrinterList();
                if(list == null || list.Count == 0)
                {
                    throw new Exception("找不到使用USB方式连接的斑马打印机！");
                }
                Connect(list[0],printerId);
            }
            try
            {
                _SessionPairs[printerId].mZebraPrinter.SendCommand(cmd);
            }
            catch(Exception e)
            {
                if (retry == maxRetry)
                {
                    throw e;
                }
                Disconnect(printerId);
                Print(cmd,retry:retry++);
            }
        }
        #endregion
    }
}
