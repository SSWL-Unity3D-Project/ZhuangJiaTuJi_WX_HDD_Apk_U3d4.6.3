
//#define SHOW_MIGU_MSG
using UnityEngine;

public class SSMiGuTvCheck : MonoBehaviour
{
    /// <summary>
    /// 是否登陆了咪咕Tv支付账号.
    /// </summary>
    //[HideInInspector]
    //public bool IsLoginMiGuTv = false;
    /// <summary>
    /// 是否已经对游戏进行了咪咕包月.
    /// </summary>
    [HideInInspector]
    public bool IsHaveBaoYueGame = false;
    /// <summary>
    /// 是否已经包月超时.
    /// </summary>
    //[HideInInspector]
    //public bool IsBaoYueChaoShi = false;
    /// <summary>
    /// 初始化.
    /// </summary>
    public void Init()
    {
        //IsLoginMiGuTv = false;
        IsDisplayMiGuPayUI = false;

        MiGuTv_InterFace miGuTvInterFace = pcvr.GetInstance().m_MiGuTv_InterFace;
        if (miGuTvInterFace != null)
        {
            //初始化咪咕支付.
            //miGuTvInterFace.MiGuTv_Initial();
            miGuTvInterFace.MiGuTv_Initial("");
            OnReceivedMiGuPayMsg("初始化咪咕支付");
        }
    }

    bool _IsDisplayMiGuPayUI = false;
    /// <summary>
    /// 是否显示了咪咕支付UI界面.
    /// </summary>
    [HideInInspector]
    public bool IsDisplayMiGuPayUI
    {
        set
        {
            _IsDisplayMiGuPayUI = value;
            if (_IsDisplayMiGuPayUI)
            {
                XKPlayerMoveCtrl.OpenAllPlayerWuDiTeXiao();
            }
            else
            {
                XKPlayerMoveCtrl.CloseAllPlayerWuDiTeXiao();
            }
        }
        get { return _IsDisplayMiGuPayUI; }
    }
    /// <summary>
    /// 查询游戏包月状态.
    /// </summary>
    public void QueryGameBaoYueState()
    {
        if (IsHaveBaoYueGame)
        {
            //玩家已经对游戏进行了包月.
            return;
        }

        MiGuTv_InterFace miGuTvInterFace = pcvr.GetInstance().m_MiGuTv_InterFace;
        if (miGuTvInterFace != null)
        {
            //待查包月信息.
            //miGuTvInterFace.MiGuTv_OnMonthPay("001");
            string dingDanId = "1234567890123456";
            if (pcvr.GetInstance() != null
                && pcvr.GetInstance().m_GameMiGuBaoYuePostNet != null)
            {
                //包月订单信息查询.
                dingDanId = pcvr.GetInstance().m_GameMiGuBaoYuePostNet.m_BaoYueDingDanData.orderId;
            }
            pcvr.GetInstance().AddDebugMsg("dingDanId == " + dingDanId);
            miGuTvInterFace.MiGuTv_OnCountPay("001", dingDanId);
            OnReceivedMiGuPayMsg("待查包月信息");
            IsDisplayMiGuPayUI = true;
        }
    }

    /// <summary>
    /// 是否在进行游戏包月检测.
    /// </summary>
    bool IsQueryGameBaoYueState = false;
    SSTimeUpCtrl m_TimeUpQueryGameBaoYueStateCom;
    /// <summary>
    /// 延迟一段时间后检测游戏包月状态.
    /// </summary>
    public void DelayQueryGameBaoYueState()
    {
        if (IsHaveBaoYueGame)
        {
            //玩家已经对游戏进行了包月,不需要继续查询包月状态.
            return;
        }

        if (IsQueryGameBaoYueState)
        {
            return;
        }
        IsQueryGameBaoYueState = true;

        m_TimeUpQueryGameBaoYueStateCom = gameObject.AddComponent<SSTimeUpCtrl>();
        //60秒后开始查询游戏是否进行了包月.
        //m_TimeUpQueryGameBaoYueStateCom.Init(60f);
        m_TimeUpQueryGameBaoYueStateCom.Init(10f); //test.
        m_TimeUpQueryGameBaoYueStateCom.OnTimeUpOverEvent += OnTimeUpQueryGameBaoYueStateEvent;
    }

    /// <summary>
    /// 开始查询游戏是否进行了包月.
    /// </summary>
    private void OnTimeUpQueryGameBaoYueStateEvent()
    {
        //查询游戏包月状态.
        //QueryGameBaoYueState();

        //创建是否选择游戏包月界面.
        if (XkGameCtrl.GetInstance() != null
            && XkGameCtrl.GetInstance().m_GameUICom != null)
        {
            XkGameCtrl.GetInstance().m_GameUICom.CreatGameBaoYuePanel();
        }
    }

    /// <summary>
    /// 关闭游戏包月检测.
    /// </summary>
    public void CloseQueryGameBaoYueState()
    {
        IsQueryGameBaoYueState = false;
        if (m_TimeUpQueryGameBaoYueStateCom != null)
        {
            //删除延时脚本.
            Destroy(m_TimeUpQueryGameBaoYueStateCom);
        }
    }

    /// <summary>
    /// 当玩家登陆咪咕TV支付平台.
    /// </summary>
    //public void OnPlayerLoginMiGuTv()
    //{
    //    IsLoginMiGuTv = true;
    //}
    
    /// <summary>
    /// 当玩家退出咪咕TV支付平台.
    /// </summary>
    //public void OnPlayerExitMiGuTv()
    //{
    //    IsLoginMiGuTv = false;
    //}

