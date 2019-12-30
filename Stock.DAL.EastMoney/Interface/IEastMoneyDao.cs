/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/23 22:18:13 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.BLL.EastMoney.Entity;

namespace Stock.DAL.EastMoney.Interface
{
    public interface IEastMoneyDao
    {
        /// <summary>
        /// 批量保存高手操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool SaveFindMast(IEnumerable<EM_MasterOperation> entity);

        /// <summary>
        /// 获取最后一条数据，用于停车点
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
          List<EM_MasterOperation> LastFindMast();
    }
}
