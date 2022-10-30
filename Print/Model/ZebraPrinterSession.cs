using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace Expand.Print.Model
{
    public class ZebraPrinterSession
    {

        /// <summary>
        /// Usb连接
        /// </summary>
        public UsbConnection mUsbConnection { get; set; }
        


        /// <summary>
        /// Tcp连接
        /// </summary>
        public TcpConnection mTcpConnect { get; set; }


        /// <summary>
        /// 实例
        /// </summary>
        public ZebraPrinter mZebraPrinter { get; set; }
    }
}
