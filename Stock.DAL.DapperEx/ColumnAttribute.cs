/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/23 23:10:45 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;

namespace Stock.DAL.DapperEx
{
    /// <summary>
    /// 列字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ColumnAttribute : BaseAttribute
    {
        /// <summary>
        /// 自增长
        /// </summary>
        public bool AutoIncrement { get; set; }
        public ColumnAttribute()
        {
            AutoIncrement = false;
        }
        /// <summary>
        /// 是否是自增长
        /// </summary>
        /// <param name="autoIncrement"></param>
        public ColumnAttribute(bool autoIncrement)
        {
            AutoIncrement = autoIncrement;
        }
    }
}
