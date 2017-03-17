using UnityEngine;
using System.Collections;

public class TestNpcAni : MonoBehaviour
{
	public bool IsShowInfo;
	void OnTriggerFireAnimation()
	{
		if (!IsShowInfo) {
			return;
		}
		Debug.Log("OnTriggerFireAnimation...");
	}
}