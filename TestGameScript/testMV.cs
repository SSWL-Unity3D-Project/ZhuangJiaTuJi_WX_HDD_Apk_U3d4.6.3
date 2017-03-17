using UnityEngine;
using System.Collections;

public class testMV : MonoBehaviour
{
	void OnGUI()
	{
		//		string strA = "PlayerPosOnCam "+PlayerPosOnCam;
		//		GUI.Box(new Rect(0f, 0f, Screen.width, 30f), strA);
		//		
		//		string strB = "PlayerPosUI "+PlayerPosUI;
		//		GUI.Box(new Rect(0f, 30f, Screen.width, 30f), strB);
	}

	void LateUpdate()
	{
		CheckObjRotation();
	}

	public Transform CheTiTr;
	public Transform TestFollowTr;
	public float FollowSpeedAr = 100f;
	void CheckObjRotation()
	{
		Vector3 forwardVal = Vector3.Lerp(transform.forward,
		                                  TestFollowTr.forward,
		                                  FollowSpeedAr * Time.deltaTime);
		transform.forward = forwardVal;
	}
}