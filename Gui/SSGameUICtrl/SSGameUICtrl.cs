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

    /// <summary>
    /// 是否继续包月支付.
    /// </summary>
    [HideInInspector]
    public SSGameBaoYueDlg m_GameBaoYueCom;
    /// <summary>
    /// 是否显示游戏包月UI界面.
    /// </summary>
    internal bool IsShowGameBaoYueUI = false;
    /// <summary>
    /// 创建是否包月支付UI界面.
    /// </summary>
    public void CreatGameBaoYuePanel()
    {
        GameObject gmDataPrefab = (GameObject)Resources.Load("Prefabs/GUI/GameBaoYue/GameBaoYueUI");
        if (gmDataPrefab != null)
        {
            UnityLog("CreatGameBaoYuePanel...");
            GameObject obj = (GameObject)Instantiate(gmDataPrefab, m_UICenterTr);
            m_GameBaoYueCom = obj.GetComponent<SSGameBaoYueDlg>();
            m_GameBaoYueCom.Init();
            IsShowGameBaoYueUI = true;
        }
        else
        {
            UnityLogWarning("CreatGameBaoYuePanel -> gmDataPrefab was null");
        }
    }

    /// <summary>
    /// 删除是否包月支付UI界面.
    /// </summary>
    public void RemoveGameBaoYuePanel()
    {
        if (m_GameBaoYueCom != null)
        {
            UnityLog("RemoveGameBaoYuePanel...");
            m_GameBaoYueCom.RemoveSelf();
            IsShowGameBaoYueUI = false;
        }
        else
        {
            UnityLogWarning("RemoveGameBaoYuePanel -> m_GameBaoYueCom was null");
        }
    }

    /// <summary>
    /// 是否继续包月支付.
    /// </summary>
    [HideInInspector]
    public SSGameJiXuBaoYueDlg m_GameJiXuBaoYueCom;
    /// <summary>
    /// 创建是否继续包月支付UI界面.
    /// </summary>
    public void CreatGameJiXuBaoYuePanel()
    {
        GameObject gmDataPrefab = (GameObject)Resources.Load("Prefabs/GUI/GameJiXuBaoYue/GameJiXuBaoYueUI");
        if (gmDataPrefab != null)
        {
            UnityLog("CreatGameJiXuBaoYuePanel...");
            GameObject obj = (GameObject)Instantiate(gmDataPrefab, m_UICenterTr);
            m_GameJiXuBaoYueCom = obj.GetComponent<SSGameJiXuBaoYueDlg>();
            m_GameJiXuBaoYueCom.Init();
            IsShowGameBaoYueUI = true;
        }
        else
        {
            UnityLogWarning("CreatGameJiXuBaoYuePanel -> gmDataPrefab was null");
        }
    }

    /// <summary>
    /// 删除是否继续包月支付UI界面.
    /// </summary>
    public void RemoveGameJiXuBaoYuePanel()
    {
        if (m_GameJiXuBaoYueCom != null)
        {
            UnityLog("RemoveGameJiXuBaoYuePanel...");
            m_GameJiXuBaoYueCom.RemoveSelf();
            IsShowGameBaoYueUI = false;
        }
        else
        {
            UnityLogWarning("RemoveGameJiXuBaoYuePanel -> m_GameJiXuBaoYueCom was null");
        }
    }
}