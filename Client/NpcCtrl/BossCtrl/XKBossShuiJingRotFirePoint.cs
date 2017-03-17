using UnityEngine;
using System.Collections;

public class XKBossShuiJingRotFirePoint : MonoBehaviour
{
	Vector3 RotAngStart;
	Vector3 RotAngEnd;
	float TimeRot;
	int IndexXiaoShuiJing;
	XKBossShuiJingCtrl BossShuiJingScript;
	public void SetBossShuiJingRotFirePointInfo(XKBossShuiJingCtrl bossScript, int indexXiaoShuiJing)
	{
		BossShuiJingScript = bossScript;
		IndexXiaoShuiJing = indexXiaoShuiJing;
		TimeRot = BossShuiJingScript.TimeRot;
		RotAngEnd = BossShuiJingScript.RotAngEnd[IndexXiaoShuiJing];
		RotAngStart = BossShuiJingScript.RotAngStart[IndexXiaoShuiJing];
		transform.localEulerAngles = RotAngStart;
	}

	public void MakeFirePointRot()
	{
		TweenRotation twRot = gameObject.AddComponent<TweenRotation>();
		//twRot.from = Vector3.zero;
		transform.localEulerAngles = RotAngStart;
		twRot.from = RotAngStart;
		twRot.to = RotAngEnd;
		twRot.duration = TimeRot;
		EventDelegate.Add(twRot.onFinished, delegate{
			MoveFirePointOver();
		});
		twRot.PlayForward();
	}

	void MoveFirePointOver()
	{
		TweenRotation twRot = gameObject.GetComponent<TweenRotation>();
		if (twRot != null) {
			DestroyObject(twRot);
		}
		BossShuiJingScript.OnCompelteRotFirePoint(IndexXiaoShuiJing);
		transform.localEulerAngles = RotAngStart;
	}
}