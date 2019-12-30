/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/24 13:46:51 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Tools.Utility
{

    /// <summary>
    /// Message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Message Code
        /// </summary>
        public string MessageCode { get; set; }

        /// <summary>
        /// Message Desc
        /// </summary>
        public string MessageDesc { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageNum { get; set; }
    }
}
