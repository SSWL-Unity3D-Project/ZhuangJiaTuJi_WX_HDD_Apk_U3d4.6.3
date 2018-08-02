//#define USE_BAO_YUE_TEST_ADD
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using UnityEngine;

/// <summary>
/// 移动咪咕电视游戏包月信息查询和记录组件.
/// </summary>
public class SSGameMiGuBaoYuePostNet : MonoBehaviour
{
/** ******************************************************************
测试网址
计费IP：120.76.43.228
计费通知url：http://game.hdiandian.com/gamePayItemOrder/migu/paid
同步USER：http://game.hdiandian.com/gamePayItemOrder/migu/userid

正式网址
计费IP：120.76.42.79
计费通知url：http://h5.hdiandian.com/gamePayItemOrder/migu/paid
同步USER：http://h5.hdiandian.com/gamePayItemOrder/migu/userid

判断用户是否支付：game.hdiandian.com/userItemRecord/checkUserItemStatus
用户是否支付，需要的参数有：channel,itemId,belongMark，参数不能为NULL
获取订单ID：game.hdiandian.com/userItemRecord/getOrderInfo
获取订单ID需要的参数：channel,itemId,mac,payUserId

channel：渠道号
itemId：用户付费项ID（红点点提供）
belongMark：用户标识（如果没有，就是Mac地址）
payUserId:用户标识，可以为null
********************************************************************/
    public class GameBaoYueData
    {
#if USE_BAO_YUE_TEST_ADD
        //包月测试网址.
        /// <summary>
        /// 计费IP.
        /// </summary>
        public string urlJiFeiIP = "120.76.43.228";
        /// <summary>
        /// 计费通知url.
        /// </summary>
        public string urlJiFeiTongZhi = "http://game.hdiandian.com/gamePayItemOrder/migu/paid";
        /// <summary>
        /// 同步USER的Url.
        /// </summary>
        public string urlTongBuUser = "http://game.hdiandian.com/gamePayItemOrder/migu/userid";
        /// <summary>
        /// Url域名开头.
        /// </summary>
        public string urlHead = "http://game.hdiandian.com";
#else
        //包月正式网址.
        /// <summary>
        /// 计费IP.
        /// </summary>
        public string urlJiFeiIP = "120.76.42.79";
        /// <summary>
        /// 计费通知url.
        /// </summary>
        public string urlJiFeiTongZhi = "http://h5.hdiandian.com/gamePayItemOrder/migu/paid";
        /// <summary>
        /// 同步USER的Url.
        /// </summary>
        public string urlTongBuUser = "http://h5.hdiandian.com/gamePayItemOrder/migu/userid";
        /// <summary>
        /// Url域名开头.
        /// </summary>
        public string urlHead = "http://h5.hdiandian.com";
#endif
        /// <summary>
        /// 判断用户是否支付url结尾.
        /// </summary>
        public string urlBaoYueChaXunEnd = "/userItemRecord/checkUserItemStatus";
        /// <summary>
        /// 判断用户是否支付url.
        /// </summary>
        public string urlBaoYueChaXun = "";
        /// <summary>
        /// 包月订单ID获取url结尾.
        /// </summary>
        public string urlDingDanIDEnd = "/userItemRecord/getOrderInfo";
        /// <summary>
        /// 包月订单ID获取url.
        /// </summary>
        public string urlDingDanID = "";
        /// <summary>
        /// 渠道号.
        /// </summary>
        public string channel = "migu";
        /// <summary>
        /// 用户付费项ID（红点点提供）
        /// 006147156001
        /// 填001就可以了.
        /// </summary>
        public string itemId = "001";
        /// <summary>
        /// 用户标识（如果没有，就是Mac地址）
        /// </summary>
        public string belongMark = "";
        /// <summary>
        /// Mac地址
        /// </summary>
        public string mac = "";
        /// <summary>
        /// 用户标识，可以为null.
        /// </summary>
        public string payUserId = "";
        public GameBaoYueData()
        {
            try
            {
                urlBaoYueChaXun = urlHead + urlBaoYueChaXunEnd;
                urlDingDanID = urlHead + urlDingDanIDEnd;
                //NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
                //foreach (NetworkInterface ni in nis)
                //{
                //    Debug.Log("------------------------------------------------");
                //    Debug.Log("Name = " + ni.Name);
                //    Debug.Log("Des = " + ni.Description);
                //    Debug.Log("Type = " + ni.NetworkInterfaceType.ToString());
                //    Debug.Log("Mac地址 = " + ni.GetPhysicalAddress().ToString());
                //    Debug.Log("------------------------------------------------");
                //    belongMark = mac = ni.GetPhysicalAddress().ToString();
                //    break;
                //}
                //wifi.getConnectionInfo()

                //string info = "";
                //if (Application.internetReachability == NetworkReachability.NotReachable)
                //    info = "当前网络：不可用";
                //else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
                //    info = "当前网络：3G/4G";
                //else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                //    info = "当前网络 : WIFI";
                if (pcvr.GetInstance() != null
                    && pcvr.GetInstance().m_MiGuTv_InterFace != null)
                {
                    belongMark = mac = pcvr.GetInstance().m_MiGuTv_InterFace.MiGuTv_GetNetMACInfo();
                    //belongMark = mac = "12:34:56:78:90:12"; //test
                }
                else
                {
                    belongMark = mac = "12:34:56:78:90:12";
                }
            }
            catch (Exception ex)
            {
                if (pcvr.GetInstance() != null)
                {
                    pcvr.GetInstance().AddDebugMsg(ex.ToString());
                }
            }
        }
    }
    /// <summary>
    /// 包月查询的数据信息.
    /// </summary>
    public GameBaoYueData m_GameBaoYueData;

