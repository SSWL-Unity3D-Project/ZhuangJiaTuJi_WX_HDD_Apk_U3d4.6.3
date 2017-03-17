using UnityEngine;
using System.Collections;

public class XKTriggerAudioBeiJing : MonoBehaviour
{
	public AiPathCtrl TestPlayerPath;
	void OnTriggerEnter(Collider other)
	{	
		if (other.GetComponent<XkPlayerCtrl>() == null) {
			return;
		}
		AudioBeiJingCtrl.StopGameBeiJingAudio();
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