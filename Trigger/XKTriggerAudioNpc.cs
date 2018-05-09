using UnityEngine;
using System.Collections;

public class XKTriggerAudioNpc : MonoBehaviour
{
	[Range(0, 50)]public int PlayCount = 1;
	AudioSource AudioSourceCom;
	int CountAudio;
	void Start()
	{
		gameObject.layer = LayerMask.NameToLayer("NGUI");
		AudioSourceCom = GetComponent<AudioSource>();
		AudioSourceCom.Stop();
	}
	
	void OnTriggerEnter(Collider other)
	{
		XKNpcHealthCtrl healthScript = other.gameObject.GetComponent<XKNpcHealthCtrl>();
		if (healthScript == null) {
			return;
		}
		CheckAudioCount();
	}

	void CheckAudioCount()
	{
		if (CountAudio >= PlayCount) {
			//gameObject.SetActive(false);
			return;
		}
		//Debug.Log("Unity:"+"CheckAudioCount -> PlayCount "+PlayCount+", CountAudio "+CountAudio);
		AudioSourceCom.Stop();
		AudioSourceCom.Play();
		CountAudio++;
	}
}