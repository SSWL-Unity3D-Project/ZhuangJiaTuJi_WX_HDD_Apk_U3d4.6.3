using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XKPlayerZhuiYaCtrl : MonoBehaviour
{
	public GameObject PlayerZhuiYaUI;
	List<XKPlayerZhuiYaUI> ZhuiYaUIList;
	int MaxPlayerFS = 12;
	static XKPlayerZhuiYaCtrl _Instance;
	public static XKPlayerZhuiYaCtrl GetInstance()
	{
		return _Instance;
	}
	// Use this for initialization
	void Start()
	{
		_Instance = this;
		ZhuiYaUIList = new List<XKPlayerZhuiYaUI>();
		GameObject obj = null;
		for (int i = 0; i < MaxPlayerFS; i++) {
			obj = (GameObject)Instantiate(PlayerZhuiYaUI);
			obj.transform.parent = transform;
			obj.transform.localScale = new Vector3(1f, 1f, 1f);
			obj.transform.localPosition = Vector3.zero;
			ZhuiYaUIList.Add(obj.GetComponent<XKPlayerZhuiYaUI>());
			obj.SetActive(false);
		}
	}
	
	XKPlayerZhuiYaUI GetXKPlayerZhuiYaUI()
	{
		GameObject obj = null;
		int valTmp = 0;
		for (int i = 0; i < MaxPlayerFS; i++) {
			obj = ZhuiYaUIList[i].gameObject;
			if (obj.activeSelf) {
				continue;
			}
			valTmp = i;
			break;
		}
		return ZhuiYaUIList[valTmp];
	}
	
	public void ShowPlayerZhuiYaUI(PlayerEnum indexVal)
	{
		int indexPlayer = (int)indexVal - 1;
		indexPlayer = (indexPlayer < 0 || indexPlayer > 3) ? 0 : indexPlayer;

		XKPlayerZhuiYaUI moveCom = GetXKPlayerZhuiYaUI();
		if (moveCom == null) {
			return;
		}
		Transform playerTr = XKPlayerMoveCtrl.GetXKPlayerMoveCtrl(indexVal).PiaoFenPoint;
		//playerTr = TestPlayerTr; //test
		
		Vector3 startPos = XkGameCtrl.GetInstance().GetWorldObjToScreenPos(playerTr.position);
		moveCom.MovePlayerZhuiYaUI(startPos);
	}
}