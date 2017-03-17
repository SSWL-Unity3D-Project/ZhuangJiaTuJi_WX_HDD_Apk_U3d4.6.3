using UnityEngine;
using System.Collections;

public class TestCheckPlayerOnCam : MonoBehaviour
{
	public bool IsTestImg;
	public Transform TestImgTr;
	Vector3 PlayerPosOnCam;
	Vector3 PlayerPosUI;
	void Update()
	{
		CheckPlayerOnCamPositionTest();
	}
	
	void OnGUI()
	{
		string strA = "PlayerPosOnCam "+PlayerPosOnCam;
		GUI.Box(new Rect(0f, 0f, Screen.width, 30f), strA);
		
		string strB = "PlayerPosUI "+PlayerPosUI;
		GUI.Box(new Rect(0f, 30f, Screen.width, 30f), strB);
	}

	void CheckPlayerOnCamPositionTest()
	{
		if (Camera.main == null) {
			return;
		}
		PlayerPosOnCam = Camera.main.WorldToScreenPoint(transform.position);
		
		if (IsTestImg) {
			Vector3 startPos = PlayerPosOnCam;
			startPos.z = 0f;
			startPos.x = (XkGameCtrl.ScreenWidth * startPos.x) / Screen.width;
			startPos.y = (XkGameCtrl.ScreenHeight * startPos.y) / Screen.height;
			PlayerPosUI = startPos;
			TestImgTr.localPosition = startPos;
		}
	}
}