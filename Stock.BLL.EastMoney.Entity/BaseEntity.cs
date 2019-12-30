/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/21 12:00:23 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;

namespace Stock.BLL.EastMoney.Entity
{
    /// <summary>
    /// 业务实体基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatDate { get; set; }

        /// <summary>
        /// 维护日期
        /// </summary>
        public DateTime? MntDate { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public long? IsDel { get; set; }

        /// <summary>
        /// 预留字段1
        /// </summary>
        public string Yl01 { get; set; }

        /// <summary>
        /// 预留字段2
        /// </summary>
        public string Yl02 { get; set; }
    }
}
