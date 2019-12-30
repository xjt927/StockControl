/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/21 11:17:12 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HtmlAgilityPack;
using Stock.BLL.EastMoney;
using Stock.BLL.EastMoney.Entity;
using Stock.Core.Base;
using Stock.Core.Log;
using Stock.Http.Core;
using Stock.Tools.Utility;

namespace Stock.EastMoney
{
    /// <summary>
    /// 发现高手
    /// </summary>
    class FindMaster
    {
        static IEastMoneyImp eastMoneyImp = ServiceFactory.GetServiceImp<IEastMoneyImp>(ServiceModule.SvcModule.EastMoneyImp);
        private static bool isStopPoint = false;

        /// <summary>
        /// 开始爬取高手操作数据
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="loopTime"></param>
        public static Message Excute(int pageNum = 1, int loopTime = 0)
        {
            System.GC.Collect();
            LogUtility.LogAction(string.Format(@"爬取第{0}页", pageNum));

            isStopPoint = false;
            string _refererUrl = "http://group.eastmoney.com/Master.html?type=GSCZ";
            string url = "http://group.eastmoney.com/findmaster{0}.html";

            IHttpProvider httpProvider = new HttpProvider();

            url = string.Format(url, pageNum);

            //Get请求方式
            HttpResponseParameter responseParameter1 = httpProvider.Excute(new HttpRequestParameter
            {
                Url = url,
                RefererUrl = _refererUrl,
                IsPost = false,
                Encoding = Encoding.UTF8
            });
            string htmlContent = responseParameter1.Body;

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);
            string nodeName = "//tbody";
            HtmlNode tbodyNode = htmlDoc.DocumentNode.SelectSingleNode(nodeName);

            if (tbodyNode != null && !string.IsNullOrWhiteSpace(tbodyNode.InnerText.Trim()))
            {
                bool isSave = AnalysisHtml(htmlDoc, pageNum);

                #region 入库失败，重试

                if (!isSave && loopTime <= 3)
                {
                    Thread.Sleep(5000);
                    LogUtility.LogAction(string.Format(@"入库失败，重新爬取第{0}次", loopTime + 1));
                    Excute(pageNum, loopTime + 1);
                }
                if (loopTime > 3)
                {
                    LogUtility.LogAction(string.Format(@"已重新爬取{0}次,退出重试", loopTime + 1));
                }
                #endregion

                if (!isStopPoint)
                {
                    Excute(pageNum + 1);
                }
            }
            else
            {
                LogUtility.LogAction("爬取到最后一页，从头开始爬取");
                //加载停止点
                EastMoneyMain.InitInfo();
                //从第一页开始爬取
                Excute();
            }
            return new Message();
        }

        /// <summary>
        /// 开始解析html，并入库
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private static bool AnalysisHtml(HtmlDocument htmlDoc, int pageNum)
        {
            List<EM_MasterOperation> masterOperation = new List<EM_MasterOperation>();

            #region 解析

            //获取所有节点为<tbody>的节点
            string nodeName = "//tbody";
            HtmlNode tbodyNode = htmlDoc.DocumentNode.SelectSingleNode(nodeName);
            if (tbodyNode != null)
            {
                var trNodes = tbodyNode.SelectNodes("tr");
                foreach (HtmlNode nodes in trNodes)
                {
                    var nodeItems = nodes.SelectNodes("td");
                    if (nodeItems != null)
                    {

                        var master = new EM_MasterOperation()
                        {
                            OpreDate = Convert.ToDateTime(DateTime.Now.Year + "-" + nodeItems[0].InnerText),
                            GroupName = nodeItems[1].InnerText,
                            GroupNameUrl = "http://group.eastmoney.com/" + nodeItems[1].FirstChild.Attributes["href"].Value,
                            OpreType = nodeItems[2].InnerText,
                            ZhengQuanName = nodeItems[3].InnerText,
                            ZhengQuanNameUrl = nodeItems[3].FirstChild.Attributes["href"].Value,
                            MasterType = nodeItems[4].InnerText,
                            MasterRate = nodeItems[5].InnerText,
                            RiRate = nodeItems[6].InnerText,
                            DealWinCnt = nodeItems[7].InnerText,
                            WinCntRate = nodeItems[8].InnerText,
                            ManageName = nodeItems[9].InnerText,
                            ManageNameUrl = "http://group.eastmoney.com/" + nodeItems[9].FirstChild.Attributes["href"].Value
                        };

                        #region 判断停止点


                        if (StopPoint.MasterOperationSP != null && ((master.OpreDate == StopPoint.MasterOperationSP.OpreDate && master.GroupName == StopPoint.MasterOperationSP.GroupName
                            && master.ZhengQuanName == StopPoint.MasterOperationSP.ZhengQuanName) || master.OpreDate < StopPoint.MasterOperationSP.OpreDate))
                        {
                            isStopPoint = true;
                            break;
                        }

                        #endregion

                        masterOperation.Add(master);
                    }
                }
            }
            #endregion

            //保存入库
            bool isSave = eastMoneyImp.SaveFindMast(masterOperation);
            if (isSave && isStopPoint && pageNum == 1 && masterOperation.Count > 0)
            {
                StopPoint.MasterOperationSP = masterOperation.First();
            }
            else if (isSave && isStopPoint && masterOperation.Count > 0)
            {
                EastMoneyMain.InitInfo();
            }

            return isSave;
        }

    }
}
