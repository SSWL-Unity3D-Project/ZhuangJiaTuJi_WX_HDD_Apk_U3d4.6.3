using UnityEngine;
using System.Collections;

public class XKGmChangeUIPos : MonoBehaviour {
	public Transform[] UITr;
	public Vector2[] UIPos;

	// Use this for initialization
	void Start()
	{
		XKGlobalData.GetInstance();
		if (XKGlobalData.GameVersionPlayer == 0) {
			return;
		}

		for (int i = 0; i < UITr.Length; i++) {
			UITr[i].localPosition = (Vector3) UIPos[i];
		}
	}
}