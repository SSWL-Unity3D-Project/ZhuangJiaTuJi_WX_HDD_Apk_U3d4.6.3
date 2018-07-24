using UnityEngine;
using System.Collections.Generic;

public enum NpcJiFenEnum
{
	Null = -1,
	ShiBing,		//士兵.
	CheLiang,		//车辆.
	ChuanBo,		//船舶.
	FeiJi,			//飞机.
	Boss,			//Boss.
}

public enum GameMode
{
	Null,
	LianJi,
	DanJiFeiJi,
	DanJiTanKe,
}

public enum GameJiTaiType
{
	Null,
	FeiJiJiTai,
	TanKeJiTai,
}

public class XkGameCtrl : SSGameMono
{
    /// <summary>
    /// 游戏UI管理.
    /// </summary>
    public SSGameUICtrl m_GameUICom;
    [System.Serializable]
    public class GameUIData
    {
        /// <summary>
        /// 动态产生的UI父级Center.
        /// </summary>
        public Transform UICenterTrParent;
        /// <summary>
        /// 确定退出游戏的UI界面预制.
        /// </summary>
        public GameObject ExitGameUIPrefab;
    }
    /// <summary>
    /// 游戏UI界面数据.
    /// </summary>
    public GameUIData m_GameUIDt;

    /// <summary>
    /// 微信头像处理组件.
    /// </summary>
    public AsyncImageDownload m_AsyImage;
	/**
	 * 在游戏场景测试玩家运动模式.
	 */
	public TKMoveState TestTKMoveSt = TKMoveState.YaoGanBan;
	/**
	 * 控制npc子弹的发射频率.
	 */
	[Range(0.1f, 100f)]public float NpcAmmoTime = 1f;
	/**
	 * 控制npc子弹的运动速度.
	 */
	[Range(0.1f, 100f)]public float NpcAmmoSpeed = 1f;
	[Range(100f, 1000000f)]public float PlayerXueLiangMax = 10000f;
	public static string NpcLayerInfo = "NpcLayer";
	public GameObject GmCamPrefab;
	public AiMark GmCamMark;
	[Range(0.1f, 100f)]public float WuDiTime = 5f;
	/**
	 * 主角闪烁间隔时长.
	 */
	[Range(0.001f, 1f)]public float TimeUnitShanShuo = 0.1f;
	/**
	 * 主角闪烁时长.
	 */
	[Range(0.1f, 10f)]public float TimeShanShuoVal = 3f;
	Transform FeiJiPlayerTran;
	int FeiJiMarkIndex = 1;
	AiPathCtrl FeiJiPlayerPath;
	int TanKeMarkIndex = 1;
	GameObject ServerCamera; //服务器飞机摄像机.
	public static GameObject ServerCameraObj;
	GameObject ServerCameraTK; //服务器坦克摄像机.
	public static GameObject ServerCameraObjTK;
	int CartoonCamMarkIndex = 1;
	public LayerMask XueTiaoCheckLayer;
	public LayerMask LandLayer;
	public LayerMask NpcAmmoHitLayer;
	public LayerMask PlayerAmmoHitLayer;
	public LayerMask NpcCollisionLayer;
	public static Transform MissionCleanup;
    [HideInInspector]
    public Transform DaoJuArray;
    public static Transform NpcObjArray;
    [HideInInspector]
    public Transform NpcObjHiddenArray;
    public static Transform NpcAmmoArray;
	public static Transform PlayerAmmoArray;
	List<Transform> NpcTranList = new List<Transform>(20);
	static List<YouLiangDianMoveCtrl> YLDLvA = new List<YouLiangDianMoveCtrl>(20);
	static List<YouLiangDianMoveCtrl> YLDLvB = new List<YouLiangDianMoveCtrl>(20);
	static List<YouLiangDianMoveCtrl> YLDLvC = new List<YouLiangDianMoveCtrl>(20);
	public static float ScreenWidth = 1360f;
	public static float ScreenHeight = 768f;
	public static string TerrainLayer = "Terrain";
	public GameObject GameFpsPrefab;
	public GameObject AudioListPrefab;
	public static GameMode TestGameModeVal = GameMode.DanJiFeiJi;
	public static GameMode GameModeVal = GameMode.DanJiFeiJi;
	public static GameJiTaiType GameJiTaiSt = GameJiTaiType.FeiJiJiTai;
	public bool IsCartoonShootTest;
	bool IsServerCameraTest;
	public static string TagNull = "Untagged";
	public static string TagMainCamera = "MainCamera";
	public static int ShiBingNumPOne;
	public static int CheLiangNumPOne;
	public static int ChuanBoNumPOne;
	public static int FeiJiNumPOne;
	public static int ShiBingNumPTwo;
	public static int CheLiangNumPTwo;
	public static int ChuanBoNumPTwo;
	public static int FeiJiNumPTwo;
	public static int ShiBingNumPThree;
	public static int CheLiangNumPThree;
	public static int ChuanBoNumPThree;
	public static int FeiJiNumPThree;
	public static int ShiBingNumPFour;
	public static int CheLiangNumPFour;
	public static int ChuanBoNumPFour;
	public static int FeiJiNumPFour;
	int GaoBaoDanBuJiNum = 50;
	int SanDanBuJiNum = 30;
	int GenZongDanBuJiNum = 35;
	int ChuanTouDanBuJiNum = 25;
	int JianSuDanBuJiNum = 25;
	int DaoDanBuJiNum = 3;
	public static int DaoDanNumPOne;
	public static int DaoDanNumPTwo;
	public static int DaoDanNumPThree;
	public static int DaoDanNumPFour;

	public static int GaoBaoDanNumPOne;
	public static int GaoBaoDanNumPTwo;
	public static int GaoBaoDanNumPThree;
	public static int GaoBaoDanNumPFour;
	
	public static int SanDanNumPOne;
	public static int SanDanNumPTwo;
	public static int SanDanNumPThree;
	public static int SanDanNumPFour;
	
	public static int GenZongDanNumPOne;
	public static int GenZongDanNumPTwo;
	public static int GenZongDanNumPThree;
	public static int GenZongDanNumPFour;
	
	public static int ChuanTouDanNumPOne;
	public static int ChuanTouDanNumPTwo;
	public static int ChuanTouDanNumPThree;
	public static int ChuanTouDanNumPFour;
	
	public static int JianSuDanNumPOne;
	public static int JianSuDanNumPTwo;
	public static int JianSuDanNumPThree;
	public static int JianSuDanNumPFour;

	public static bool IsMoveOnPlayerDeath = true;
	public static float YouLiangBuJiNum = 30f;
	public static float PlayerYouLiangMax = 60f;
	public static float PlayerYouLiangCur = 60f;
	public static bool IsActivePlayerOne;
	public static bool IsActivePlayerTwo;
	public static bool IsActivePlayerThree;
	public static bool IsActivePlayerFour;
	public static bool IsLoadingLevel;
	float TimeCheckNpcAmmo;
	float TimeCheckNpcTranList;
	public static float TriggerBoxSize_Z = 1.5f;
	static List<XKTriggerRemoveNpc> CartoonTriggerSpawnList;
	public static int CountNpcAmmo;
	static List<GameObject> NpcAmmoList;
	public static bool IsDonotCheckError = false;
	public static bool IsShowDebugInfoBox = false;
	static bool IsActiveFinishTask;
	public static bool IsPlayGamePOne;
	public static bool IsPlayGamePTwo;
	public static bool IsPlayGamePThree;
	public static bool IsPlayGamePFour;
	public static int YouLiangDianAddPOne;
	public static int YouLiangDianAddPTwo;
	public static GameObject ServerCameraPar;
	int YouLiangJingGaoVal = 10;
	public static bool IsAddPlayerYouLiang;
	public static bool IsGameOnQuit;
	/// <summary>
	/// 是否绘制主角路径.
	/// </summary>
	public static bool IsDrawGizmosObj = true;
	public static int AmmoNumMaxNpc = 30;
	public static int PlayerActiveNum;
	static float MaxPlayerHealth = 1000000f;
	public static float MinBloodUIAmount = 0.34f;
	static float[] PlayerHealthArray = {0f, 0f, 0f, 0f};
	public static int[] PlayerJiFenArray = {0, 0, 0, 0};
/**
 * 主角进行游戏的圈数.
 * PlayerQuanShu[0] -> 主角1的圈数.
 * PlayerQuanShu[1] -> 主角2的圈数.
 * PlayerQuanShu[2] -> 主角3的圈数.
 * PlayerQuanShu[3] -> 主角4的圈数.
 */
	int[] PlayerQuanShu = {1, 1, 1, 1};
//	public static int TestGameEndLv = (int)GameLevel.Scene_2;
	static XkGameCtrl _Instance;
	public static XkGameCtrl GetInstance()
	{
		return _Instance;
	}

