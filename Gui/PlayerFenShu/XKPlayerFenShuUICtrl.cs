using UnityEngine;
using System.Collections;

public class XKPlayerFenShuUICtrl : MonoBehaviour
{
	public PlayerEnum PlayerSt;
	UISprite FenShuSprite;
	static XKPlayerFenShuUICtrl _InstanceOne;
	static XKPlayerFenShuUICtrl _InstanceTwo;
	static XKPlayerFenShuUICtrl _InstanceThree;
	static XKPlayerFenShuUICtrl _InstanceFour;
	public static XKPlayerFenShuUICtrl GetInstanceFenShu(PlayerEnum indexPlayer)
	{
		XKPlayerFenShuUICtrl huoLiOpenScript = null;
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
		FenShuSprite = GetComponent<UISprite>();
		FenShuSprite.fillDirection = UISprite.FillDirection.Vertical;
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
		HiddenFenShuUI();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		CheckFenShuUISprite();
	}
	
	float TimeStart;
	void CheckFenShuUISprite()
	{
		if (!XkGameCtrl.GetIsActivePlayer(PlayerSt)) {
			HiddenFenShuUI();
			return;
		}

		float keyHD = -FenShuTime;
		float fillVal = ((Time.realtimeSinceStartup - TimeStart) / keyHD) + 1f;
		fillVal = fillVal < 0f ? 0f : fillVal;
		fillVal = fillVal > 1f ? 1f : fillVal;
		FenShuSprite.fillAmount = fillVal;
		if (fillVal <= 0f) {
			HiddenFenShuUI();
		}
	}
	
	float FenShuTime;
	public void ShowFenShuUI(float timeVal = 1f)
	{
		FenShuTime = timeVal;
		TimeStart = Time.realtimeSinceStartup;
		FenShuSprite.fillAmount = 1f;
		gameObject.SetActive(true);
	}
	
	public void HiddenFenShuUI()
	{
		gameObject.SetActive(false);
		XKDaoJuGlobalDt.ResetPlayerFenShuBeiLv(PlayerSt);
	}
}