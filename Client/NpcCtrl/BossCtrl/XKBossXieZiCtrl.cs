using UnityEngine;

public class XKBossXieZiCtrl : MonoBehaviour
{
	public XKNpcHealthCtrl BossXieZiHealth;
	/**
	 * 对BossXieZiShouBi动态添加XKBossXieZiShouBiCtrl脚本.
	 */
	public GameObject[] BossXieZiShouBi;
	XKBossXieZiShouBiCtrl[] BossShouBiScript;
	// Use this for initialization
	void Start()
	{
		Transform tranPath = null;
		BossShouBiScript = new XKBossXieZiShouBiCtrl[BossXieZiShouBi.Length];
		for (int i = 0; i < BossXieZiShouBi.Length; i++) {
			BossShouBiScript[i] = BossXieZiShouBi[i].AddComponent<XKBossXieZiShouBiCtrl>();
			tranPath = XKBossXieZiPathCtrl.GetInstance().BossXieZiPath[i].transform;
			BossShouBiScript[i].SetXieZiShouBiInfo(tranPath);
		}

		BossXieZiHealth.SetBossXieZiScript(this);
	}

	public void ResetBossXieZiShouBiInfo()
	{
		foreach (var item in BossShouBiScript) {
			item.ResetXieZiShouBiInfo();
		}
	}
}