    /// <summary>
    /// post命令消息.
    /// </summary>
    enum PostCmd
    {
        /// <summary>
        /// 包月查询.
        /// </summary>
        BaoYueChaXun = 0,
        /// <summary>
        /// 包月订单ID获取.
        /// </summary>
        BaoYueDingDanIDGet = 1,
    }

    /// <summary>
    /// 包月支付状态.
    /// </summary>
    enum BaoYueState
    {
        /// <summary>
        /// 包月支付成功.
        /// </summary>
        Success = 0,
    }

    /// <summary>
    /// 包月查询得到的数据.
    /// </summary>
    class BaoYueChaXunData
    {
        public string code = "";
        public string msg = "";
        /// <summary>
        /// 用户体验时间.
        /// </summary>
        public string validTime = "";
        public string experienceTime = "";
    }
    BaoYueChaXunData m_BaoYueChaXunData;
    
    /// <summary>
    /// 包月订单数据信息.
    /// </summary>
    public class BaoYueDingDanData
    {
        /// <summary>
        /// 0：正常，其他表示异常.
        /// </summary>
        public string code = "";
        /// <summary>
        /// 状态说明.
        /// </summary>
        public string msg = "";
        /// <summary>
        /// 订单金额.
        /// </summary>
        public string amount = "";
        /// <summary>
        /// 服务器生成的订单编号.
        /// </summary>
        public string orderId = "";
    }
    internal BaoYueDingDanData m_BaoYueDingDanData;

