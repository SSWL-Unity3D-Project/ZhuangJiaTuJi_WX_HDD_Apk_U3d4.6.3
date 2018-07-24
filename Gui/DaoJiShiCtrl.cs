﻿using UnityEngine;

public class DaoJiShiCtrl : MonoBehaviour
{
	public PlayerEnum PlayerIndex = PlayerEnum.PlayerOne;
	public GameObject ContinueGameObj;
	public GameObject GameOverObj;
    /// <summary>
    /// 电视遥控器确认按键图片.
    /// </summary>
    public GameObject m_TVYaoKongEnterObj;
	GameObject DaoJiShiObj;
	UISprite DaoJiShiSprite;
	bool IsPlayDaoJishi;
	int DaoJiShiCount = 9;
	public static int CountDaoJiShi;
	
	static DaoJiShiCtrl InstanceOne;
	public static DaoJiShiCtrl GetInstanceOne()
	{
		return InstanceOne;
	}
	
	static DaoJiShiCtrl InstanceTwo;
	public static DaoJiShiCtrl GetInstanceTwo()
	{
		return InstanceTwo;
	}
	
	static DaoJiShiCtrl InstanceThree;
	public static DaoJiShiCtrl GetInstanceThree()
	{
		return InstanceThree;
	}
	
	static DaoJiShiCtrl InstanceFour;
	public static DaoJiShiCtrl GetInstanceFour()
	{
		return InstanceFour;
	}

	public static DaoJiShiCtrl GetInstance(PlayerEnum indexPlayer)
	{
		DaoJiShiCtrl djsInstance = null;
		switch (indexPlayer) {
		case PlayerEnum.PlayerOne:
			djsInstance = InstanceOne;
			break;
		case PlayerEnum.PlayerTwo:
			djsInstance = InstanceTwo;
			break;
		case PlayerEnum.PlayerThree:
			djsInstance = InstanceThree;
			break;
		case PlayerEnum.PlayerFour:
			djsInstance = InstanceFour;
			break;
		}
		return djsInstance;
	}

	// Use this for initialization
	void Start()
	{
		CountDaoJiShi = 0;
		switch (PlayerIndex) {
		case PlayerEnum.PlayerOne:
			InstanceOne = this;
			break;
			
		case PlayerEnum.PlayerTwo:
			InstanceTwo = this;
			break;
			
		case PlayerEnum.PlayerThree:
			InstanceThree = this;
			break;
			
		case PlayerEnum.PlayerFour:
			InstanceFour = this;
			break;
		}
//		IsActivePlayerOne = false;
//		IsActivePlayerTwo = false;
		DaoJiShiObj = gameObject;
		DaoJiShiSprite = GetComponent<UISprite>();
		DaoJiShiObj.SetActive(false);
		ContinueGameObj.SetActive(false);
        m_TVYaoKongEnterObj.SetActive(false);
        HiddenGameOverObj();
	}

    public static void HiddenAllTVYaoKongEnterUI()
    {
        if (InstanceOne != null)
        {
            InstanceOne.HiddenTVYaoKongEnterObj();
        }

        if (InstanceTwo != null)
        {
            InstanceTwo.HiddenTVYaoKongEnterObj();
        }

        if (InstanceThree != null)
        {
            InstanceThree.HiddenTVYaoKongEnterObj();
        }

        if (InstanceFour != null)
        {
            InstanceFour.HiddenTVYaoKongEnterObj();
        }
    }

    void HiddenTVYaoKongEnterObj()
    {
        m_TVYaoKongEnterObj.SetActive(false);
    }

    public void StartPlayDaoJiShi()
	{
        if (XkGameCtrl.GetInstance() != null && XkGameCtrl.GetInstance().m_GameUICom != null)
        {
            bool isFuHuoPlayer = XkGameCtrl.GetInstance().m_GameUICom.GetIsFuHuoPlayer();
            if (!isFuHuoPlayer)
            {
                Debug.LogWarning("Player cannot fuHuo...");
                HiddenAllTVYaoKongEnterUI();
                if (XkGameCtrl.PlayerActiveNum <= 0)
                {
                    //没有激活的玩家,显示游戏结束界面.
                    GameOverCtrl.GetInstance().ShowGameOver();
                    //关闭咪咕电视游戏包月查询事件.
                    pcvr.GetInstance().CloseDelayQueryMiGuTVGameBaoYueStata();
                }
                return;
            }
        }

		if (GameOverCtrl.IsShowGameOver) {
			return;
		}

		if (IsPlayDaoJishi) {
			return;
		}
		IsPlayDaoJishi = true;
		CountDaoJiShi++;
		DaoJiShiCount = 9;
		DaoJiShiSprite.spriteName = "daoJiShi9";
		//DaoJiShiObj.SetActive(true);
		//ContinueGameObj.SetActive(true);
        m_TVYaoKongEnterObj.SetActive(true);
        //ShowDaoJiShiInfo();
		//XKGlobalData.GetInstance().StopAudioRanLiaoJingGao();
		pcvr.CloseAllQiNangArray(PlayerIndex, 1);
	}

	public void StopDaoJiShi()
	{
		HiddenGameOverObj();
		if (!IsPlayDaoJishi) {
			return;
		}
		IsPlayDaoJishi = false;
		CountDaoJiShi--;
		ContinueGameObj.SetActive(false);
		DaoJiShiObj.SetActive(false);
        m_TVYaoKongEnterObj.SetActive(false);

    }

	void ShowDaoJiShiInfo()
	{
		XKGlobalData.GetInstance().PlayAudioXuBiDaoJiShi();
		TweenScale tweenScaleCom = GetComponent<TweenScale>();
		if (tweenScaleCom != null) {
			DestroyObject(tweenScaleCom);
		}

		tweenScaleCom = DaoJiShiObj.AddComponent<TweenScale>();
		tweenScaleCom.enabled = false;
		tweenScaleCom.duration = 2.2f;
		tweenScaleCom.from = new Vector3(6f, 6f, 1f);
		tweenScaleCom.to = new Vector3(4f, 4f, 1f);
		EventDelegate.Add(tweenScaleCom.onFinished, delegate{
			ChangeDaoJiShiVal();
		});
		tweenScaleCom.enabled = true;
		tweenScaleCom.PlayForward();
	}

	void ChangeDaoJiShiVal()
	{
		if (JiFenJieMianCtrl.GetInstance().GetIsShowFinishTask()) {
			StopDaoJiShi();
			return;
		}

		if (DaoJiShiCount <= 1) {
			StopDaoJiShi();
			ShowGameOverObj();
//			if (XkGameCtrl.PlayerActiveNum <= 0 && CountDaoJiShi > 0) {
//				Debug.LogWarning("Unity:"+"ChangeDaoJiShiVal -> CountDaoJiShi "+CountDaoJiShi);
//			}

			if (XkGameCtrl.PlayerActiveNum <= 0 && CountDaoJiShi <= 0) {
				GameOverCtrl.GetInstance().ShowGameOver();
			}
			return;
		}

		DaoJiShiCount--;
		DaoJiShiSprite.spriteName = "daoJiShi" + DaoJiShiCount;
		ShowDaoJiShiInfo();
	}

	public bool GetIsPlayDaoJishi()
	{
		return IsPlayDaoJishi;
	}

	void ShowGameOverObj()
	{
		GameOverObj.SetActive(true);
		Invoke("HiddenGameOverObj", 3f);
	}

	void HiddenGameOverObj()
	{
		if (!GameOverObj.activeSelf) {
			return;
		}
		CancelInvoke("HiddenGameOverObj");
		GameOverObj.SetActive(false);
	}
}