﻿using System;

namespace Stock.Core.Config.Models
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    [Serializable]
    public class DaoConfig : ConfigFileBase
    {
        public DaoConfig()
        {
        }
        #region 序列化属性
        public String Account { get; set; }
        public string OrdersDB { get; set; }
        public String Log { get; set; }
        public String Cms { get; set; }
        public String Crm { get; set; }
        public String OA { get; set; }
        #endregion
    }
}
