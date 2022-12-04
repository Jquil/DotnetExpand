namespace Expand.File.Interface
{
    public interface IJsonFileHelper
    {


        /// <summary>
        /// 读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        T Read<T>(string path);




        /// <summary>
        /// 写入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="data"></param>
        void Write<T>(string path, T data);
    }
}
