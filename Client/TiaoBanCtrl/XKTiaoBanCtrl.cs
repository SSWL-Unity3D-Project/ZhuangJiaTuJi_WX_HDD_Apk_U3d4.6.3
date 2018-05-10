using UnityEngine;
using System.Collections;

public class XKTiaoBanCtrl : MonoBehaviour
{
	public Transform TiaoDianTr;
	//[Range(0f, 100f)]public float PlayerDamageDis = 10f;
	void OnTriggerEnter(Collider other)
	{
		XKPlayerMoveCtrl playerMoveScript = other.GetComponent<XKPlayerMoveCtrl>();
		if (playerMoveScript == null) {
			return;
		}
		Debug.Log("Unity:"+"XKTiaoBanCtrl::OnTriggerEnter -> hit "+other.name);
		//XkGameCtrl.ActivePlayerToGame(playerMoveScript.PlayerIndex, true);
		playerMoveScript.MakePlayerToTiaoYueDian(TiaoDianTr);
	}
}