	// Use this for initialization
	void Awake()
	{
		try
		{
			_Instance = this;
			pcvr.OpenDongGanState();
			pcvr.OpenAllPlayerFangXiangPanPower();
			switch (XKGlobalData.GameDiff)
			{
				case "0":
					PlayerXueLiangMax = 14000f;
					break;
				case "2":
					PlayerXueLiangMax = 6000f;
					break;
				default:
					PlayerXueLiangMax = 10000f;
					break;
			}
			MaxPlayerHealth = PlayerXueLiangMax;
			KeyBloodUI = (1f - MinBloodUIAmount) / MaxPlayerHealth;
			XKSpawnNpcPoint.ClearFiJiNpcPointList();
			XKTriggerStopMovePlayer.IsActiveTrigger = false;
			XKTriggerYuLeCtrl.IsActiveYuLeTrigger = false;
			XKPlayerHeTiData.IsActiveHeTiPlayer = false;
			XKTriggerClosePlayerUI.IsActiveHeTiCloseUI = false;
			XKTriggerClosePlayerUI.IsClosePlayerUI = false;
			XKTriggerKaQiuShaFire.IsFireKaQiuSha = false;
			XKTriggerOpenPlayerUI.IsActiveOpenPlayerUI = false;
			XKGlobalData.GetInstance().StopModeBeiJingAudio();
			CountNpcAmmo = 0;
			PlayerJiFenArray = new int[] { 0, 0, 0, 0 };

			ShiBingNumPOne = 0;
			CheLiangNumPOne = 0;
			ChuanBoNumPOne = 0;
			FeiJiNumPOne = 0;

			ShiBingNumPTwo = 0;
			CheLiangNumPTwo = 0;
			ChuanBoNumPTwo = 0;
			FeiJiNumPTwo = 0;

			ShiBingNumPThree = 0;
			CheLiangNumPThree = 0;
			ChuanBoNumPThree = 0;
			FeiJiNumPThree = 0;

			ShiBingNumPFour = 0;
			CheLiangNumPFour = 0;
			ChuanBoNumPFour = 0;
			FeiJiNumPFour = 0;

			GaoBaoDanNumPOne = 0;
			GaoBaoDanNumPTwo = 0;

			YouLiangDianAddPOne = 0;
			YouLiangDianAddPTwo = 0;

			for (int i = 0; i < 4; i++)
			{
				PlayerHealthArray[i] = MaxPlayerHealth;
			}
			IsActiveFinishTask = false;
			IsAddPlayerYouLiang = false;
			ScreenDanHeiCtrl.IsStartGame = false;

			if (GameFpsPrefab != null)
			{
				Instantiate(GameFpsPrefab);
			}

			PlayerAmmoCtrl.PlayerAmmoHitLayer = PlayerAmmoHitLayer;
			PlayerAmmoCtrl.NpcCollisionLayer = NpcCollisionLayer;
			NpcAmmoList = new List<GameObject>();
			CartoonTriggerSpawnList = new List<XKTriggerRemoveNpc>();
			if (Network.peerType == NetworkPeerType.Disconnected)
			{
				if (!GameMovieCtrl.IsActivePlayer)
				{
					if (IsServerCameraTest)
					{
						TestGameModeVal = GameMode.LianJi;
					}
					GameModeVal = TestGameModeVal != GameMode.Null ? TestGameModeVal : GameModeVal; //TestGame
				}
				else {
					if (GameTypeCtrl.AppTypeStatic == AppGameType.DanJiFeiJi
						|| GameTypeCtrl.AppTypeStatic == AppGameType.LianJiFeiJi)
					{
						GameModeVal = GameMode.DanJiFeiJi;
					}
					else if (GameTypeCtrl.AppTypeStatic == AppGameType.DanJiTanKe
							 || GameTypeCtrl.AppTypeStatic == AppGameType.LianJiTanKe)
					{
						GameModeVal = GameMode.DanJiTanKe;
					}
				}
			}
			else {
				GameModeVal = GameMode.LianJi;
				if (Network.peerType == NetworkPeerType.Server)
				{
					GameJiTaiSt = GameJiTaiType.Null;
				}
				else if (Network.peerType == NetworkPeerType.Client)
				{
					if (GameTypeCtrl.AppTypeStatic == AppGameType.DanJiFeiJi
						|| GameTypeCtrl.AppTypeStatic == AppGameType.LianJiFeiJi)
					{
						GameJiTaiSt = GameJiTaiType.FeiJiJiTai;
					}
					else if (GameTypeCtrl.AppTypeStatic == AppGameType.DanJiTanKe
							 || GameTypeCtrl.AppTypeStatic == AppGameType.LianJiTanKe)
					{
						GameJiTaiSt = GameJiTaiType.TanKeJiTai;
					}
				}
			}

			if (GameMovieCtrl.IsActivePlayer)
			{
				IsCartoonShootTest = false;
				IsServerCameraTest = false;
				Screen.showCursor = false;
			}
			else {
				pcvr.TKMoveSt = TestTKMoveSt;
			}

			if (IsServerCameraTest)
			{
				IsCartoonShootTest = false;
			}

			if (IsCartoonShootTest)
			{
				Screen.SetResolution(1280, 720, false);
			}
			else if (!XkGameCtrl.IsGameOnQuit)
			{
				if (Screen.fullScreen
					|| Screen.currentResolution.width != 1280
					|| Screen.currentResolution.height != 720)
				{
					if (GameMovieCtrl.IsActivePlayer && !GameMovieCtrl.IsTestXiaoScreen)
					{
						Screen.SetResolution(1280, 720, false);
					}
				}
			}

			NpcAmmoCtrl.NpcAmmoHitLayer = NpcAmmoHitLayer;
			GameObject obj = null;
			XkPlayerCtrl playerScript = null;
			Transform pathTran = null;
			if (GmCamMark != null)
			{
				FeiJiPlayerTran = GmCamMark.transform;
				FeiJiPlayerPath = FeiJiPlayerTran.parent.GetComponent<AiPathCtrl>();
				pathTran = FeiJiPlayerPath.transform;

				for (int i = 0; i < pathTran.childCount; i++)
				{
					if (FeiJiPlayerTran == pathTran.GetChild(i))
					{
						FeiJiMarkIndex = i + 1;
						break;
					}
				}
			}
			else {
				Debug.LogWarning("Unity:" + "FeiJiPlayerMark was wrong!");
				obj.name = "null";
				return;
			}

			Vector3 posPlayerFJ = new Vector3(0f, -1700f, 0f);
			switch (GameModeVal)
			{
				case GameMode.DanJiFeiJi:
					GameJiTaiSt = GameJiTaiType.FeiJiJiTai; //test.
					obj = (GameObject)Instantiate(GmCamPrefab, posPlayerFJ, FeiJiPlayerTran.rotation);
					playerScript = obj.GetComponent<XkPlayerCtrl>();
					playerScript.SetAiPathScript(FeiJiPlayerPath);
					break;

				case GameMode.DanJiTanKe:
					break;

				case GameMode.LianJi:
					//Debug.Log("Unity:"+"peerType "+Network.peerType);
					if (Network.peerType == NetworkPeerType.Disconnected)
					{
						obj = (GameObject)Instantiate(GmCamPrefab, posPlayerFJ, FeiJiPlayerTran.rotation);
						playerScript = obj.GetComponent<XkPlayerCtrl>();
						playerScript.SetAiPathScript(FeiJiPlayerPath);
					}
					else {
						if (Network.peerType == NetworkPeerType.Client)
						{
							Invoke("DelaySpawnClientPlayer", 6f);
						}
						else {
							AmmoNumMaxNpc = 25;
						}
					}
					break;
			}

			//CartoonCamPlayer
			//		if (GameModeVal != GameMode.LianJi || Network.peerType == NetworkPeerType.Disconnected) {
			//			obj = (GameObject)Instantiate(CartoonCamPlayer, CartoonCamPlayerTran.position, CartoonCamPlayerTran.rotation);
			//			playerScript = obj.GetComponent<XkPlayerCtrl>();
			//			playerScript.SetAiPathScript(CartoonCamPlayerPath);
			//		}

			GameObject objMiss = new GameObject();
			objMiss.name = "MissionCleanup";
			objMiss.transform.parent = transform;
			MissionCleanup = objMiss.transform;

            objMiss = new GameObject();
            objMiss.name = "DaoJuArray";
            objMiss.transform.parent = MissionCleanup;
            DaoJuArray = objMiss.transform;

            objMiss = new GameObject();
			objMiss.name = "NpcAmmoArray";
			objMiss.transform.parent = MissionCleanup;
			NpcAmmoArray = objMiss.transform;

			objMiss = new GameObject();
			objMiss.name = "NpcObjArray";
			objMiss.transform.parent = MissionCleanup;
			NpcObjArray = objMiss.transform;

            objMiss = new GameObject();
            objMiss.name = "NpcObjHiddenArray";
            objMiss.transform.parent = MissionCleanup;
            NpcObjHiddenArray = objMiss.transform;
            objMiss.SetActive(false);

            objMiss = new GameObject();
			objMiss.name = "PlayerAmmoArray";
			objMiss.transform.parent = MissionCleanup;
			PlayerAmmoArray = objMiss.transform;
			XKNpcSpawnListCtrl.GetInstance();

			PlayerYouLiangCur = 0f;
			Invoke("DelayResetIsLoadingLevel", 2f);
			Invoke("TestInitCameraRender", 0.5f);

			//if (!GameMovieCtrl.IsActivePlayer)
			//{
			//	if (XKGlobalData.GameVersionPlayer == 0)
			//	{
			//		SetActivePlayerOne(true);
			//	}
			//	else {
			//		SetActivePlayerThree(true);
			//	}
			//}
			IsPlayGamePOne = IsActivePlayerOne;
			IsPlayGamePTwo = IsActivePlayerTwo;
			IsPlayGamePThree = IsActivePlayerThree;
			IsPlayGamePFour = IsActivePlayerFour;
			AudioBeiJingCtrl.IndexBeiJingAd = 0;
			XKGlobalData.GetInstance().PlayGuanKaBeiJingAudio();
            InputEventCtrl.GetInstance().ClickTVYaoKongExitBtEvent += ClickTVYaoKongExitBtEvent;
            pcvr.GetInstance().AddTVYaoKongBtEvent();

            if (m_GameUICom != null)
            {
                m_GameUICom.CreatFuHuoCiShuPanel();
            }
        }
		catch (System.Exception e)
		{
			Debug.Log("Unity:!!!!!!!!!!!!!XKGameCtrl!!!!!!!!!!!!!!!!!!");
			Debug.LogException(e);
			Debug.Log("Unity:2" + e.Message);
		}
	}

    private void ClickTVYaoKongExitBtEvent(ButtonState val)
    {
        if (val == ButtonState.UP)
        {
            if (m_ExitUICom == null)
            {
                SpawnExitGameUI();
            }
        }
    }

#if UNITY_EDITOR
    bool IsFixedAllNpcSpawnTrigger;
	public XKTriggerSpawnNpc[] TriggerSpawnNpcList;
	void OnDrawGizmosSelected()
	{
		if (!enabled) {
			return;
		}

		if (IsFixedAllNpcSpawnTrigger) {
			IsFixedAllNpcSpawnTrigger = false;
			TriggerSpawnNpcList = GameObject.FindObjectsOfType(typeof(XKTriggerSpawnNpc)) as XKTriggerSpawnNpc[];
			for (int i = 0; i < TriggerSpawnNpcList.Length; i++) {
				TriggerSpawnNpcList[i].name = "TriggerSpawnNpc_"+i;
				TriggerSpawnNpcList[i].CheckSpawnPointInfo();
			}
		}
	}
#endif

    public static bool IsNoFireLiZi = true;
    public static bool IsNoAmmoBaoZhaLiZi = true;
    public static bool IsNoNpcBaoZhaLiZi = true;
    public static void TestDelayActivePlayerOne()
	{
		if (GameMovieCtrl.IsActivePlayer) {
			return;
		}
		XKPlayerMoveCtrl.GetInstancePOne().HiddenGamePlayer(1);
		if (XKGlobalData.GameVersionPlayer == 0) {
			SetActivePlayerOne(true);
		}
		else {
			SetActivePlayerThree(true);
		}
	}

	public void ChangeAudioListParent()
	{
		if (Network.peerType == NetworkPeerType.Server) {
			return;
		}

		if (Application.loadedLevel != (int)GameLevel.Scene_1) {
			return;
		}

		Debug.Log("Unity:"+"ChangeAudioListParent -> GameJiTaiSt "+GameJiTaiSt);
		switch (GameJiTaiSt) {
		case GameJiTaiType.FeiJiJiTai:
			if (XkPlayerCtrl.GetInstanceFeiJi() != null) {
				AudioManager.Instance.SetParentTran(XkPlayerCtrl.GetInstanceFeiJi().transform);
			}
			break;

		case GameJiTaiType.TanKeJiTai:
			if (XkPlayerCtrl.GetInstanceTanKe() != null) {
				AudioManager.Instance.SetParentTran(XkPlayerCtrl.GetInstanceTanKe().transform);
			}
			break;
		}
	}

	void DelaySpawnClientPlayer()
	{
		Vector3 posPlayerFJ = new Vector3(0f, -1700f, 0f);
		if (GameTypeCtrl.AppTypeStatic == AppGameType.LianJiFeiJi) {
			NetworkServerNet.GetInstance().SpawnNetPlayerObj(GmCamPrefab,
			                                                      FeiJiPlayerPath,
			                                                      posPlayerFJ,
			                                                 GmCamPrefab.transform.rotation);
		}
	}

	void Update()
	{
		if (!pcvr.bIsHardWare) {
//			if (IsCartoonShootTest) {
//				if (Input.GetKeyUp(KeyCode.N)) {
//					if (!XkGameCtrl.IsGameOnQuit && (Application.loadedLevel+1) < Application.levelCount) {
//						System.GC.Collect();
//						Application.LoadLevel((Application.loadedLevel+1));
//					}
//				}
//			}

			if (Input.GetKeyUp(KeyCode.X)) {
				IsShowDebugInfoBox = !IsShowDebugInfoBox; //test

                //if (XKNpcSpawnListCtrl.GetInstance() != null)
                //{
                //    XKNpcSpawnListCtrl.GetInstance().CleanGameNoUsedNpcData(); //test
                //}
            }

//			if (Input.GetKeyUp(KeyCode.P)) {
//				float bloodVal = 5000f;
//				SubGamePlayerHealth(PlayerEnum.PlayerOne, bloodVal, true);
//				SubGamePlayerHealth(PlayerEnum.PlayerTwo, bloodVal, true);
//				SubGamePlayerHealth(PlayerEnum.PlayerThree, bloodVal, true);
//				SubGamePlayerHealth(PlayerEnum.PlayerFour, bloodVal, true);
				//XKPlayerCamera.GetInstanceFeiJi().HandlePlayerCameraShake();
				//JiFenJieMianCtrl.GetInstance().ActiveJiFenJieMian();
				//XKDaoJuGlobalDt.SetTimeFenShuBeiLv(PlayerEnum.PlayerOne);
				//ActivePlayerToGame(PlayerEnum.PlayerOne, true);
				//XKGameStageCtrl.GetInstance().MoveIntoStageUI();
				//XKBossLXCtrl.GetInstance().StartPlayBossLaiXi();
				//BossRemoveAllNpcAmmo();
//			}
		}
		CheckNpcTranFromList();
	}

	void DelayResetIsLoadingLevel()
	{
		XkGameCtrl.ResetIsLoadingLevel();
		Debug.Log("Unity:!!!!!!DelayResetIsLoadingLevel!!!!!!");

	}

	public static void ResetIsLoadingLevel()
	{
		IsLoadingLevel = false;
	}

	void TestInitCameraRender()
	{
		if (GameModeVal != GameMode.LianJi) {
			return;
		}

		if(XKPlayerCamera.GetInstanceCartoon() != null) {
			XKPlayerCamera.GetInstanceCartoon().SetActiveCamera(false);
		}

		if (XKPlayerCamera.GetInstanceTanKe() != null) {
			XKPlayerCamera.GetInstanceTanKe().SetActiveCamera(false);
		}

		if (XKPlayerCamera.GetInstanceFeiJi() != null) {
			XKPlayerCamera.GetInstanceFeiJi().SetActiveCamera(false);
		}
		
		if (XKPlayerCamera.GetInstanceTanKe() != null) {
			XKPlayerCamera.GetInstanceTanKe().SetActiveCamera(true);
		}
		
		if (XKPlayerCamera.GetInstanceFeiJi() != null) {
			XKPlayerCamera.GetInstanceFeiJi().SetActiveCamera(true);
			XKPlayerCamera.GetInstanceFeiJi().camera.targetTexture = null;
		}

		if(XKPlayerCamera.GetInstanceCartoon() != null) {
			XKPlayerCamera.GetInstanceCartoon().SetActiveCamera(true);
		}

		if (Network.peerType == NetworkPeerType.Disconnected) {
			if (XKPlayerCamera.GetInstanceFeiJi() != null && XKPlayerCamera.GetInstanceTanKe() != null) {
				XKPlayerCamera.GetInstanceFeiJi().gameObject.tag = TagMainCamera;
				XKPlayerCamera.GetInstanceTanKe().gameObject.tag = TagNull;
			}
			else if (XKPlayerCamera.GetInstanceFeiJi() != null) {
				XKPlayerCamera.GetInstanceFeiJi().gameObject.tag = TagMainCamera;
			}
			else if (XKPlayerCamera.GetInstanceTanKe() != null) {
				XKPlayerCamera.GetInstanceTanKe().gameObject.tag = TagMainCamera;
			}
		}
		else {
			if (GameTypeCtrl.AppTypeStatic == AppGameType.LianJiTanKe) {
				if (XKPlayerCamera.GetInstanceTanKe() != null) {
					XKPlayerCamera.GetInstanceTanKe().gameObject.tag = TagMainCamera;
				}
			}
			else {
				if (XKPlayerCamera.GetInstanceFeiJi() != null) {
					XKPlayerCamera.GetInstanceFeiJi().gameObject.tag = TagMainCamera;
				}
			}
		}
	}

