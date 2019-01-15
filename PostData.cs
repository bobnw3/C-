using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace _2dFire
{
    class PostData
    {
        #region dataTostring
        public static String dataToString(Dictionary<String, String> data)
        {
            StringBuilder buffer = new StringBuilder();
            int i = 0;
            foreach (String key in data.Keys)
            {
                if (i > 0)
                {
                    buffer.AppendFormat("&{0}={1}", key, data[key]);
                }
                else
                {
                    buffer.AppendFormat("{0}={1}", key, data[key]);
                }
                i++;
            }
            return buffer.ToString();


        }
        #endregion
        #region post请求
        public static String postOrderDate(String data, String url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded;charset=\"utf-8\"";
                request.Method = "post";
                request.Timeout = 10000;
                byte[] dataStr = Encoding.UTF8.GetBytes(data);
                request.ContentLength = dataStr.Length;

                Stream requestream = request.GetRequestStream();
                requestream.Write(dataStr, 0, dataStr.Length);
                requestream.Close();


                using (HttpWebResponse rs = (HttpWebResponse)request.GetResponse())
                {
                    StreamReader sr = new StreamReader(rs.GetResponseStream(), Encoding.UTF8);
                    return sr.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        #endregion
    }
}
