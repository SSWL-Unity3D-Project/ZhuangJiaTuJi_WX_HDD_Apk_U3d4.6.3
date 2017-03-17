using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XKPlayerJiJiuBaoCtrl : MonoBehaviour
{
	public GameObject PlayerJiJiuBaoPre;
	List<XKPlayerJiJiuBaoMove> JiJiuBaoList;
	const int MaxPlayerFS = 12;
	static  XKPlayerJiJiuBaoCtrl _Instance;
	public static XKPlayerJiJiuBaoCtrl GetInstance()
	{
		return _Instance;
	}
	// Use this for initialization
	void Start()
	{
		_Instance = this;
		JiJiuBaoList = new List<XKPlayerJiJiuBaoMove>();
		GameObject obj = null;
		for (int i = 0; i < MaxPlayerFS; i++) {
			obj = (GameObject)Instantiate(PlayerJiJiuBaoPre);
			obj.transform.parent = transform;
			obj.transform.localScale = new Vector3(1f, 1f, 1f);
			obj.transform.localPosition = Vector3.zero;
			JiJiuBaoList.Add(obj.GetComponent<XKPlayerJiJiuBaoMove>());
			obj.SetActive(false);
		}
	}
	
	XKPlayerJiJiuBaoMove GetXKPlayerJiJiuBaoMove()
	{
		GameObject obj = null;
		int valTmp = 0;
		for (int i = 0; i < MaxPlayerFS; i++) {
			obj = JiJiuBaoList[i].gameObject;
			if (obj.activeSelf) {
				continue;
			}
			valTmp = i;
			break;
		}
		return JiJiuBaoList[valTmp];
	}
	
	public void ShowPlayerJiJiuBao(PlayerEnum indexVal)
	{
		XKPlayerJiJiuBaoMove huoLiOpenMoveCom = GetXKPlayerJiJiuBaoMove();
		if (huoLiOpenMoveCom == null) {
			return;
		}
		
		Transform playerTr = XKPlayerMoveCtrl.GetXKPlayerMoveCtrl(indexVal).PiaoFenPoint;
		Vector3 startPos = XkGameCtrl.GetInstance().GetWorldObjToScreenPos(playerTr.position);
		huoLiOpenMoveCom.SetPlayerJiJiuBaoVal(startPos);
	}
}