using UnityEngine;

public class SSExitGameUI : MonoBehaviour
{
    public Vector3 m_BigScale = new Vector3(1.2f, 1.2f, 1f);
    public Vector3 m_SmallScale = Vector3.one;
    public UITexture QueDingUI;
    /// <summary>
    /// QueDingImg[0] 确定弹起.
    /// QueDingImg[1] 确定按下.
    /// </summary>
    public Texture[] QueDingImg;
    public UITexture QuXiaoUI;
    /// <summary>
    /// QuXiaoImg[0] 取消弹起.
    /// QuXiaoImg[1] 取消按下.
    /// </summary>
    public Texture[] QuXiaoImg;
    enum ExitEnum
    {
        QueDing,
        QuXiao,
    }
    ExitEnum m_ExitType = ExitEnum.QueDing;

    public void Init ()
    {
        QueDingUI.transform.localScale = m_BigScale;
        QuXiaoUI.transform.localScale = m_SmallScale;
        InputEventCtrl.GetInstance().ClickTVYaoKongEnterBtEvent += ClickTVYaoKongEnterBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongExitBtEvent += ClickTVYaoKongExitBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongLeftBtEvent += ClickTVYaoKongLeftBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongRightBtEvent += ClickTVYaoKongRightBtEvent;
    }

    public void RemoveSelf()
    {
        InputEventCtrl.GetInstance().ClickTVYaoKongEnterBtEvent -= ClickTVYaoKongEnterBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongExitBtEvent -= ClickTVYaoKongExitBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongLeftBtEvent -= ClickTVYaoKongLeftBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongRightBtEvent -= ClickTVYaoKongRightBtEvent;
        Destroy(gameObject);
    }
    
    private void ClickTVYaoKongLeftBtEvent(ButtonState val)
    {
        m_ExitType = ExitEnum.QuXiao;
        QueDingUI.transform.localScale = m_SmallScale;
        QuXiaoUI.transform.localScale = m_BigScale;
    }

    private void ClickTVYaoKongRightBtEvent(ButtonState val)
    {
        m_ExitType = ExitEnum.QueDing;
        QueDingUI.transform.localScale = m_BigScale;
        QuXiaoUI.transform.localScale = m_SmallScale;
    }

    private void ClickTVYaoKongEnterBtEvent(ButtonState val)
    {

        if (m_ExitType == ExitEnum.QuXiao)
        {
            switch (val)
            {
                case ButtonState.DOWN:
                    {
                        QuXiaoUI.mainTexture = QuXiaoImg[1];
                        break;
                    }
                case ButtonState.UP:
                    {
                        QuXiaoUI.mainTexture = QuXiaoImg[0];
                        Debug.Log("Unity:" + "Player close exit game ui...");
                        if (XkGameCtrl.GetInstance() != null)
                        {
                            XkGameCtrl.GetInstance().RemoveExitGameUI();
                        }

                        if (GameMovieCtrl.GetInstance() != null)
                        {
                            GameMovieCtrl.GetInstance().RemoveExitGameUI();
                        }
                        break;
                    }
            }
        }

        if (m_ExitType == ExitEnum.QueDing)
        {
            switch (val)
            {
                case ButtonState.DOWN:
                    {
                        QueDingUI.mainTexture = QueDingImg[1];
                        break;
                    }
                case ButtonState.UP:
                    {
                        QueDingUI.mainTexture = QueDingImg[0];
                        Debug.Log("Unity:" + "Player exit application...");
                        if (XkGameCtrl.GetInstance() != null)
                        {
                            XkGameCtrl.GetInstance().RemoveExitGameUI();
                        }

                        if (GameMovieCtrl.GetInstance() != null)
                        {
                            GameMovieCtrl.GetInstance().RemoveExitGameUI();
                        }
                        Application.Quit();
                        break;
                    }
            }
        }
    }

    private void ClickTVYaoKongExitBtEvent(ButtonState val)
    {
        switch (val)
        {
            case ButtonState.DOWN:
                {
                    QuXiaoUI.mainTexture = QuXiaoImg[1];
                    break;
                }
            case ButtonState.UP:
                {
                    QuXiaoUI.mainTexture = QuXiaoImg[0];
                    Debug.Log("Unity:" + "Player close exit game ui...");
                    if (XkGameCtrl.GetInstance() != null)
                    {
                        XkGameCtrl.GetInstance().RemoveExitGameUI();
                    }

                    if (GameMovieCtrl.GetInstance() != null)
                    {
                        GameMovieCtrl.GetInstance().RemoveExitGameUI();
                    }
                    break;
                }
        }
    }
}