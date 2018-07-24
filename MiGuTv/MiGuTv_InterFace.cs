using UnityEngine;
using System.Collections;

/// <summary>
/// 咪咕TV充值接口
/// </summary>
public class MiGuTv_InterFace : MonoBehaviour
{

	/**
	* 初始化咪咕应用
	public void initializeApp()
	*/
	public void MiGuTv_Initial()
	{
#if UNITY_ANDROID && !UNITY_EDITOR

        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("initializeApp");
            }
        }
#endif
	}
	/**
	* 初始化咪咕应用
	* @param loginNo 游戏账号，需CP传入，可以为空
	public void initializeApp(String loginNo)
	*/
	public void MiGuTv_Initial(string loginNo)
	{
#if UNITY_ANDROID && !UNITY_EDITOR

        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("initializeApp",loginNo);
            }
        }
#endif
	}
	/**
	* 点播支付
	* @param billingIndex 计费点索引
	* @param extend 透传参数 仅支持字母大小写、数字和9个特殊字符 ! # * / = + - ,其中不能含有空格
	public void onCountPay(String billingIndex, String extend)
	*/
	public void MiGuTv_OnCountPay(string billingIndex,string extend)
	{
#if UNITY_ANDROID && !UNITY_EDITOR

        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("onCountPay",billingIndex,extend);
            }
        }
#endif
	}
	/**
	* 包月支付
	* @param  lvl 待查询包月等级
	public void onMonthPay(String lvl)
	*/
	public void MiGuTv_OnMonthPay(string Lv)
	{
#if UNITY_ANDROID && !UNITY_EDITOR

        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("onMonthPay",Lv);
            }
        }
#endif
	}
	/**
	*
	*调用咪咕接口退出游戏
	public void MiGuExit()
	*/
	public void MiGuTv_MiGuExit()
	{
#if UNITY_ANDROID && !UNITY_EDITOR

        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("MiGuExit");
            }
        }
#endif
	}

	/*
	==================接收支付反馈的函数=======================
	*/
	/// <summary>
	/// 点播支付返回
	/// </summary>
	/// <param name="s">#号隔开 返回结果码#购买项索引</param>
	public void MiGuTv_OnPayFinishFun(string s)
	{
		Debug.Log("Unity:"+ "MiGuTv_OnPayFinishFun:"+s);
        if (pcvr.GetInstance().m_SSMiGuTvCheck != null)
        {
            pcvr.GetInstance().m_SSMiGuTvCheck.OnPayFinish(s);
        }
    }
	/// <summary>
	/// 包月支付返回
	/// </summary>
	/// <param name="s">#号隔开 返回结果码#结果</param>
	public void MiGuTv_OnMonthPayFinishFun(string s)
	{
		Debug.Log("Unity:" + "MiGuTv_OnMonthPayFinishFun:" + s);
        if (pcvr.GetInstance().m_SSMiGuTvCheck != null)
        {
            pcvr.GetInstance().m_SSMiGuTvCheck.OnMonthPayFinish(s);
        }
	}

    /// <summary>
    /// 退出返回
    /// </summary>
    /// <param name="s">#号隔开 返回结果#userexit;usercancelexit</param>
    public void MiGuTv_UserExitFun(string s)
    {
        Debug.Log("Unity:" + "MiGuTv_UserExitFun:" + s);
        if (pcvr.GetInstance().m_SSMiGuTvCheck != null)
        {
            pcvr.GetInstance().m_SSMiGuTvCheck.OnExitMiGuPaySDK(s);
        }
    }
}