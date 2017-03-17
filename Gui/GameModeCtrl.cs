using UnityEngine;
using System.Collections;

public class GameModeCtrl : MonoBehaviour {
	public static GameMode ModeVal = GameMode.Null;
	public GameObject LoadingObj;
	public static bool IsSelectDanJiMode;
	bool IsLoadingGame;
	static GameModeCtrl _Instance;
	public static GameModeCtrl GetInstance()
	{
		return _Instance;
	}

	// Use this for initialization
	void Start()
	{
		_Instance = this;
		if (GameTypeCtrl.AppTypeStatic == AppGameType.LianJiServer) {
			ModeVal = GameMode.LianJi;
		}
		else {
			ModeVal = GameMode.Null;
		}

		IsSelectDanJiMode = false;
		SetActiveLoading(false);
	}

	public void SetActiveLoading(bool isActive)
	{
		if (LoadingObj.activeSelf == isActive) {
			return;
		}
		LoadingObj.SetActive(isActive);

		if (isActive) {
			if (!IsLoadingGame) {
				IsLoadingGame = true;
				Invoke("DelayLoadingGame", 0.2f);
			}
		}
	}

	void DelayLoadingGame()
	{
		XkGameCtrl.LoadingGameScene_1();
	}
}