	public void ChangePlayerCameraTag()
	{
		if (GameModeVal != GameMode.LianJi) {
			return;
		}

		if (GameTypeCtrl.AppTypeStatic == AppGameType.LianJiTanKe) {
			if (XKPlayerCamera.GetInstanceTanKe() != null) {
				XKPlayerCamera.GetInstanceTanKe().gameObject.tag = TagMainCamera;
				XKPlayerCamera.GetInstanceTanKe().SetActiveCamera(true);
				XKPlayerCamera.GetInstanceTanKe().ActivePlayerCamera();
			}

			if (XKPlayerCamera.GetInstanceFeiJi() != null) {
				XKPlayerCamera.GetInstanceFeiJi().gameObject.tag = TagNull;
			}
		}
		else {
			if (XKPlayerCamera.GetInstanceTanKe() != null) {
				XKPlayerCamera.GetInstanceTanKe().gameObject.tag = TagNull;
			}

			if (XKPlayerCamera.GetInstanceFeiJi() != null) {
				XKPlayerCamera.GetInstanceFeiJi().gameObject.tag = TagMainCamera;
			}
		}
	}

	public List<Transform> GetNpcTranList()
	{
		return NpcTranList;
	}

	public void AddNpcTranToList(Transform tran)
	{
		if (tran == null || NpcTranList.Contains(tran)) {
			return;
		}
		NpcTranList.Add(tran);
	}

	public void RemoveNpcTranFromList(Transform tran)
	{
		if (tran == null || !NpcTranList.Contains(tran)) {
			return;
		}
		NpcTranList.Remove(tran);
	}

	public void CheckNpcTranFromList()
	{
		float dTime = Time.realtimeSinceStartup - TimeCheckNpcTranList;
		if (dTime < 0.1f) {
			return;
		}
		TimeCheckNpcTranList = Time.realtimeSinceStartup;

		int max = NpcTranList.Count;
		int[] countArray = new int[max];
		int indexCount = 0;
		for (int i = 0; i < max; i++) {
			if (NpcTranList[i] == null) {
				countArray[indexCount] = i;
				indexCount++;
			}
		}
		
		for (int i = 0; i < max; i++) {
			if (countArray[i] == 0 && i > 0) {
				break;
			}

			if (countArray[i] >= NpcTranList.Count) {
				break;
			}

			if (NpcTranList[countArray[i]] == null) {
				NpcTranList.RemoveAt(countArray[i]);
			}
		}
	}

	public void AddPlayerKillNpc(PlayerEnum playerSt, NpcJiFenEnum npcSt, int jiFenVal)
	{
//		Debug.Log("Unity:"+"AddPlayerKillNpc -> playerSt "+playerSt
//		          + ", jiFenVal "+jiFenVal);
		switch (playerSt) {
		case PlayerEnum.Null:
			if (XkGameCtrl.IsActivePlayerOne) {
				AddKillNpcToPlayerOne(npcSt, jiFenVal);
			}

			if (XkGameCtrl.IsActivePlayerTwo) {
				AddKillNpcToPlayerTwo(npcSt, jiFenVal);
			}
			
			if (XkGameCtrl.IsActivePlayerThree) {
				AddKillNpcToPlayerThree(npcSt, jiFenVal);
			}
			
			if (XkGameCtrl.IsActivePlayerFour) {
				AddKillNpcToPlayerFour(npcSt, jiFenVal);
			}
			break;

		case PlayerEnum.PlayerOne:
			AddKillNpcToPlayerOne(npcSt, jiFenVal);
			break;

		case PlayerEnum.PlayerTwo:
			AddKillNpcToPlayerTwo(npcSt, jiFenVal);
			break;
			
		case PlayerEnum.PlayerThree:
			AddKillNpcToPlayerThree(npcSt, jiFenVal);
			break;
			
		case PlayerEnum.PlayerFour:
			AddKillNpcToPlayerFour(npcSt, jiFenVal);
			break;
		}
	}

	void AddKillNpcToPlayerOne(NpcJiFenEnum npcSt, int jiFenVal)
	{
		switch (npcSt) {
		case NpcJiFenEnum.ShiBing:
			ShiBingNumPOne++;
			break;

		case NpcJiFenEnum.CheLiang:
			CheLiangNumPOne++;
			break;

		case NpcJiFenEnum.ChuanBo:
			ChuanBoNumPOne++;
			break;

		case NpcJiFenEnum.FeiJi:
			FeiJiNumPOne++;
			break;
		}
		XKPlayerFenShuCtrl.GetInstance().ShowPlayerFenShu(PlayerEnum.PlayerOne, jiFenVal);
	}

	void AddKillNpcToPlayerTwo(NpcJiFenEnum npcSt, int jiFenVal)
	{
		switch (npcSt) {
		case NpcJiFenEnum.ShiBing:
			ShiBingNumPTwo++;
			break;
			
		case NpcJiFenEnum.CheLiang:
			CheLiangNumPTwo++;
			break;
			
		case NpcJiFenEnum.ChuanBo:
			ChuanBoNumPTwo++;
			break;
			
		case NpcJiFenEnum.FeiJi:
			FeiJiNumPTwo++;
			break;
		}
		XKPlayerFenShuCtrl.GetInstance().ShowPlayerFenShu(PlayerEnum.PlayerTwo, jiFenVal);
	}
	
	void AddKillNpcToPlayerThree(NpcJiFenEnum npcSt, int jiFenVal)
	{
		switch (npcSt) {
		case NpcJiFenEnum.ShiBing:
			ShiBingNumPThree++;
			break;
			
		case NpcJiFenEnum.CheLiang:
			CheLiangNumPThree++;
			break;
			
		case NpcJiFenEnum.ChuanBo:
			ChuanBoNumPThree++;
			break;
			
		case NpcJiFenEnum.FeiJi:
			FeiJiNumPThree++;
			break;
		}
		XKPlayerFenShuCtrl.GetInstance().ShowPlayerFenShu(PlayerEnum.PlayerThree, jiFenVal);
	}
	
	void AddKillNpcToPlayerFour(NpcJiFenEnum npcSt, int jiFenVal)
	{
		switch (npcSt) {
		case NpcJiFenEnum.ShiBing:
			ShiBingNumPFour++;
			break;
			
		case NpcJiFenEnum.CheLiang:
			CheLiangNumPFour++;
			break;
			
		case NpcJiFenEnum.ChuanBo:
			ChuanBoNumPFour++;
			break;
			
		case NpcJiFenEnum.FeiJi:
			FeiJiNumPFour++;
			break;
		}
		XKPlayerFenShuCtrl.GetInstance().ShowPlayerFenShu(PlayerEnum.PlayerFour, jiFenVal);
	}

