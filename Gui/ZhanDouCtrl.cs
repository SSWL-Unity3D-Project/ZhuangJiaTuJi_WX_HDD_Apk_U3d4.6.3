using UnityEngine;
using System.Collections;

public class ZhanDouCtrl : MonoBehaviour
{
	public void ShowZhanDouObj()
	{
		gameObject.SetActive(true);
		TweenPosition tweenCom = GetComponent<TweenPosition>();
		if (tweenCom != null) {
			DestroyObject(tweenCom);
		}
		
		tweenCom = gameObject.AddComponent<TweenPosition>();
		tweenCom.enabled = false;
		tweenCom.duration = 0.2f;
		tweenCom.from = new Vector3(33f, -70f, 0f);
		tweenCom.to = new Vector3(33f, 42f, 0f);
		EventDelegate.Add(tweenCom.onFinished, delegate{
			DelayHiddenZhanDou();
		});
		tweenCom.enabled = true;
		tweenCom.PlayForward();
	}

	void DelayHiddenZhanDou()
	{
		Invoke("HiddenZhanDou", 3f);
	}

	void HiddenZhanDou()
	{
		gameObject.SetActive(false);
	}
}