/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/23 23:14:33 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Stock.DAL.DapperEx
{
    public class DbBase : IDisposable
    {
        private static readonly object LockThis = new object();
        private string paramPrefix = "@";
        private string providerName = "System.Data.SqlClient";
        private IDbConnection dbConnecttion;
        private DbProviderFactory dbFactory;
        private DBType _dbType = DBType.SqlServer;
        private static DbBase _dbBase; 
        public IDbConnection DbConnecttion
        {
            get
            {
                return dbConnecttion;
            }
        }
        public string ParamPrefix
        {
            get
            {
                return paramPrefix;
            }
        }
        public string ProviderName
        {
            get
            {
                return providerName;
            }
            set
            {
                providerName = value;
            }
        }
        public DBType DbType
        {
            get
            {
                return _dbType;
            }
        }

        public static DbBase CreateDbBase()
        {
            //if (_dbBase == null)
            //{
            //    lock (LockThis)
            //    {
            //        if (_dbBase == null)
            //        {
            //            _dbBase = new DbBase();
            //        }
            //    }
            //}
            //return _dbBase;
            return new DbBase();
        }

        DbBase()
        {
            try
            {
                providerName = "MySql.Data.MySqlClient";
                string connectionStr = "User Id=root;pwd=sa123;Host=localhost;Port=3306;Database=stock;Character Set=utf8";
                dbFactory = DbProviderFactories.GetFactory(providerName);
                dbConnecttion = dbFactory.CreateConnection();
                dbConnecttion.ConnectionString = connectionStr;
                dbConnecttion.Open();
                SetParamPrefix();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DbBase CreateDbBase(string connectionName, string pName = "")
        {
            return new DbBase(connectionName, pName);
        }

        DbBase(string connectionStringName, string pName = "")
        {
            var connStr = "";
            if (!string.IsNullOrWhiteSpace(pName))
            {
                providerName = pName;
                connStr = connectionStringName;
            }
            else
            {
                //配置文件
                connStr = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
                if (!string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName))
                    providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
                else
                    throw new Exception("ConnectionStrings中没有配置提供程序ProviderName！");
            }

            dbFactory = DbProviderFactories.GetFactory(providerName);
            dbConnecttion = dbFactory.CreateConnection();
            dbConnecttion.ConnectionString = connStr;
            dbConnecttion.Open();
            SetParamPrefix();
        }


        private void SetParamPrefix()
        {
            string dbtype = (dbFactory == null ? dbConnecttion.GetType() : dbFactory.GetType()).Name;

            // 使用类型名判断
            if (dbtype.StartsWith("MySql")) _dbType = DBType.MySql;
            else if (dbtype.StartsWith("SqlCe")) _dbType = DBType.SqlServerCE;
            else if (dbtype.StartsWith("Npgsql")) _dbType = DBType.PostgreSQL;
            else if (dbtype.StartsWith("Oracle")) _dbType = DBType.Oracle;
            else if (dbtype.StartsWith("SQLite")) _dbType = DBType.SQLite;
            else if (dbtype.StartsWith("System.Data.SqlClient.")) _dbType = DBType.SqlServer;
            // else try with provider name
            else if (providerName.IndexOf("MySql", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.MySql;
            else if (providerName.IndexOf("SqlServerCe", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.SqlServerCE;
            else if (providerName.IndexOf("Npgsql", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.PostgreSQL;
            else if (providerName.IndexOf("Oracle", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.Oracle;
            else if (providerName.IndexOf("SQLite", StringComparison.InvariantCultureIgnoreCase) >= 0) _dbType = DBType.SQLite;

            if (_dbType == DBType.MySql && dbConnecttion != null && dbConnecttion.ConnectionString != null && dbConnecttion.ConnectionString.IndexOf("Allow User Variables=true") >= 0)
                paramPrefix = "?";
            if (_dbType == DBType.Oracle)
                paramPrefix = ":";
        }

        public void Dispose()
        {
            if (dbConnecttion != null)
            {
                try
                {
                    dbConnecttion.Dispose();
                }
                catch { }
            }
        }
    }
    public enum DBType
    {
        SqlServer,
        SqlServerCE,
        MySql,
        PostgreSQL,
        Oracle,
        SQLite
    }
}
