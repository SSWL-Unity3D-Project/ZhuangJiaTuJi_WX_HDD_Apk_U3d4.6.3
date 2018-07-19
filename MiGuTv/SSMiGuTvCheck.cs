<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;
=======
﻿#define SHOW_MIGU_MSG
using UnityEngine;
>>>>>>> f6bfdc6b3365e6c36abe2c3306cbac02ebec4c5c

public class SSMiGuTvCheck : MonoBehaviour
{
    /// <summary>
    /// 是否登陆了咪咕Tv支付账号.
    /// </summary>
    [HideInInspector]
    public bool IsLoginMiGuTv = false;
    /// <summary>
    /// 是否已经对游戏进行了咪咕包月.
    /// </summary>
    [HideInInspector]
    public bool IsHaveBaoYueGame = false;
    /// <summary>
    /// 是否已经包月超时.
    /// </summary>
    [HideInInspector]
    public bool IsBaoYueChaoShi = false;
    /// <summary>
    /// 初始化.
    /// </summary>
    public void Init()
    {
        IsLoginMiGuTv = false;

        MiGuTv_InterFace miGuTvInterFace = pcvr.GetInstance().m_MiGuTv_InterFace;
        if (miGuTvInterFace != null)
        {
            //初始化咪咕支付.
            miGuTvInterFace.MiGuTv_Initial();
            //待查包月信息.
            miGuTvInterFace.MiGuTv_OnMonthPay("001");
        }
    }

    /// <summary>
    /// 当玩家登陆咪咕TV支付平台.
    /// </summary>
    public void OnPlayerLoginMiGuTv()
    {
        IsLoginMiGuTv = true;
    }
    
    /// <summary>
    /// 当玩家退出咪咕TV支付平台.
    /// </summary>
    public void OnPlayerExitMiGuTv()
    {
        IsLoginMiGuTv = false;
    }
    
    /// <summary>
    /// 包月支付返回
    /// </summary>
    /// <param name="s">#号隔开 返回结果码#结果</param>
    public void OnMonthPayFinish(string s)
    {
        Debug.Log("Unity:" + "OnMonthPayFinish:" + s);
<<<<<<< HEAD
        TestCount++;
        TestMsg = "count == " + TestCount + ", msg == " + s;
=======
>>>>>>> f6bfdc6b3365e6c36abe2c3306cbac02ebec4c5c
    }

    /// <summary>
    /// 是否退出咪咕Tv支付.
    /// </summary>
    bool IsExitMiGuTvPay = false;
    /// <summary>
    /// 是游戏退出migujhzhifu
    /// </summary>
    void MakeGameExitMiGuTvPay()
    {
        if (!IsExitMiGuTvPay)
        {
            if (pcvr.GetInstance().m_MiGuTv_InterFace != null)
            {
                IsExitMiGuTvPay = true;
                pcvr.GetInstance().m_MiGuTv_InterFace.MiGuTv_MiGuExit();
            }
        }
    }

<<<<<<< HEAD
    string TestMsg = "";
    int TestCount = 0;
    void OnGUI()
    {
        GUI.Box(new Rect(5f, 5f, Screen.width - 10f, 35f), TestMsg);
    }
=======
    /// <summary>
    /// 收到咪咕支付的返回消息.
    /// </summary>
    /// <param name="s">#号隔开 返回结果码#结果</param>
    public void OnReceivedPayMsg(string s)
    {
#if SHOW_MIGU_MSG
        Debug.Log("Unity:" + "OnReceivedPayMsg:" + s);
        TestCount++;
        TestMsg += "count == " + TestCount + ", msg == " + s;
#endif
    }

#if SHOW_MIGU_MSG
    string TestMsg = "MiGuPayMsg: ";
    int TestCount = 0;
    void OnGUI()
    {
        GUI.Box(new Rect(5f, 5f, Screen.width - 10f, 350f), TestMsg);
    }
#endif
>>>>>>> f6bfdc6b3365e6c36abe2c3306cbac02ebec4c5c
}