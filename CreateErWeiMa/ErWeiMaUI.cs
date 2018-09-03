﻿using System.Collections;
using UnityEngine;

public class ErWeiMaUI : MonoBehaviour
{
    static ErWeiMaUI _Instance;
    public static ErWeiMaUI GetInstance()
    {
        return _Instance;
    }

    /// <summary>
    /// UI摄像机.
    /// </summary>
    public Camera m_Camera;
    /// <summary>
    /// 二维码UI.
    /// </summary>
    public UITexture m_ErWeiMaUI;
    /// <summary>
    /// 截图起点.
    /// </summary>
    public Transform m_StartTr;
    /// <summary>
    /// 截图终点.
    /// </summary>
    public Transform m_EndTr;
    // Use this for initialization
    void Start()
    {
        _Instance = this;
        if (pcvr.IsHongDDShouBing == false)
        {
            //不是红点点微信手柄版本游戏.
            return;
        }

        switch (pcvr.GetInstance().m_WXShouBingType)
        {
            case SSBoxPostNet.WeiXinShouBingEnum.H5:
                {
                    LoadGameWXPadH5ErWeiMa();
                    break;
                }
            case SSBoxPostNet.WeiXinShouBingEnum.XiaoChengXu:
                {
                    if (pcvr.GetInstance().m_BarcodeCam.m_ErWeuMaImg != null)
                    {
                        //直接加载微信小程序二维码.
                        ReloadGameWXPadXiaoChengXuErWeiMa();
                    }
                    else
                    {
                        //先隐藏二维码.
                        gameObject.SetActive(false);
                    }
                    break;
                }
        }
    }

    /// <summary>
    /// 加载微信虚拟手柄H5程序二维码.
    /// </summary>
    void LoadGameWXPadH5ErWeiMa()
    {
        if (pcvr.GetInstance().m_WXShouBingType != SSBoxPostNet.WeiXinShouBingEnum.H5)
        {
            //不是采用微信虚拟手柄H5程序.
            return;
        }

        try
        {
            if (pcvr.IsHongDDShouBing)
            {
                if (pcvr.GetInstance().m_SSBoxPostNet != null)
                {
                    if (pcvr.GetInstance().m_BarcodeCam.m_ErWeuMaImg == null)
                    {
                        string url = pcvr.GetInstance().m_SSBoxPostNet.m_BoxLoginData.hDianDianGamePadUrl;
                        m_ErWeiMaUI.mainTexture = pcvr.GetInstance().m_BarcodeCam.CreateErWeiMaImg(url);
                        StartCoroutine(CaptureScreenshot2());
                    }
                    else
                    {
                        m_ErWeiMaUI.mainTexture = pcvr.GetInstance().m_BarcodeCam.m_ErWeuMaImg;
                    }
                }
                else
                {
                    Debug.LogWarning("Unity: m_SSBoxPostNet was null");
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("ex -> " + ex);
        }
    }


    /// <summary>
    /// 重新加载微信虚拟游戏手柄二维码.
    /// 该二维码是微信小程序二维码.
    /// </summary>
    public void ReloadGameWXPadXiaoChengXuErWeiMa()
    {
        if (pcvr.GetInstance().m_WXShouBingType != SSBoxPostNet.WeiXinShouBingEnum.XiaoChengXu)
        {
            //不是采用微信小程序虚拟手柄.
            return;
        }

        try
        {
            if (pcvr.IsHongDDShouBing)
            {
                if (pcvr.GetInstance().m_SSBoxPostNet != null)
                {
                    if (pcvr.GetInstance().m_BarcodeCam.m_ErWeuMaImg == null)
                    {
                        string url = pcvr.GetInstance().m_SSBoxPostNet.m_BoxLoginData.hDianDianGamePadUrl;
                        pcvr.GetInstance().m_SSBoxPostNet.DelayReloadWeiXinXiaoChengXuErWeiMa(m_ErWeiMaUI);
                    }
                    else
                    {
                        m_ErWeiMaUI.mainTexture = pcvr.GetInstance().m_BarcodeCam.m_ErWeuMaImg;
                    }
                }
                else
                {
                    Debug.LogWarning("Unity: m_SSBoxPostNet was null");
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("ex -> " + ex);
        }
    }
    
    IEnumerator CaptureScreenshot2()
    {
        Vector2 imgDt = Vector2.zero;
        imgDt.x = (Screen.width * m_ErWeiMaUI.width) / 1360f;
        imgDt.y = (Screen.height * m_ErWeiMaUI.height) / 768f;

        Vector3 startPos = m_Camera.WorldToScreenPoint(m_StartTr.position);
        Vector3 endPos = m_Camera.WorldToScreenPoint(m_EndTr.position);
        Rect rect = new Rect(startPos.x, startPos.y - 2f, Mathf.Abs(endPos.x - startPos.x) + 4f, Mathf.Abs(endPos.y - startPos.y) + 4f);

        //Vector3 pos = m_Camera.WorldToScreenPoint(transform.position);
        //pos.x = pos.x - 0.5f * imgDt.x;
        //pos.y = pos.y - 0.5f * imgDt.y;

        //Vector2 offset = new Vector2(10f, 10f);
        //offset.x = (Screen.width * offset.x) / 1360f;
        //offset.y = (Screen.height * offset.y) / 768f;
        //Rect rect = new Rect(pos.x + offset.x, pos.y + offset.y, imgDt.x - (2f * offset.x), imgDt.y - (2f * offset.y));
        //Debug.Log("rect == " + rect);

        yield return new WaitForEndOfFrame();
        // 先创建一个的空纹理，大小可根据实现需要来设置.
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        // 读取屏幕像素信息并存储为纹理数据，
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();    
        m_ErWeiMaUI.mainTexture = screenShot;
        pcvr.GetInstance().m_BarcodeCam.m_ErWeuMaImg = screenShot;
    }

    bool IsRemoveSelf = false;
    public void RemoveSelf()
    {
        if (!IsRemoveSelf)
        {
            IsRemoveSelf = true;
            Destroy(gameObject);
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.P))
    //    {
    //        ReloadGameWXPadErWeiMa();
    //    }
    //}

    //#if UNITY_EDITOR
    //    void OnGUI()
    //    {
    //        Vector3 startPos = m_Camera.WorldToScreenPoint(m_StartTr.position);
    //        Vector3 endPos = m_Camera.WorldToScreenPoint(m_EndTr.position);
    //        GUI.Box(new Rect(startPos.x, Screen.height - startPos.y, 4f, 4f), "");
    //        GUI.Box(new Rect(endPos.x, Screen.height -  endPos.y, 4f, 4f), "");
    //    }
    //#endif
}