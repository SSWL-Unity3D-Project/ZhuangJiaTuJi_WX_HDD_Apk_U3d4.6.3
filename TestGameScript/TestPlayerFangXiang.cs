using UnityEngine;
using System.Collections;

public class TestPlayerFangXiang : MonoBehaviour
{
	[Range(0f, 90f)]public float MaxFangXiangAngle = 45f;
	float MinFangXiangAngle = 45f;
	Transform GameCameraTran;
	float KeyFangXiang = 0f;
	// Use this for initialization
	void Start()
	{
		PlayerTran = transform;
		MinFangXiangAngle = -MaxFangXiangAngle;
		GameCameraTran = Camera.main.transform;
		KeyFangXiang = (MaxFangXiangAngle - MinFangXiangAngle) / 2f;
	}
	
	// Update is called once per frame
	void Update()
	{
		UpdatePlayerRotation(Input.GetAxis("Horizontal"));
	}

	Transform PlayerTran;
//	public float TestAngle;
	void InitUpdatePlayerRotation()
	{
		PlayerTran = transform;
		MinFangXiangAngle = -MaxFangXiangAngle;
		GameCameraTran = Camera.main.transform;
		KeyFangXiang = (MaxFangXiangAngle - MinFangXiangAngle) / 2f;
	}

	void UpdatePlayerRotation(float curFangXiang)
	{
		//float curFangXiang = Input.GetAxis("Horizontal");
		float curAngle = MaxFangXiangAngle - KeyFangXiang * (1f - curFangXiang);
		
		Vector3 veA = GameCameraTran.forward;
		Vector3 veB = Vector3.forward;
		veA.y = veB.y = 0f;
		float angleTmp = Vector3.Angle(veA, veB);
		if (Vector3.Dot(veA, Vector3.right) < 0f) {
			angleTmp = -angleTmp;
		}
		curAngle = curAngle + angleTmp;
//		TestAngle = curAngle;

		Quaternion rotVal = Quaternion.AngleAxis(curAngle, Vector3.up);
		Vector3 euA = rotVal.eulerAngles;
		Vector3 euB = PlayerTran.rotation.eulerAngles;
		euA.x = euB.x;
		euA.z = euB.z;
		rotVal.eulerAngles = euA;
		PlayerTran.rotation = Quaternion.Lerp(PlayerTran.rotation, rotVal, 0.5f);
	}
}