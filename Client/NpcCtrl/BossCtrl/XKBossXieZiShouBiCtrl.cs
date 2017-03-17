using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Boss蝎子手臂制作时,必须使其局部坐标位置和转向信息归零.
 */
public class XKBossXieZiShouBiCtrl : MonoBehaviour
{
	Transform NpcParentTr;
	Transform NpcPathTr;
	GameObject NpcObj;
	XKCannonCtrl CannonScript;
	XKNpcHealthCtrl HealthScript;
	public void SetXieZiShouBiInfo(Transform pathTr = null)
	{
		NpcParentTr = transform.parent;
		NpcObj = gameObject;
		NpcPathTr = pathTr;
		NpcPathTr.parent = XkGameCtrl.MissionCleanup;
		HealthScript = GetComponent<XKNpcHealthCtrl>();
		HealthScript.IsCanHitNpc = false;
		HealthScript.SetIsDeathNpc(true);
		CannonScript = GetComponent<XKCannonCtrl>();
		CannonScript.FireDis = 0f;
		Invoke("DelayMoveXieZiShouBi", 30f);
	}

	void DelayMoveXieZiShouBi()
	{
		transform.parent = null;
		List<Transform> markList = new List<Transform>(NpcPathTr.GetComponentsInChildren<Transform>());
		markList.RemoveAt(0);
		Transform[] tranArray = markList.ToArray();
		NpcMark markScript = tranArray[0].GetComponent<NpcMark>();
		float mvSpeed = markScript.MvSpeed;
		iTween.MoveTo(NpcObj, iTween.Hash("path", tranArray,
		                                  "speed", mvSpeed,
		                                  "orienttopath", true,
		                                  "easeType", iTween.EaseType.linear,
		                                  "oncomplete", "MoveNpcOnCompelteITween"));
	}

	void MoveNpcOnCompelteITween()
	{
		//激活蝎子手臂的警戒范围.
		CannonScript.FireDis = 1000f;
		HealthScript.SetIsDeathNpc(false);
	}

	public void ResetXieZiShouBiInfo()
	{
		transform.parent = NpcParentTr;
		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;
	}
}