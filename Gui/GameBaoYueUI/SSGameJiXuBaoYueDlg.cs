using UnityEngine;

public class SSGameJiXuBaoYueDlg : MonoBehaviour
{
    public Vector3 m_BigScale = new Vector3(1.2f, 1.2f, 1f);
    public Vector3 m_SmallScale = Vector3.one;
    /// <summary>
    /// 确定按键的闪烁UI.
    /// </summary>
    public GameObject m_QueDingFlashObj;
    public UITexture QueDingUI;
    /// <summary>
    /// QueDingImg[0] 确定弹起.
    /// QueDingImg[1] 确定按下.
    /// </summary>
    public Texture[] QueDingImg;
    /// <summary>
    /// 返回按键的闪烁UI.
    /// </summary>
    public GameObject m_QuXiaoFlashObj;
    public UITexture QuXiaoUI;
    /// <summary>
    /// QuXiaoImg[0] 取消弹起.
    /// QuXiaoImg[1] 取消按下.
    /// </summary>
    public Texture[] QuXiaoImg;
    enum DlgEnum
    {
        QueDing,
        QuXiao,
    }
    DlgEnum m_ExitType = DlgEnum.QueDing;

    public void Init()
    {
        Debug.Log("SSGameJiXuBaoYueDlg::Init...");
        m_ExitType = DlgEnum.QueDing;
        switch (m_ExitType)
        {
            case DlgEnum.QueDing:
                {
                    SetQueDingUITexture(1);
                    SetQuXiaoUITexture(0);
                    SetAcitveBtFlash();
                    QueDingUI.transform.localScale = m_BigScale;
                    QuXiaoUI.transform.localScale = m_SmallScale;
                    break;
                }
            case DlgEnum.QuXiao:
                {
                    SetQueDingUITexture(0);
                    SetQuXiaoUITexture(1);
                    SetAcitveBtFlash();
                    QueDingUI.transform.localScale = m_SmallScale;
                    QuXiaoUI.transform.localScale = m_BigScale;
                    break;
                }
        }
        InputEventCtrl.GetInstance().ClickTVYaoKongEnterBtEvent += ClickTVYaoKongEnterBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongLeftBtEvent += ClickTVYaoKongLeftBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongRightBtEvent += ClickTVYaoKongRightBtEvent;
    }

    void SetQueDingUITexture(int index)
    {
        if (QueDingImg.Length > index
            && QueDingImg[index] != null)
        {
            QueDingUI.mainTexture = QueDingImg[index];
        }
    }

    void SetQuXiaoUITexture(int index)
    {
        if (QuXiaoImg.Length > index
            && QuXiaoImg[index] != null)
        {
            QuXiaoUI.mainTexture = QuXiaoImg[index];
        }
    }

    public void RemoveSelf()
    {
        Debug.Log("SSGameJiXuBaoYueDlg::RemoveSelf...");
        SSUIRoot.GetInstance().m_ExitUICom = null;
        InputEventCtrl.GetInstance().ClickTVYaoKongEnterBtEvent -= ClickTVYaoKongEnterBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongLeftBtEvent -= ClickTVYaoKongLeftBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongRightBtEvent -= ClickTVYaoKongRightBtEvent;
        Destroy(gameObject);
    }

    private void ClickTVYaoKongLeftBtEvent(ButtonState val)
    {
        if (val == ButtonState.UP)
        {
            return;
        }
        m_ExitType = DlgEnum.QuXiao;
        SetQueDingUITexture(0);
        SetQuXiaoUITexture(1);
        QueDingUI.transform.localScale = m_SmallScale;
        QuXiaoUI.transform.localScale = m_BigScale;
        SetAcitveBtFlash();
    }

    private void ClickTVYaoKongRightBtEvent(ButtonState val)
    {
        if (val == ButtonState.UP)
        {
            return;
        }
        m_ExitType = DlgEnum.QueDing;
        SetQueDingUITexture(1);
        SetQuXiaoUITexture(0);
        QueDingUI.transform.localScale = m_BigScale;
        QuXiaoUI.transform.localScale = m_SmallScale;
        SetAcitveBtFlash();
    }

    void SetAcitveBtFlash()
    {
        if (m_QueDingFlashObj == null || m_QuXiaoFlashObj == null)
        {
            return;
        }

        switch (m_ExitType)
        {
            case DlgEnum.QueDing:
                {
                    m_QueDingFlashObj.SetActive(true);
                    m_QuXiaoFlashObj.SetActive(false);
                    break;
                }
            case DlgEnum.QuXiao:
                {
                    m_QueDingFlashObj.SetActive(false);
                    m_QuXiaoFlashObj.SetActive(true);
                    break;
                }
        }
    }

    private void ClickTVYaoKongEnterBtEvent(ButtonState val)
    {
        if (m_ExitType == DlgEnum.QuXiao)
        {
            switch (val)
            {
                case ButtonState.DOWN:
                    {
                        SetQuXiaoUITexture(1);
                        break;
                    }
                case ButtonState.UP:
                    {
                        SetQuXiaoUITexture(0);
                        Debug.Log("Unity:" + "Player close GameJiXuBaoYueDlg ui...");
                        if (XkGameCtrl.GetInstance() != null
                            && XkGameCtrl.GetInstance().m_GameUICom != null)
                        {
                            XkGameCtrl.GetInstance().m_GameUICom.RemoveGameJiXuBaoYuePanel();
                        }
                        
                        if (pcvr.GetInstance().m_SSMiGuTvCheck != null)
                        {
                            //关闭游戏包月检测.
                            pcvr.GetInstance().m_SSMiGuTvCheck.CloseQueryGameBaoYueState();
                        }

                        //使游戏返回循环动画入口界面.
                        XkGameCtrl.IsLoadingLevel = false;
                        XkGameCtrl.LoadingGameMovie();
                        break;
                    }
            }
        }

        if (m_ExitType == DlgEnum.QueDing)
        {
            switch (val)
            {
                case ButtonState.DOWN:
                    {
                        SetQueDingUITexture(1);
                        break;
                    }
                case ButtonState.UP:
                    {
                        SetQueDingUITexture(0);
                        Debug.Log("Unity:" + "Player select baoYue zhiFu...");
                        if (XkGameCtrl.GetInstance() != null
                            && XkGameCtrl.GetInstance().m_GameUICom != null)
                        {
                            XkGameCtrl.GetInstance().m_GameUICom.RemoveGameJiXuBaoYuePanel();
                        }

                        //打开安卓包月支付界面.
                        if (pcvr.GetInstance() != null)
                        {
                            pcvr.GetInstance().OpenMiGuBaoYueZhiFuPanel();
                        }
                        break;
                    }
            }
        }
    }
}