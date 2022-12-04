using ESP.Expand.File.Impl;
using ESP.Expand.File.Interface;
using System.Data;
using System.Text;

namespace ConsoleApp1.File.Impl
{
    public class CSVFileHelper : BaseFileHelper, ICSVFileHelper
    {

        #region 写入
        public void Write(string path, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            #region 表头
            for (var i = 0; i < dt.Columns.Count; i++)
            {
                string str = string.Format("{0},", dt.Columns[i].ColumnName);
                if (i == dt.Columns.Count - 1)
                {
                    sb.AppendLine(str);
                }
                else
                {
                    sb.Append(str);
                }
            }
            #endregion


            #region 内容
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    string str = string.Format("{0},", dt.Rows[i][j]);
                    if (j == dt.Columns.Count - 1)
                    {
                        sb.AppendLine(str);
                    }
                    else
                    {
                        sb.Append(str);
                    }
                }
            }
            #endregion

            base.Write(path, sb.ToString());
        }
        #endregion
    }
}
