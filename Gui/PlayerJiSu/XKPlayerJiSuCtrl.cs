using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XKPlayerJiSuCtrl : MonoBehaviour
{
	public GameObject PlayerJiSuPre;
	List<XKPlayerJiSuMove> JiSuList;
	const int MaxPlayerFS = 12;
	static  XKPlayerJiSuCtrl _Instance;
	public static XKPlayerJiSuCtrl GetInstance()
	{
		return _Instance;
	}
	// Use this for initialization
	void Start()
	{
		_Instance = this;
		JiSuList = new List<XKPlayerJiSuMove>();
		GameObject obj = null;
		for (int i = 0; i < MaxPlayerFS; i++) {
			obj = (GameObject)Instantiate(PlayerJiSuPre);
			obj.transform.parent = transform;
			obj.transform.localScale = new Vector3(1f, 1f, 1f);
			obj.transform.localPosition = Vector3.zero;
			JiSuList.Add(obj.GetComponent<XKPlayerJiSuMove>());
			obj.SetActive(false);
		}
	}
	
	//	public Transform TestPlayerTr;
	//	public Transform TestJiSuTr;
	//	[Range(1, 999999)]public int JiSuTest = 1;
	//	public Vector3 TestPos;
	//	void OnGUI()
	//	{
	//		GUI.Box(new Rect(0f, 0f, 500f, 25f), TestPos.ToString());
	//	}
	// Update is called once per frame
	//	void Update()
	//	{
	//		if (Camera.main == null) {
	//			return;
	//		}
	//		TestPlayerTr = XkPlayerCtrl.GetInstanceFeiJi().TestCubeTr;
	//		Vector3 startPos = Camera.main.WorldToScreenPoint(TestPlayerTr.position);
	//		startPos.z = 0f;
	//		startPos.x = startPos.x < 0f ? 0f : startPos.x;
	//		startPos.x = startPos.x > Screen.width ? Screen.width : startPos.x;
	//		startPos.y = startPos.y < 0f ? 0f : startPos.y;
	//		startPos.y = startPos.y > Screen.height ? Screen.height : startPos.y;
	//
	//		startPos.x *= (XkGameCtrl.ScreenWidth / Screen.width);
	//		startPos.y *= (XkGameCtrl.ScreenHeight / Screen.height);
	//		
	//		startPos.x = startPos.x < 0f ? 0f : startPos.x;
	//		startPos.x = startPos.x > XkGameCtrl.ScreenWidth ? XkGameCtrl.ScreenWidth : startPos.x;
	//		startPos.y = startPos.y < 0f ? 0f : startPos.y;
	//		startPos.y = startPos.y > XkGameCtrl.ScreenHeight ? XkGameCtrl.ScreenHeight : startPos.y;
	//
	//		int jiSuLen = JiSuTest.ToString().Length;
	//		startPos.x += 9f * (jiSuLen - 1);
	//		TestPos = startPos;
	//		TestJiSuTr.localPosition = startPos;
	
	//		if (Input.GetKeyDown(KeyCode.P)) {
	//			//ShowPlayerJiSu(PlayerEnum.Null, JiSuTest);
	//			ShowPlayerJiSu(PlayerEnum.PlayerOne, Random.Range(1, 999999));
	//		}
	//	}
	
	XKPlayerJiSuMove GetXKPlayerJiSuMove()
	{
		GameObject obj = null;
		int valTmp = 0;
		for (int i = 0; i < MaxPlayerFS; i++) {
			obj = JiSuList[i].gameObject;
			if (obj.activeSelf) {
				continue;
			}
			valTmp = i;
			break;
		}
		return JiSuList[valTmp];
	}
	
	public void ShowPlayerJiSu(PlayerEnum indexVal)
	{
		XKPlayerJiSuMove jiSuMoveCom = GetXKPlayerJiSuMove();
		if (jiSuMoveCom == null) {
			return;
		}

		Transform playerTr = XKPlayerMoveCtrl.GetXKPlayerMoveCtrl(indexVal).PiaoFenPoint;
//		playerTr = TestPlayerTr; //test
		
		Vector3 startPos = XkGameCtrl.GetInstance().GetWorldObjToScreenPos(playerTr.position);
		jiSuMoveCom.SetPlayerJiSuVal(startPos);
	}
}