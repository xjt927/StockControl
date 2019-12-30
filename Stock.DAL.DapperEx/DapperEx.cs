/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/23 23:17:04 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace Stock.DAL.DapperEx
{

    public static class DapperEx
    {
        /// <summary>
        /// 扩展插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="t"></param>
        /// <param name="useTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static bool Insert<T>(this DbBase dbs, T t, bool useTransaction = false, int? commandTimeout = null) where T : class,new()
        {
            var db = dbs.DbConnecttion;
            IDbTransaction tran = null;
            if (useTransaction)
                tran = db.BeginTransaction();
            var result = false;
            var tbName = Common.GetTableName<T>();
            var columns = Common.GetExecColumns<T>();

            var flag = db.Execute(CreateInertSql(tbName, columns, dbs.ParamPrefix), t, tran, commandTimeout);
            if (tran != null)
            {
                try
                {
                    tran.Commit();
                    result = true;
                }
                catch
                {
                    tran.Rollback();
                }
            }
            else
            {
                return flag == 1;
            }
            return result;
        }
        /// <summary>
        /// 批量插入，传入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="lt"></param>
        /// <param name="useTransaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static bool InsertBatch<T>(this DbBase dbs, IList<T> lt, bool useTransaction = false, int? commandTimeout = null) where T : class,new()
        {
            var db = dbs.DbConnecttion;
            IDbTransaction tran = null;
            if (useTransaction)
                tran = db.BeginTransaction();
            var result = false;
            var tbName = Common.GetTableName<T>();
            var columns = Common.GetExecColumns<T>();
            var flag = db.Execute(CreateInertSql(tbName, columns, dbs.ParamPrefix), lt, tran, commandTimeout);
            if (tran != null)
            {
                try
                {
                    tran.Commit();
                    result = true;
                }
                catch
                {
                    tran.Rollback();
                }
            }
            else
            {
                return flag == lt.Count;
            }
            return result;
        }
        /// <summary>
        /// 组装插入语句
        /// </summary>
        /// <param name="tbName"></param>
        /// <param name="colums"></param>
        /// <returns></returns>
        private static string CreateInertSql(string tbName, IList<ParamColumnModel> colums, string ParamPrefix)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(string.Format("INSERT INTO {0}(", tbName));
            for (int i = 0; i < colums.Count; i++)
            {
                if (i == 0) sql.Append(colums[i].ColumnName);
                else sql.Append(string.Format(",{0}", colums[i].ColumnName));
            }
            sql.Append(")");
            sql.Append(" VALUES(");
            for (int i = 0; i < colums.Count; i++)
            {
                if (i == 0) sql.Append(string.Format("{0}{1}", ParamPrefix, colums[i].FieldName));
                else sql.Append(string.Format(",{0}{1}", ParamPrefix, colums[i].FieldName));
            }
            sql.Append(") ");
            return sql.ToString();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="entities"></param>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static int InsertMultiple<T>(this DbBase dbs, string sql, IEnumerable<T> entities) where T : class, new()
        {
            using (IDbConnection cnn = dbs.DbConnecttion)
            {
                int records = 0;
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        records = cnn.Execute(sql, entities, trans, 30, CommandType.Text);
                    }
                    catch (DataException ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
                //foreach (T entity in entities)
                //{
                //    records += cnn.Execute(sql, entity);
                //}
                return records;
            }
        }
    }
}
