using UnityEngine;

public class ErWeiMaUI : MonoBehaviour
{
    /// <summary>
    /// 二维码UI.
    /// </summary>
    public UITexture m_ErWeiMaUI;
    // Use this for initialization
    void Start()
    {
        if (pcvr.GetInstance().m_SSBoxPostNet != null)
        {
            string url = pcvr.GetInstance().m_SSBoxPostNet.m_BoxLoginData.hDianDianGamePadUrl;
            m_ErWeiMaUI.mainTexture = pcvr.GetInstance().m_BarcodeCam.CreateErWeiMaImg(url);
        }
        else
        {
            Debug.LogWarning("Unity: m_SSBoxPostNet was null");
        }
    }
}