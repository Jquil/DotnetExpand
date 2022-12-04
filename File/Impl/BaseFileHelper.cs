using ESP.Expand.File.Interface;
using System;
using System.IO;
using System.Text;

namespace ESP.Expand.File.Impl
{
    public class BaseFileHelper : IBaseFileHelper
    {

        #region 文件读取
        public string Read(string path)
        {
            string data = string.Empty;
            if (!System.IO.File.Exists(path))
            {
                throw new Exception("文件不存在");
            }
            using (StreamReader sr = new StreamReader(path))
            {
                data = sr.ReadToEnd();
            }
            return data;
        }
        #endregion



        #region 文件写入
        public void Write(string path, string data)
        {
            var dirInfo = Directory.GetParent(path);
            if (!dirInfo.Exists) {
                Directory.CreateDirectory(dirInfo.FullName);
            }
            using (StreamWriter sr = new StreamWriter(path, false, Encoding.UTF8))
            {
                sr.Write(data);
            }
        }
        #endregion
    }
}
