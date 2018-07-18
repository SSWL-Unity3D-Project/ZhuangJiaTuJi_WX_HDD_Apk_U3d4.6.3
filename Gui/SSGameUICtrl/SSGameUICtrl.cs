using UnityEngine;

/// <summary>
/// 游戏UI界面管理.
/// </summary>
public class SSGameUICtrl : SSGameMono
{
    /// <summary>
    /// UI中心锚点.
    /// </summary>
    public Transform m_UICenterTr;
    [HideInInspector]
    public SSFuHuoCiShuCtrl m_FuHuoCiShuCom;
    /// <summary>
    /// 创建复活次数UI界面.
    /// </summary>
    public void CreatFuHuoCiShuPanel()
    {

        GameObject gmDataPrefab = (GameObject)Resources.Load("Prefabs/GUI/FuHuoCiShu/FuHuoCiShu");
        if (gmDataPrefab != null)
        {
            UnityLog("CreatFuHuoCiShuPanel...");
            GameObject obj = (GameObject)Instantiate(gmDataPrefab, m_UICenterTr);
            m_FuHuoCiShuCom = obj.GetComponent<SSFuHuoCiShuCtrl>();
            ShowGameFuHuoCiShiInfo();
        }
        else
        {
            UnityLogWarning("CreatFuHuoCiShuPanel -> gmDataPrefab was null");
        }
    }

    public void ShowGameFuHuoCiShiInfo()
    {
        if (m_FuHuoCiShuCom == null)
        {
            UnityLogWarning("ShowGameFuHuoCiShiInfo -> m_FuHuoCiShuCom was null");
            return;
        }

        int jiHuoCiShu = XKGlobalData.GetInstance().m_JiHuoCiShu;
        int fuHuoCiShu = m_FuHuoCiShuCom.m_FuHuoCiShu - jiHuoCiShu;
        if (fuHuoCiShu > -1)
        {
			m_FuHuoCiShuCom.ShowPlayerFuHuoCiShu(fuHuoCiShu);
        }

        if (fuHuoCiShu <= 0)
        {
            RemoveErWeiMaUI();
            DaoJiShiCtrl.HiddenAllTVYaoKongEnterUI();
        }
    }

    /// <summary>
    /// 是否可以复活玩家.
    /// </summary>
    public bool GetIsFuHuoPlayer()
    {
        if (m_FuHuoCiShuCom == null)
        {
            UnityLogWarning("ShowGameFuHuoCiShiInfo -> m_FuHuoCiShuCom was null");
            return false;
        }

        bool isFuHuoPlayer = false;
        int jiHuoCiShu = XKGlobalData.GetInstance().m_JiHuoCiShu;
        int fuHuoCiShu = m_FuHuoCiShuCom.m_FuHuoCiShu - jiHuoCiShu;
        if (fuHuoCiShu > 0)
        {
            isFuHuoPlayer = true;
        }
        else
        {
            isFuHuoPlayer = false;
        }
        return isFuHuoPlayer;
    }

    /// <summary>
    /// 二维码UI.
    /// </summary>
    public ErWeiMaUI m_ErWeiMaUICom;
    /// <summary>
    /// 删除二维码UI.
    /// </summary>
    void RemoveErWeiMaUI()
    {
        if (m_ErWeiMaUICom != null)
        {
            m_ErWeiMaUICom.RemoveSelf();
        }
    }
}