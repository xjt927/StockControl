﻿/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/23 23:11:23 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;

namespace Stock.DAL.DapperEx
{
    /// <summary>
    /// 忽略字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IgnoreAttribute : BaseAttribute
    {
    }
}
