using UnityEngine;
using System.Collections;

public class GameJiLuFenShuCtrl : MonoBehaviour
{
	public UISprite[] UISpriteJF;
	static GameJiLuFenShuCtrl _Instance;
	public static GameJiLuFenShuCtrl GetInstance()
	{
		return _Instance;
	}

	void Start()
	{
		_Instance = this;
		int jfVal = PaiMingCtrl.XinJiLuVal == 0 ? 50000 : PaiMingCtrl.XinJiLuVal;
		PaiMingCtrl.XinJiLuVal = jfVal;
		SetPlayerJiFen(UISpriteJF, jfVal);
		if (Application.loadedLevel == (int)GameLevel.Movie) {
			HiddenGameJiLuFenShu();
		}
//		TweenPosition twPos = gameObject.AddComponent<TweenPosition>();
//		twPos.from = new Vector3(800f, 255f, 0f);
//		twPos.to = new Vector3(-1000f, 255f, 0f);
//		twPos.duration = 5f;
//		twPos.style = UITweener.Style.Loop;
//		transform.position = twPos.from;
//		twPos.PlayForward();
	}

	public void ShowGameJiLuScore()
	{
		int jfVal = PaiMingCtrl.XinJiLuVal == 0 ? 50000 : PaiMingCtrl.XinJiLuVal;
		PaiMingCtrl.XinJiLuVal = jfVal;
		SetPlayerJiFen(UISpriteJF, jfVal);
		gameObject.SetActive(true);
	}
	
	void SetPlayerJiFen(UISprite[] uiSpriteJF, int jiFen)
	{
		int max = uiSpriteJF.Length;
		int numVal = jiFen;
		int valTmp = 0;
		int powVal = 0;
		for (int i = 0; i < max; i++) {
			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("Unity:"+"valTmp *** "+valTmp);
			uiSpriteJF[i].spriteName = "timeJiFen_" + valTmp;
			numVal -= valTmp * powVal;
		}
	}

	public void HiddenGameJiLuFenShu()
	{
		gameObject.SetActive(false);
	}
}