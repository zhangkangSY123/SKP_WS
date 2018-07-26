using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMCPCommService.Interface;
using LogServiceHelper;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using DB2012;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LGB_SKP_FY
{
    [Description("推送费用.")]
    [DisplayName("FYService")]
    [Guid("34539d51-a170-404a-89b2-8fc4cece211c")]
    [Serializable]
    public class FYService : IService
    {
        private ServiceEntity _se;
        private DB2012Class SHOPDB_Center;

        #region IService 成员

        public void Initialize(ServiceEntity serviceEntity)
        {
            _se = serviceEntity;
        }

        private bool _isRunning;

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            _isRunning = true;
            string rMsg = "";

            SHOPDB_Center = new DB2012Class(ref rMsg, "473080b8a5c7ce14657e92e595540b143155f94d337b53bdbc66beb8fe26ff2d835d3bce1efa21116ab2288250b3bc6ffb93e939372cac5db3780d9ad371a003dc56594500fe3779c48511428b7dbf3097af7942c533a6a25623695c098cf692ad9b7e9661f86c910d593a8917028ea696abdccdd17d76a2bc47a0552b68cb9a390f2eae99f03a9a548286b71e5ff2bfc0ddd76f046e62cd352e2eb8569abc73a11b34e8f19f83c69a041edac680c198b92e781c9c6bc822", "E2D4A6EF", true);

            while (_isRunning)
            {
                try
                {
                    //CreateWXTs();
                    PushProc();
                }
                catch (Exception exception)
                {
                    LogHelper.Logger.Error("写入日志出错。", exception);
                }
                Thread.Sleep(60000);
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }

        #endregion

        #region 基础函数
        private string RequestUrl(string RequestType, string url, Dictionary<string, string> Params = null)
        {
            int errCount = 0;
            HttpWebRequest request;
            HttpWebResponse response;
            RequestType = RequestType.ToUpper();
            if (Params != null)
            {
                if (Params.Count > 0)
                {
                    if (RequestType == "GET")
                    {
                        if (url.IndexOf("?") < 0)
                        {
                            url += "?";
                            int count = 0;
                            foreach (string key in Params.Keys)
                            {
                                if (count != 0)
                                {
                                    url += "&" + key + "=" + Params[key];
                                }
                                else
                                {
                                    url += key + "=" + Params[key];
                                }
                                count++;
                            }
                        }
                        else
                        {
                            foreach (string key in Params.Keys)
                            {
                                url += "&" + key + "=" + Params[key];
                            }
                        }
                    }
                    else if (RequestType == "POST")
                    {

                    }
                }
            }
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = RequestType;
            if (RequestType == "GET") { request.ContentLength = 0; }
            else if (RequestType == "POST") { request.ContentLength = 0; }
        BeginRequest:
            try
            {
                //获取服务器返回的资源  
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException)
            {
                if (errCount < 3)
                {
                    errCount++;
                    goto BeginRequest;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetUrl(string url)
        {
            int errCount = 0;
            HttpWebRequest request;
            HttpWebResponse response;

            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ContentLength = 0;
        BeginRequest:
            try
            {
                //获取服务器返回的资源  
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException)
            {
                if (errCount < 3)
                {
                    errCount++;
                    goto BeginRequest;
                }
                else { return null; }
            }
            catch (Exception) { return null; }
        }

        private string PostUrl(string url, Dictionary<string, string> Params = null)
        {
            return RequestUrl("POST", url, Params);
        }
        #endregion

        #region 业务处理


        private void PushProc()
        {
            string rMsg = string.Empty;
            try
            {
                string rSql = "select JLBH,CONTENT from s_pushfy_000020 where sendTime is null";
                DataTable dt = new DataTable();
                SHOPDB_Center.DB2012Class_GetDataRefTable(rSql, ref rMsg, dt);

                if (rMsg != "")
                {
                    LogHelper.Logger.ErrorFormat("Name={0},查询推送数据出错:" + rMsg, _se.Name);
                    return;
                }

                if (dt.Rows.Count == 0)
                {
                    LogHelper.Logger.InfoFormat("Name={0},没有可推送的数据!", _se.Name);
                    return;
                }
                WebSKPFY.FService f = new WebSKPFY.FService();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string result = f.ReceiveFY_LGB(dt.Rows[i]["CONTENT"].ToString());
                    JObject tmpResult = (JObject)JsonConvert.DeserializeObject(result);
                    string pFLAG = (tmpResult.GetValue("FLAG") ?? "").ToString();
                    string pMSG = (tmpResult.GetValue("MSG") ?? "").ToString();

                    if (pFLAG == "0")
                    {
                        rSql = "update s_pushfy_000020 set sendTime = sysdate where JLBH = :pJLBH~pJLBH,NUMBER," + dt.Rows[i]["JLBH"].ToString();
                        SHOPDB_Center.DB2012Class_ExecOneCommand(rSql, ref rMsg);
                        if (rMsg != "") { LogHelper.Logger.ErrorFormat("Name={0},推送[" + dt.Rows[i]["JLBH"].ToString() + "]更新推送结果出错," + rMsg, _se.Name); }
                        else { LogHelper.Logger.InfoFormat("Name={0},推送[" + dt.Rows[i]["JLBH"].ToString() + "]成功", _se.Name); }

                    }
                    else
                    {
                        LogHelper.Logger.ErrorFormat("Name={0},推送[" + dt.Rows[i]["JLBH"].ToString() + "]出错," + result, _se.Name);

                    }
                }


            }
            catch (Exception ex)
            {
                LogHelper.Logger.ErrorFormat("Name={0},推送出错," + ex.Message, _se.Name); return;
            }
        }
        #endregion
    }
}