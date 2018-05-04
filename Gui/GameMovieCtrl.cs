//#define TEST_MOVIE
using UnityEngine;

public class GameMovieCtrl : MonoBehaviour
{
    /// <summary>
    /// Websocket预制.
    /// </summary>
    public GameObject m_WebSocketBoxPrefab;
	public MovieTexture Movie;
	public static bool IsTestLJGame; //测试联机小窗口游戏.
	AudioSource AudioSourceObj;
	bool IsStopMovie;
	public static bool IsActivePlayer;
	float TimeVal;
	public static bool IsTestXiaoScreen;
	public static bool IsOpenFXZhenDong = true;
	enum QualityLevelEnum
	{
		Fastest,
		Fast,
		Simple,
		Good,
		Beautiful,
		Fantastic
	}
	static GameMovieCtrl _instance;
	public static GameMovieCtrl GetInstance()
	{
		return _instance;
	}
	
	// Use this for initialization
	void Start()
	{
		_instance = this;
		XKGlobalData.GetInstance();
		AudioListener.volume = (float)XKGlobalData.GameAudioVolume / 10f;
		if (AudioListCtrl.GetInstance() != null) {
			AudioListCtrl.GetInstance().CloseGameAudioBJ();
		}
		Screen.showCursor = false;
		LoadingGameCtrl.ResetLoadingInfo();
		Time.timeScale = 1.0f;
		AudioManager.Instance.SetParentTran(null);
		GameOverCtrl.IsShowGameOver = false;
		if (IsOpenFXZhenDong) {
			pcvr.OpenAllPlayerFangXiangPanPower();
		}
		pcvr.CloseAllQiNangArray(PlayerEnum.Null, 1);
		//IsTestLJGame = true; //test
		//IsTestXiaoScreen = true; //test
		if (!XkGameCtrl.IsGameOnQuit) {
			if (Screen.fullScreen
			    || Screen.currentResolution.width != 1280
			    || Screen.currentResolution.height != 720) {
				if (!IsTestLJGame && !IsTestXiaoScreen) {
					Screen.SetResolution(1280, 720, false);
				}
			}
		}

		if (!IsTestLJGame) {
			IsActivePlayer = true;
			if (IsTestXiaoScreen) {
				Screen.SetResolution(680, 384, false); //test
			}
		}

		QualitySettings.SetQualityLevel((int)QualityLevelEnum.Fast);
		AudioSourceObj = transform.GetComponent<AudioSource>();
		Invoke("DelayResetIsLoadingLevel", 4f);
		if (IsOpenFXZhenDong) {
			IsOpenFXZhenDong = false;
			Invoke("CloseAllFangXiangPanPower", 10f);
		}
		PlayMovie();
	}

	void DelayResetIsLoadingLevel()
	{
		XkGameCtrl.ResetIsLoadingLevel();
		if (NetworkServerNet.GetInstance() != null) {
			NetworkServerNet.GetInstance().TryToCreateServer();
		}
	}
	
	void PlayMovie()
	{
		if (renderer != null) {
			renderer.enabled = true;
			renderer.material.mainTexture = Movie;
		}
		Movie.loop = true;
		Movie.Play();
		
		#if TEST_MOVIE
		TimeMv = Time.realtimeSinceStartup;
		#endif
		
		if (AudioSourceObj != null) {
			AudioSourceObj.clip = Movie.audioClip;
			AudioSourceObj.enabled = true;
			AudioSourceObj.Play();
		}
	}

	public void StopPlayMovie()
	{
		if (IsStopMovie) {
			return;
		}
		IsStopMovie = true;
		Movie.Stop();
		if (AudioSourceObj != null) {
			AudioSourceObj.Stop();
			AudioSourceObj.enabled = false;
		}

		if (Network.peerType == NetworkPeerType.Server) {
			return;
		}
		gameObject.SetActive(false);
	}

	void CloseAllFangXiangPanPower()
	{
		pcvr.GetInstance().CloseFangXiangPanPower();
	}

    /// <summary>
    /// 产生Websocket预制.
    /// </summary>
    public GameObject SpawnWebSocketBox(Transform tr)
    {
        Debug.Log("SpawnWebSocketBox...");
        GameObject obj = (GameObject)Instantiate(m_WebSocketBoxPrefab);
        obj.transform.parent = tr;
        return obj;
    }

#if TEST_MOVIE
	int CountMv = 0;
	float TimeMv = 0f;
	void OnGUI()
	{
		float timeMvPlay = Time.realtimeSinceStartup - TimeMv;
		if (timeMvPlay >= Movie.duration) {
			TimeMv = Time.realtimeSinceStartup;
			timeMvPlay = 0f;
			CountMv++;
		}

		string mvInfo = "MvDuration "+Movie.duration.ToString()
						+", CountMv "+CountMv
						+", timeMvPlay "+timeMvPlay.ToString("f2");
		GUI.Box(new Rect(0f, 0f, 500f, 35f), mvInfo);
	}
#endif
}