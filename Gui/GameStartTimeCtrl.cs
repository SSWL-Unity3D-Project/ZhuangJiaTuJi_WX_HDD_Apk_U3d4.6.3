using UnityEngine;
using System.Collections;

public class GameStartTimeCtrl : MonoBehaviour {
	public Texture[] TimeTexture;
	UITexture StartTimeTexture;
	int TimeCount;
	bool IsInitPlay;
	static GameStartTimeCtrl _Instance;
	public static GameStartTimeCtrl GetInstance()
	{
		return _Instance;
	}

	// Use this for initialization
	void Awake()
	{
		_Instance = this;
		StartTimeTexture = GetComponent<UITexture>();
		gameObject.SetActive(false);
	}

	public void InitPlayStartTimeUI()
	{
		if (IsInitPlay) {
			return;
		}
		IsInitPlay = true;
		gameObject.SetActive(true);
		PlayStartTimeUI();
	}

	void PlayStartTimeUI()
	{
		TweenScale tweenScaleCom = GetComponent<TweenScale>();
		if (tweenScaleCom != null) {
			DestroyObject(tweenScaleCom);
		}
		
		tweenScaleCom = gameObject.AddComponent<TweenScale>();
		tweenScaleCom.enabled = false;
		tweenScaleCom.duration = 1.2f;
		tweenScaleCom.from = new Vector3(3f, 3f, 1f);
		tweenScaleCom.to = new Vector3(1f, 1f, 1f);
		EventDelegate.Add(tweenScaleCom.onFinished, delegate{
			ChangeStartTimeUI();
		});
		tweenScaleCom.enabled = true;
		tweenScaleCom.PlayForward();
	}

	void ChangeStartTimeUI()
	{
		TimeCount++;
		if (TimeCount >= TimeTexture.Length) {
			Debug.Log("ChangeStartTimeUI -> change over!");
			gameObject.SetActive(false);
			ScreenDanHeiCtrl.GetInstance().ActiveGameUiCamera();
			XkPlayerCtrl.GetInstanceFeiJi().RestartMovePlayer();
			GameTimeCtrl.GetInstance().ActiveIsCheckTimeSprite();
			return;
		}

		//Debug.Log("ChangeStartTimeUI -> TimeCount "+TimeCount);
		StartTimeTexture.mainTexture = TimeTexture[TimeCount];
		PlayStartTimeUI();
	}
}