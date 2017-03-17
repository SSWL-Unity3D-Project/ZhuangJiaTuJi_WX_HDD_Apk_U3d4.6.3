using UnityEngine;
using System.Collections;

public class SetPanelCtrl : MonoBehaviour {
	static private SetPanelCtrl Instance = null;
	static public SetPanelCtrl GetInstance()
	{
		if(Instance == null)
		{
			GameObject obj = new GameObject("_SetPanelCtrl");
			Instance = obj.AddComponent<SetPanelCtrl>();
		}
		return Instance;
	}

	// Use this for initialization
	void Start()
	{
		//Debug.Log("SetPanelCtrl::init...");
		InputEventCtrl.GetInstance().ClickSetEnterBtEvent += ClickSetEnterBtEvent;
	}

	void ClickSetEnterBtEvent(ButtonState val)
	{
		if (HardwareCheckCtrl.IsTestHardWare) {
			return;
		}

		if (val == ButtonState.DOWN) {
			return;
		}

		if (Application.loadedLevel == (int)GameLevel.SetPanel) {
			return;
		}
		loadLevelSetPanel();
	}

	void loadLevelSetPanel()
	{
		if (XkGameCtrl.IsLoadingLevel) {
//			Debug.Log("*************Loading...");
			return;
		}
		
		if (!XkGameCtrl.IsGameOnQuit) {
			System.GC.Collect();
			Application.LoadLevel( (int)GameLevel.SetPanel );
		}
	}
}