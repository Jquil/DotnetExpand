namespace Expand.File.Interface
{
    public interface IBaseFileHelper
    {

        /// <summary>
        /// 文件读取
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <returns></returns>
        string Read(string path);




        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="path">文件完整路径</param>
        /// <param name="data">数据</param>
        void Write(string path,string data);
    }
}
