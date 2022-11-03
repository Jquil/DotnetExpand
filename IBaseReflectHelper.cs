using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ESP.Expand.Reflect.Interface
{
    public interface IBaseReflectHelper
    {

        /// <summary>
        /// 对象转化
        /// </summary>
        /// <typeparam name="T1">待转化对象</typeparam>
        /// <typeparam name="T2">数据源</typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        T1 ObjectTransformation<T1, T2>(T2 root) where T1 : new();



        /// <summary>
        /// DataTable转化为List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        List<T> DataTableToList<T>(DataTable dt);



        /// <summary>
        /// List转为为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        DataTable ListToDataTable<T>(List<T> data);
    }
}
