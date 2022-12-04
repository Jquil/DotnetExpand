using ESP.Expand.File.Interface;
using Newtonsoft.Json;

namespace Expand.File.Impl
{
    public class JsonFileHelper : BaseFileHelper,IJsonFileHelper
    {

        #region 读取
        public T Read<T>(string path)
        {
            string str = base.Read(path);
            return JsonConvert.DeserializeObject<T>(str);
        }
        #endregion


        #region 写入
        public void Write<T>(string path, T data)
        {
            string str = JsonConvert.SerializeObject(data);
            base.Write(path, str);
        }
        #endregion
    }
}
