/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/24 13:31:21 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.BLL.EastMoney.Entity;

namespace Stock.EastMoney
{
    /// <summary>
    /// 停止点，用于记录最后一次抓取结果
    /// </summary>
    class StopPoint
    {
        /// <summary>
        /// 高手操作停止点
        /// </summary>
        public static EM_MasterOperation MasterOperationSP { get; set; }
    }
}
