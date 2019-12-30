/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/23 23:06:24 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Stock.DAL.DapperEx 
{
    public class Common
    {

        /// <summary>
        /// 获取对象对应数据库表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetTableName<T>()
        {
            var ty = typeof(T);
            var arri = ty.GetCustomAttributes(typeof(BaseAttribute), true).FirstOrDefault();
            if (arri is TableAttribute && (!string.IsNullOrEmpty((arri as BaseAttribute).Name)))
            {
                return (arri as BaseAttribute).Name;
            }
            return ty.Name;
        }
        /// <summary>
        /// 在没有指定排序时，获取一个默认的排序列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetDefaultOrderField<T>()
        {
            var name = "";
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var arri = propertyInfo.GetCustomAttributes(typeof(BaseAttribute), true).FirstOrDefault();
                if (arri is IgnoreAttribute)
                {
                    arri = null;
                    continue;
                }
                name = (arri == null || string.IsNullOrEmpty((arri as BaseAttribute).Name)) ? propertyInfo.Name : (arri as BaseAttribute).Name;
                break;
            }
            return name;
        }
        /// <summary>
        /// 获取要执行SQL时的列,添加和修改数据时
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<ParamColumnModel> GetExecColumns<T>() where T : class
        {
            var columns = new List<ParamColumnModel>();
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var arri = propertyInfo.GetCustomAttributes(typeof(BaseAttribute), true).FirstOrDefault();
                if (arri is IgnoreAttribute)
                {
                    arri = null;
                    continue;
                }
                else if (arri is IdAttribute)
                {
                    if ((arri as IdAttribute).CheckAutoId)
                    {
                        arri = null;
                        continue;
                    }
                }
                else if (arri is ColumnAttribute)
                {
                    if ((arri as ColumnAttribute).AutoIncrement)
                    {
                        arri = null;
                        continue;
                    }
                }
                string name = (arri == null || string.IsNullOrEmpty((arri as BaseAttribute).Name)) ? propertyInfo.Name : (arri as BaseAttribute).Name;
                columns.Add(new ParamColumnModel() { ColumnName = name, FieldName = propertyInfo.Name });
            }
            return columns;
        }
        /// <summary>
        /// 获取对象的主键标识列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PropertyName">对应实体属性名</param>
        /// <returns></returns>
        public static string GetPrimaryKey<T>(out string PropertyName) where T : class
        {
            string name = "";
            PropertyName = "";
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var arri = propertyInfo.GetCustomAttributes(typeof(BaseAttribute), true).FirstOrDefault();
                if (arri is IdAttribute)
                {
                    name = (arri == null || string.IsNullOrEmpty((arri as BaseAttribute).Name)) ? propertyInfo.Name : (arri as BaseAttribute).Name;
                    PropertyName = propertyInfo.Name;
                    break;
                }

            }
            if (string.IsNullOrEmpty(PropertyName))
            {
                throw new Exception("没有任何列标记为主键特性");
            }
            return name;
        }
        /// <summary>
        /// 通过属性名获取对应的数据列名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetExecCloumName<T>(string propertyName) where T : class
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);

            var arri = propertyInfo.GetCustomAttributes(typeof(BaseAttribute), true).FirstOrDefault();
            if (arri is IgnoreAttribute)
            {
                arri = null;
            }
            string name = (arri == null || string.IsNullOrEmpty((arri as BaseAttribute).Name)) ? propertyInfo.Name : (arri as BaseAttribute).Name;
            return name;
        }
        /// <summary>
        /// 通过表达示树获取属性名对应列名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetNameByExpress<T>(Expression<Func<T, object>> expr) where T : class
        {
            var pname = "";
            if (expr.Body is UnaryExpression)
            {
                var uy = expr.Body as UnaryExpression;
                pname = (uy.Operand as MemberExpression).Member.Name;
            }
            else
            {
                pname = (expr.Body as MemberExpression).Member.Name;
            }
            var propertyInfo = typeof(T).GetProperty(pname);
            var arri = propertyInfo.GetCustomAttributes(typeof(BaseAttribute), true).FirstOrDefault();
            if (arri is IgnoreAttribute)
            {
                throw new Exception(string.Format("{0}不能进行SQL处理", pname));
            }
            string name = (arri == null || string.IsNullOrEmpty((arri as BaseAttribute).Name)) ? propertyInfo.Name : (arri as BaseAttribute).Name;
            return name;
        }
        /// <summary>
        /// 字符串中连续多个空格合并成一个空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnitMoreSpan(string str)
        {
            Regex replaceSpace = new Regex(@"\s{1,}", RegexOptions.IgnoreCase);
            return replaceSpace.Replace(str, " ").Trim();
        }
    }
}