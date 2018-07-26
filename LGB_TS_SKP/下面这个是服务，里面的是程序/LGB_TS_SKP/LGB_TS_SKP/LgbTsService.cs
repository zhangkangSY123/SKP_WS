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

namespace LGB_TS_SKP
{
    [Description("微信推送.")]
    [DisplayName("LgbTsService")]
    [Guid("7a0df670-96ba-4787-b39b-2df84715961d")]
    [Serializable]
    public class LgbTsService : IService
    {
        private ServiceEntity _se;
        private static string url = "http://58.83.209.43/WeChatServices/mainForm.ashx?SerType=sendTemplate&Data={0}";
        private DB2012Class DB_Center;
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
            string _PublicDBLink = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=198.20.7.161)(PORT=1521))(CONNECT_DATA=(SERVER = DEDICATED)(SERVICE_NAME=BUCENTER)));";
            string _PublicDBLinkUserPSW = "7ec0e239f1757b5bf2f54eba69443998395bbd406ac277a2507ea4651097434fcb021a4405cf973d7b4001f36b49a9b7";
            DB_Center = new DB2012Class(ref rMsg, _PublicDBLink, _PublicDBLinkUserPSW, "E2D4A6EF", true);
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
                //找推送报表的时间点，人员。
                //string selectAllTS = "select RID,CODE,SJD,SHOPCODE from BUCOWNER.BMCP_WeChat_Auth@BUCENTER ";
                //DataTable dt = new DataTable();
                //DB_Center.DB2012Class_GetDataRefTable(selectAllTS, ref rMsg, dt);
                //if (rMsg != "")
                //{
                //    LogHelper.Logger.ErrorFormat("查询推送报表和人员信息出错,", _se.Name);
                //    return;
                //}
                //if (dt.Rows.Count == 0)
                //{
                //    LogHelper.Logger.InfoFormat("没有要推送的报表和人员!", _se.Name);
                //    return;
                //}
                int hour = DateTime.Now.Hour;
                int Minute = DateTime.Now.Minute;
                string strT = DateTime.Now.ToString("s").Substring(0, DateTime.Now.ToString("s").IndexOf('T'));//2003-09-23 
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string rid = dt.Rows[i]["RID"].ToString();//报表id
                    string code = dt.Rows[i]["CODE"].ToString();//人员code
                    string sjd = dt.Rows[i]["SJD"].ToString();//时间点
                    string SHOPCODE = dt.Rows[i]["SHOPCODE"].ToString();//SHOPCODE
                    
                    //if (SHOPCODE == null || SHOPCODE != "000020")
                    //{
                    //    continue;
                    //}
                    string[] sjdsz = sjd.Split(',');
                    if (sjd != null && sjd != "" && sjdsz.Length > 0)
                    {
                        for (int j = 0; j < sjdsz.Length; j++)
                        {
                            if (hour.ToString() == sjdsz[j].ToString())
                            {//当前时间点 == 数据库中的时间
                                //if (Minute != 10)
                                //{
                                //    return;
                                //}

                                //找数据库中有没有插入，当前时间点，当前人员，当前报表的信息。

                                string selectHasSql = "select JLBH ,to_char(CREATETIME, 'YYYY-MM-dd hh24') CREATETIME,OPENID,RID from bmcp_wechat_send "
                                     + " where OPENID = (select OPENID from L_SHOP_USER where CODE = '" + code + "' ) "
                                     + " and to_char(CREATETIME,'YYYY-MM-dd hh24') = '" + (strT + " " + hour) + "'"
                                     + " and RID = " + rid;
                                DataTable dtHas = new DataTable();
                                DB_Center.DB2012Class_GetDataRefTable(selectHasSql, ref rMsg, dtHas);
                                if (rMsg != "")
                                {
                                    LogHelper.Logger.ErrorFormat("查询微信推送报表信息出错", _se.Name);
                                    return;
                                }
                                if (dtHas.Rows.Count != 0)
                                {
                                    //LogHelper.Logger.InfoFormat("已经添加过了不需要再次添加", _se.Name);
                                    continue;
                                }

                                string touser = "";
                                string template_id = "";
                                string url = "";
                                string shopCode2 = "";
                                string RBELONG = "";

                                //找推送报表，的RID，RURL，
                                string sel = "select bw.RID,bw.RNAME,bw.RURL,bw.TSQL,bw.SHOPCODE,(select SHOPNAME from L_SHOP where SHOPCODE=bw.SHOPCODE) SHOPNAME,bw.RBELONG,bt.TEMPLATE_ID "
                                    + " from BUCOWNER.BMCP_WeChat_Rule @BUCENTER bw,BUCOWNER.BMCP_WeChat_TEMPLATE @BUCENTER bt where bw.TID = bt.TID and bw.RSTATUS = 1 and bw.RID = " + rid;
                                DataTable dtt = new DataTable();
                                DB_Center.DB2012Class_GetDataRefTable(sel, ref rMsg, dtt);
                                if (rMsg != "")
                                {
                                    LogHelper.Logger.ErrorFormat("查询报表详细信息出错,编号为" + rid, _se.Name);
                                    return;
                                }
                                if (dtt.Rows.Count == 0)
                                {
                                    LogHelper.Logger.InfoFormat("没有编号为" + rid + "的报表详细信息!", _se.Name);
                                    continue;
                                }
                                url = dtt.Rows[0]["RURL"].ToString() + "?RQ=" + strT + "%26SJD=" + (hour + "00");//%26为url编码时&符号
                                shopCode2 = dtt.Rows[0]["SHOPCODE"].ToString();
                                template_id = dtt.Rows[0]["TEMPLATE_ID"].ToString();
                                string sql = dtt.Rows[0]["TSQL"].ToString().Replace("\"", "'");
                                string shopName = dtt.Rows[0]["SHOPNAME"].ToString();
                                RBELONG = dtt.Rows[0]["RBELONG"].ToString();
                                string RNAME = dtt.Rows[0]["RNAME"].ToString();

                                if (!SHOPCODE.Equals(shopCode2))
                                {
                                    continue;
                                }

                                //根据人员CODE,找openid
                                string sqlUser = "select CODE, OPENID from BUCOWNER.L_SHOP_USER@BUCENTER where CODE='" + code + "'";
                                DataTable dtUser = new DataTable();
                                DB_Center.DB2012Class_GetDataRefTable(sqlUser, ref rMsg, dtUser);
                                if (rMsg != "")
                                {
                                    LogHelper.Logger.ErrorFormat("查询人员账号为" + code + "详细信息出错,", _se.Name);
                                    return;
                                }
                                if (dtUser.Rows.Count == 0)
                                {
                                    LogHelper.Logger.InfoFormat("没有人员账号为" + code + "的详细信息!", _se.Name);
                                    continue;
                                }

                                if (dtUser.Rows[0]["OPENID"] == null || dtUser.Rows[0]["OPENID"].ToString() == "")
                                {
                                    LogHelper.Logger.InfoFormat("账号为" + code + "人员没有绑定微信账号，无法推送!", _se.Name);
                                    continue;
                                }
                                touser = dtUser.Rows[0]["OPENID"].ToString();//人员的openid
                                //组装推送的内容
                                string firstVl = shopName + "\\n" + strT + "  " + hour + "点" + RNAME;
                                string keyword1Vl = "";
                                string keyword2Vl = "";
                                string keyword3Vl = "";
                                DataTable dtyy = new DataTable();

                                string sqlIsNew = sql.Substring(0, sql.IndexOf('['));

                                sqlIsNew += (hour + "00");
                                SHOPDB_Center.DB2012Class_GetDataRefTable(sqlIsNew, ref rMsg, dtyy);
                                if (rMsg != "")
                                {
                                    LogHelper.Logger.ErrorFormat("查询出错,", _se.Name);
                                    return;
                                }
                                if (dtyy.Rows.Count == 0)
                                {
                                    keyword1Vl = "0元";
                                    keyword2Vl = "0笔";
                                    keyword3Vl = "\\n" + "客单价：0元";
                                }
                                else
                                {
                                    keyword1Vl = dtyy.Rows[0]["keyword1"].ToString() + "元";
                                    keyword2Vl = dtyy.Rows[0]["keyword2"].ToString() + "笔";
                                    keyword3Vl = "\\n" + dtyy.Rows[0]["keyword3"].ToString() + "元";
                                }

                                string all = "{\"touser\":\"" + touser + "\",\"template_id\":\"" + template_id + "\",\"url\":\"" + url
                                  + "\",\"data\":{\"first\":{\"value\":\"" + firstVl + "\" } ,\"keyword1\":{\"value\":\"" + keyword1Vl + "\" },\"keyword2\":{\"value\":\"" + keyword2Vl + "\" },\"keyword3\":{\"value\":\"" + keyword3Vl + "\" },\"remark\":{\"value\":\"点击了解详情\" } } }";


                                //找推送表的id
                                string tsjlbh = DB_Center.DB2012Class_SelectDataFromOne("SELECT bmcp_wechat_send_tb_seq.NEXTVAL FROM DUAL", ref rMsg);
                                if (rMsg != "")
                                {
                                    LogHelper.Logger.ErrorFormat("生成记录推送编号出错,", _se.Name);
                                    return;
                                }
                                //插入信息
                                string insertSql = "insert into bmcp_wechat_send(JLBH,shopcode,bmcp_lx,createtime,openid,sendcontent,RID) "
                                     + " values('" + tsjlbh + "','" + shopCode2 + "', '" + RBELONG + "', sysdate, '" + touser + "','" + all + "'," + rid + ") ";
                                DB_Center.DB2012Class_ExecOneCommand(insertSql, ref rMsg, true);
                                if (rMsg == "")
                                {
                                    LogHelper.Logger.InfoFormat("生成推送记录成功,编号为" + tsjlbh, _se.Name);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Logger.ErrorFormat("生成推送内容出错" + ex.Message, _se.Name); return;
            }
        }
        #endregion
    }
}