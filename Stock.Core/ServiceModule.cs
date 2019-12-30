/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/15 21:46:26 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Stock.Core.Base
{
    public class ServiceModule
    {
        /// <summary>
        /// 服务模块
        /// </summary>
        public enum SvcModule
        {
            [Description("东方财富")]
            EastMoneyMain,
            [Description("EastMoneyImp")]
            EastMoneyImp
        }
    }
}
