using System;
using UnityEngine;

public class SSExitGameUI : MonoBehaviour
{
	public void Init ()
    {
        InputEventCtrl.GetInstance().ClickTVYaoKongEnterBtEvent += ClickTVYaoKongEnterBtEvent;
        InputEventCtrl.GetInstance().ClickTVYaoKongExitBtEvent += ClickTVYaoKongExitBtEvent;
    }

    private void ClickTVYaoKongEnterBtEvent(ButtonState val)
    {
        Debug.Log("Unity:" + "Player exit application...");
        XkGameCtrl.GetInstance().RemoveExitGameUI();
        Application.Quit();
    }

    private void ClickTVYaoKongExitBtEvent(ButtonState val)
    {
        Debug.Log("Unity:" + "Player close exit game ui...");
        XkGameCtrl.GetInstance().RemoveExitGameUI();
    }
}