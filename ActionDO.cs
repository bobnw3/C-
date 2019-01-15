using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Web;

//翻兜
namespace Beer2dFire
{
    public class ActionDO
    {
        public static String url = System.Configuration.ConfigurationManager.AppSettings["url"];
        public static String orderlistmethod = System.Configuration.ConfigurationManager.AppSettings["orderlistmethod"];
        public static String v = System.Configuration.ConfigurationManager.AppSettings["v"];
        public static String instancelist = System.Configuration.ConfigurationManager.AppSettings["instancelist"];
        public static String shoplist = System.Configuration.ConfigurationManager.AppSettings["shoplist"];
        

        #region 查询店铺订单列表
        /// <summary>
        /// 查询店铺订单列表
        /// </summary>
        /// <param name="entityId">门店id</param>
        /// <param name="currDate">统计时间</param>
        /// <returns></returns>
        public String shopOrderList(String entityId,String currDate,String appKey,String appsecret)
        {
            Dictionary<String, String> dataDic = new Dictionary<String, String>();
            dataDic.Add("appKey", appKey);
            dataDic.Add("timestamp", _2dFire.SignGet.GetTimestamp());
            dataDic.Add("v", v);
            dataDic.Add("method", orderlistmethod);
            dataDic.Add("entityId", entityId);
            dataDic.Add("currDate", currDate);

            Dictionary<String, String> ascdata = dataDic.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);//根据key升序
            String sign = _2dFire.SignGet.signStringGet(ascdata,appsecret);
            dataDic.Add("sign", sign);
            String data = _2dFire.PostData.dataToString(dataDic);
            return _2dFire.PostData.postOrderDate(data,url);
        } 
        #endregion

        #region 查询店铺店铺订单详细列表
        /// <summary>
        /// 查询店铺店铺订单详细列表
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="orderids"></param>
        /// <returns></returns>
        public String shopOrderInfo(String entityId,String orderIds,String currDate, String appKey, String appsecret)
        {
            Dictionary<String, String> dataDic = new Dictionary<String, String>();
            dataDic.Add("appKey",appKey);
            dataDic.Add("timestamp",_2dFire.SignGet.GetTimestamp());
            dataDic.Add("v",v);
            dataDic.Add("method",instancelist);
            dataDic.Add("entityId",entityId);
            dataDic.Add("currDate",currDate);
            dataDic.Add("orderIds",orderIds);
            Dictionary<String, String> dic = dataDic.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            String sign = _2dFire.SignGet.signStringGet(dic, appsecret);
            dataDic.Add("sign", sign);
            return _2dFire.PostData.postOrderDate(_2dFire.PostData.dataToString(dataDic), url);
        }

        #endregion

        #region 查询绑定店铺订单列表
        public String shoplistData(String appKey, String appsecret)
        {
            Dictionary<String, String> dataDic = new Dictionary<String, String>();
            dataDic.Add("appKey", appKey);
            dataDic.Add("timestamp", _2dFire.SignGet.GetTimestamp());
            dataDic.Add("v", v);
            dataDic.Add("method", shoplist);
            Dictionary<String, String> dic = dataDic.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            String sign = _2dFire.SignGet.signStringGet(dic, appsecret);
            dataDic.Add("sign", sign);
            return _2dFire.PostData.postOrderDate(_2dFire.PostData.dataToString(dataDic), url);
        }
        #endregion





    }
}
