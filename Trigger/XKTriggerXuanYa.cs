using UnityEngine;
using System.Collections;

public class XKTriggerXuanYa : MonoBehaviour
{
	[Range(0f, 10000f)]public float PlayerDamage = 50f;
	[Range(0f, 100f)]public float TimeResetPlayer = 2f;
	void OnTriggerEnter(Collider other)
	{
		XKPlayerMoveCtrl playerMoveScript = other.GetComponent<XKPlayerMoveCtrl>();
		if (playerMoveScript == null) {
			return;
		}

		if (!XkGameCtrl.GetIsActivePlayer(playerMoveScript.PlayerIndex)
		    || playerMoveScript.GetIsActiveZhuiYa()) {
			return;
		}
		//Debug.Log("XKTriggerXuanYa::OnTriggerEnter -> hit "+other.name);
		StartCoroutine(DelayActivePlayerToGame(playerMoveScript));
	}

	IEnumerator DelayActivePlayerToGame(XKPlayerMoveCtrl playerScript)
	{
		playerScript.SetIsActiveZhuiYa(true);
		XKGlobalData.GetInstance().PlayAudioXuanYaDiaoLuo();
		XkGameCtrl.GetInstance().SubGamePlayerHealth(playerScript.PlayerIndex, PlayerDamage, true);
		if (!XkGameCtrl.GetIsActivePlayer(playerScript.PlayerIndex)) {
			playerScript.SetIsActiveZhuiYa(false);
			yield break;
		}
		yield return new WaitForSeconds(TimeResetPlayer);
		XkGameCtrl.ActivePlayerToGame(playerScript.PlayerIndex, true);
		playerScript.SetIsActiveZhuiYa(false);
	}
}