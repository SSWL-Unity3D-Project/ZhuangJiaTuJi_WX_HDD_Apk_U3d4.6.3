using System.Collections.Generic;
using UnityEngine;

public class SSGamePayUICtrl : MonoBehaviour
{
    /// <summary>
    /// 电视游戏支付类型.
    /// </summary>
    public enum TVGamePayState
    {
        Null = -1,
        /// <summary>
        /// 视博云安卓支付平台.
        /// </summary>
        ShiBoYunApk = 0,
        /// <summary>
        /// 咪咕电视安卓游戏支付平台.
        /// </summary>
        MiGuApk = 1,
    }

    /// <summary>
    /// 需要更新的UI界面.
    /// </summary>
    public UITexture m_UITexture;
    [System.Serializable]
    public class UIData
    {
        public TVGamePayState Type = TVGamePayState.Null;
        public Texture Img;
    }
    public List<UIData> m_UIData = new List<UIData>();

    void Start()
    {
        TVGamePayState type = XKGlobalData.GetInstance().m_TVGamePayType;
        UIData data = m_UIData.Find((dt) => { return dt.Type.Equals(type); });
        if (data != null)
        {
            if (m_UITexture != null && data.Img != null)
            {
                //更新不同支付平台下的UI界面.
                m_UITexture.mainTexture = data.Img;
            }
        }
    }
}