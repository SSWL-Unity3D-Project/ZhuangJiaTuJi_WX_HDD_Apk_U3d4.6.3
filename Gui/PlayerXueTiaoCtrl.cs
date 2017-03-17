using UnityEngine;
using System.Collections;

public class PlayerXueTiaoCtrl : MonoBehaviour
{
	public PlayerEnum PlayerSt = PlayerEnum.Null;
	public Renderer NengLiangRenderer;
	Transform CameraTran;
	Transform NengLianTran;
	Transform NengLianParentTr;
	Vector3 OffsetXT;
	static PlayerXueTiaoCtrl _InstanceOne;
	public static PlayerXueTiaoCtrl GetInstanceOne()
	{
		return _InstanceOne;
	}
	
	static PlayerXueTiaoCtrl _InstanceTwo;
	public static PlayerXueTiaoCtrl GetInstanceTwo()
	{
		return _InstanceTwo;
	}
	
	static PlayerXueTiaoCtrl _InstanceThree;
	public static PlayerXueTiaoCtrl GetInstanceThree()
	{
		return _InstanceThree;
	}
	
	static PlayerXueTiaoCtrl _InstanceFour;
	public static PlayerXueTiaoCtrl GetInstanceFour()
	{
		return _InstanceFour;
	}

	public static PlayerXueTiaoCtrl GetInstance(PlayerEnum indexPlayer)
	{
		PlayerXueTiaoCtrl playerXT = null;
		switch (indexPlayer) {
		case PlayerEnum.PlayerOne:
			playerXT = _InstanceOne;
			break;
		case PlayerEnum.PlayerTwo:
			playerXT = _InstanceTwo;
			break;
		case PlayerEnum.PlayerThree:
			playerXT = _InstanceThree;
			break;
		case PlayerEnum.PlayerFour:
			playerXT = _InstanceFour;
			break;
		}
		return playerXT;
	}
	// Use this for initialization
	void Start()
	{
		CameraTran = Camera.main != null ? Camera.main.transform : null;
	}

	// Update is called once per frame
	void Update()
	{
		if (CameraTran == null) {
			CameraTran = Camera.main != null ? Camera.main.transform : null;
			return;
		}
		Vector3 forwardVal = CameraTran.forward;
		forwardVal.y = 0f;
		NengLianTran.forward = forwardVal;

		Vector3 pos = NengLianParentTr.position;
		pos += forwardVal * OffsetXT.z;
		pos.x += OffsetXT.x;
		pos.y += OffsetXT.y;
		NengLianTran.position = pos;
	}

	public void HandlePlayerXueTiaoInfo(float fillVal)
	{
		float xueLiangVal = 1f - fillVal;
		xueLiangVal = Mathf.Clamp01(xueLiangVal);
		NengLiangRenderer.materials[0].SetTextureOffset("_MainTex", new Vector2(xueLiangVal, 0f));
		bool isActiveXT = xueLiangVal >= 1f ? false : true;
		gameObject.SetActive(isActiveXT);
	}
	
	public void SetPlayerIndex(PlayerEnum playerIndex)
	{
		bool isActiveXT = XkGameCtrl.GetIsActivePlayer(playerIndex);
		PlayerSt = playerIndex;
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

		NengLianTran = transform;
		OffsetXT = NengLianTran.localPosition;
		NengLianParentTr = NengLianTran.parent;
		NengLianTran.parent = null;
		gameObject.SetActive(isActiveXT);
	}
}