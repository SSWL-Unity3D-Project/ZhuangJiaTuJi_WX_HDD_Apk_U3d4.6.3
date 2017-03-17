using UnityEngine;
using System.Collections;

public class XKPlayerMoveCarCameraCtrl : MonoBehaviour
{
	Transform CamTr;
	public Transform CarFollowTr;
	public Transform CamAimTr;
	public float FollowSpeed = 2f;
	public float FollowRotSpeed = 2f;
	public LayerMask CamZhuDangLayer;
	public static XKPlayerMoveCarCtrl PlayerMoveCar;
	public static float PerCameraFollowSpeed = 1f;
	void Awake()
	{
		CamTr = transform;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (PlayerMoveCar == null) {
			return;
		}
		
		Vector3 camFollowPos = CarFollowTr.position;
		Vector3 forwardVal = CarFollowTr.position - CamAimTr.position;
		RaycastHit hitInfo;
		float disCamFA = Vector3.Distance(CamAimTr.position, CamTr.position);
		Physics.Raycast(CamAimTr.position, forwardVal.normalized, out hitInfo, disCamFA, CamZhuDangLayer);
		if (hitInfo.collider != null){
			camFollowPos = hitInfo.point;
		}

		float disFollowPoint = Vector3.Distance(CamTr.position, camFollowPos);
		if (PlayerMoveCar.GetCarMoveSpeed() < 5f && disFollowPoint < 0.2f) {
			return;
		}
		CamTr.position = Vector3.Lerp(CamTr.position, camFollowPos, Time.deltaTime * FollowSpeed * PerCameraFollowSpeed);

		forwardVal = CamAimTr.position - CarFollowTr.position;
		CamTr.forward = Vector3.Lerp(CamTr.forward, forwardVal, Time.deltaTime * FollowRotSpeed * PerCameraFollowSpeed);
	}
}