using ESP.Expand.Reflect.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ESP.Expand.Reflect
{
    public class BaseReflectHelper : IBaseReflectHelper
    {

        #region 对象转化
        public T1 ObjectTransformation<T1, T2>(T2 root) where T1 : new()
        {
            T1 t1 = new T1();
            var props = typeof(T1).GetProperties();
            var rootProps = root.GetType().GetProperties();
            foreach (var item in props)
            {
                var prop = rootProps.FirstOrDefault(x => x.Name == item.Name);
                item.SetValue(t1, prop.GetValue(root, null),null);
                //item.SetValue(t1, prop.GetValue(root,null));
            }
            return t1;
        }
        #endregion


        #region DataTable转化为List
        public List<T> DataTableToList<T>(DataTable dt)
        {
            var list = new List<T>();
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());
            foreach (DataRow item in dt.Rows)
            {
                T s = Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        try
                        {
                            if (!Convert.IsDBNull(item[i]))
                            {
                                object v = null;
                                if (info.PropertyType.ToString().Contains("System.Nullable"))
                                    v = Convert.ChangeType(item[i], Nullable.GetUnderlyingType(info.PropertyType));
                                else v = Convert.ChangeType(item[i], info.PropertyType);
                                info.SetValue(s, v, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("字段[" + info.Name + "]转换出错," + ex.Message);
                        }
                    }
                }
                list.Add(s);
            }
            return list;

        }
        #endregion



        #region List转化为DataTable
        public DataTable ListToDataTable<T>(List<T> data)
        {
            DataTable dt = new DataTable();
            var row = 0;
            data.ForEach(item =>
            {
                var props = item.GetType().GetProperties();
                var dataRow = new List<object>();
                foreach (var prop in props)
                {
                    var value = prop.GetValue(item, null);
                    if (row == 0)
                    {
                        dt.Columns.Add(value.ToString());
                    }
                    dataRow.Add(value);
                }
                if(row != 0)
                {
                    dt.Rows.Add(dataRow.ToArray());
                }
                row++;
            });
            
            return dt;
        }
        #endregion
    }
}

