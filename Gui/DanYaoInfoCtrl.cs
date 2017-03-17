using UnityEngine;
using System.Collections;

public class DanYaoInfoCtrl : MonoBehaviour {
	public PlayerEnum PlayerSt = PlayerEnum.Null;
	UITexture GaoBaoTextureCom; //gaoBao or jiQiang
	UITexture DaoDanTextureCom;
//	UISprite HuoLiJQSprite;
//	UISprite DaoDanSprite;
	/**
	 * DanYaoTK[0] -> jiQiang, DanYaoTK[1] -> gaoBao, DanYaoTK[2] -> daoDan
	 */
	Texture[] DanYaoTK;
	/**
	 * DanYaoFJ[0] -> jiQiang, DanYaoFJ[1] -> gaoBao, DanYaoFJ[2] -> daoDan
	 */
	Texture[] DanYaoFJ;
//	UISprite[] AmmoDaoDan;
	/**
	 * AmmoGaoBao[0-1] -> gaoBaoAmmoNum, AmmoGaoBao[2] -> jiQiangAmmoNum
	 */
	public UISprite[] AmmoGaoBao;
	int GaoBaoAmmoNum = -1;
	int SanDanAmmoNum = -1;
	//int DaoDanAmmoNum = -1;
	int ChuanTouAmmoNum = -1;
	int GenZongAmmoNum = -1;
	int JianSuAmmoNum = -1;
	static DanYaoInfoCtrl InstanceOne;
	public static DanYaoInfoCtrl GetInstanceOne()
	{
		return InstanceOne;
	}

	static DanYaoInfoCtrl InstanceTwo;
	public static DanYaoInfoCtrl GetInstanceTwo()
	{
		return InstanceTwo;
	}
	
	static DanYaoInfoCtrl InstanceThree;
	public static DanYaoInfoCtrl GetInstanceThree()
	{
		return InstanceThree;
	}
	
	static DanYaoInfoCtrl InstanceFour;
	public static DanYaoInfoCtrl GetInstanceFour()
	{
		return InstanceFour;
	}

