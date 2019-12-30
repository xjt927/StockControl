/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/23 23:09:47 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

namespace Stock.DAL.DapperEx
{
    /// <summary>
    /// 生成SQL时参数里面的列名和对应值名称
    /// </summary>
    public class ParamColumnModel
    {
        /// <summary>
        /// 数据库列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 对应类属性名
        /// </summary>
        public string FieldName { get; set; }
    }
}
