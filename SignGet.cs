using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace _2dFire
{
    class SignGet
    {
        #region 获取当前时间戳
        public static String GetTimestamp()
        {
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString(); //精确到毫秒
        }
        #endregion
        #region sign获取
        /// <summary>
        /// 拼接字符串原串
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static String signStringGet(Dictionary<String, String> dic, String appsecret)
        {
            String orignString = "";
            foreach (KeyValuePair<String, String> kv in dic)
            {
                orignString += kv.Key + kv.Value;
            }
            return getSignature(orignString, appsecret);
        }
        private static String getSignature(String orignString, String appsecret)
        {
            String secretString = appsecret + orignString + appsecret;
            byte[] strRes = Encoding.UTF8.GetBytes(secretString);
            using (SHA1 sha1 = new SHA1CryptoServiceProvider())
            {
                strRes = sha1.ComputeHash(strRes);//获取sha1值
                StringBuilder stringbuilder = new StringBuilder();
                foreach (byte item in strRes)
                {
                    stringbuilder.AppendFormat("{0:x2}", item);
                }
                System.Diagnostics.Debug.WriteLine("sign的值：", stringbuilder.ToString().ToUpper());
                return stringbuilder.ToString().ToUpper();//输出hex字符串
            }
        }

        #endregion
    }
}
