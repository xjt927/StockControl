/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/23 22:21:00 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Stock.BLL.EastMoney.Entity;
using Stock.Core.Log;
using Stock.DAL.DapperEx;
using Stock.DAL.EastMoney.Interface;

namespace Stock.DAL.EastMoney
{
    public class EastMoneyDao : IEastMoneyDao
    {
        public bool SaveFindMast(IEnumerable<EM_MasterOperation> entity)
        {
            try
            {
                using (var db = DbBase.CreateDbBase())
                {
                    string baseSql = @"INSERT INTO EM_MasterOperation (
	OpreDate,
	GroupName,
	GroupNameUrl,
	OpreType,
	ZhengQuanName,
	ZhengQuanNameUrl,
	MasterType,
	MasterRate,
	RiRate,
	DealWinCnt,
	WinCntRate,
	ManageName,
	ManageNameUrl
)
VALUES
	(
		@OpreDate ,@GroupName ,@GroupNameUrl ,@OpreType ,@ZhengQuanName ,@ZhengQuanNameUrl ,
@MasterType ,@MasterRate ,@RiRate ,@DealWinCnt ,@WinCntRate ,@ManageName ,@ManageNameUrl  )";
                    var result = db.InsertMultiple<EM_MasterOperation>(baseSql, entity);
                    if (result == 0)
                    {
                        return false;
                    }

                    LogUtility.LogAction(string.Format(@"成功入库{0}条", result));
                    return true;
                }

                ////实体批量插入
                //using (var db = DbBase.CreateDbBase())
                //{
                //    var result = db.InsertBatch<EM_MasterOperation>(entity.ToList(), true);
                //    return result;
                //}

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 获取最后一条数据，用于停车点
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<EM_MasterOperation> LastFindMast()
        {
            try
            {
                using (var db = DbBase.CreateDbBase())
                {
                    string baseSql = @"select  * from em_masteroperation  t order by t.OpreDate desc limit 1";
                    var result = db.DbConnecttion.Query<EM_MasterOperation>(baseSql);
                    if (result==null)
                    {
                        return null;
                    }
                    return result.ToList();

                } 

            }
            catch (Exception ex)
            { 
                return null;
            }
        }
    }
}
