/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/21 11:59:44 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;

namespace Stock.BLL.EastMoney.Entity
{
    /// <summary>
    /// 高手操作
    /// </summary>
    public class EM_MasterOperation : BaseEntity
    {
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OpreDate { get; set; }

        /// <summary>
        /// 组合名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 组合名称Url
        /// </summary>
        public string GroupNameUrl { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string OpreType { get; set; }

        /// <summary>
        /// 证券名称
        /// </summary>
        public string ZhengQuanName { get; set; }

        /// <summary>
        /// 证券名称Url
        /// </summary>
        public string ZhengQuanNameUrl { get; set; }

        /// <summary>
        /// 高手类型
        /// </summary>
        public string MasterType { get; set; }

        /// <summary>
        /// 高手收益
        /// </summary>
        public string MasterRate { get; set; }

        /// <summary>
        /// 日收益
        /// </summary>
        public string RiRate { get; set; }

        /// <summary>
        /// 盈亏比
        /// </summary>
        public string DealWinCnt { get; set; }

        /// <summary>
        /// 胜率
        /// </summary>
        public string WinCntRate { get; set; }

        /// <summary>
        /// 管理人
        /// </summary>
        public string ManageName { get; set; }

        /// <summary>
        /// 管理人Url
        /// </summary>
        public string ManageNameUrl { get; set; }

    }
}
