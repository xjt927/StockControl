/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/23 23:49:34 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.BLL.EastMoney.Entity;
using Stock.Core.Log;
using Stock.DAL.EastMoney;

namespace Stock.BLL.EastMoney
{
    public class EastMoneyImp : IEastMoneyImp
    {
        private EastMoneyDao eastMoneyDao;
        public bool SaveFindMast(IEnumerable<EM_MasterOperation> entity)
        {
            if (entity.ToList().Count > 0)
            {
                return eastMoneyDao.SaveFindMast(entity);
            }
            else
            {
                LogUtility.LogAction("爬取完成，解析0条数据");
            }
            return true;
        }

        /// <summary>
        /// 获取最后一条数据，用于停车点
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<EM_MasterOperation> LastFindMast()
        {
            return eastMoneyDao.LastFindMast();
        }
    }
}
