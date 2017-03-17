using UnityEngine;
using System.Collections;

public class XKTriggerOpenStage : MonoBehaviour
{
	public AiPathCtrl TestPlayerPath;
	void OnDrawGizmosSelected()
	{
		if (!XkGameCtrl.IsDrawGizmosObj) {
			return;
		}
		
		if (!enabled) {
			return;
		}
		
		if (TestPlayerPath != null) {
			TestPlayerPath.DrawPath();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		XkPlayerCtrl playerScript = other.GetComponent<XkPlayerCtrl>();
		if (playerScript == null) {
			return;
		}
		XKGameStageCtrl.GetInstance().MoveIntoStageUI();
		gameObject.SetActive(false);
	}
}