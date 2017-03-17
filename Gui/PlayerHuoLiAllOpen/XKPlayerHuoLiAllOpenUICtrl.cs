using UnityEngine;
using System.Collections;

public class XKPlayerHuoLiAllOpenUICtrl : MonoBehaviour
{
	public PlayerEnum PlayerSt;
	UISprite HuoLiSprite;
	static XKPlayerHuoLiAllOpenUICtrl _InstanceOne;
	static XKPlayerHuoLiAllOpenUICtrl _InstanceTwo;
	static XKPlayerHuoLiAllOpenUICtrl _InstanceThree;
	static XKPlayerHuoLiAllOpenUICtrl _InstanceFour;
	public static XKPlayerHuoLiAllOpenUICtrl GetInstanceHuoLiOpen(PlayerEnum indexPlayer)
	{
		XKPlayerHuoLiAllOpenUICtrl huoLiOpenScript = null;
		switch (indexPlayer) {
		case PlayerEnum.PlayerOne:
			huoLiOpenScript = _InstanceOne;
			break;
		case PlayerEnum.PlayerTwo:
			huoLiOpenScript = _InstanceTwo;
			break;
		case PlayerEnum.PlayerThree:
			huoLiOpenScript = _InstanceThree;
			break;
		case PlayerEnum.PlayerFour:
			huoLiOpenScript = _InstanceFour;
			break;
		}
		return huoLiOpenScript;
	}
	
	// Use this for initialization
	void Start()
	{
		HuoLiSprite = GetComponent<UISprite>();
		HuoLiSprite.fillDirection = UISprite.FillDirection.Vertical;
		switch (PlayerSt) {
		case PlayerEnum.PlayerOne:
			_InstanceOne = this;
			break;
			
		case PlayerEnum.PlayerTwo:
			_InstanceTwo = this;
			break;
			
		case PlayerEnum.PlayerThree:
			_InstanceThree = this;
			break;
			
		case PlayerEnum.PlayerFour:
			_InstanceFour = this;
			break;
		}
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		CheckHuoLiUISprite();
	}
	
	float TimeStart;
	void CheckHuoLiUISprite()
	{
		if (!XkGameCtrl.GetIsActivePlayer(PlayerSt)) {
			HiddenHuoLiOpenUI();
			return;
		}

		float keyHD = -HuoLiTime;
		float fillVal = ((Time.realtimeSinceStartup - TimeStart) / keyHD) + 1f;
		fillVal = fillVal < 0f ? 0f : fillVal;
		fillVal = fillVal > 1f ? 1f : fillVal;
		HuoLiSprite.fillAmount = fillVal;
		if (fillVal <= 0f) {
			HiddenHuoLiOpenUI();
		}
	}
	
	float HuoLiTime;
	public void ShowHuoLiOpenUI(float timeVal = 1f)
	{
		HuoLiTime = timeVal;
		TimeStart = Time.realtimeSinceStartup;
		HuoLiSprite.fillAmount = 1f;
		gameObject.SetActive(true);
	}
	
	public void HiddenHuoLiOpenUI()
	{
		gameObject.SetActive(false);
		XKDaoJuGlobalDt.SetPlayerIsHuoLiAllOpen(PlayerSt, false);
	}
}