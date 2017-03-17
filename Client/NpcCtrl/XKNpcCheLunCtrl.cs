using UnityEngine;
using System.Collections;

public class XKNpcCheLunCtrl : MonoBehaviour
{
	TweenRotation TwRot;
	// Use this for initialization
	void Start()
	{
		TwRot = GetComponent<TweenRotation>();
		SetCheLunIsRun(false);
	}

	public void SetCheLunIsRun(bool isRun)
	{
		TwRot.enabled = isRun;
	}
}