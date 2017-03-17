using UnityEngine;
using System.Collections;

public class XKBossXieZiPathCtrl : MonoBehaviour {
	/**
	 * 控制Boss蝎子手臂运动路径.
	 */
	public NpcPathCtrl[] BossXieZiPath;
	static XKBossXieZiPathCtrl _Instance;
	public static XKBossXieZiPathCtrl GetInstance()
	{
		return _Instance;
	}
	// Use this for initialization
	void Awake()
	{
		_Instance = this;
		gameObject.SetActive(false);
	}
}