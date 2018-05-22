using System;
using UnityEngine;

public class SSExitGameUI : MonoBehaviour
{
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
    public void Init ()
    {
        InputEventCtrl.GetInstance().ClickTVYaoKongEnterBtEvent += ClickTVYaoKongEnterBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongExitBtEvent += ClickTVYaoKongExitBtEvent;
    }

    public void RemoveSelf()
    {
        InputEventCtrl.GetInstance().ClickTVYaoKongEnterBtEvent -= ClickTVYaoKongEnterBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongExitBtEvent -= ClickTVYaoKongExitBtEvent;
        Destroy(gameObject);
    }

    private void ClickTVYaoKongEnterBtEvent(ButtonState val)
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