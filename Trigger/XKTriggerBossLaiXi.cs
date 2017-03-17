using UnityEngine;
using System.Collections;

public class XKTriggerBossLaiXi : MonoBehaviour
{
	public AiPathCtrl TestPlayerPath;
	void OnTriggerEnter(Collider other)
	{	
		if (other.GetComponent<XkPlayerCtrl>() == null) {
			return;
		}
		XKBossLXCtrl.GetInstance().StartPlayBossLaiXi();
	}

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
}