    /// <summary>
    /// 点播支付返回.
    /// </summary>
    /// <param name="s">#号隔开 返回结果码#结果</param>
    public void OnPayFinish(string s)
    {
        Debug.Log("Unity: OnPayFinish:" + s);
        OnReceivedMiGuPayMsg("点播支付返回消息:" + s);

        //包月支付界面已经关闭.
        IsDisplayMiGuPayUI = false;
        //咪咕电视游戏包月查询完毕.
        IsQueryGameBaoYueState = false;

        string[] msgArray = s.Split('#');
        MonthPayState type = MonthPayState.Failed;
        if (msgArray[0] == "1")
        {
            //咪咕电视游戏包月支付订购成功.
            type = MonthPayState.Succeess;
        }

        switch (type)
        {
            case MonthPayState.Succeess:
                {
                    //游戏包月成功或游戏已经进行过包月.
                    IsHaveBaoYueGame = true;
                    break;
                }
            case MonthPayState.Failed:
                {
                    //游戏包月失败.
                    //使游戏返回循环动画入口界面.
                    //XkGameCtrl.IsLoadingLevel = false;
                    //XkGameCtrl.LoadingGameMovie();

                    //包月支付失败,创建是否继续包月.
                    if (XkGameCtrl.GetInstance() != null
                        && XkGameCtrl.GetInstance().m_GameUICom != null)
                    {
                        XkGameCtrl.GetInstance().m_GameUICom.CreatGameJiXuBaoYuePanel();
                    }
                    break;
                }
        }

        OnReceivedMiGuPayMsg("CountPayState == " + type);
    }

    /// <summary>
    /// 包月支付返回状态.
    /// </summary>
    enum MonthPayState
    {
        Succeess = 0,
        Failed = 1,
    }

    /// <summary>
    /// 包月支付返回.
    /// </summary>
    /// <param name="s">#号隔开 返回结果码#结果</param>
    public void OnMonthPayFinish(string s)
    {
        Debug.Log("Unity: OnMonthPayFinish:" + s);
        OnReceivedMiGuPayMsg("包月支付返回消息:" + s);
        //包月支付界面已经关闭.
        IsDisplayMiGuPayUI = false;
        //咪咕电视游戏包月查询完毕.
        IsQueryGameBaoYueState = false;

        string[] msgArray = s.Split('#');
        MonthPayState type = MonthPayState.Failed;
        if (msgArray[0] == "1")
        {
            //咪咕电视游戏包月支付订购成功.
            type = MonthPayState.Succeess;
        }

        switch (type)
        {
            case MonthPayState.Succeess:
                {
                    //游戏包月成功或游戏已经进行过包月.
                    IsHaveBaoYueGame = true;
                    break;
                }
            case MonthPayState.Failed:
                {
                    //游戏包月失败.
                    //使游戏返回循环动画入口界面.
                    XkGameCtrl.IsLoadingLevel = false;
                    XkGameCtrl.LoadingGameMovie();
                    break;
                }
        }
        OnReceivedMiGuPayMsg("MonthPayState == " + type);
    }

    enum ExitMiGuSDKState
    {
        /// <summary>
        /// 确认退出咪咕电视游戏.
        /// </summary>
        Confirm = 0,
        /// <summary>
        /// 取消退出咪咕电视游戏.
        /// </summary>
        Cancel = 1,
    }
    /// <summary>
    /// 退出咪咕SDK反回消息.
    /// </summary>
    /// <param name="s">#号隔开 返回结果码#结果</param>
    public void OnExitMiGuPaySDK(string s)
    {
        Debug.Log("Unity: OnExitMiGuPaySDK:" + s);
        OnReceivedMiGuPayMsg("退出咪咕SDK反回消息:" + s);

        //退出咪咕电视游戏的界面已经关闭了.
        IsExitMiGuTvPay = false;
        ExitMiGuSDKState type = ExitMiGuSDKState.Confirm;
        if (s == "usercancelexit")
        {
            type = ExitMiGuSDKState.Cancel;
        }

        switch (type)
        {
            case ExitMiGuSDKState.Confirm:
                {
                    //确认退出咪咕电视游戏.
                    Application.Quit();
                    break;
                }
            case ExitMiGuSDKState.Cancel:
                {
                    //取消退出咪咕电视游戏.
                    break;
                }
        }
        OnReceivedMiGuPayMsg("ExitMiGuSDKState == " + type);
    }

    /// <summary>
    /// 是否退出咪咕Tv支付.
    /// </summary>
    bool IsExitMiGuTvPay = false;
    /// <summary>
    /// 使游戏退出咪咕支付.
    /// </summary>
    public void MakeGameExitMiGuTvPay()
    {
        if (!IsExitMiGuTvPay)
        {
            if (pcvr.GetInstance().m_MiGuTv_InterFace != null)
            {
                IsExitMiGuTvPay = true;
                pcvr.GetInstance().m_MiGuTv_InterFace.MiGuTv_MiGuExit();
                OnReceivedMiGuPayMsg("退出咪咕支付SDK");
            }
        }
    }
    
    /// <summary>
    /// 收到咪咕支付的返回消息.
    /// </summary>
    /// <param name="s">#号隔开 返回结果码#结果</param>
    void OnReceivedMiGuPayMsg(string s)
    {
        if (pcvr.GetInstance() != null)
        {
            pcvr.GetInstance().AddDebugMsg(s);
        }
#if SHOW_MIGU_MSG
        Debug.Log("Unity:" + "OnReceivedPayMsg:" + s);
        TestCount++;
        TestMsg += " count == " + TestCount + ", msg == " + s;
#endif
    }

#if SHOW_MIGU_MSG
    string TestMsg = "MiGuPayMsg:";
    int TestCount = 0;
    void OnGUI()
    {
        Rect rt = new Rect(5f, 5f, Screen.width - 10f, 350f);
        GUI.Box(rt, "");
        GUI.Label(rt, TestMsg);
    }
#endif
}