	// Use this for initialization
	void Start()
	{
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			InstanceOne = this;
			if (!XkGameCtrl.IsActivePlayerOne) {
				HiddenPlayerDanYaoInfo();
			}
			break;

		case PlayerEnum.PlayerTwo:
			InstanceTwo = this;
			if (!XkGameCtrl.IsActivePlayerTwo) {
				HiddenPlayerDanYaoInfo();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			InstanceThree = this;
			if (!XkGameCtrl.IsActivePlayerThree) {
				HiddenPlayerDanYaoInfo();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			InstanceFour = this;
			if (!XkGameCtrl.IsActivePlayerFour) {
				HiddenPlayerDanYaoInfo();
			}
			break;
		}
//		HiddenPlayerDanYaoInfo(); //test
//		XkGameCtrl.GaoBaoDanNumPOne = 5; //test
//		XkGameCtrl.GaoBaoDanNumPTwo = 5; //test
		InitPlayerDanYaoInfo();
	}

//	void Update()
//	{
		//if (!pcvr.bIsHardWare && Input.GetKeyUp(KeyCode.M)) {
			//test
			//ShowHuoLiJQSprite(PlayerAmmoType.GaoBaoAmmo);
			//ShowDaoDanSprite();
		//}

		//CheckPlayerGaoBaoAmmoNum();
		//CheckPlayerSanDanAmmoNum();
		//CheckShowPlayerPuTongAmmo();
//	}

	public void ShowPlayerDanYaoInfo()
	{
		if (Network.peerType == NetworkPeerType.Server) {
			return;
		}

		if (gameObject.activeSelf) {
			return;
		}
		gameObject.SetActive(true);
	}
	
	public void HiddenPlayerDanYaoInfo()
	{
		if (Network.peerType == NetworkPeerType.Server) {
			return;
		}

		if (!gameObject.activeSelf) {
			return;
		}
		gameObject.SetActive(false);
//		HiddenDaoDanSprite();
//		HiddenHuoLiJQSprite();
	}

	void InitPlayerDanYaoInfo()
	{
//		HuoLiJQSprite.gameObject.SetActive(false);
//		DaoDanSprite.gameObject.SetActive(false);

//		GameJiTaiType jiTaiType = XkGameCtrl.GameJiTaiSt;
//		switch (jiTaiType) {
//		case GameJiTaiType.FeiJiJiTai:
//			GaoBaoTextureCom.mainTexture = DanYaoFJ[0];
//			DaoDanTextureCom.mainTexture = DanYaoFJ[2];
//			break;
//
//		case GameJiTaiType.TanKeJiTai:
//			GaoBaoTextureCom.mainTexture = DanYaoTK[0];
//			DaoDanTextureCom.mainTexture = DanYaoTK[2];
//			break;
//		}
		ShowJiQiangAmmoNum();

//		if (Network.peerType == NetworkPeerType.Server) {
//			gameObject.SetActive(false);
//		}
	}

	void ShowJiQiangAmmoNum()
	{
		AmmoGaoBao[0].enabled = false;
		AmmoGaoBao[1].enabled = false;
//		if (!AmmoGaoBao[1].enabled && AmmoGaoBao[2].enabled) {
//			return;
//		}
//		AmmoGaoBao[0].enabled = false;
//		AmmoGaoBao[1].enabled = false;
//		AmmoGaoBao[2].enabled = true;
		
//		GameJiTaiType jiTaiType = XkGameCtrl.GameJiTaiSt;
//		switch (jiTaiType) {
//		case GameJiTaiType.FeiJiJiTai:
//			GaoBaoTextureCom.mainTexture = DanYaoFJ[0];
//			break;
//			
//		case GameJiTaiType.TanKeJiTai:
//			GaoBaoTextureCom.mainTexture = DanYaoTK[0];
//			break;
//		}
	}

	void ShowGaoBaoAmmoNum()
	{
		//if (AmmoGaoBao[1].enabled && !AmmoGaoBao[2].enabled) {
		if (AmmoGaoBao[1].enabled) {
			return;
		}
		AmmoGaoBao[0].enabled = true;
		AmmoGaoBao[1].enabled = true;
//		AmmoGaoBao[2].enabled = false;

//		GameJiTaiType jiTaiType = XkGameCtrl.GameJiTaiSt;
//		switch (jiTaiType) {
//		case GameJiTaiType.FeiJiJiTai:
//			GaoBaoTextureCom.mainTexture = DanYaoFJ[1];
//			break;
//
//		case GameJiTaiType.TanKeJiTai:
//			GaoBaoTextureCom.mainTexture = DanYaoTK[1];
//			break;
//		}
	}

	void CheckGaoBaoAmmoNum(int ammoNum)
	{
		if (GaoBaoAmmoNum == ammoNum) {
			return;
		}
		GaoBaoAmmoNum = ammoNum;

		int max = 2;
		int numVal = ammoNum;
		int valTmp = 0;
		int powVal = 0;
		bool isShowZero = false;
		for (int i = 0; i < 2; i++) {
			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("valTmp *** "+valTmp);
			if (!isShowZero) {
				if (valTmp > 0) {
					isShowZero = true;
				}
				else {
					if (i < (max - 1)) {
						AmmoGaoBao[i].enabled = false;
						continue;
					}
				}
			}
			
			if (!AmmoGaoBao[i].enabled) {
				AmmoGaoBao[i].enabled = true;
			}
			AmmoGaoBao[i].spriteName = "GBNum_" + valTmp;
			numVal -= valTmp * powVal;
		}
	}

	void CheckSanDanAmmoNum(int ammoNum)
	{
		if (SanDanAmmoNum == ammoNum) {
			return;
		}
		SanDanAmmoNum = ammoNum;
		
		int max = 2;
		int numVal = ammoNum;
		int valTmp = 0;
		int powVal = 0;
		bool isShowZero = false;
		for (int i = 0; i < 2; i++) {
			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("valTmp *** "+valTmp);
			if (!isShowZero) {
				if (valTmp > 0) {
					isShowZero = true;
				}
				else {
					if (i < (max - 1)) {
						AmmoGaoBao[i].enabled = false;
						continue;
					}
				}
			}
			
			if (!AmmoGaoBao[i].enabled) {
				AmmoGaoBao[i].enabled = true;
			}
			AmmoGaoBao[i].spriteName = "GBNum_" + valTmp;
			numVal -= valTmp * powVal;
		}
	}

	void CheckGenZongAmmoNum(int ammoNum)
	{
		if (GenZongAmmoNum == ammoNum) {
			return;
		}
		GenZongAmmoNum = ammoNum;
		
		int max = 2;
		int numVal = ammoNum;
		int valTmp = 0;
		int powVal = 0;
		bool isShowZero = false;
		for (int i = 0; i < 2; i++) {
			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("valTmp *** "+valTmp);
			if (!isShowZero) {
				if (valTmp > 0) {
					isShowZero = true;
				}
				else {
					if (i < (max - 1)) {
						AmmoGaoBao[i].enabled = false;
						continue;
					}
				}
			}
			
			if (!AmmoGaoBao[i].enabled) {
				AmmoGaoBao[i].enabled = true;
			}
			AmmoGaoBao[i].spriteName = "GBNum_" + valTmp;
			numVal -= valTmp * powVal;
		}
	}

	void CheckChuanTouAmmoNum(int ammoNum)
	{
		if (ChuanTouAmmoNum == ammoNum) {
			return;
		}
		ChuanTouAmmoNum = ammoNum;
		
		int max = 2;
		int numVal = ammoNum;
		int valTmp = 0;
		int powVal = 0;
		bool isShowZero = false;
		for (int i = 0; i < 2; i++) {
			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("valTmp *** "+valTmp);
			if (!isShowZero) {
				if (valTmp > 0) {
					isShowZero = true;
				}
				else {
					if (i < (max - 1)) {
						AmmoGaoBao[i].enabled = false;
						continue;
					}
				}
			}
			
			if (!AmmoGaoBao[i].enabled) {
				AmmoGaoBao[i].enabled = true;
			}
			AmmoGaoBao[i].spriteName = "GBNum_" + valTmp;
			numVal -= valTmp * powVal;
		}
	}
	
	void CheckJianSuAmmoNum(int ammoNum)
	{
		if (JianSuAmmoNum == ammoNum) {
			return;
		}
		JianSuAmmoNum = ammoNum;
		
		int max = 2;
		int numVal = ammoNum;
		int valTmp = 0;
		int powVal = 0;
		bool isShowZero = false;
		for (int i = 0; i < 2; i++) {
			powVal = (int)Mathf.Pow(10, max - i - 1);
			valTmp = numVal / powVal;
			//Debug.Log("valTmp *** "+valTmp);
			if (!isShowZero) {
				if (valTmp > 0) {
					isShowZero = true;
				}
				else {
					if (i < (max - 1)) {
						AmmoGaoBao[i].enabled = false;
						continue;
					}
				}
			}
			
			if (!AmmoGaoBao[i].enabled) {
				AmmoGaoBao[i].enabled = true;
			}
			AmmoGaoBao[i].spriteName = "GBNum_" + valTmp;
			numVal -= valTmp * powVal;
		}
	}

//	void CheckDaoDanAmmoNum(int ammoNum)
//	{
//		if (DaoDanAmmoNum == ammoNum) {
//			return;
//		}
//		DaoDanAmmoNum = ammoNum;
//		
//		int max = 2;
//		int numVal = ammoNum;
//		int valTmp = 0;
//		int powVal = 0;
//		bool isShowZero = false;
//		for (int i = 0; i < 2; i++) {
//			powVal = (int)Mathf.Pow(10, max - i - 1);
//			valTmp = numVal / powVal;
//			//Debug.Log("valTmp *** "+valTmp+", numVal *** "+numVal);
//			if (!isShowZero) {
//				if (valTmp > 0) {
//					isShowZero = true;
//				}
//				else {
//					if (i < (max - 1)) {
//						AmmoDaoDan[i].enabled = false;
//						continue;
//					}
//				}
//			}
//
//			if (!AmmoDaoDan[i].enabled) {
//				AmmoDaoDan[i].enabled = true;
//			}
//			AmmoDaoDan[i].spriteName = "DaoDanNum_" + valTmp;
//			numVal -= valTmp * powVal;
//		}
//	}

	void CheckShowPlayerPuTongAmmo()
	{
		bool isActivePuTongAmmo = true;
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			if (XkGameCtrl.GaoBaoDanNumPOne > 0
			    || XkGameCtrl.DaoDanNumPOne > 0
			    || XkGameCtrl.SanDanNumPOne > 0
			    || XkGameCtrl.GenZongDanNumPOne > 0
			    || XkGameCtrl.ChuanTouDanNumPOne > 0
			    || XkGameCtrl.JianSuDanNumPOne > 0) {
				isActivePuTongAmmo = false;
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (XkGameCtrl.GaoBaoDanNumPTwo > 0
			    || XkGameCtrl.DaoDanNumPTwo > 0
			    || XkGameCtrl.SanDanNumPTwo > 0
			    || XkGameCtrl.GenZongDanNumPTwo > 0
			    || XkGameCtrl.ChuanTouDanNumPTwo > 0
			    || XkGameCtrl.JianSuDanNumPTwo > 0) {
				isActivePuTongAmmo = false;
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (XkGameCtrl.GaoBaoDanNumPThree > 0
			    || XkGameCtrl.DaoDanNumPThree > 0
			    || XkGameCtrl.SanDanNumPThree > 0
			    || XkGameCtrl.GenZongDanNumPThree > 0
			    || XkGameCtrl.ChuanTouDanNumPThree > 0
			    || XkGameCtrl.JianSuDanNumPThree > 0) {
				isActivePuTongAmmo = false;
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (XkGameCtrl.GaoBaoDanNumPFour > 0
			    || XkGameCtrl.DaoDanNumPFour > 0
			    || XkGameCtrl.SanDanNumPFour > 0
			    || XkGameCtrl.GenZongDanNumPFour > 0
			    || XkGameCtrl.ChuanTouDanNumPFour > 0
			    || XkGameCtrl.JianSuDanNumPFour > 0) {
				isActivePuTongAmmo = false;
			}
			break;
		}

		if (isActivePuTongAmmo) {
			ShowJiQiangAmmoNum();
		}
	}

	public void CheckPlayerGaoBaoAmmoNum()
	{
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			if (XkGameCtrl.GaoBaoDanNumPOne > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.GaoBaoDanNumPOne != GaoBaoAmmoNum) {
					CheckGaoBaoAmmoNum(XkGameCtrl.GaoBaoDanNumPOne);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;

		case PlayerEnum.PlayerTwo:
			if (XkGameCtrl.GaoBaoDanNumPTwo > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.GaoBaoDanNumPTwo != GaoBaoAmmoNum) {
					CheckGaoBaoAmmoNum(XkGameCtrl.GaoBaoDanNumPTwo);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (XkGameCtrl.GaoBaoDanNumPThree > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.GaoBaoDanNumPThree != GaoBaoAmmoNum) {
					CheckGaoBaoAmmoNum(XkGameCtrl.GaoBaoDanNumPThree);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;

		case PlayerEnum.PlayerFour:
			if (XkGameCtrl.GaoBaoDanNumPFour > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.GaoBaoDanNumPFour != GaoBaoAmmoNum) {
					CheckGaoBaoAmmoNum(XkGameCtrl.GaoBaoDanNumPFour);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
		}
	}

	public void CheckPlayerSanDanAmmoNum()
	{
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			if (XkGameCtrl.SanDanNumPOne > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.SanDanNumPOne != SanDanAmmoNum) {
					CheckSanDanAmmoNum(XkGameCtrl.SanDanNumPOne);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (XkGameCtrl.SanDanNumPTwo > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.SanDanNumPTwo != SanDanAmmoNum) {
					CheckSanDanAmmoNum(XkGameCtrl.SanDanNumPTwo);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (XkGameCtrl.SanDanNumPThree > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.SanDanNumPThree != SanDanAmmoNum) {
					CheckSanDanAmmoNum(XkGameCtrl.SanDanNumPThree);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (XkGameCtrl.SanDanNumPFour > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.SanDanNumPFour != SanDanAmmoNum) {
					CheckSanDanAmmoNum(XkGameCtrl.SanDanNumPFour);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
		}
	}
	
	public void CheckPlayerGenZongDanAmmoNum()
	{
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			if (XkGameCtrl.GenZongDanNumPOne > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.GenZongDanNumPOne != GenZongAmmoNum) {
					CheckGenZongAmmoNum(XkGameCtrl.GenZongDanNumPOne);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (XkGameCtrl.GenZongDanNumPTwo > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.GenZongDanNumPTwo != GenZongAmmoNum) {
					CheckGenZongAmmoNum(XkGameCtrl.GenZongDanNumPTwo);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (XkGameCtrl.GenZongDanNumPThree > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.GenZongDanNumPThree != GenZongAmmoNum) {
					CheckGenZongAmmoNum(XkGameCtrl.GenZongDanNumPThree);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (XkGameCtrl.GenZongDanNumPFour > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.GenZongDanNumPFour != GenZongAmmoNum) {
					CheckGenZongAmmoNum(XkGameCtrl.GenZongDanNumPFour);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
		}
	}

	public void CheckPlayerChuanTouDanAmmoNum()
	{
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			if (XkGameCtrl.ChuanTouDanNumPOne > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.ChuanTouDanNumPOne != ChuanTouAmmoNum) {
					CheckChuanTouAmmoNum(XkGameCtrl.ChuanTouDanNumPOne);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (XkGameCtrl.ChuanTouDanNumPTwo > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.ChuanTouDanNumPTwo != ChuanTouAmmoNum) {
					CheckChuanTouAmmoNum(XkGameCtrl.ChuanTouDanNumPTwo);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (XkGameCtrl.ChuanTouDanNumPThree > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.ChuanTouDanNumPThree != ChuanTouAmmoNum) {
					CheckChuanTouAmmoNum(XkGameCtrl.ChuanTouDanNumPThree);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (XkGameCtrl.ChuanTouDanNumPFour > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.ChuanTouDanNumPFour != ChuanTouAmmoNum) {
					CheckChuanTouAmmoNum(XkGameCtrl.ChuanTouDanNumPFour);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
		}
	}
	
	public void CheckPlayerJianSuDanAmmoNum()
	{
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			if (XkGameCtrl.JianSuDanNumPOne > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.JianSuDanNumPOne != JianSuAmmoNum) {
					CheckJianSuAmmoNum(XkGameCtrl.JianSuDanNumPOne);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (XkGameCtrl.JianSuDanNumPTwo > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.JianSuDanNumPTwo != JianSuAmmoNum) {
					CheckJianSuAmmoNum(XkGameCtrl.JianSuDanNumPTwo);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (XkGameCtrl.JianSuDanNumPThree > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.JianSuDanNumPThree != JianSuAmmoNum) {
					CheckJianSuAmmoNum(XkGameCtrl.JianSuDanNumPThree);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (XkGameCtrl.JianSuDanNumPFour > 0) {
				ShowGaoBaoAmmoNum();
				if (XkGameCtrl.JianSuDanNumPFour != JianSuAmmoNum) {
					CheckJianSuAmmoNum(XkGameCtrl.JianSuDanNumPFour);
				}
			}
			else {
				ShowJiQiangAmmoNum();
			}
			break;
		}
	}

	public void ShowHuoLiJQSprite(PlayerAmmoType ammoType)
	{
		if (Network.peerType == NetworkPeerType.Server) {
			return;
		}
		
		ResetPlayerAmmoNum(ammoType);
		switch (ammoType) {
		case PlayerAmmoType.ChuanTouAmmo:
			CheckPlayerChuanTouDanAmmoNum();
			break;
			
		case PlayerAmmoType.DaoDanAmmo:
			break;
			
		case PlayerAmmoType.GaoBaoAmmo:
			CheckPlayerGaoBaoAmmoNum();
			break;
			
		case PlayerAmmoType.GenZongAmmo:
			CheckPlayerGenZongDanAmmoNum();
			break;
			
		case PlayerAmmoType.JianSuAmmo:
			CheckPlayerJianSuDanAmmoNum();
			break;
			
		case PlayerAmmoType.SanDanAmmo:
			CheckPlayerSanDanAmmoNum();
			break;
		}

//		if (HuoLiJQSprite.gameObject.activeSelf) {
//			return;
//		}
//		HuoLiJQSprite.fillAmount = 0f;
//		HuoLiJQSprite.gameObject.SetActive(true);
//		StartCoroutine(ChangeHuoLiJQSpriteAmount());
	}
	
	/*void HiddenHuoLiJQSprite()
	{
		//Debug.Log("HiddenHuoLiJQSprite...");
		if (!HuoLiJQSprite.gameObject.activeSelf) {
			return;
		}
		HuoLiJQSprite.gameObject.SetActive(false);
	}*/

	/*IEnumerator ChangeHuoLiJQSpriteAmount()
	{
		bool isStopChange = false;
		do {
			HuoLiJQSprite.fillAmount += 0.2f;
			if (HuoLiJQSprite.fillAmount >= 1f) {
				HuoLiJQSprite.fillAmount = 1f;
				isStopChange = true;
				Invoke("HiddenHuoLiJQSprite", 3f);
				yield break;
			}
			else {
				yield return new WaitForSeconds(0.05f);
			}
		} while (!isStopChange);
	}*/
	
//	public void ShowDaoDanSprite()
//	{
//		if (Network.peerType == NetworkPeerType.Server) {
//			return;
//		}
//
//		if (!gameObject.activeSelf) {
//			return;
//		}
//
////		if (DaoDanSprite.gameObject.activeSelf) {
////			return;
////		}
//		
//		GameJiTaiType jiTaiType = XkGameCtrl.GameJiTaiSt;
//		switch (PlayerSt) {
//		case PlayerEnum.PlayerOne:
//			if (jiTaiType == GameJiTaiType.FeiJiJiTai) {
//				DaoDanSprite.spriteName = "1PDaoDan";
//			}
//			else if (jiTaiType == GameJiTaiType.TanKeJiTai) {
//				DaoDanSprite.spriteName = "1PPaoDan";
//			}
//			break;
//			
//		case PlayerEnum.PlayerTwo:
//			if (jiTaiType == GameJiTaiType.FeiJiJiTai) {
//				DaoDanSprite.spriteName = "2PDaoDan";
//			}
//			else if (jiTaiType == GameJiTaiType.TanKeJiTai) {
//				DaoDanSprite.spriteName = "2PPaoDan";
//			}
//			break;
//		}
//		DaoDanSprite.fillAmount = 0f;
//		DaoDanSprite.gameObject.SetActive(true);
//		StartCoroutine(ChangeDaoDanSpriteAmount());
//	}
	
	/*void HiddenDaoDanSprite()
	{
		//Debug.Log("HiddenDaoDanSprite...");
		if (!DaoDanSprite.gameObject.activeSelf) {
			return;
		}
		DaoDanSprite.gameObject.SetActive(false);
	}*/
	
	/*IEnumerator ChangeDaoDanSpriteAmount()
	{
		bool isStopChange = false;
		do {
			DaoDanSprite.fillAmount += 0.2f;
			if (DaoDanSprite.fillAmount >= 1f) {
				DaoDanSprite.fillAmount = 1f;
				isStopChange = true;
				Invoke("HiddenDaoDanSprite", 3f);
				yield break;
			}
			else {
				yield return new WaitForSeconds(0.05f);
			}
		} while (!isStopChange);
	}*/
	
	void ResetPlayerAmmoNum(PlayerAmmoType ammoType)
	{
		switch (ammoType) {
		case PlayerAmmoType.ChuanTouAmmo:
			GaoBaoAmmoNum = -1;
			SanDanAmmoNum = -1;
			//DaoDanAmmoNum = -1;
			//ChuanTouAmmoNum = -1;
			GenZongAmmoNum = -1;
			JianSuAmmoNum = -1;
			break;
			
		case PlayerAmmoType.DaoDanAmmo:
			GaoBaoAmmoNum = -1;
			SanDanAmmoNum = -1;
			//DaoDanAmmoNum = -1;
			ChuanTouAmmoNum = -1;
			GenZongAmmoNum = -1;
			JianSuAmmoNum = -1;
			break;
			
		case PlayerAmmoType.GaoBaoAmmo:
			//GaoBaoAmmoNum = -1;
			SanDanAmmoNum = -1;
			//DaoDanAmmoNum = -1;
			ChuanTouAmmoNum = -1;
			GenZongAmmoNum = -1;
			JianSuAmmoNum = -1;
			break;
			
		case PlayerAmmoType.GenZongAmmo:
			GaoBaoAmmoNum = -1;
			SanDanAmmoNum = -1;
			//DaoDanAmmoNum = -1;
			ChuanTouAmmoNum = -1;
			//GenZongAmmoNum = -1;
			JianSuAmmoNum = -1;
			break;
			
		case PlayerAmmoType.JianSuAmmo:
			GaoBaoAmmoNum = -1;
			SanDanAmmoNum = -1;
			//DaoDanAmmoNum = -1;
			ChuanTouAmmoNum = -1;
			GenZongAmmoNum = -1;
			//JianSuAmmoNum = -1;
			break;
			
		case PlayerAmmoType.SanDanAmmo:
			GaoBaoAmmoNum = -1;
			//SanDanAmmoNum = -1;
			//DaoDanAmmoNum = -1;
			ChuanTouAmmoNum = -1;
			GenZongAmmoNum = -1;
			JianSuAmmoNum = -1;
			break;
		}
	}
}