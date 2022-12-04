using System.Data;

namespace Expand.File.Interface
{
    public interface ICSVFileHelper
    {

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dt"></param>
        void Write(string path,DataTable dt);
    }
}
