/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/8 18:09:50 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Stock.BLL.EastMoney;
using Stock.Core.Base;
using Stock.Core.Log;
using Stock.Http.Core;

namespace Stock.EastMoney
{
    public class EastMoneyMain : IDoWork
    {
        private static IEastMoneyImp eastMoneyImp;
        public void DoWork()
        {
            LogUtility.LogAction("东方财富开始爬取...");
            InitInfo();
            Task.Factory.StartNew(SpiderStart);
        }

        /// <summary>
        /// 加载停止点
        /// </summary>
        public static void InitInfo()
        {
            var findMast = eastMoneyImp.LastFindMast();
            StopPoint.MasterOperationSP = findMast.FirstOrDefault();
        }

        private void SpiderStart()
        {
            while (true)
            {
                FindMaster.Excute();
                Thread.Sleep(10000);
            }
        }
    }
}
