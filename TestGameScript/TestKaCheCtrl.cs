using UnityEngine;
using System.Collections;

public class TestKaCheCtrl : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		InitKaCheInfo();
	}
	
	void Update()
	{
		MoveKaChe();
		CheckKaCheRotation();
	}
	
	public Transform KaCheTouTr;
	public Transform KaCheLianGanTr;
	void InitKaCheInfo()
	{
		Transform kaChePar = KaCheTouTr.parent;
		KaCheTouTr.parent = null;
		kaChePar.parent = KaCheTouTr;
		kaChePar.localPosition = Vector3.zero;
		kaChePar.localEulerAngles = Vector3.zero;
		
		KaCheLianGanTr.parent = null;
	}
	
	[Range(0f, 100f)]public float KaCheFollowPosSp = 5f;
	[Range(0f, 100f)]public float KaCheFollowRotSp = 5f;
	void CheckKaCheRotation()
	{
		KaCheLianGanTr.position = Vector3.Lerp(KaCheLianGanTr.position,
		                                       KaCheTouTr.position,
		                                       KaCheFollowPosSp * Time.deltaTime);
		
		Vector3 forwardKC = KaCheLianGanTr.forward;
		Vector3 forwardKCT = KaCheTouTr.forward;
		forwardKC = Vector3.Lerp(forwardKC,
		                         forwardKCT,
		                         KaCheFollowRotSp * Time.deltaTime);
		KaCheLianGanTr.forward = forwardKC;
	}
	
	[Range(0f, 100f)]public float MvSpeed = 1f;
	[Range(-100f, 100f)]public float RtSpeed = 1f;
	void MoveKaChe()
	{
		KaCheTouTr.position += KaCheTouTr.forward * MvSpeed * Time.deltaTime;
		
		Vector3 rotSpeed = Vector3.up * RtSpeed * Time.deltaTime;
		KaCheTouTr.Rotate(rotSpeed);
	}
}