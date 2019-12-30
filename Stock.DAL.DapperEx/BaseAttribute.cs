/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/23 23:08:24 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;

namespace Stock.DAL.DapperEx 
{
    public class BaseAttribute : Attribute
    {
        /// <summary>
        /// 别名，对应数据里面的名字
        /// </summary>
        public string Name { get; set; }
    }
}
