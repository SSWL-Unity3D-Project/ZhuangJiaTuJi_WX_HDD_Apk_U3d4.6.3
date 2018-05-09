using UnityEngine;
using System.Collections;

public class BossJiFenCtrl : MonoBehaviour
{
	static BossJiFenCtrl _Instance;
	public static BossJiFenCtrl GetInstance()
	{
		return _Instance;
	}
	void Awake()
	{
		_Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		//_Instance = this;
		HiddenBossJiFenInfo();
	}

	public UISprite[] TimeSpriteArray;
	public void ShowBossJiFenInfo(int timeVal)
	{
		//timeVal = 109;//test
		if (timeVal <= 0) {
			HiddenBossJiFenInfo();
			return;
		}


		int max = TimeSpriteArray.Length;
		int numVal = timeVal;
		int valTmp = 0;
		int powVal = 0;
		bool isHiddenZero = true;
		//Debug.Log("Unity:"+"ShowBossJiFenInfo -> timeVal "+timeVal+", max "+max);
		for (int i = 0; i < max; i++) {
			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("Unity:"+"valTmp "+valTmp+", powVal "+powVal);
			if (valTmp == 0 && isHiddenZero) {
				TimeSpriteArray[i].gameObject.SetActive(false);
				continue;
			}
			isHiddenZero = false;
			TimeSpriteArray[i].spriteName = "timeJiFen_" + valTmp;
			numVal -= valTmp * powVal;
			TimeSpriteArray[i].gameObject.SetActive(true);
		}
		gameObject.SetActive(true);
	}

	void HiddenBossJiFenInfo()
	{
		gameObject.SetActive(false);
	}
}