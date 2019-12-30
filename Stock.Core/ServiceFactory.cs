/******************************************************************************** 
** Copyright(c) 2016  All Rights Reserved. 
** auth： 薛江涛 
** mail： xjt927@126.com 
** date： 2016/10/15 21:37:14 
** desc： 尚未编写描述 
** Ver :  V1.0.0 
*********************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context.Support;
using Stock.Tools.Utility;

namespace Stock.Core.Base
{
    public class ServiceFactory
    {
        // key: Module + user
        // value: service
        private static readonly Hashtable CreatedServices = new Hashtable();
        /// <summary>
        /// 获取spring注册对象工厂
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static T GetServiceImp<T>(ServiceModule.SvcModule svcModule)
        {
            string user = svcModule.GetDefaultValue();
            return (T)CreateEnergyService(svcModule, user);
        }
        private static object CreateEnergyService(ServiceModule.SvcModule module, string user)
        {
            var svc = FindService(module, user);
            if (svc == null)
            {
                var objId = ObjIdByModule(module);
                svc = ContextRegistry.GetContext()[objId];
                if (svc == null)
                {
                    throw new NotImplementedException(); // TODO: declare our own exceptions
                }

                CacheService(module, user, svc);
            }

            return svc;
        }

        private static string ObjIdByModule(ServiceModule.SvcModule module)
        {
            switch (module)
            {
                case ServiceModule.SvcModule.EastMoneyMain:
                    return "EastMoneyMain";
                case ServiceModule.SvcModule.EastMoneyImp:
                    return "EastMoneyImp";
                default:
                    return null;
            }
        }

        private static object FindService(ServiceModule.SvcModule module, string user)
        {
            var key = BuildKey(module, user);

            //todo 丢弃缓存权限数据


            return CreatedServices[key];
            // return CreatedServices.Contains(key) ? CreatedServices[key] : null;
        }

        private static void CacheService(ServiceModule.SvcModule module, string user, object svc)
        {
            var key = BuildKey(module, user);

            if (!CreatedServices.ContainsKey(key))
            {
                lock (CreatedServices)
                {
                    if (!CreatedServices.ContainsKey(key))
                    {
                        CreatedServices.Add(key, svc);
                    }
                }
            }
        }

        private static string BuildKey(ServiceModule.SvcModule module, string user)
        {
            return module + user;
        }


    }
}