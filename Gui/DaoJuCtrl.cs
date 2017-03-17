using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DaoJuCtrl : MonoBehaviour {
	//UITexture[] UITexturePlayer;
	/**
	 * DaoJuEndTr[0-3]   -> 高爆弹,散弹,跟踪弹,穿透弹,减速弹.
	 * DaoJuEndTr[4-7]   -> 火力全开.
	 * DaoJuEndTr[8-11]  -> 分数倍率道具.
	 * DaoJuEndTr[12-15] -> 急速道具.
	 * DaoJuEndTr[16-19] -> 急救道具.
	 */
	public Transform[] DaoJuEndTr;
	/**
	 * DaoJuTexture[0] -> 高爆弹.
	 * DaoJuTexture[1] -> 散弹.
	 * DaoJuTexture[2] -> 跟踪弹.
	 * DaoJuTexture[3] -> 穿透弹（穿甲弹）.
	 * DaoJuTexture[4] -> 减速弹.
	 * DaoJuTexture[5] -> 能量护盾.
	 * DaoJuTexture[6] -> 分数道具.
	 * DaoJuTexture[7] -> 急速道具.
	 * DaoJuTexture[8] -> 医疗包道具.
	 * DaoJuTexture[9] -> 加倍分数道具.
	 * DaoJuTexture[10] -> 前后发射道具.
	 * DaoJuTexture[11] -> 长程机枪道具.
	 * DaoJuTexture[12] -> 散弹机枪道具.
	 * DaoJuTexture[13] -> 强击机枪道具.
	 * DaoJuTexture[14] -> 迫击炮道具.
	 * DaoJuTexture[15] -> 主炮散弹道具.
	 * DaoJuTexture[16] -> 主炮和机枪火力全开道具.
	 */
	public Texture[] DaoJuTexture;
	List<GameObject> DaoJuObjList;
	static DaoJuCtrl _Instance;
	public static DaoJuCtrl GetInstance()
	{
		return _Instance;
	}

	const int DaoJuLength = 16;
	// Use this for initialization
	void Start()
	{
		_Instance = this;
		DaoJuObjList = new List<GameObject>();
		/*UITexturePlayer = new UITexture[DaoJuLength];
		for (int i = 0; i < DaoJuLength; i++) {
			UITexturePlayer[i] = DaoJuEndTr[i].GetComponent<UITexture>();
		}*/
		HiddenPlayerDaoJuObj(PlayerEnum.PlayerOne);
		HiddenPlayerDaoJuObj(PlayerEnum.PlayerTwo);
		HiddenPlayerDaoJuObj(PlayerEnum.PlayerThree);
		HiddenPlayerDaoJuObj(PlayerEnum.PlayerFour);
	}

//	public PlayerEnum TestPlayerIndex;
//	public Transform DaoJuTest;
	// Update is called once per frame
//	void Update()
//	{
//		if (pcvr.bIsHardWare && Camera.main == null) {
//			return;
//		}
//
//		if (Input.GetKeyUp(KeyCode.M)) {
//			MoveDaoJuObjToPlayer(TestPlayerIndex, DaoJuTest);
//		}
//	}

	public void MoveDaoJuObjToPlayer(PlayerEnum indexVal, Transform daoJuTr)
	{
		int indexDJ = (int)indexVal - 1;
		Vector3[] path = new Vector3[2];
		path[0] = Camera.main.WorldToScreenPoint(daoJuTr.position);
		path[0].x = (XkGameCtrl.ScreenWidth * path[0].x) / Screen.width;
		path[0].y = (XkGameCtrl.ScreenHeight * path[0].y) / Screen.height;
		BuJiBaoCtrl buJiScript = daoJuTr.GetComponent<BuJiBaoCtrl>();
		BuJiBaoType buJiBaoVal = buJiScript.BuJiBao;
		switch (buJiBaoVal) {
		case BuJiBaoType.ShuangBeiFenShuDJ:
			path[1] = DaoJuEndTr[8 + indexDJ].position;
			break;
		case BuJiBaoType.JiSuDJ:
			path[1] = DaoJuEndTr[12 + indexDJ].position;
			break;
		case BuJiBaoType.NLHuDun:
		case BuJiBaoType.HuoLiAllOpenDJ:
			path[1] = DaoJuEndTr[4 + indexDJ].position;
			break;
		default:
			path[1] = DaoJuEndTr[indexDJ].position;
			break;
		}

		int daoJuMax = DaoJuObjList.Count;
		GameObject daoJu = null;
		for (int i = 0; i < daoJuMax; i++) {
			if (DaoJuObjList[i] != null && !DaoJuObjList[i].activeSelf) {
				daoJu = DaoJuObjList[i];
				break;
			}
		}

		if (daoJu == null) {
			daoJu = DaoJuMoveCtrl.SpawnDaoJuMoveObj(transform);
			DaoJuObjList.Add(daoJu);
		}
		DaoJuMoveCtrl daoJuMove = daoJu.GetComponent<DaoJuMoveCtrl>();
		int indexBJ = (int)buJiScript.BuJiBao - 2;
		//Debug.Log("indexBJ *** "+indexBJ);
		daoJuMove.MoveDaoJuToPlayer(DaoJuTexture[indexBJ], indexVal, buJiScript.BuJiBao, path);
	}

	public void ShowPlayerDaoJu(PlayerEnum indexPlayer, BuJiBaoType buJiState)
	{
		/*int indexBJ = (int)buJiState - 2;
		int indexVal = -1;
		switch (buJiState) {
		case BuJiBaoType.NLHuDun:
		case BuJiBaoType.HuoLiAllOpenDJ:
			break;
		case BuJiBaoType.JiSuDJ:
			indexVal = (int)indexPlayer + 11;
			break;
		default:
			indexVal = (int)indexPlayer - 1;
			break;
		}*/

		/*if (indexVal != -1) {
			UITexturePlayer[indexVal].mainTexture = DaoJuTexture[indexBJ];
			DaoJuEndTr[indexVal].gameObject.SetActive(true);
		}*/

		switch (buJiState) {
		case BuJiBaoType.ChuanTouDan:
			XkGameCtrl.GetInstance().AddChuanTouDanNum(indexPlayer);
			break;
			
		case BuJiBaoType.GaoBaoDan:
			XkGameCtrl.GetInstance().AddGaoBaoDanNum(indexPlayer);
			break;
			
		case BuJiBaoType.GenZongDan:
			XkGameCtrl.GetInstance().AddGenZongDanNum(indexPlayer);
			break;
			
		case BuJiBaoType.JianSuDan:
			XkGameCtrl.GetInstance().AddJianSuDanNum(indexPlayer);
			break;
			
		case BuJiBaoType.SanDan:
			XkGameCtrl.GetInstance().AddSanDanNum(indexPlayer);
			break;

		case BuJiBaoType.NLHuDun:
			XkGameCtrl.GetInstance().ActivePlayerWuDiState(indexPlayer);
			break;

		case BuJiBaoType.HuoLiAllOpenDJ:
			break;

		case BuJiBaoType.ShuangBeiFenShuDJ:
			XKPlayerFenShuUICtrl fenShuScript = XKPlayerFenShuUICtrl.GetInstanceFenShu(indexPlayer);
			fenShuScript.ShowFenShuUI(XKDaoJuGlobalDt.GetInstance().TimeShuangBeiVal);
			break;

		case BuJiBaoType.JiSuDJ:
			XKPlayerJiSuUICtrl jiSuScript = XKPlayerJiSuUICtrl.GetInstanceJiSu(indexPlayer);
			jiSuScript.ShowJiSuUI(XKDaoJuGlobalDt.GetInstance().JiSuTimeVal);
			break;
		}
	}

	public void HiddenPlayerDaoJuObj(PlayerEnum indexPlayer, BuJiBaoType buJiState = BuJiBaoType.Null)
	{
		int indexVal = (int)indexPlayer - 1;
		switch (buJiState) {
		case BuJiBaoType.NLHuDun:
			break;
		case BuJiBaoType.ShuangBeiFenShuDJ:
			indexVal = (int)indexPlayer + 7;
			DaoJuEndTr[indexVal].gameObject.SetActive(false);
			break;
		case BuJiBaoType.JiSuDJ:
			indexVal = (int)indexPlayer + 11;
			DaoJuEndTr[indexVal].gameObject.SetActive(false);
			break;
		default:
			DaoJuEndTr[indexVal].gameObject.SetActive(false);
			indexVal = (int)indexPlayer + 3;
			DaoJuEndTr[indexVal].gameObject.SetActive(false);
			indexVal = (int)indexPlayer + 7;
			DaoJuEndTr[indexVal].gameObject.SetActive(false);
			indexVal = (int)indexPlayer + 11;
			DaoJuEndTr[indexVal].gameObject.SetActive(false);
			break;
		}
	}

	public void HiddenAllPlayerDaoJu()
	{
		gameObject.SetActive(false);
	}
	
	public void ShowAllPlayerDaoJu()
	{
		gameObject.SetActive(true);
	}
}