	public void AddDaoDanNum(PlayerEnum playerSt)
	{
		switch(playerSt) {
		case PlayerEnum.PlayerOne:
			DaoDanNumPOne += DaoDanBuJiNum;
			if (DaoDanNumPOne > 99) {
				DaoDanNumPOne = 99;
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			DaoDanNumPTwo += DaoDanBuJiNum;
			if (DaoDanNumPTwo > 99) {
				DaoDanNumPTwo = 99;
			}
			break;
		}
	}
	
	public void SubDaoDanNum(PlayerEnum playerSt)
	{
		switch(playerSt) {
		case PlayerEnum.PlayerOne:
			DaoDanNumPOne--;
			break;
			
		case PlayerEnum.PlayerTwo:
			DaoDanNumPTwo--;
			break;
		}
	}

	public void AddGaoBaoDanNum(PlayerEnum playerSt)
	{
		switch(playerSt) {
		case PlayerEnum.PlayerOne:
			if (!IsActivePlayerOne) {
				return;
			}

			GaoBaoDanNumPOne += GaoBaoDanBuJiNum;
			if (GaoBaoDanNumPOne > 99) {
				GaoBaoDanNumPOne = 99;
			}

			if (DanYaoInfoCtrl.GetInstanceOne() != null) {
				DanYaoInfoCtrl.GetInstanceOne().ShowHuoLiJQSprite(PlayerAmmoType.GaoBaoAmmo);
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (!IsActivePlayerTwo) {
				return;
			}
			
			GaoBaoDanNumPTwo += GaoBaoDanBuJiNum;
			if (GaoBaoDanNumPTwo > 99) {
				GaoBaoDanNumPTwo = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceTwo() != null) {
				DanYaoInfoCtrl.GetInstanceTwo().ShowHuoLiJQSprite(PlayerAmmoType.GaoBaoAmmo);
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (!IsActivePlayerThree) {
				return;
			}
			
			GaoBaoDanNumPThree += GaoBaoDanBuJiNum;
			if (GaoBaoDanNumPThree > 99) {
				GaoBaoDanNumPThree = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceThree() != null) {
				DanYaoInfoCtrl.GetInstanceThree().ShowHuoLiJQSprite(PlayerAmmoType.GaoBaoAmmo);
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (!IsActivePlayerFour) {
				return;
			}

			GaoBaoDanNumPFour += GaoBaoDanBuJiNum;
			if (GaoBaoDanNumPFour > 99) {
				GaoBaoDanNumPFour = 99;
			}

			if (DanYaoInfoCtrl.GetInstanceFour() != null) {
				DanYaoInfoCtrl.GetInstanceFour().ShowHuoLiJQSprite(PlayerAmmoType.GaoBaoAmmo);
			}
			break;
		}
		ResetPlayerAmmoNum(playerSt, BuJiBaoType.GaoBaoDan);
		XKPlayerAutoFire.GetInstanceAutoFire(playerSt).SetAmmoStateJiQiang(PlayerAmmoType.GaoBaoAmmo);
	}
	
	public void SubGaoBaoDanNum(PlayerEnum playerSt)
	{
		bool isHiddenDaoJuGui = false;
		switch (playerSt) {
		case PlayerEnum.PlayerOne:
			//Debug.Log("Unity:"+"SubGaoBaoDanNumPOne -> GaoBaoDanNumPOne "+GaoBaoDanNumPOne);
			GaoBaoDanNumPOne--;
			if (GaoBaoDanNumPOne <= 0) {
				isHiddenDaoJuGui = true;
			}

			if (DanYaoInfoCtrl.GetInstanceOne() != null) {
				DanYaoInfoCtrl.GetInstanceOne().CheckPlayerGaoBaoAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			//Debug.Log("Unity:"+"SubGaoBaoDanNumPTwo -> GaoBaoDanNumPTwo "+GaoBaoDanNumPTwo);
			GaoBaoDanNumPTwo--;
			if (GaoBaoDanNumPTwo <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceTwo() != null) {
				DanYaoInfoCtrl.GetInstanceTwo().CheckPlayerGaoBaoAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			//Debug.Log("Unity:"+"SubGaoBaoDanNumPThree -> GaoBaoDanNumPThree "+GaoBaoDanNumPThree);
			GaoBaoDanNumPThree--;
			if (GaoBaoDanNumPThree <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceThree() != null) {
				DanYaoInfoCtrl.GetInstanceThree().CheckPlayerGaoBaoAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			//Debug.Log("Unity:"+"SubGaoBaoDanNumPFour -> GaoBaoDanNumPFour "+GaoBaoDanNumPFour);
			GaoBaoDanNumPFour--;
			if (GaoBaoDanNumPFour <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceFour() != null) {
				DanYaoInfoCtrl.GetInstanceFour().CheckPlayerGaoBaoAmmoNum();
			}
			break;
		}

		if (isHiddenDaoJuGui) {
			DaoJuCtrl.GetInstance().HiddenPlayerDaoJuObj(playerSt, BuJiBaoType.GaoBaoDan);
			XKPlayerAutoFire.GetInstanceAutoFire(playerSt).SetAmmoStateJiQiang(PlayerAmmoType.PuTongAmmo);
		}
	}
	
	public void AddSanDanNum(PlayerEnum playerSt)
	{
		switch(playerSt) {
		case PlayerEnum.PlayerOne:
			if (!IsActivePlayerOne) {
				return;
			}
			
			SanDanNumPOne += SanDanBuJiNum;
			if (SanDanNumPOne > 99) {
				SanDanNumPOne = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceOne() != null) {
				DanYaoInfoCtrl.GetInstanceOne().ShowHuoLiJQSprite(PlayerAmmoType.SanDanAmmo);
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (!IsActivePlayerTwo) {
				return;
			}
			
			SanDanNumPTwo += SanDanBuJiNum;
			if (SanDanNumPTwo > 99) {
				SanDanNumPTwo = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceTwo() != null) {
				DanYaoInfoCtrl.GetInstanceTwo().ShowHuoLiJQSprite(PlayerAmmoType.SanDanAmmo);
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (!IsActivePlayerThree) {
				return;
			}
			
			SanDanNumPThree += SanDanBuJiNum;
			if (SanDanNumPThree > 99) {
				SanDanNumPThree = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceThree() != null) {
				DanYaoInfoCtrl.GetInstanceThree().ShowHuoLiJQSprite(PlayerAmmoType.SanDanAmmo);
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (!IsActivePlayerFour) {
				return;
			}
			
			SanDanNumPFour += SanDanBuJiNum;
			if (SanDanNumPFour > 99) {
				SanDanNumPFour = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceFour() != null) {
				DanYaoInfoCtrl.GetInstanceFour().ShowHuoLiJQSprite(PlayerAmmoType.SanDanAmmo);
			}
			break;
		}
		ResetPlayerAmmoNum(playerSt, BuJiBaoType.SanDan);
		XKPlayerAutoFire.GetInstanceAutoFire(playerSt).SetAmmoStateJiQiang(PlayerAmmoType.SanDanAmmo);
	}
	
	public void SubSanDanNum(PlayerEnum playerSt)
	{
		bool isHiddenDaoJuGui = false;
		switch (playerSt) {
		case PlayerEnum.PlayerOne:
			//Debug.Log("Unity:"+"SubSanDanNumPOne -> SanDanNumPOne "+SanDanNumPOne);
			SanDanNumPOne--;
			if (SanDanNumPOne <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceOne() != null) {
				DanYaoInfoCtrl.GetInstanceOne().CheckPlayerSanDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			//Debug.Log("Unity:"+"SubSanDanNumPTwo -> SanDanNumPTwo "+SanDanNumPTwo);
			SanDanNumPTwo--;
			if (SanDanNumPTwo <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceTwo() != null) {
				DanYaoInfoCtrl.GetInstanceTwo().CheckPlayerSanDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			//Debug.Log("Unity:"+"SubSanDanNumPThree -> SanDanNumPThree "+SanDanNumPThree);
			SanDanNumPThree--;
			if (SanDanNumPThree <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceThree() != null) {
				DanYaoInfoCtrl.GetInstanceThree().CheckPlayerSanDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			//Debug.Log("Unity:"+"SubSanDanNumPFour -> SanDanNumPFour "+SanDanNumPFour);
			SanDanNumPFour--;
			if (SanDanNumPFour <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceFour() != null) {
				DanYaoInfoCtrl.GetInstanceFour().CheckPlayerSanDanAmmoNum();
			}
			break;
		}
		
		if (isHiddenDaoJuGui) {
			DaoJuCtrl.GetInstance().HiddenPlayerDaoJuObj(playerSt, BuJiBaoType.SanDan);
			XKPlayerAutoFire.GetInstanceAutoFire(playerSt).SetAmmoStateJiQiang(PlayerAmmoType.PuTongAmmo);
		}
	}

	public void AddGenZongDanNum(PlayerEnum playerSt)
	{
		switch(playerSt) {
		case PlayerEnum.PlayerOne:
			if (!IsActivePlayerOne) {
				return;
			}
			
			GenZongDanNumPOne += GenZongDanBuJiNum;
			if (GenZongDanNumPOne > 99) {
				GenZongDanNumPOne = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceOne() != null) {
				DanYaoInfoCtrl.GetInstanceOne().ShowHuoLiJQSprite(PlayerAmmoType.GenZongAmmo);
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (!IsActivePlayerTwo) {
				return;
			}
			
			GenZongDanNumPTwo += GenZongDanBuJiNum;
			if (GenZongDanNumPTwo > 99) {
				GenZongDanNumPTwo = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceTwo() != null) {
				DanYaoInfoCtrl.GetInstanceTwo().ShowHuoLiJQSprite(PlayerAmmoType.GenZongAmmo);
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (!IsActivePlayerThree) {
				return;
			}
			
			GenZongDanNumPThree += GenZongDanBuJiNum;
			if (GenZongDanNumPThree > 99) {
				GenZongDanNumPThree = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceThree() != null) {
				DanYaoInfoCtrl.GetInstanceThree().ShowHuoLiJQSprite(PlayerAmmoType.GenZongAmmo);
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (!IsActivePlayerFour) {
				return;
			}
			
			GenZongDanNumPFour += GenZongDanBuJiNum;
			if (GenZongDanNumPFour > 99) {
				GenZongDanNumPFour = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceFour() != null) {
				DanYaoInfoCtrl.GetInstanceFour().ShowHuoLiJQSprite(PlayerAmmoType.GenZongAmmo);
			}
			break;
		}
		ResetPlayerAmmoNum(playerSt, BuJiBaoType.GenZongDan);
		XKPlayerAutoFire.GetInstanceAutoFire(playerSt).SetAmmoStateJiQiang(PlayerAmmoType.GenZongAmmo);
	}

	public void SubGenZongDanNum(PlayerEnum playerSt)
	{
		bool isHiddenDaoJuGui = false;
		switch (playerSt) {
		case PlayerEnum.PlayerOne:
			//Debug.Log("Unity:"+"SubGenZongDanNumPOne -> GenZongDanNumPOne "+GenZongDanNumPOne);
			GenZongDanNumPOne--;
			if (GenZongDanNumPOne <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceOne() != null) {
				DanYaoInfoCtrl.GetInstanceOne().CheckPlayerGenZongDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			//Debug.Log("Unity:"+"SubGenZongDanNumPTwo -> GenZongDanNumPTwo "+GenZongDanNumPTwo);
			GenZongDanNumPTwo--;
			if (GenZongDanNumPTwo <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceTwo() != null) {
				DanYaoInfoCtrl.GetInstanceTwo().CheckPlayerGenZongDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			//Debug.Log("Unity:"+"SubGenZongDanNumPThree -> GenZongDanNumPThree "+GenZongDanNumPThree);
			GenZongDanNumPThree--;
			if (GenZongDanNumPThree <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceThree() != null) {
				DanYaoInfoCtrl.GetInstanceThree().CheckPlayerGenZongDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			//Debug.Log("Unity:"+"SubGenZongDanNumPFour -> GenZongDanNumPFour "+GenZongDanNumPFour);
			GenZongDanNumPFour--;
			if (GenZongDanNumPFour <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceFour() != null) {
				DanYaoInfoCtrl.GetInstanceFour().CheckPlayerGenZongDanAmmoNum();
			}
			break;
		}
		
		if (isHiddenDaoJuGui) {
			DaoJuCtrl.GetInstance().HiddenPlayerDaoJuObj(playerSt, BuJiBaoType.GenZongDan);
			XKPlayerAutoFire.GetInstanceAutoFire(playerSt).SetAmmoStateJiQiang(PlayerAmmoType.PuTongAmmo);
		}
	}

	public void AddChuanTouDanNum(PlayerEnum playerSt)
	{
		switch(playerSt) {
		case PlayerEnum.PlayerOne:
			if (!IsActivePlayerOne) {
				return;
			}
			
			ChuanTouDanNumPOne += ChuanTouDanBuJiNum;
			if (ChuanTouDanNumPOne > 99) {
				ChuanTouDanNumPOne = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceOne() != null) {
				DanYaoInfoCtrl.GetInstanceOne().ShowHuoLiJQSprite(PlayerAmmoType.ChuanTouAmmo);
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (!IsActivePlayerTwo) {
				return;
			}
			
			ChuanTouDanNumPTwo += ChuanTouDanBuJiNum;
			if (ChuanTouDanNumPTwo > 99) {
				ChuanTouDanNumPTwo = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceTwo() != null) {
				DanYaoInfoCtrl.GetInstanceTwo().ShowHuoLiJQSprite(PlayerAmmoType.ChuanTouAmmo);
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (!IsActivePlayerThree) {
				return;
			}
			
			ChuanTouDanNumPThree += ChuanTouDanBuJiNum;
			if (ChuanTouDanNumPThree > 99) {
				ChuanTouDanNumPThree = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceThree() != null) {
				DanYaoInfoCtrl.GetInstanceThree().ShowHuoLiJQSprite(PlayerAmmoType.ChuanTouAmmo);
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (!IsActivePlayerFour) {
				return;
			}
			
			ChuanTouDanNumPFour += ChuanTouDanBuJiNum;
			if (ChuanTouDanNumPFour > 99) {
				ChuanTouDanNumPFour = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceFour() != null) {
				DanYaoInfoCtrl.GetInstanceFour().ShowHuoLiJQSprite(PlayerAmmoType.ChuanTouAmmo);
			}
			break;
		}
		ResetPlayerAmmoNum(playerSt, BuJiBaoType.ChuanTouDan);
		XKPlayerAutoFire.GetInstanceAutoFire(playerSt).SetAmmoStateZhuPao(PlayerAmmoType.ChuanTouAmmo);
	}
	
	public void SubChuanTouDanNum(PlayerEnum playerSt)
	{
		bool isHiddenDaoJuGui = false;
		switch (playerSt) {
		case PlayerEnum.PlayerOne:
			//Debug.Log("Unity:"+"SubChuanTouDanNumPOne -> ChuanTouDanNumPOne "+ChuanTouDanNumPOne);
			ChuanTouDanNumPOne--;
			if (ChuanTouDanNumPOne <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceOne() != null) {
				DanYaoInfoCtrl.GetInstanceOne().CheckPlayerChuanTouDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			//Debug.Log("Unity:"+"SubChuanTouDanNumPTwo -> ChuanTouDanNumPTwo "+ChuanTouDanNumPTwo);
			ChuanTouDanNumPTwo--;
			if (ChuanTouDanNumPTwo <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceTwo() != null) {
				DanYaoInfoCtrl.GetInstanceTwo().CheckPlayerChuanTouDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			//Debug.Log("Unity:"+"SubChuanTouDanNumPThree -> ChuanTouDanNumPThree "+ChuanTouDanNumPThree);
			ChuanTouDanNumPThree--;
			if (ChuanTouDanNumPThree <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceThree() != null) {
				DanYaoInfoCtrl.GetInstanceThree().CheckPlayerChuanTouDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			//Debug.Log("Unity:"+"SubChuanTouDanNumPFour -> ChuanTouDanNumPFour "+ChuanTouDanNumPFour);
			ChuanTouDanNumPFour--;
			if (ChuanTouDanNumPFour <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceFour() != null) {
				DanYaoInfoCtrl.GetInstanceFour().CheckPlayerChuanTouDanAmmoNum();
			}
			break;
		}
		
		if (isHiddenDaoJuGui) {
			//DaoJuCtrl.GetInstance().HiddenPlayerDaoJuObj(playerSt, BuJiBaoType.ChuanTouDan);
			XKPlayerAutoFire.GetInstanceAutoFire(playerSt).SetAmmoStateZhuPao(PlayerAmmoType.DaoDanAmmo);
		}
	}

	public void AddJianSuDanNum(PlayerEnum playerSt)
	{
		switch(playerSt) {
		case PlayerEnum.PlayerOne:
			if (!IsActivePlayerOne) {
				return;
			}
			
			JianSuDanNumPOne += JianSuDanBuJiNum;
			if (JianSuDanNumPOne > 99) {
				JianSuDanNumPOne = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceOne() != null) {
				DanYaoInfoCtrl.GetInstanceOne().ShowHuoLiJQSprite(PlayerAmmoType.JianSuAmmo);
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (!IsActivePlayerTwo) {
				return;
			}
			
			JianSuDanNumPTwo += JianSuDanBuJiNum;
			if (JianSuDanNumPTwo > 99) {
				JianSuDanNumPTwo = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceTwo() != null) {
				DanYaoInfoCtrl.GetInstanceTwo().ShowHuoLiJQSprite(PlayerAmmoType.JianSuAmmo);
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (!IsActivePlayerThree) {
				return;
			}
			
			JianSuDanNumPThree += JianSuDanBuJiNum;
			if (JianSuDanNumPThree > 99) {
				JianSuDanNumPThree = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceThree() != null) {
				DanYaoInfoCtrl.GetInstanceThree().ShowHuoLiJQSprite(PlayerAmmoType.JianSuAmmo);
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (!IsActivePlayerFour) {
				return;
			}
			
			JianSuDanNumPFour += JianSuDanBuJiNum;
			if (JianSuDanNumPFour > 99) {
				JianSuDanNumPFour = 99;
			}
			
			if (DanYaoInfoCtrl.GetInstanceFour() != null) {
				DanYaoInfoCtrl.GetInstanceFour().ShowHuoLiJQSprite(PlayerAmmoType.JianSuAmmo);
			}
			break;
		}
		ResetPlayerAmmoNum(playerSt, BuJiBaoType.JianSuDan);
		XKPlayerAutoFire.GetInstanceAutoFire(playerSt).SetAmmoStateJiQiang(PlayerAmmoType.JianSuAmmo);
	}
	
	public void SubJianSuDanNum(PlayerEnum playerSt)
	{
		bool isHiddenDaoJuGui = false;
		switch (playerSt) {
		case PlayerEnum.PlayerOne:
			//Debug.Log("Unity:"+"SubJianSuDanNumPOne -> JianSuDanNumPOne "+JianSuDanNumPOne);
			JianSuDanNumPOne--;
			if (JianSuDanNumPOne <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceOne() != null) {
				DanYaoInfoCtrl.GetInstanceOne().CheckPlayerJianSuDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			//Debug.Log("Unity:"+"SubJianSuDanNumPTwo -> JianSuDanNumPTwo "+JianSuDanNumPTwo);
			JianSuDanNumPTwo--;
			if (JianSuDanNumPTwo <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceTwo() != null) {
				DanYaoInfoCtrl.GetInstanceTwo().CheckPlayerJianSuDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			//Debug.Log("Unity:"+"SubJianSuDanNumPThree -> JianSuDanNumPThree "+JianSuDanNumPThree);
			JianSuDanNumPThree--;
			if (JianSuDanNumPThree <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceThree() != null) {
				DanYaoInfoCtrl.GetInstanceThree().CheckPlayerJianSuDanAmmoNum();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			//Debug.Log("Unity:"+"SubJianSuDanNumPFour -> JianSuDanNumPFour "+JianSuDanNumPFour);
			JianSuDanNumPFour--;
			if (JianSuDanNumPFour <= 0) {
				isHiddenDaoJuGui = true;
			}
			
			if (DanYaoInfoCtrl.GetInstanceFour() != null) {
				DanYaoInfoCtrl.GetInstanceFour().CheckPlayerJianSuDanAmmoNum();
			}
			break;
		}
		
		if (isHiddenDaoJuGui) {
			XKPlayerAutoFire.GetInstanceAutoFire(playerSt).SetAmmoStateJiQiang(PlayerAmmoType.PuTongAmmo);
			DaoJuCtrl.GetInstance().HiddenPlayerDaoJuObj(playerSt, BuJiBaoType.JianSuDan);
		}
	}
	
	public void ActivePlayerWuDiState(PlayerEnum playerSt)
	{
		if (playerSt == PlayerEnum.Null) {
			return;
		}
		XKPlayerMoveCtrl playerScript = XKPlayerMoveCtrl.GetXKPlayerMoveCtrl(playerSt);
		playerScript.ActivePlayerWuDiState();
	}

	public void AddPlayerYouLiang(float val)
	{
		IsAddPlayerYouLiang = false;

		float startVal = PlayerYouLiangCur;
		PlayerYouLiangCur += val;
		if (PlayerYouLiangCur > PlayerYouLiangMax) {
			PlayerYouLiangCur = PlayerYouLiangMax;
		}
		YouLiangCtrl.GetInstance().InitChangePlayerYouLiangFillAmout(startVal, PlayerYouLiangCur);

		if (PlayerYouLiangCur > YouLiangJingGaoVal) {
			YouLiangCtrl.GetInstance().SetActiveYouLiangFlash(false);
		}
	}

	void ResetPlayerYouLiangVal()
	{
		//YouLiangDianVal = 0;
		if (YouLiangAddCtrl.GetInstance() != null) {
			YouLiangAddCtrl.GetInstance().SetYouLiangSpriteAmount(0f);
		}
	}

	public static void SetActivePlayerOne(bool isActive)
	{
		IsActivePlayerOne = isActive;
		pcvr.OpenAllPlayerFangXiangPanPower(PlayerEnum.PlayerOne);
		CheckPlayerActiveNum();
		if (isActive) {
			IsPlayGamePOne = true;
			XKPlayerScoreCtrl.ShowPlayerScore(PlayerEnum.PlayerOne);
            XKGlobalData.GetInstance().AddJiHuoCiShu();
		}
		else {
			XKPlayerScoreCtrl.HiddenPlayerScore(PlayerEnum.PlayerOne);
		}

		if (_Instance != null) {
			_Instance.InitGamePlayerInfo(PlayerEnum.PlayerOne, isActive);
		}

		if (XkGameCtrl.GetInstance() == null ||
			(XkGameCtrl.GetInstance() != null && !XkGameCtrl.GetInstance().IsCartoonShootTest) ) {
			if (isActive && Application.loadedLevel == (int)GameLevel.Movie) {
				StopMovie();
			}
        }
        pcvr.GetInstance().SetIndexPlayerActiveGameState(0, (byte)(isActive == true ? 1 : 0));
    }

	public static void SetActivePlayerTwo(bool isActive)
	{
		IsActivePlayerTwo = isActive;
		pcvr.OpenAllPlayerFangXiangPanPower(PlayerEnum.PlayerTwo);
		CheckPlayerActiveNum();
		if (isActive) {
			IsPlayGamePTwo = true;
			XKPlayerScoreCtrl.ShowPlayerScore(PlayerEnum.PlayerTwo);
            XKGlobalData.GetInstance().AddJiHuoCiShu();
        }
		else {
			XKPlayerScoreCtrl.HiddenPlayerScore(PlayerEnum.PlayerTwo);
		}

		if (_Instance != null) {
			_Instance.InitGamePlayerInfo(PlayerEnum.PlayerTwo, isActive);
		}
		
		if (XkGameCtrl.GetInstance() == null ||
		    (XkGameCtrl.GetInstance() != null && !XkGameCtrl.GetInstance().IsCartoonShootTest) ) {
			if (isActive && Application.loadedLevel == (int)GameLevel.Movie) {
				StopMovie();
			}
        }
        pcvr.GetInstance().SetIndexPlayerActiveGameState(1, (byte)(isActive == true ? 1 : 0));
    }
	
	public static void SetActivePlayerThree(bool isActive)
	{
		IsActivePlayerThree = isActive;
		pcvr.OpenAllPlayerFangXiangPanPower(PlayerEnum.PlayerThree);
		CheckPlayerActiveNum();
		if (isActive) {
			IsPlayGamePThree = true;
			XKPlayerScoreCtrl.ShowPlayerScore(PlayerEnum.PlayerThree);
            XKGlobalData.GetInstance().AddJiHuoCiShu();
        }
		else {
			XKPlayerScoreCtrl.HiddenPlayerScore(PlayerEnum.PlayerThree);
		}

		if (_Instance != null) {
			_Instance.InitGamePlayerInfo(PlayerEnum.PlayerThree, isActive);
		}
		
		if (XkGameCtrl.GetInstance() == null ||
		    (XkGameCtrl.GetInstance() != null && !XkGameCtrl.GetInstance().IsCartoonShootTest) ) {
			if (isActive && Application.loadedLevel == (int)GameLevel.Movie) {
				StopMovie();
			}
        }
        pcvr.GetInstance().SetIndexPlayerActiveGameState(2, (byte)(isActive == true ? 1 : 0));
    }

	public static void SetActivePlayerFour(bool isActive)
	{
		IsActivePlayerFour = isActive;
		pcvr.OpenAllPlayerFangXiangPanPower(PlayerEnum.PlayerFour);
		CheckPlayerActiveNum();
		if (isActive) {
			IsPlayGamePFour = true;
			XKPlayerScoreCtrl.ShowPlayerScore(PlayerEnum.PlayerFour);
            XKGlobalData.GetInstance().AddJiHuoCiShu();
        }
		else {
			XKPlayerScoreCtrl.HiddenPlayerScore(PlayerEnum.PlayerFour);
		}

		if (_Instance != null) {
			_Instance.InitGamePlayerInfo(PlayerEnum.PlayerFour, isActive);
		}
		
		if (XkGameCtrl.GetInstance() == null ||
		    (XkGameCtrl.GetInstance() != null && !XkGameCtrl.GetInstance().IsCartoonShootTest) ) {
			if (isActive && Application.loadedLevel == (int)GameLevel.Movie) {
				StopMovie();
			}
        }
        pcvr.GetInstance().SetIndexPlayerActiveGameState(3, (byte)(isActive == true ? 1 : 0));
    }

	static void SetPlayerFireMaxAmmoCount()
	{		
		if (!IsActivePlayerOne || !IsActivePlayerTwo) {
			XKPlayerAutoFire.MaxAmmoCount = 15;
		}
		else {
			XKPlayerAutoFire.MaxAmmoCount = 30;
		}
	}

	static void StopMovie()
	{
		if (Application.loadedLevel != (int)GameLevel.Movie) {
			return;
		}
		GameMovieCtrl.GetInstance().StopPlayMovie();
		switch (GameTypeCtrl.AppTypeStatic) {
		case AppGameType.DanJiFeiJi:
		case AppGameType.DanJiTanKe:
			GameModeCtrl.GetInstance().SetActiveLoading(true);
			break;

		case AppGameType.LianJiFeiJi:
		case AppGameType.LianJiTanKe:
//			GameModeCtrl.GetInstance().ShowGameMode();
			break;
		}
	}

	public static void LoadingGameScene_1()
	{
		//Debug.Log("Unity:"+"LoadingGameScene_1...");
		if (GameJiLuFenShuCtrl.GetInstance() != null) {
			GameJiLuFenShuCtrl.GetInstance().HiddenGameJiLuFenShu();
		}

		GameMovieCtrl.GetInstance().StopPlayMovie();
		XkGameCtrl.IsLoadingLevel = true;
		if (!XkGameCtrl.IsGameOnQuit) {
			System.GC.Collect();
			Application.LoadLevel((int)GameLevel.Scene_1);
		}
	}

	public static void LoadingGameMovie(int key = 0)
	{
		if (IsLoadingLevel) {
			return;
		}

		if (NetworkServerNet.GetInstance() != null && NetCtrl.GetInstance() != null && key == 0) {
			NetworkServerNet.GetInstance().MakeClientDisconnect(); //Close ClientNet
			NetworkServerNet.GetInstance().MakeServerDisconnect(); //Close ServerNet
		}
		ResetGameInfo();
		
		IsLoadingLevel = true;
		SetActivePlayerOne(false);
		SetActivePlayerTwo(false);
		SetActivePlayerThree(false);
		SetActivePlayerFour(false);
        
        pcvr.GetInstance().ClearGameWeiXinData();
		if (!IsGameOnQuit) {
            XKGlobalData.GetInstance().ResetJiHuoCiShu();
            System.GC.Collect();
			Application.LoadLevel((int)GameLevel.Movie);
		}
	}

	public static void AddPlayerYouLiangToMax()
	{
		if (Application.loadedLevel == (int)GameLevel.Scene_1) {
			PlayerYouLiangMax = 120f;
		}
		else {
			PlayerYouLiangMax = 60f;
		}
		//Debug.Log("Unity:"+"AddPlayerYouLiangToMax -> PlayerYouLiangMax "+PlayerYouLiangMax);
		PlayerYouLiangCur = PlayerYouLiangMax;
//		PlayerYouLiangCur = 10f; //test
		if (YouLiangCtrl.GetInstance() != null) {
			YouLiangCtrl.GetInstance().SetActiveYouLiangFlash(false);
		}
	}

	public static void OnPlayerFinishTask()
	{
		if (IsActiveFinishTask) {
			return;
		}
		IsActiveFinishTask = true;
		JiFenJieMianCtrl.GetInstance().ShowFinishTaskInfo();
	}
	
	public int GetFeiJiMarkIndex()
	{
		return FeiJiMarkIndex;
	}

	public int GetTanKeMarkIndex()
	{
		return TanKeMarkIndex;
	}

	public int GetCartoonCamMarkIndex()
	{
		return CartoonCamMarkIndex;
	}

	public static void AddCartoonTriggerSpawnList(XKTriggerRemoveNpc script)
	{
		if (script == null) {
			return;
		}

		if (CartoonTriggerSpawnList.Contains(script)) {
			return;
		}
		CartoonTriggerSpawnList.Add(script);
	}

	public static void ClearCartoonSpawnNpc()
	{
		Debug.Log("Unity:"+"ClearCartoonSpawnNpc...");
		int max = CartoonTriggerSpawnList.Count;
		for (int i = 0; i < max; i++) {
			CartoonTriggerSpawnList[i].RemoveSpawnPointNpc();
		}
		CartoonTriggerSpawnList.Clear();
	}
	
	public static void AddNpcAmmoList(GameObject obj)
	{
		if (NpcAmmoList.Contains(obj)) {
			return;
		}
		CountNpcAmmo++;
		NpcAmmoList.Add(obj);
	}
	
	public static void RemoveNpcAmmoList(GameObject obj)
	{
		if (!NpcAmmoList.Contains(obj)) {
			return;
		}
		CountNpcAmmo--;
		NpcAmmoList.Remove(obj);
	}

	void CheckNpcAmmoList()
	{
		float dTime = Time.realtimeSinceStartup - TimeCheckNpcAmmo;
		if (dTime < 0.1f) {
			return;
		}
		TimeCheckNpcAmmo = Time.realtimeSinceStartup;
		
		int maxAmmo = AmmoNumMaxNpc;
		if (NpcAmmoList.Count <= maxAmmo) {
			return;
		}
		
		int num = NpcAmmoList.Count - maxAmmo;
		GameObject[] ammoArray = new GameObject[num];
		for (int i = 0; i < num; i++) {
			ammoArray[i] = NpcAmmoList[i];
		}
		
		NpcAmmoCtrl script = null;
		for (int i = 0; i < num; i++) {
			if (ammoArray[i] == null) {
				continue;
			}
			
			script = ammoArray[i].GetComponent<NpcAmmoCtrl>();
			if (script == null) {
				NpcAmmoList.Remove(ammoArray[i]);
				continue;
			}
			script.GameNeedRemoveAmmo();
		}
	}

	public static void BossRemoveAllNpcAmmo()
	{
		NpcAmmoCtrl[] npcAmmoCom = NpcAmmoArray.GetComponentsInChildren<NpcAmmoCtrl>();
		for (int i = 0; i < npcAmmoCom.Length; i++) {
			npcAmmoCom[i].RemoveAmmo(1);
		}
	}

	public void ChangeBoxColliderSize(Transform tran)
	{
		Vector3 scaleVal = tran.localScale;
		scaleVal.z = 1f;
		tran.localScale = scaleVal;

		BoxCollider box = tran.GetComponent<BoxCollider>();
		Vector3 sizeBox = box.size;
		sizeBox.z = TriggerBoxSize_Z;
		box.size = sizeBox;
	}

	public static void SetServerCameraTran(Transform tran)
	{
		if (ServerCameraPar != null) {
			ServerCameraPar.SetActive(false);
		}
		ServerCameraPar = tran.gameObject;

		if (!tran.gameObject.activeSelf) {
			tran.gameObject.SetActive(true);
		}

		if (tran.camera != null && tran.camera.enabled) {
			tran.camera.enabled = false;
		}

		Transform serverCamTran = ServerCameraObj.transform;
		serverCamTran.parent = tran;
		serverCamTran.localPosition = Vector3.zero;
		serverCamTran.localEulerAngles = Vector3.zero;
		if (!ServerCameraObj.activeSelf) {
			ServerCameraObj.SetActive(true);
		}
	}

	public static void CheckObjDestroyThisTimed(GameObject obj)
	{
		if (GameMovieCtrl.IsActivePlayer) {
			return;
		}

		if (obj == null) {
			return;
		}

		DestroyThisTimed script = obj.GetComponent<DestroyThisTimed>();
		if (script == null) {
			script = obj.AddComponent<DestroyThisTimed>();
			script.TimeRemove = 5f;
			Debug.LogError("Unity:"+"obj is not find DestroyThisTimed! name is "+obj.name);
		}
	}

	public static void ResetGameInfo()
	{
		DaoDanNumPOne = 0;
		DaoDanNumPTwo = 0;
		GaoBaoDanNumPOne = 0;
		GaoBaoDanNumPTwo = 0;
	}
	
	public static void SetParentTran(Transform tran, Transform parTran)
	{
		tran.parent = parTran;
		tran.localPosition = Vector3.zero;
		tran.localEulerAngles = Vector3.zero;
	}

	public static void HiddenMissionCleanup()
	{
		if (MissionCleanup == null || !MissionCleanup.gameObject.activeSelf) {
			return;
		}

//		if (Network.peerType == NetworkPeerType.Client) {
//			return;
//		}
		if (GameModeVal == GameMode.LianJi) {
			return;
		}
		MissionCleanup.gameObject.SetActive(false);
	}

	public static bool GetMissionCleanupIsActive()
	{
		return MissionCleanup.gameObject.activeSelf;
	}

	public static void ActiveServerCameraTran()
	{
		Debug.Log("Unity:"+"ActiveServerCameraTran...");
		ServerPortCameraCtrl.RandOpenServerPortCamera();
	}
	
	public static void AddYLDLv(YouLiangDianMoveCtrl script)
	{
		YouLiangDengJi levelValTmp = script.LevelVal;
		switch (levelValTmp) {
		case YouLiangDengJi.Level_1:
			if (YLDLvA.Contains(script)) {
				return;
			}
			YLDLvA.Add(script);
			break;
			
		case YouLiangDengJi.Level_2:
			if (YLDLvB.Contains(script)) {
				return;
			}
			YLDLvB.Add(script);
			break;
			
		case YouLiangDengJi.Level_3:
			if (YLDLvC.Contains(script)) {
				return;
			}
			YLDLvC.Add(script);
			break;
		}
	}

	public static YouLiangDianMoveCtrl GetYLDMoveScript(YouLiangDengJi levelValTmp)
	{
		int maxNum = 0;
		YouLiangDianMoveCtrl yldScript = null;
		switch (levelValTmp) {
		case YouLiangDengJi.Level_1:
			maxNum = YLDLvA.Count;
			for (int i = 0; i < maxNum; i++) {
				if (YLDLvA[i] != null && !YLDLvA[i].gameObject.activeSelf) {
					yldScript = YLDLvA[i];
					break;
				}
			}
			break;
			
		case YouLiangDengJi.Level_2:
			maxNum = YLDLvB.Count;
			for (int i = 0; i < maxNum; i++) {
				if (YLDLvB[i] != null && !YLDLvB[i].gameObject.activeSelf) {
					yldScript = YLDLvB[i];
					break;
				}
			}
			break;
			
		case YouLiangDengJi.Level_3:
			maxNum = YLDLvC.Count;
			for (int i = 0; i < maxNum; i++) {
				if (YLDLvC[i] != null && !YLDLvC[i].gameObject.activeSelf) {
					yldScript = YLDLvC[i];
					break;
				}
			}
			break;
		}
		
		if (yldScript == null) {
			yldScript = YouLiangDianUICtrl.GetInstance().SpawnYouLiangDianUI(levelValTmp);
			AddYLDLv(yldScript);
		}
		return yldScript;
	}

	public static void CheckPlayerActiveNum()
	{
		int countPlayer = 0;
		if (IsActivePlayerOne) {
			countPlayer++;
			ActivePlayerToGame(PlayerEnum.PlayerOne);
//			PlayerJiFenArray[0] = PlayerJiFenArray[0] == 0 ? 10 : PlayerJiFenArray[0];
		}
		
		if (IsActivePlayerTwo) {
			countPlayer++;
			ActivePlayerToGame(PlayerEnum.PlayerTwo);
//			PlayerJiFenArray[1] = PlayerJiFenArray[1] == 0 ? 10 : PlayerJiFenArray[1];
		}
		
		if (IsActivePlayerThree) {
			countPlayer++;
			ActivePlayerToGame(PlayerEnum.PlayerThree);
//			PlayerJiFenArray[2] = PlayerJiFenArray[2] == 0 ? 10 : PlayerJiFenArray[2];
		}
		
		if (IsActivePlayerFour) {
			countPlayer++;
			ActivePlayerToGame(PlayerEnum.PlayerFour);
//			PlayerJiFenArray[3] = PlayerJiFenArray[3] == 0 ? 10 : PlayerJiFenArray[3];
		}
		PlayerActiveNum = countPlayer;
	}

	public static void ActivePlayerToGame(PlayerEnum indexVal, bool isChangePos = false)
	{
		if (XKPlayerCamera.GetInstanceFeiJi() == null) {
			return;
		}

		if (!GetIsActivePlayer(indexVal)) {
			return;
		}

		Vector3 pos = Vector3.zero;
		Transform tranPoint = null;
		switch (indexVal) {
		case PlayerEnum.PlayerOne:
			tranPoint = XKPlayerCamera.GetInstanceFeiJi().PlayerSpawnPoint[0];
			pos = _Instance.GetActivePlayerPos(tranPoint, PlayerEnum.PlayerOne);
			XKPlayerMoveCtrl.GetInstancePOne().ActivePlayerToPos(pos, tranPoint.up, isChangePos);
			break;
			
		case PlayerEnum.PlayerTwo:
			tranPoint = XKPlayerCamera.GetInstanceFeiJi().PlayerSpawnPoint[1];
			pos = _Instance.GetActivePlayerPos(tranPoint, PlayerEnum.PlayerTwo);
			XKPlayerMoveCtrl.GetInstancePTwo().ActivePlayerToPos(pos, tranPoint.up, isChangePos);
			break;
			
		case PlayerEnum.PlayerThree:
			if (XKGlobalData.GameVersionPlayer == 0) {
				tranPoint = XKPlayerCamera.GetInstanceFeiJi().PlayerSpawnPoint[2];
			}
			else {
				tranPoint = XKPlayerCamera.GetInstanceFeiJi().PlayerSpawnPoint[1];
			}
			pos = _Instance.GetActivePlayerPos(tranPoint, PlayerEnum.PlayerThree);
			XKPlayerMoveCtrl.GetInstancePThree().ActivePlayerToPos(pos, tranPoint.up, isChangePos);
			break;
			
		case PlayerEnum.PlayerFour:
			if (XKGlobalData.GameVersionPlayer == 0) {
				tranPoint = XKPlayerCamera.GetInstanceFeiJi().PlayerSpawnPoint[3];
			}
			else {
				tranPoint = XKPlayerCamera.GetInstanceFeiJi().PlayerSpawnPoint[2];
			}
			pos = _Instance.GetActivePlayerPos(tranPoint, PlayerEnum.PlayerFour);
			XKPlayerMoveCtrl.GetInstancePFour().ActivePlayerToPos(pos, tranPoint.up, isChangePos);
			break;
		}
	}

	Vector3 GetActivePlayerPos(Transform tran, PlayerEnum indexPlayer = PlayerEnum.Null)
	{
		Vector3 startPos = tran.position;
		Vector3 posTmp = startPos;
		Vector3 forwardVal = tran.forward;
		RaycastHit hitInfo;
		float disVal = 25f;
		Physics.Raycast(startPos, forwardVal, out hitInfo, disVal, LandLayer);
		if (hitInfo.collider != null){
			posTmp = hitInfo.point;
		}
		else {
			bool isContinue = true;
			int indexVal = 0;
			int indexTmp = (int)(indexPlayer - 1);
			Transform tranTmp = null;
			do {
				//Debug.Log("Unity:"+"indexVal "+indexVal);
				if (indexVal >= 4) {
					isContinue = false;
					startPos = tran.position;
					posTmp = startPos;
					break;
				}

				if (indexVal == indexTmp) {
					indexVal++;
					continue;
				}

				tranTmp = XKPlayerCamera.GetInstanceFeiJi().PlayerSpawnPoint[indexVal];
				startPos = tranTmp.position;
				posTmp = startPos;
				forwardVal = tranTmp.forward;
				Physics.Raycast(startPos, forwardVal, out hitInfo, disVal, LandLayer);
				if (hitInfo.collider != null){
					posTmp = hitInfo.point;
					isContinue = false;
					break;
				}
				indexVal++;
			} while(isContinue);
		}
		return posTmp;
	}

	void InitGamePlayerInfo(PlayerEnum indexVal, bool isActive)
	{
		int indexPlayer = (int)indexVal - 1;
		DaoJiShiCtrl djsCtrl = null;
		DanYaoInfoCtrl dyCtrl = null;
		XueKuangCtrl xkCtrl = null;
		PlayerXueTiaoCtrl xtCtrl = null;
		switch (indexVal) {
		case PlayerEnum.PlayerOne:
			xkCtrl = XueKuangCtrl.GetInstanceOne();
			xtCtrl = PlayerXueTiaoCtrl.GetInstanceOne();
			djsCtrl = DaoJiShiCtrl.GetInstanceOne();
			dyCtrl = DanYaoInfoCtrl.GetInstanceOne();
			break;
			
		case PlayerEnum.PlayerTwo:
			xkCtrl = XueKuangCtrl.GetInstanceTwo();
			xtCtrl = PlayerXueTiaoCtrl.GetInstanceTwo();
			djsCtrl = DaoJiShiCtrl.GetInstanceTwo();
			dyCtrl = DanYaoInfoCtrl.GetInstanceTwo();
			break;
			
		case PlayerEnum.PlayerThree:
			xkCtrl = XueKuangCtrl.GetInstanceThree();
			xtCtrl = PlayerXueTiaoCtrl.GetInstanceThree();
			djsCtrl = DaoJiShiCtrl.GetInstanceThree();
			dyCtrl = DanYaoInfoCtrl.GetInstanceThree();
			break;
			
		case PlayerEnum.PlayerFour:
			xkCtrl = XueKuangCtrl.GetInstanceFour();
			xtCtrl = PlayerXueTiaoCtrl.GetInstanceFour();
			djsCtrl = DaoJiShiCtrl.GetInstanceFour();
			dyCtrl = DanYaoInfoCtrl.GetInstanceFour();
			break;
		}

		if (isActive) {
			PlayerHealthArray[indexPlayer] = MaxPlayerHealth;
			if (djsCtrl != null) {
				djsCtrl.StopDaoJiShi();
			}
			
			if (dyCtrl != null) {
				dyCtrl.ShowPlayerDanYaoInfo();
			}
			
			if (xtCtrl != null) {
				xtCtrl.HandlePlayerXueTiaoInfo(1f);
			}
		}
		else {
			PlayerHealthArray[indexPlayer] = 0f;
			if (!IsLoadingLevel) {
				if (djsCtrl != null) {
					djsCtrl.StartPlayDaoJiShi();
				}
				
				if (dyCtrl != null) {
					dyCtrl.HiddenPlayerDanYaoInfo();
				}
			}
			
			if (xtCtrl != null) {
				xtCtrl.HandlePlayerXueTiaoInfo(0f);
			}
		}

		if (xkCtrl != null) {
			xkCtrl.HandleXueKuangNum();
		}
	}

	public static float KeyBloodUI = 0f;
	/**
	 * isSubHealth == true -> 无论是否为无敌状态均要减血,掉落悬崖.
	 */
	public void SubGamePlayerHealth(PlayerEnum indexVal, float valSub, bool isSubHealth = false)
	{
		if (valSub == 0) {
			return;
		}

		if (XKBossXueTiaoCtrl.IsWuDiPlayer) {
			return;
		}
        
        if (pcvr.GetInstance().m_SSMiGuTvCheck != null
            && pcvr.GetInstance().m_SSMiGuTvCheck.IsDisplayMiGuPayUI)
        {
            //咪咕电视游戏支付界面显示时,玩家不受npc子弹的伤害.
            return;
        }

        /*if (indexVal != PlayerEnum.Null) {
			int indexTmp = (int)indexVal - 1;
			if (isCheckHealth && PlayerHealthArray[indexTmp] - valSub <= 0f) {
				return;
			}
		}*/

        XKPlayerMoveCtrl playerMoveCtrl = XKPlayerMoveCtrl.GetXKPlayerMoveCtrl(indexVal);
		if (playerMoveCtrl != null) {
			if (playerMoveCtrl.GetIsMoveToTiaoYueDian()) {
				return;
			}
			
//			Debug.Log("Unity:"+"SubGamePlayerHealth -> indexVal "+indexVal
//			          +", isWuDi "+playerMoveCtrl.GetIsWuDiState()
//			          +", isShanShuo "+playerMoveCtrl.GetIsShanShuoState());
			if (!playerMoveCtrl.GetIsWuDiState()
			    && !playerMoveCtrl.GetIsShanShuoState()) {
				if (XKDaoJuGlobalDt.GetPlayerIsHuoLiAllOpen(indexVal) == true) {
					//XKDaoJuGlobalDt.SetPlayerIsHuoLiAllOpen(indexVal, false);
					XKPlayerHuoLiAllOpenUICtrl.GetInstanceHuoLiOpen(indexVal).HiddenHuoLiOpenUI();
				}
				else {
					playerMoveCtrl.SetIsQianHouFire(false);
					playerMoveCtrl.SetIsChangChengFire(false);
					playerMoveCtrl.SetIsJiQiangSanDanFire(false);
					playerMoveCtrl.SetIsQiangJiFire(false);
					playerMoveCtrl.SetIsPaiJiPaoFire(false);
					playerMoveCtrl.SetIsSanDanZPFire(false);
					XKPlayerAutoFire.GetInstanceAutoFire(indexVal).SetAmmoStateZhuPao(PlayerAmmoType.DaoDanAmmo);
					XKPlayerAutoFire.GetInstanceAutoFire(indexVal).SetAmmoStateJiQiang(PlayerAmmoType.PuTongAmmo);
				}
			}
		}

		if (IsCartoonShootTest) {
			return;
		}
		pcvr.GetInstance().ActiveFangXiangDouDong(indexVal, false);

		switch (indexVal) {
		case PlayerEnum.PlayerOne:
			if (!IsActivePlayerOne
			    || (!isSubHealth && XKPlayerMoveCtrl.GetInstancePOne().GetIsShanShuoState())) {
				return;
			}
			XKPlayerMoveCtrl.GetInstancePOne().ShowPlayerShanShuo();
			
			valSub *= PlayerQuanShu[0];
			PlayerHealthArray[0] -= valSub;
			if (XueKuangCtrl.GetInstanceOne() != null) {
				XueKuangCtrl.GetInstanceOne().HandlePlayerXueTiaoInfo(PlayerHealthArray[0]);
			}
			
			if (PlayerXueTiaoCtrl.GetInstanceOne() != null) {
				PlayerXueTiaoCtrl.GetInstanceOne().HandlePlayerXueTiaoInfo(PlayerHealthArray[0] / MaxPlayerHealth);
			}

			if (PlayerHealthArray[0] <= 0f) {
				Debug.Log("Unity:"+"SubGamePlayerHealth -> PlayerOne is death!");
				PlayerHealthArray[0] = 0f;
				PlayerQuanShu[0] = 1;
				SetActivePlayerOne(false);
				XKPlayerMoveCtrl.GetInstancePOne().HiddenGamePlayer();
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (!IsActivePlayerTwo
			    || (!isSubHealth && XKPlayerMoveCtrl.GetInstancePTwo().GetIsShanShuoState())) {
				return;
			}
			XKPlayerMoveCtrl.GetInstancePTwo().ShowPlayerShanShuo();
			
			valSub *= PlayerQuanShu[1];
			PlayerHealthArray[1] -= valSub;
			if (XueKuangCtrl.GetInstanceTwo() != null) {
				XueKuangCtrl.GetInstanceTwo().HandlePlayerXueTiaoInfo(PlayerHealthArray[1]);
			}
			
			if (PlayerXueTiaoCtrl.GetInstanceTwo() != null) {
				PlayerXueTiaoCtrl.GetInstanceTwo().HandlePlayerXueTiaoInfo(PlayerHealthArray[1] / MaxPlayerHealth);
			}

			if (PlayerHealthArray[1] <= 0f) {
				Debug.Log("Unity:"+"SubGamePlayerHealth -> PlayerTwo is death!");
				PlayerHealthArray[1] = 0f;
				PlayerQuanShu[1] = 1;
				SetActivePlayerTwo(false);
				XKPlayerMoveCtrl.GetInstancePTwo().HiddenGamePlayer();
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (!IsActivePlayerThree
			    || (!isSubHealth && XKPlayerMoveCtrl.GetInstancePThree().GetIsShanShuoState())) {
				return;
			}
			XKPlayerMoveCtrl.GetInstancePThree().ShowPlayerShanShuo();
			
			valSub *= PlayerQuanShu[2];
			PlayerHealthArray[2] -= valSub;
			if (XueKuangCtrl.GetInstanceThree() != null) {
				XueKuangCtrl.GetInstanceThree().HandlePlayerXueTiaoInfo(PlayerHealthArray[2]);
			}
			
			if (PlayerXueTiaoCtrl.GetInstanceThree() != null) {
				PlayerXueTiaoCtrl.GetInstanceThree().HandlePlayerXueTiaoInfo(PlayerHealthArray[2] / MaxPlayerHealth);
			}

			if (PlayerHealthArray[2] <= 0f) {
//				#if UNITY_EDITOR
//				Debug.Log("Unity:"+"SubGamePlayerHealth -> PlayerThree is death!");
//				#endif
				PlayerHealthArray[2] = 0f;
				PlayerQuanShu[2] = 1;
				SetActivePlayerThree(false);
				XKPlayerMoveCtrl.GetInstancePThree().HiddenGamePlayer();
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (!IsActivePlayerFour
			    || (!isSubHealth && XKPlayerMoveCtrl.GetInstancePFour().GetIsShanShuoState())) {
				return;
			}
			XKPlayerMoveCtrl.GetInstancePFour().ShowPlayerShanShuo();
			
			valSub *= PlayerQuanShu[3];
			PlayerHealthArray[3] -= valSub;
			if (XueKuangCtrl.GetInstanceFour() != null) {
				XueKuangCtrl.GetInstanceFour().HandlePlayerXueTiaoInfo(PlayerHealthArray[3]);
			}
			
			if (PlayerXueTiaoCtrl.GetInstanceFour() != null) {
				PlayerXueTiaoCtrl.GetInstanceFour().HandlePlayerXueTiaoInfo(PlayerHealthArray[3] / MaxPlayerHealth);
			}

			if (PlayerHealthArray[3] <= 0f) {
				Debug.Log("Unity:"+"SubGamePlayerHealth -> PlayerFour is death!");
				PlayerHealthArray[3] = 0f;
				PlayerQuanShu[3] = 1;
				SetActivePlayerFour(false);
				XKPlayerMoveCtrl.GetInstancePFour().HiddenGamePlayer();
			}
			break;
		}
		
		if (isSubHealth) {
			XKPlayerZhuiYaCtrl.GetInstance().ShowPlayerZhuiYaUI(indexVal);
		}
	}

	public static void AddPlayerHealth(PlayerEnum playerIndex, float healthVal)
	{
		if (playerIndex == PlayerEnum.Null) {
			return;
		}

		int indexVal = (int)playerIndex - 1;
		PlayerHealthArray[indexVal] += healthVal;
		if (PlayerHealthArray[indexVal] > MaxPlayerHealth) {
			PlayerHealthArray[indexVal] = MaxPlayerHealth;
		}

		XueKuangCtrl xueKuangScript = XueKuangCtrl.GetXueKuangCtrl(playerIndex);
		if (xueKuangScript != null) {
			xueKuangScript.HandlePlayerXueTiaoInfo(PlayerHealthArray[indexVal]);
		}
		
		if (PlayerXueTiaoCtrl.GetInstance(playerIndex) != null) {
			PlayerXueTiaoCtrl.GetInstance(playerIndex).HandlePlayerXueTiaoInfo(PlayerHealthArray[indexVal] / MaxPlayerHealth);
		}
	}

	public GameObject GetRandAimPlayerObj()
	{
		if (XkGameCtrl.PlayerActiveNum <= 0) {
			return null;
		}

		int count = 0;
		GameObject playerObj = null;
		int randVal = Random.Range(0, 100) % 4;
		do {
			switch (randVal) {
			case 0:
				if (XkGameCtrl.IsActivePlayerOne) {
					playerObj = XKPlayerMoveCtrl.GetInstancePOne().GenZongDanAimPoint;
				}
				break;
				
			case 1:
				if (XkGameCtrl.IsActivePlayerTwo) {
					playerObj = XKPlayerMoveCtrl.GetInstancePTwo().GenZongDanAimPoint;
				}
				break;
				
			case 2:
				if (XkGameCtrl.IsActivePlayerThree) {
					playerObj = XKPlayerMoveCtrl.GetInstancePThree().GenZongDanAimPoint;
				}
				break;
				
			case 3:
				if (XkGameCtrl.IsActivePlayerFour) {
					playerObj = XKPlayerMoveCtrl.GetInstancePFour().GenZongDanAimPoint;
				}
				break;
			}
			
			if (playerObj != null) {
				break;
			}
			randVal = Random.Range(0, 100) % 4;
			count++;
			if (count > 8) {
				break;
			}
		} while (playerObj == null);
		//Debug.Log("Unity:"+"GetRandAimPlayerObj -> player "+playerObj.name);
		return playerObj;
	}

	public GameObject GetMaxHealthPlayer()
	{
		GameObject playerObj = null;
		List<float> healthList = new List<float>(PlayerHealthArray);
		healthList.Sort();
		healthList.Reverse();
		for (int j = 0; j < 4; j++) {
			if (XkGameCtrl.PlayerJiFenArray[0] == healthList[j]) {
				switch (j) {
				case 0:
					playerObj = XKPlayerMoveCtrl.GetInstancePOne().GenZongDanAimPoint;
					break;
				case 1:
					playerObj = XKPlayerMoveCtrl.GetInstancePTwo().GenZongDanAimPoint;
					break;
				case 2:
					playerObj = XKPlayerMoveCtrl.GetInstancePThree().GenZongDanAimPoint;
					break;
				case 3:
					playerObj = XKPlayerMoveCtrl.GetInstancePFour().GenZongDanAimPoint;
					break;
				}
				break;
			}
		}
		return playerObj;
	}

	public void AddPlayerQuanShu()
	{
		if (IsActivePlayerOne) {
			PlayerQuanShu[0]++;
		}
		
		if (IsActivePlayerTwo) {
			PlayerQuanShu[1]++;
		}
		
		if (IsActivePlayerThree) {
			PlayerQuanShu[2]++;
		}

		if (IsActivePlayerFour) {
			PlayerQuanShu[3]++;
		}
	}
	
	void ResetPlayerAmmoNum(PlayerEnum playerSt, BuJiBaoType bjType)
	{
		switch (playerSt) {
		case PlayerEnum.PlayerOne:
			if (bjType == BuJiBaoType.ChuanTouDan) {
				GaoBaoDanNumPOne = 0;
				SanDanNumPOne = 0;
				GenZongDanNumPOne = 0;
				//ChuanTouDanNumPOne = 0;
				JianSuDanNumPOne = 0;
			}
			
			if (bjType == BuJiBaoType.GaoBaoDan) {
				//GaoBaoDanNumPOne = 0;
				SanDanNumPOne = 0;
				GenZongDanNumPOne = 0;
				ChuanTouDanNumPOne = 0;
				JianSuDanNumPOne = 0;
			}
			
			if (bjType == BuJiBaoType.GenZongDan) {
				GaoBaoDanNumPOne = 0;
				SanDanNumPOne = 0;
				//GenZongDanNumPOne = 0;
				ChuanTouDanNumPOne = 0;
				JianSuDanNumPOne = 0;
			}
			
			if (bjType == BuJiBaoType.JianSuDan) {
				GaoBaoDanNumPOne = 0;
				SanDanNumPOne = 0;
				GenZongDanNumPOne = 0;
				ChuanTouDanNumPOne = 0;
				//JianSuDanNumPOne = 0;
			}
			
			if (bjType == BuJiBaoType.SanDan) {
				GaoBaoDanNumPOne = 0;
				//SanDanNumPOne = 0;
				GenZongDanNumPOne = 0;
				ChuanTouDanNumPOne = 0;
				JianSuDanNumPOne = 0;
			}
			break;
			
		case PlayerEnum.PlayerTwo:
			if (bjType == BuJiBaoType.ChuanTouDan) {
				GaoBaoDanNumPTwo = 0;
				SanDanNumPTwo = 0;
				GenZongDanNumPTwo = 0;
				//ChuanTouDanNumPTwo = 0;
				JianSuDanNumPTwo = 0;
			}
			
			if (bjType == BuJiBaoType.GaoBaoDan) {
				//GaoBaoDanNumPTwo = 0;
				SanDanNumPTwo = 0;
				GenZongDanNumPTwo = 0;
				ChuanTouDanNumPTwo = 0;
				JianSuDanNumPTwo = 0;
			}
			
			if (bjType == BuJiBaoType.GenZongDan) {
				GaoBaoDanNumPTwo = 0;
				SanDanNumPTwo = 0;
				//GenZongDanNumPTwo = 0;
				ChuanTouDanNumPTwo = 0;
				JianSuDanNumPTwo = 0;
			}
			
			if (bjType == BuJiBaoType.JianSuDan) {
				GaoBaoDanNumPTwo = 0;
				SanDanNumPTwo = 0;
				GenZongDanNumPTwo = 0;
				ChuanTouDanNumPTwo = 0;
				//JianSuDanNumPTwo = 0;
			}
			
			if (bjType == BuJiBaoType.SanDan) {
				GaoBaoDanNumPTwo = 0;
				//SanDanNumPTwo = 0;
				GenZongDanNumPTwo = 0;
				ChuanTouDanNumPTwo = 0;
				JianSuDanNumPTwo = 0;
			}
			break;
			
		case PlayerEnum.PlayerThree:
			if (bjType == BuJiBaoType.ChuanTouDan) {
				GaoBaoDanNumPThree = 0;
				SanDanNumPThree = 0;
				GenZongDanNumPThree = 0;
				//ChuanTouDanNumPThree = 0;
				JianSuDanNumPThree = 0;
			}
			
			if (bjType == BuJiBaoType.GaoBaoDan) {
				//GaoBaoDanNumPThree = 0;
				SanDanNumPThree = 0;
				GenZongDanNumPThree = 0;
				ChuanTouDanNumPThree = 0;
				JianSuDanNumPThree = 0;
			}
			
			if (bjType == BuJiBaoType.GenZongDan) {
				GaoBaoDanNumPThree = 0;
				SanDanNumPThree = 0;
				//GenZongDanNumPThree = 0;
				ChuanTouDanNumPThree = 0;
				JianSuDanNumPThree = 0;
			}
			
			if (bjType == BuJiBaoType.JianSuDan) {
				GaoBaoDanNumPThree = 0;
				SanDanNumPThree = 0;
				GenZongDanNumPThree = 0;
				ChuanTouDanNumPThree = 0;
				//JianSuDanNumPThree = 0;
			}
			
			if (bjType == BuJiBaoType.SanDan) {
				GaoBaoDanNumPThree = 0;
				//SanDanNumPThree = 0;
				GenZongDanNumPThree = 0;
				ChuanTouDanNumPThree = 0;
				JianSuDanNumPThree = 0;
			}
			break;
			
		case PlayerEnum.PlayerFour:
			if (bjType == BuJiBaoType.ChuanTouDan) {
				GaoBaoDanNumPFour = 0;
				SanDanNumPFour = 0;
				GenZongDanNumPFour = 0;
				//ChuanTouDanNumPFour = 0;
				JianSuDanNumPFour = 0;
			}
			
			if (bjType == BuJiBaoType.GaoBaoDan) {
				//GaoBaoDanNumPFour = 0;
				SanDanNumPFour = 0;
				GenZongDanNumPFour = 0;
				ChuanTouDanNumPFour = 0;
				JianSuDanNumPFour = 0;
			}
			
			if (bjType == BuJiBaoType.GenZongDan) {
				GaoBaoDanNumPFour = 0;
				SanDanNumPFour = 0;
				//GenZongDanNumPFour = 0;
				ChuanTouDanNumPFour = 0;
				JianSuDanNumPFour = 0;
			}
			
			if (bjType == BuJiBaoType.JianSuDan) {
				GaoBaoDanNumPFour = 0;
				SanDanNumPFour = 0;
				GenZongDanNumPFour = 0;
				ChuanTouDanNumPFour = 0;
				//JianSuDanNumPFour = 0;
			}
			
			if (bjType == BuJiBaoType.SanDan) {
				GaoBaoDanNumPFour = 0;
				//SanDanNumPFour = 0;
				GenZongDanNumPFour = 0;
				ChuanTouDanNumPFour = 0;
				JianSuDanNumPFour = 0;
			}
			break;
		}
	}

	public Vector3 GetWorldObjToScreenPos(Vector3 worldPos)
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
		screenPos.z = 0f;
		screenPos.x = screenPos.x < 0f ? 0f : screenPos.x;
		screenPos.x = screenPos.x > Screen.width ? Screen.width : screenPos.x;
		screenPos.y = screenPos.y < 0f ? 0f : screenPos.y;
		screenPos.y = screenPos.y > Screen.height ? Screen.height : screenPos.y;
		
		screenPos.x *= (XkGameCtrl.ScreenWidth / Screen.width);
		screenPos.y *= (XkGameCtrl.ScreenHeight / Screen.height);
		
		screenPos.x = screenPos.x < 0f ? 0f : screenPos.x;
		screenPos.x = screenPos.x > XkGameCtrl.ScreenWidth ? XkGameCtrl.ScreenWidth : screenPos.x;
		screenPos.y = screenPos.y < 0f ? 0f : screenPos.y;
		screenPos.y = screenPos.y > XkGameCtrl.ScreenHeight ? XkGameCtrl.ScreenHeight : screenPos.y;
		return screenPos;
	}

	public static bool GetIsActivePlayer(PlayerEnum playerIndex)
	{
		bool isActivePlayer = false;
		switch (playerIndex) {
		case PlayerEnum.PlayerOne:
			isActivePlayer = IsActivePlayerOne;
			break;
		case PlayerEnum.PlayerTwo:
			isActivePlayer = IsActivePlayerTwo;
			break;
		case PlayerEnum.PlayerThree:
			isActivePlayer = IsActivePlayerThree;
			break;
		case PlayerEnum.PlayerFour:
			isActivePlayer = IsActivePlayerFour;
			break;
		}
		return isActivePlayer;
	}

	public static bool CheckNpcIsMoveToCameraBack(Transform npcTr)
	{
		if (Camera.main == null
		    || XKPlayerMvFanWei.GetInstanceHou() == null
		    || npcTr == null) {
			return false;
		}

		Transform camTr = Camera.main.transform;
		Transform camBackTr = XKPlayerMvFanWei.GetInstanceHou().transform;
		Vector3 vecA = camTr.forward;
		Vector3 vecB = npcTr.position - camBackTr.position;
		vecA.y = vecB.y = 0f;
		if (Vector3.Dot(vecA, vecB) < 0f) {
			return true;
		}
		return false;

	}
    
    /// <summary>
    /// 退出游戏UI界面控制脚本.
    /// </summary>
    SSExitGameUI m_ExitUICom;
    /// <summary>
    /// 产生退出游戏UI界面.
    /// </summary>
    void SpawnExitGameUI()
    {
        Debug.Log("Unity: SpawnExitGameUI...");
        if (m_ExitUICom == null)
        {
            GameObject obj = (GameObject)Instantiate(m_GameUIDt.ExitGameUIPrefab, m_GameUIDt.UICenterTrParent);
            m_ExitUICom = obj.GetComponent<SSExitGameUI>();
            m_ExitUICom.Init();
        }
    }

    public void RemoveExitGameUI()
    {
        Debug.Log("Unity: RemoveExitGameUI...");
        if (m_ExitUICom != null)
        {
            m_ExitUICom.RemoveSelf();
        }
    }

    void OnGUI()
	{
		if (IsCartoonShootTest || !IsShowDebugInfoBox) {
			return;
		}

		float hight = 20f;
		float width = 600;
		string infoA = "PH1: "+PlayerHealthArray[0]+", PH2: "+PlayerHealthArray[1]
		+", PH3: "+PlayerHealthArray[2]+", PH4: "+PlayerHealthArray[3];
		GUI.Box(new Rect(0f, 0f, width, hight), infoA);

		infoA = "PJF1: "+PlayerJiFenArray[0]+", PJF2: "+PlayerJiFenArray[1]
		+", PJF3: "+PlayerJiFenArray[2]+", PJF4: "+PlayerJiFenArray[3];
		GUI.Box(new Rect(0f, hight, width, hight), infoA);
		
		infoA = "PQN1: "+pcvr.QiNangArray[0]+" "+pcvr.QiNangArray[1]+" "+pcvr.QiNangArray[2]+" "+pcvr.QiNangArray[3]
		+", PQN2: "+pcvr.QiNangArray[4]+" "+pcvr.QiNangArray[5]+" "+pcvr.QiNangArray[6]+" "+pcvr.QiNangArray[7]
		+", PQN3: "+pcvr.QiNangArray[8]+" "+pcvr.QiNangArray[9]+" "+pcvr.QiNangArray[10]+" "+pcvr.QiNangArray[11]
		+", PQN4: "+pcvr.QiNangArray[12]+" "+pcvr.QiNangArray[13]+" "+pcvr.QiNangArray[14]+" "+pcvr.QiNangArray[15]
		+", PZYQN: "+pcvr.QiNangArray[16]+" "+pcvr.QiNangArray[17]+" "+pcvr.QiNangArray[18]+" "+pcvr.QiNangArray[19];
		GUI.Box(new Rect(0f, hight * 2f, width, hight), infoA);
		
		infoA = "PRZY1: "+pcvr.RunZuoYiState[0]
		+", PRZY2: "+pcvr.RunZuoYiState[1]
		+", PRZY3: "+pcvr.RunZuoYiState[2]
		+", PRZY4: "+pcvr.RunZuoYiState[3]
		+", DongGanState "+pcvr.DongGanState;
		GUI.Box(new Rect(0f, hight * 3f, width, hight), infoA);

		infoA = "Coin1: "+XKGlobalData.CoinPlayerOne
				+", Coin2: "+XKGlobalData.CoinPlayerTwo
				+", Coin3: "+XKGlobalData.CoinPlayerThree
				+", Coin4: "+XKGlobalData.CoinPlayerFour;
		GUI.Box(new Rect(0f, hight * 4f, width, hight), infoA);

		infoA = "fxZD1 "+pcvr.FangXiangPanDouDongVal[0].ToString("x2")
				+", fxZD2 "+pcvr.FangXiangPanDouDongVal[1].ToString("x2")
				+", fxZD3 "+pcvr.FangXiangPanDouDongVal[2].ToString("x2")
				+", fxZD4 "+pcvr.FangXiangPanDouDongVal[3].ToString("x2");
		GUI.Box(new Rect(0f, hight * 5f, width, hight), infoA);
	}
}