using UnityEngine;
using System.Collections;

public class BossZuDangCtrl : MonoBehaviour
{
	static BossZuDangCtrl _Instance;
	public static BossZuDangCtrl GetInstance()
	{
		return _Instance;
	}
	// Use this for initialization
	void Start()
	{
		_Instance = this;
		BoxCollider[] boxColArray = gameObject.GetComponentsInChildren<BoxCollider>();
		foreach (BoxCollider item in boxColArray) {
			item.gameObject.layer = LayerMask.NameToLayer("UI");
			item.renderer.enabled = false;
		}
		SetIsActiveBossZuDang(false);
	}

	public void SetIsActiveBossZuDang(bool isActive)
	{
		gameObject.SetActive(isActive);
	}
}