    /// <summary>
    /// 初始化.
    /// </summary>
    internal void Init()
    {
        if (m_BaoYueChaXunData == null)
        {
            m_BaoYueChaXunData = new BaoYueChaXunData();
        }

        if (m_BaoYueDingDanData == null)
        {
            m_BaoYueDingDanData = new BaoYueDingDanData();
        }

        if (m_GameBaoYueData == null)
        {
            m_GameBaoYueData = new GameBaoYueData();
            //玩家是否包月的信息查询.
            HttpSendPostGameBaoYueChaXun();
            //包月订单信息查询.
            //HttpSendPostGetDingDanIDGameBaoYue(); //test
        }
    }

    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.P))
    //    {
    //        //test
    //        HttpSendPostGameBaoYueChaXun();
    //        HttpSendPostGetDingDanIDGameBaoYue();
    //    }
    //}

    /// <summary>
    /// Post网络数据.
    /// </summary>
    IEnumerator SendPost(string _url, JsonData jData, PostCmd cmd)
    {
        Debug.Log("Unity: url == " + _url);
        byte[] postBytes = Encoding.Default.GetBytes(jData.ToJson());
        Dictionary<string, string> header = new Dictionary<string, string>();
        //设置Content-Type.
        header["Content-Type"] = "application/json";
        WWW postData = new WWW(_url, postBytes, header);
        yield return postData;
        if (postData.error != null)
        {
            Debug.Log("Unity:" + "PostError: " + postData.error);
        }
        else
        {
            Debug.Log("Unity:" + cmd + " -> PostData: " + postData.text);
            if (pcvr.GetInstance() != null)
            {
                pcvr.GetInstance().AddDebugMsg(cmd + " -> PostData: " + postData.text);
            }

            switch (cmd)
            {
                case PostCmd.BaoYueChaXun:
                    {
                        //咪咕包月查询.
                        /** ***************************************************************
                        ##### 3.1.1 返回值说明
                        参数类型 | 参数说明
                        ---|---
                        code | 0：用户支付成功，其他返回值表示支付失败，一般只需要判断是否为0即可
                        validTime | 
                        parameters | 用户体验时间
                        ******************************************************************/
                        //PostData: {"code":3,"msg":"未支付","data":{"validTime":2592000,"parameters":"{\"experienceTime\":0}"}}
                        JsonData jd = JsonMapper.ToObject(postData.text);
                        if (m_BaoYueChaXunData != null)
                        {
                            m_BaoYueChaXunData.code = jd["code"].ToString();
                            m_BaoYueChaXunData.msg = jd["msg"].ToString();
                            m_BaoYueChaXunData.validTime = jd["data"]["validTime"].ToString();
                            //m_BaoYueChaXunData.experienceTime = jd["data"]["parameters"]["experienceTime"].ToString();
                        }

                        if (Convert.ToInt32(jd["code"].ToString()) == (int)BaoYueState.Success)
                        {
                            Debug.Log("Unity:" + "Player have baoYue! code == " + jd["code"]);
                        }
                        else
                        {
                            Debug.Log("Unity:" + "Player have not baoYue! code == " + jd["code"]);
                        }
                        break;
                    }
                case PostCmd.BaoYueDingDanIDGet:
                    {
                        //获取包月订单ID.
                        /** ****************************************
                        ##### 3.2.1
                        参数类型 | 参数说明
                        ---|---
                        amount | 订单金额
                        orderId | 服务器生成的订单编号
                        code | 0：正常，其他表示异常
                        msg | 状态说明
                        ********************************************/
                        //PostData: {"code":0,"msg":"","data":{"amount":0.01,"orderId":10000055}}
                        JsonData jd = JsonMapper.ToObject(postData.text);
                        if (m_BaoYueDingDanData != null)
                        {
                            m_BaoYueDingDanData.code = jd["code"].ToString();
                            m_BaoYueDingDanData.msg = jd["msg"].ToString();
                            m_BaoYueDingDanData.amount = jd["data"]["amount"].ToString();
                            m_BaoYueDingDanData.orderId = jd["data"]["orderId"].ToString();
                        }
                        break;
                    }
            }
        }
    }

    /// <summary>
    /// 发送玩家的游戏包月查询消息.
    /// 查看玩家是否对游戏进行了包月.
    /// </summary>
    public void HttpSendPostGameBaoYueChaXun()
    {
        Debug.Log("Unity:" + "HttpSendPostGameBaoYueChaXun...");
        //POST方法.
        JsonData data = new JsonData();
        data["channel"] = m_GameBaoYueData.channel;
        data["itemId"] = m_GameBaoYueData.itemId;
        data["belongMark"] = m_GameBaoYueData.belongMark;
        string info = "channel == " + m_GameBaoYueData.channel
            + ", itemId == " + m_GameBaoYueData.itemId
            + ", belongMark == " + m_GameBaoYueData.belongMark;
        Debug.Log(info);

        if (pcvr.GetInstance() != null)
        {
            pcvr.GetInstance().AddDebugMsg(info);
        }
        StartCoroutine(SendPost(m_GameBaoYueData.urlBaoYueChaXun, data, PostCmd.BaoYueChaXun));
    }

    /// <summary>
    /// 发送获取游戏包月订单ID的查询消息.
    /// </summary>
    public void HttpSendPostGetDingDanIDGameBaoYue()
    {
        if (m_BaoYueDingDanData != null
            && m_BaoYueDingDanData.code != ""
            && Convert.ToInt32(m_BaoYueDingDanData.code) == (int)BaoYueState.Success
            && m_BaoYueDingDanData.orderId != "")
        {
            //包月订单信息已经获取过,无需再次请求信息.
            return;
        }

        Debug.Log("Unity:" + "HttpSendPostGetDingDanGameBaoYue...");
        //POST方法.
        JsonData data = new JsonData();
        data["channel"] = m_GameBaoYueData.channel;
        data["itemId"] = m_GameBaoYueData.itemId;
        data["mac"] = m_GameBaoYueData.mac;
        data["payUserId"] = m_GameBaoYueData.payUserId;
        Debug.Log("channel == " + m_GameBaoYueData.channel
            + ", itemId == " + m_GameBaoYueData.itemId
            + ", mac == " + m_GameBaoYueData.mac
            + ", payUserId == " + m_GameBaoYueData.payUserId);
        StartCoroutine(SendPost(m_GameBaoYueData.urlDingDanID, data, PostCmd.BaoYueDingDanIDGet));
    }

    /// <summary>
    /// 获取玩家是否对游戏进行了包月.
    /// </summary>
    public bool GetPlayerIsBaoYueGame()
    {
        bool isBaoYueGame = false;
        string code = "1";
        if (m_BaoYueChaXunData != null)
        {
            code = m_BaoYueChaXunData.code;
        }

        if (Convert.ToInt32(code) == (int)BaoYueState.Success)
        {
            Debug.Log("Unity: GetPlayerIsBaoYueGame -> " + "Player have baoYue! code == " + code);
            isBaoYueGame = true;
        }
        else
        {
            Debug.Log("Unity: GetPlayerIsBaoYueGame -> " + "Player have not baoYue! code == " + code);
        }
        return isBaoYueGame;
    }
}