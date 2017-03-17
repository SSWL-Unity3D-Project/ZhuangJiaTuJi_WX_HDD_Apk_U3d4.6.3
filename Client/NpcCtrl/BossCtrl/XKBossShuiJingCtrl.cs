using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XKBossShuiJingCtrl : MonoBehaviour
{
	public GameObject XiaoShuiJingXuLiPrefab;
	public XKCannonCtrl DaShuiJing;
	public XKNpcHealthCtrl DaShuiJingHealth;
	/**
	 * 小水晶控制数组.
	 * 小水晶子弹发射点的局部转角必须配置为0.
	 */
	public XKCannonCtrl[] XiaoShuiJingArray;
	/**
	 * 大水晶攻击延迟时间.
	 */
	[Range(0.1f, 100f)]public float TimeDaShuiJingFireYanChi = 2f;
	/**
	 * 大水晶攻击时间.
	 */
	[Range(0.1f, 10f)]public float TimeDaShuiJingFire = 2f;
	/**
	 * 小水晶蓄力时间.
	 */
	[Range(0.1f, 10f)]public float TimeXiaoShuiJingXuLi = 1.5f;
	/**
	 * 小水晶直线开火时间.
	 */
	[Range(0.1f, 10f)]public float TimeXiaoShuiJingFire = 2f;
	List<Transform> DaShuiJingFireTr;
	void Start()
	{
		DaShuiJingFireTr = new List<Transform>(DaShuiJing.SpawnAmmoPoint);
		BossRotFireScript = new XKBossShuiJingRotFirePoint[XiaoShuiJingArray.Length];
		GameObject firePointObj = null;
		for (int i = 0; i < XiaoShuiJingArray.Length; i++) {
			firePointObj = XiaoShuiJingArray[i].SpawnAmmoPoint[0].gameObject;
			BossRotFireScript[i] = firePointObj.AddComponent<XKBossShuiJingRotFirePoint>();
			BossRotFireScript[i].SetBossShuiJingRotFirePointInfo(this, i);
		}
		CloseXiaoShuiFire(-1);
		Invoke("OpenDaShuiJingFire", TimeDaShuiJingFireYanChi);
	}

	/**
	 * BossShuiJingJieDuan == 1 -> boss4的第一阶段.
	 * BossShuiJingJieDuan == 2 -> boss4的第二阶段.
	 */
	int BossShuiJingJieDuan = 0;
	void OpenDaShuiJingFire()
	{
		float bossXueLiangAmount = DaShuiJingHealth.GetBossFillAmount();
		if (bossXueLiangAmount > 0.4f) {
			//阶段1.
			if (BossShuiJingJieDuan != 1) {
				BossShuiJingJieDuan = 1;
				List<Transform> daShuiJingFirePoint = DaShuiJingFireTr;
				int countPoint = daShuiJingFirePoint.Count;
				daShuiJingFirePoint.RemoveAt(countPoint - 1);
				daShuiJingFirePoint.RemoveAt(countPoint - 2);
				DaShuiJing.SpawnAmmoPoint = daShuiJingFirePoint.ToArray();
			}
		}
		else {
			//阶段2.
			if (BossShuiJingJieDuan != 2) {
				BossShuiJingJieDuan = 2;
				DaShuiJing.SpawnAmmoPoint = DaShuiJingFireTr.ToArray();
			}
		}
		DaShuiJing.FireDis = 1000f;
		Invoke("CloseDaShuiJingFire", TimeDaShuiJingFire);
	}

//	int CountXiaoShuiJingFire;
	void CloseDaShuiJingFire()
	{
		DaShuiJing.FireDis = 0f;
		OpenXiaoShuiJingXuLi(0);
	}
	
	/**
	 * indexVal == -1 -> 打开小水晶1和2的开火蓄力.
	 * indexVal == 0 -> 打开小水晶1的开火蓄力.
	 * indexVal == 1 -> 打开小水晶2的开火蓄力.
	 */
	void OpenXiaoShuiJingXuLi(int indexVal)
	{
//		Debug.Log("OpenXiaoShuiJingXuLi --> indexVal "+indexVal);
		Transform xuLiTr = null;
		GameObject obj = null;
		switch (indexVal) {
		case 0:
		case 1:
			XiaoShuiJingFireState = indexVal;
			xuLiTr = XiaoShuiJingArray[indexVal].transform;
			obj = (GameObject)Instantiate(XiaoShuiJingXuLiPrefab, xuLiTr.position, xuLiTr.rotation);
			XkGameCtrl.CheckObjDestroyThisTimed(obj);
			obj.transform.parent = XkGameCtrl.NpcAmmoArray;
			break;
		default:
			XiaoShuiJingFireState = 2;
			for (int i = 0; i < XiaoShuiJingArray.Length; i++) {
				xuLiTr = XiaoShuiJingArray[i].transform;
				obj = (GameObject)Instantiate(XiaoShuiJingXuLiPrefab, xuLiTr.position, xuLiTr.rotation);
				XkGameCtrl.CheckObjDestroyThisTimed(obj);
				obj.transform.parent = XkGameCtrl.NpcAmmoArray;
			}
			break;
		}
		StartCoroutine(OpenXiaoShuiJingFire(indexVal));
	}

	void ResetXiaoShuiJingFirePointAimForward(int indexVal)
	{
		XiaoShuiJingArray[indexVal].SpawnAmmoPoint[0].localEulerAngles = Vector3.zero;
	}

	/**
	 * indexVal == -1 -> 打开小水晶1和2的开火逻辑.
	 * indexVal == 0 -> 打开小水晶1的开火逻辑.
	 * indexVal == 1 -> 打开小水晶2的开火逻辑.
	 */
	IEnumerator OpenXiaoShuiJingFire(int indexVal = -1)
	{
		yield return new WaitForSeconds(TimeXiaoShuiJingXuLi);
		switch (indexVal) {
		case 0:
		case 1:
			ResetXiaoShuiJingFirePointAimForward(indexVal);
			XiaoShuiJingArray[indexVal].FireDis = 1000f;
			break;
		default:
			for (int i = 0; i < XiaoShuiJingArray.Length; i++) {
				ResetXiaoShuiJingFirePointAimForward(i);
				XiaoShuiJingArray[i].FireDis = 1000f;
			}
			break;
		}

		yield return new WaitForSeconds(TimeXiaoShuiJingFire);
		//开启小水晶的转向攻击.
		switch (indexVal) {
		case 0:
		case 1:
			BossRotFireScript[indexVal].MakeFirePointRot();
			break;
		default:
			foreach (var item in BossRotFireScript) {
				item.MakeFirePointRot();
			}
			break;
		}
	}

	/**
	 * RotAngEnd[index] -> 小水晶开火点转动角度控制.
	 */
	public Vector3[] RotAngEnd = new Vector3[2];
	public Vector3[] RotAngStart = new Vector3[2];
	/**
	 * 小水晶开火点转动时间.
	 */
	[Range(0.1f, 10f)]public float TimeRot = 5f;
	/**
	 * 大水晶阶段1开火等待时间.
	 */
	[Range(0.1f, 100f)]public float TimeDaShuiJingFireJieDuan1 = 3f;
	/**
	 * 大水晶阶段2开火等待时间.
	 */
	[Range(0.1f, 100f)]public float TimeDaShuiJingFireJieDuan2 = 1f;
	XKBossShuiJingRotFirePoint[] BossRotFireScript;
	/**
	 * XiaoShuiJingFireState == 0 -> 第1个小水晶转动.
	 * XiaoShuiJingFireState == 1 -> 第2个小水晶转动.
	 * XiaoShuiJingFireState == 2 -> 2个小水晶同时转动.
	 */
	int XiaoShuiJingFireState;
	public void OnCompelteRotFirePoint(int indexXiaoShuiJing)
	{
		if (DaShuiJingHealth.GetIsDeathNpc()) {
			return;
		}
		StartCoroutine(CheckXiaoShuiJingFireState(indexXiaoShuiJing));
	}

	[Range(0f, 50f)]public float TimeXiaoShuiJingFireDengDai = 0f;
	/**
	 * indexVal == 0 -> 关闭小水晶1的开火.
	 * indexVal == 1 -> 关闭小水晶2的开火.
	 * indexVal == -1 -> 关闭小水晶1和2的开火.
	 */
	void CloseXiaoShuiFire(int indexVal)
	{
		switch (indexVal) {
		case 0:
		case 1:
			XiaoShuiJingArray[indexVal].FireDis = 0f;
			break;
		default:
			for (int i = 0; i < XiaoShuiJingArray.Length; i++) {
				XiaoShuiJingArray[i].FireDis = 0f;
			}
			break;
		}
	}

	IEnumerator CheckXiaoShuiJingFireState(int indexXiaoShuiJing)
	{	
		bool isOpenDaShuiJingFire = false;
		float timeDaShuiJing = 0f;
		float bossXueLiangAmount = DaShuiJingHealth.GetBossFillAmount();
		if (bossXueLiangAmount > 0.4f) {
			//阶段1.
			if (BossShuiJingJieDuan != 1) {
				BossShuiJingJieDuan = 1;
			}
		}
		else {
			//阶段2.
			if (BossShuiJingJieDuan != 2) {
				BossShuiJingJieDuan = 2;
			}
		}

//		Debug.Log("CheckXiaoShuiJingFireState -> BossShuiJingJieDuan "+BossShuiJingJieDuan
//		          +", XiaoShuiJingFireState "+XiaoShuiJingFireState);
		switch (XiaoShuiJingFireState) {
		case 0:
			yield return new WaitForSeconds(TimeXiaoShuiJingFireDengDai);
			CloseXiaoShuiFire(0);
			OpenXiaoShuiJingXuLi(1);
			break;
		case 1:
			if (BossShuiJingJieDuan == 1) {
				yield return new WaitForSeconds(TimeXiaoShuiJingFireDengDai);
				CloseXiaoShuiFire(1);
				isOpenDaShuiJingFire = true;
				timeDaShuiJing = TimeDaShuiJingFireJieDuan1 - TimeXiaoShuiJingFireDengDai;
			}
			else {
				//open boss All xiaoShuiJingFire.
				yield return new WaitForSeconds(TimeXiaoShuiJingFireDengDai);
				CloseXiaoShuiFire(1);
				OpenXiaoShuiJingXuLi(-1);
			}
			break;
		case 2:
			if (indexXiaoShuiJing == 1) {
				yield return new WaitForSeconds(TimeXiaoShuiJingFireDengDai);
				CloseXiaoShuiFire(-1);
				isOpenDaShuiJingFire = true;
				timeDaShuiJing = TimeDaShuiJingFireJieDuan2 - TimeXiaoShuiJingFireDengDai;
			}
//			else {
//				Debug.Log("stop fire! indexXiaoShuiJing "+indexXiaoShuiJing);
//			}
			break;
		}
		
		if (isOpenDaShuiJingFire) {
			//open boss daShuiJingFire.
			yield return new WaitForSeconds(timeDaShuiJing);
			OpenDaShuiJingFire();
		}
	}
}