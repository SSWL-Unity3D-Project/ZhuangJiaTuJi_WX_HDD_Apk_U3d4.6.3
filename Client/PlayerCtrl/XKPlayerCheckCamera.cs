using UnityEngine;
using System.Collections;

public enum PointState
{
	Qian,
	Hou,
	Zuo,
	You,
}

public class XKPlayerCheckCamera : MonoBehaviour {
	bool IsOutGameCamera;
	void OnBecameVisible()
	{
		IsOutGameCamera = false;
//		Debug.Log("Unity:"+"OnBecameVisible -> PointSt Qian");
	}

	void OnBecameInvisible()
	{
		IsOutGameCamera = true;
//		Debug.Log("Unity:"+"OnBecameInvisible -> PointSt Qian");
	}
	
	public bool GetIsOutGameCamera()
	{
		return IsOutGameCamera;
	}
}