using UnityEngine;
using System.Collections;

public class XKPlayerMoveCarCtrl : MonoBehaviour
{
	public Transform RealCarTr;
	/**
	 * CarFrontWheel[0] -> 左前轮.
	 * CarFrontWheel[1] -> 右前轮.
	 */
	public Transform[] CarFrontWheel;
	/**
	 * CarHitTr用来检测周围的碰撞.
	 * CarHitTr[0-2]用来检测卡车左侧的碰撞.
	 * CarHitTr[3-5]用来检测卡车右侧的碰撞.
	 */
	public Transform[] CarHitTr;
	public TweenRotation[] CarBackWheelTwRot;
	XKCarMoveCtrl CarController;
	Rigidbody CarRig;
	Transform CarTr;
	float KeyRotSpeed = 1f;
	// Use this for initialization
	void Start ()
	{
		XKPlayerMoveCarCameraCtrl.PlayerMoveCar = this;
		CarController = GetComponent<XKCarMoveCtrl>();
		CarController.m_MaximumSteerAngle = MinAngleVal;
		CarController.m_CarDriveType = CarDriveType.FourWheelDrive;

		CarRig = GetComponent<Rigidbody>();
		CarTr = transform;

		CarPiaoYiDirCom.enabled = false;
		KeyRotSpeed = (80f - 0f) / (MinAngleVal - MaxAngleVal);
		for (int i = 0; i < CarHitTr.Length; i++) {
			CarHitTr[i].gameObject.SetActive(false);
		}
	}

//	[Range(0f, 10000f)]public float PiaoYiForceVal = 10f;
//	[Range(0f, 100f)]public float PiaoYiVal = 5f;
	/**
	 * 获取漂移时车辆正前方和车辆左或右侧的夹角数据.
	 */
	[Range(0f, 1f)]public float PiaoYiOffsetVal = 0.5f;
//	[Range(0f, 100f)]public float ShouShaSpeedVal = 50f;
	[Range(0f, 100f)]public float PiaoYiAngleVal = 50f;
//	[Range(0f, 100f)]public float PiaoYiGoSpeed = 50f;
	public float RealCarAngleY;
	bool IsRunKaCheWheel = true;
	/**
	 * CarRunState == 1 -> 车辆前进.
	 * CarRunState == 0 -> 车辆停止.
	 * CarRunState == -1 -> 车辆后退.
	 */
	public int CarRunState;
	/**
	 * CarMoveSpeedState == 0 -> 车辆速度低于5时.
	 * CarMoveSpeedState == 1 -> 车辆速度高于5时.
	 */
	byte CarMoveSpeedState;
	public TweenScale CarPiaoYiDirCom;
	float CarSpeedVal;
	[Range(0f, 90f)]public float MinAngleVal = 5f;
	[Range(0f, 90f)]public float MaxAngleVal = 25f;
	bool IsActivePiaoYi;
	const float MinCarQianJinSpeed = 20f;
	const float MaxCarHouTuiSpeed = 10f;
	float TimePiaoYiLast;
	/**
	 * 延迟结束漂移.
	 */
	const float DealyOverPiaoYiTime = 0.5f;
	float TimePiaoYiOver = -100f;
	float PiaoYiOverSpeed;
	// Update is called once per frame
	void FixedUpdate ()
	{
		float steer = Input.GetAxis("Horizontal");
		float accel = Input.GetAxis("Vertical");
		float handBrake = Input.GetAxis("Jump");

		UpdateKaCheCheTiQiBuOrStop(accel, handBrake);
		UpdateKaCheFrontWheelLRRot(steer);
		FixKaCheRotPos();
		if (CarSpeedVal <= 0.5f && Mathf.Abs(accel) <= 0.05f) {
			if (!CarRig.isKinematic) {
				CarRig.isKinematic = true;

				CarRunState = 0;
				IsRunKaCheWheel = false;
				for (int i = 0; i < CarBackWheelTwRot.Length; i++) {
					CarBackWheelTwRot[i].enabled = false;
				}
			}
			return;
		}

		if (CarRig.isKinematic) {
			CarRig.isKinematic = false;
		}
		UpdateKaCheLunZiRot(accel);

		if (accel > 0f) {
			if (Vector3.Dot(CarRig.velocity, CarTr.forward) < 0f || CarSpeedVal < MinCarQianJinSpeed) {
				Vector3 minCarSpeed = CarTr.forward * MinCarQianJinSpeed;
				CarRig.velocity = Vector3.Lerp(CarRig.velocity, minCarSpeed, Time.deltaTime * 4f);
			}
		}

		if (accel < 0f) {
			if (Vector3.Dot(CarRig.velocity, -CarTr.forward) < 0f || CarSpeedVal < MaxCarHouTuiSpeed) {
				Vector3 minCarSpeed = -CarTr.forward * MaxCarHouTuiSpeed;
				CarRig.velocity = Vector3.Lerp(CarRig.velocity, minCarSpeed, Time.deltaTime * 4f);
			}

			if (CarSpeedVal >= MaxCarHouTuiSpeed) {
				accel = 0f;
			}
		}

		if (accel == 0f && CarSpeedVal > 0.2f) {
			Vector3 minCarSpeed = CarTr.forward * 0.1f;
			CarRig.velocity = Vector3.Lerp(CarRig.velocity, minCarSpeed, Time.deltaTime * 2f);
		}

		if ((handBrake > 0f && steer != 0f && CarSpeedVal > 30f)
		    || (IsActivePiaoYi && handBrake <= 0f && Time.realtimeSinceStartup - TimePiaoYiLast < DealyOverPiaoYiTime)) {
			if (handBrake > 0f) {
				TimePiaoYiLast = Time.realtimeSinceStartup;
			}
			IsActivePiaoYi = true;
			handBrake = 0f;
			XKPlayerMoveCarCameraCtrl.PerCameraFollowSpeed = 1f;
			steer = steer > 0f ? PiaoYiOffsetVal : -PiaoYiOffsetVal;
			CarController.m_MaximumSteerAngle = MaxAngleVal;
			if (Mathf.Abs(RealCarAngleY) < PiaoYiAngleVal) {
				float carAngleY = steer > 0f ? PiaoYiAngleVal : -PiaoYiAngleVal;
				if (!CarPiaoYiDirCom.enabled && RealCarAngleY == 0f) {
					CarPiaoYiDirCom.from = Vector3.zero;
					CarPiaoYiDirCom.to = new Vector3(0f, carAngleY, 0f);
					CarPiaoYiDirCom.ResetToBeginning();
					CarPiaoYiDirCom.PlayForward();
					CarPiaoYiDirCom.enabled = true;
				}
				RealCarAngleY = CarPiaoYiDirCom.transform.localScale.y;
				//Debug.Log("Unity:"+"RealCarAngleY "+RealCarAngleY);

				if (Mathf.Abs(RealCarAngleY) >= PiaoYiAngleVal) {
					RealCarAngleY = carAngleY;
				}
				RealCarTr.localEulerAngles = new Vector3(0f, RealCarAngleY, 0f);
			}
		}
		else {
			if (IsActivePiaoYi) {
				XKPlayerMoveCarCameraCtrl.PerCameraFollowSpeed = 0.5f;
				TimePiaoYiOver = Time.realtimeSinceStartup;
				PiaoYiOverSpeed = CarSpeedVal;
			}
			IsActivePiaoYi = false;

			float rotSpeedVal = (CarSpeedVal / KeyRotSpeed) + MaxAngleVal;
			CarController.m_MaximumSteerAngle = Mathf.Clamp(rotSpeedVal, MinAngleVal, MaxAngleVal);
			if (RealCarAngleY != 0f) {
				RealCarAngleY = 0f;
				CarPiaoYiDirCom.enabled = false;
				CarPiaoYiDirCom.ResetToBeginning();
				RealCarTr.parent = null;
				CarTr.forward = RealCarTr.forward;
				RealCarTr.parent = CarTr;
				RealCarTr.localPosition = Vector3.zero;
				RealCarTr.localEulerAngles = Vector3.zero;
				RealCarTr.localScale = Vector3.one;
			}
		}

		if (Time.realtimeSinceStartup - TimePiaoYiOver < 2f) {
			Vector3 piaoYiSpeedVec = CarTr.forward * PiaoYiOverSpeed * 1.2f;
			CarRig.velocity = Vector3.Lerp(CarRig.velocity, piaoYiSpeedVec, Time.deltaTime * 15f);
		}
		else {
			if (!IsActivePiaoYi && XKPlayerMoveCarCameraCtrl.PerCameraFollowSpeed < 1f) {
//				float valSpeed = XKPlayerMoveCarCameraCtrl.PerCameraFollowSpeed;
				XKPlayerMoveCarCameraCtrl.PerCameraFollowSpeed += Time.deltaTime * 2f;
				if (XKPlayerMoveCarCameraCtrl.PerCameraFollowSpeed >= 0.97f) {
					XKPlayerMoveCarCameraCtrl.PerCameraFollowSpeed = 1f;
				}
			}
		}

		CarController.Move(steer, accel, accel, handBrake);
		CheckDaoYanHit();
		UpdateKaCheCheTiRotZ(steer);
	}

	/**
	 * 更新卡车前轮左右转向.
	 */
	void UpdateKaCheFrontWheelLRRot(float steering)
	{
		steering = Mathf.Clamp(steering, -1, 1);
		Vector3 eulerAnglesVal = new Vector3(0f, steering*CarController.m_MaximumSteerAngle, 0f);
		if (IsActivePiaoYi) {
			eulerAnglesVal = new Vector3(0f, steering*25f, 0f);
		}
		CarFrontWheel[0].localEulerAngles = eulerAnglesVal;
		CarFrontWheel[1].localEulerAngles = eulerAnglesVal;
	}

//	[Range(-1, 1)]public int TestFixState;
//	void Update()
//	{
//    	if (Input.GetKeyUp(KeyCode.T)) {
//			InitFixKaCheRotPos(Random.Range(0, 2) == 0 ? 1 : -1);
//			InitFixKaCheRotPos(TestFixState);
//		}
//	}

	public LayerMask DaoYanLayer;
	[Range(0f, 100f)]public float DisHitDaoYan = 1f;
	void CheckDaoYanHit()
	{
		if (IsFixKaCheRotPos || DisHitDaoYan <= 0f) {
			return;
		}

		TerrainCollider terrainCol = null;
		Collider[]hits = null;
		int hitState = 0;
		for (int i = 0; i < CarHitTr.Length; i++) {
			hits = Physics.OverlapSphere(CarHitTr[i].position, DisHitDaoYan, DaoYanLayer);
			if (hits == null || hits.Length <= 1) {
				continue;
			}

			foreach (Collider c in hits) {
				// Don't collide with triggers
				if (c.isTrigger) {
					continue;
				}

				terrainCol = c.GetComponent<TerrainCollider>();
				if (c.transform.root == CarTr || terrainCol != null) {
					continue;
				}

				hitState = i <= 2 ? 1 : -1;
				InitFixKaCheRotPos(hitState);
				return;
			}
		}
	}

	/**
	 * FixRotPosState == 1 -> 向右修正.
	 * FixRotPosState == -1 -> 向左修正.
	 */
	int FixRotPosState;
	float TimeFixRotPos;
	[Range(0f, 100f)]public float MaxTimeFixRP = 3f;
	bool IsFixKaCheRotPos;

	void FixKaCheRotPos()
	{
		if (!IsFixKaCheRotPos) {
			return;
		}

		if (Time.realtimeSinceStartup - TimeFixRotPos > MaxTimeFixRP) {
			IsFixKaCheRotPos = false;
			return;
		}
		
		float rotFix = FixRotHit * FixRotPosState; //test;
		rotFix *= Time.deltaTime;
		CarTr.Rotate(Vector3.up * rotFix, Space.World);

		float posFix = FixPosHit * FixRotPosState;
		posFix *= Time.deltaTime;
		Vector3 moveVec = CarTr.right * posFix;
		moveVec.y = 0f;
		CarTr.Translate(moveVec, Space.World);
	}

	/**
	 * state == 1 -> 使车辆向右推.
	 * state == -1 -> 使车辆向左推.
	 */
	void InitFixKaCheRotPos(int state)
	{
		if (IsFixKaCheRotPos && FixRotPosState == state) {
			return;
		}
		//Debug.Log("Unity:"+"InitFixKaCheRotPos -> state "+(state == 1 ? "right" : "left"));
		FixRotPosState = state;
		TimeFixRotPos = Time.realtimeSinceStartup;
		IsFixKaCheRotPos = true;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger) {
			return;
		}
		OnPlayerKaCheHitObj(other.transform);
	}

	[Range(0f, 100f)]public float FixRotHit = 1f;
	[Range(0f, 100f)]public float FixPosHit = 1f;
	public void OnPlayerKaCheHitObj(Transform hitTr)
	{
		//用于吃道具逻辑.
		if (hitTr == null) {
			return;
		}
	}
	
	const float MaxCheTiQiBuXRot = 2f;
	const float MaxCheTiJianSuXRot = 1f;
	public float CheTiXRot;
	/**
	 * 起步或停车时第一阶段的速度.
	 */
	[Range(0f, 100f)]public float CheTiXRotSpeed1 = 1f;
	/**
	 * 起步或停车时第二阶段的速度.
	 */
	[Range(0f, 100f)]public float CheTiXRotSpeed2 = 1f;
	/**
	 * QiBuJianSuState == 1 -> 起步.
	 * QiBuJianSuState == 0 -> 静止.
	 * QiBuJianSuState == -1 -> 减速.
	 */
	public int QiBuJianSuState;
	/**
	 * 起步减速均为3个阶段.
	 * 起步: 1 ->> XRot: xCur -> -5, 2 ->> 持续一定时间, 3 ->> -5 -> 0.
	 * 减速: 1 ->> XRot: xCur -> 5, 2 ->> 当速度为0时,持续一定时间, 3 ->> 5 -> 0.
	 */
	public byte QiBuJianSuJieDuan;
	float TimeQiBuJieDuan2;
	/**
	 * 起步第2阶段时长.
	 */
	const float MaxTimeQiBuJieDuan2 = 2f;
	/**
	 * 更新卡车车体起步或停车动作.
	 */
	void UpdateKaCheCheTiQiBuOrStop(float accel, float handBrake)
	{
		if (accel > 0f && QiBuJianSuState != 1) {
			QiBuJianSuState = 1;
			QiBuJianSuJieDuan = 1;
//			Debug.Log("Unity:"+"start qiBu...");
//			Debug.Log("Unity:"+"stop jianSu...");
		}

		if (QiBuJianSuState == 1 && (accel <= 0f || handBrake > 0f)) {
			QiBuJianSuState = -1;
			QiBuJianSuJieDuan = 1;
//			Debug.Log("Unity:"+"stop qiBu...");
//			Debug.Log("Unity:"+"start jianSu...");
		}
		
		float speed = 0f;
		float kaCheRotZ = KaCheCheTiTr.localEulerAngles.z;
		switch (QiBuJianSuState) {
		case 1:
		{
			switch (QiBuJianSuJieDuan) {
			case 1:
			{
				if (CheTiXRot <= -MaxCheTiQiBuXRot) {
					CheTiXRot = -MaxCheTiQiBuXRot;
					KaCheCheTiTr.localEulerAngles = new Vector3(CheTiXRot, 0f, kaCheRotZ);
					TimeQiBuJieDuan2 = Time.realtimeSinceStartup;
					QiBuJianSuJieDuan = 2;
					return;
				}
				speed = -CheTiXRotSpeed1;
			}
			break;

			case 2:
			{
				if (Time.realtimeSinceStartup - TimeQiBuJieDuan2 >= MaxTimeQiBuJieDuan2) {
					QiBuJianSuJieDuan = 3;
				}

				CheTiXRot = -MaxCheTiQiBuXRot;
				if (KaCheCheTiTr.localEulerAngles.x  != CheTiXRot) {
					KaCheCheTiTr.localEulerAngles = new Vector3(CheTiXRot, 0f, kaCheRotZ);
				}
				
				if (QiBuJianSuJieDuan != 3) {
					return;
				}
			}
			break;

			case 3:
			{
				if (CheTiXRot >= 0f) {
					CheTiXRot = 0f;
					if (KaCheCheTiTr.localEulerAngles.x != CheTiXRot) {
						KaCheCheTiTr.localEulerAngles = new Vector3(CheTiXRot, 0f, kaCheRotZ);
					}
					return;
				}
				speed = CheTiXRotSpeed2;
			}
			break;
			}
		}
		break;

			
		case -1:
		{
			switch (QiBuJianSuJieDuan) {
			case 1:
			{
				if (CheTiXRot >= MaxCheTiJianSuXRot) {
					CheTiXRot = MaxCheTiJianSuXRot;
					KaCheCheTiTr.localEulerAngles = new Vector3(CheTiXRot, 0f, kaCheRotZ);
					QiBuJianSuJieDuan = 2;
					return;
				}
				speed = CheTiXRotSpeed1;
			}
			break;
				
			case 2:
			{
				if (CarSpeedVal <= 0 && CarRig.isKinematic) {
					QiBuJianSuJieDuan = 3;
				}
				
				CheTiXRot = MaxCheTiJianSuXRot;
				if (KaCheCheTiTr.localEulerAngles.x  != CheTiXRot) {
					KaCheCheTiTr.localEulerAngles = new Vector3(CheTiXRot, 0f, kaCheRotZ);
				}

				if (QiBuJianSuJieDuan != 3) {
					return;
				}
			}
			break;
				
			case 3:
			{
				if (CheTiXRot <= 0f) {
					CheTiXRot = 0f;
					if (KaCheCheTiTr.localEulerAngles.x != CheTiXRot) {
						KaCheCheTiTr.localEulerAngles = new Vector3(CheTiXRot, 0f, kaCheRotZ);
					}
					return;
				}
				speed = -CheTiXRotSpeed2;
			}
			break;
			}
		}
		break;
		}

		CheTiXRot += Time.deltaTime * speed;
		KaCheCheTiTr.localEulerAngles = new Vector3(CheTiXRot, 0f, kaCheRotZ);
	}

	public Transform KaCheCheTiTr;
	[Range(0f, 100f)]public float CheTiZRotSpeed;
	public float CheTiZRot;
	const float MaxCheTiZRot = 5f;
	/**
	 * 更新卡车车身的左右倾斜动作.
	 */
	void UpdateKaCheCheTiRotZ(float steerVal)
	{
		if (CarSpeedVal < 10f || KaCheCheTiTr == null) {
			return;
		}

		steerVal = Mathf.Clamp(steerVal, -1f, 1f);
//		if (Mathf.Abs(steerVal) < 0.01f) {
//			steerVal = 0f;
//		}
		
		float kaCheRotX = KaCheCheTiTr.localEulerAngles.x;
		float speed = CheTiZRotSpeed;
		bool isReturn = false;
		int key = 0;
		if (steerVal == 0f) {
			if (CheTiZRot == 0f) {
				CheTiZRot = 0f;
				isReturn = true;
			}
			speed = CheTiZRot > 0f ? -CheTiZRotSpeed : CheTiZRotSpeed;
		}

		if (steerVal > 0f) {
			if (CheTiZRot >= MaxCheTiZRot) {
				CheTiZRot = MaxCheTiZRot;
				isReturn = true;
			}
			key = 1;
		}

		if (steerVal < 0f) {
			if (CheTiZRot <= -MaxCheTiZRot) {
				CheTiZRot = -MaxCheTiZRot;
				isReturn = true;
			}
			speed = -CheTiZRotSpeed;
			key = -1;
		}

		if (isReturn) {
			if (KaCheCheTiTr.localEulerAngles.z != CheTiZRot) {
				KaCheCheTiTr.localEulerAngles = new Vector3(kaCheRotX, 0f, CheTiZRot);
			}
			return;
		}
		
		CheTiZRot += Time.deltaTime * speed;
		switch (key) {
		case 1:
			if (CheTiZRot >= MaxCheTiZRot) {
				CheTiZRot = MaxCheTiZRot;
			}
			break;
		case 0:
			if (Mathf.Abs(CheTiZRot) <= (1.5f * CheTiZRotSpeed * Time.deltaTime)) {
				CheTiZRot = 0f;
			}
			break;
		case -1:
			if (CheTiZRot <= -MaxCheTiZRot) {
				CheTiZRot = -MaxCheTiZRot;
			}
			break;
		}
		CheTiZRot = Mathf.Clamp(CheTiZRot, -MaxCheTiZRot, MaxCheTiZRot);
		KaCheCheTiTr.localEulerAngles = new Vector3(kaCheRotX, 0f, CheTiZRot);
	}
	
	/**
	 * 更新卡车轮子X轴的转向.
	 */
	void UpdateKaCheLunZiRot(float accel)
	{
		int runState = accel >= 0f ? 1 : -1;
		if (runState != CarRunState) {
			IsRunKaCheWheel = false;
		}
		
		if (!IsRunKaCheWheel) {
			IsRunKaCheWheel = true;
			CarRunState = runState;
			for (int i = 0; i < CarBackWheelTwRot.Length; i++) {
				CarBackWheelTwRot[i].ResetToBeginning();
				Vector3 fromVal = new Vector3(360f, 180f, 0f);
				Vector3 toVal = new Vector3(0f, 180f, 0f);
				if (CarRunState == -1) {
					fromVal = new Vector3(0f, 180f, 0f);
					toVal = new Vector3(360f, 180f, 0f);
				}
				CarBackWheelTwRot[i].from = fromVal;
				CarBackWheelTwRot[i].to = toVal;
				CarBackWheelTwRot[i].PlayForward();
				CarBackWheelTwRot[i].enabled = true;
			}
		}
		
		if (CarSpeedVal <= 5f && CarMoveSpeedState != 0) {
			CarMoveSpeedState = 0;
			for (int i = 0; i < CarBackWheelTwRot.Length; i++) {
				CarBackWheelTwRot[i].duration = 5f;
			}
		}
		
		if (CarSpeedVal > 5f && CarMoveSpeedState != 1) {
			CarMoveSpeedState = 1;
			for (int i = 0; i < CarBackWheelTwRot.Length; i++) {
				CarBackWheelTwRot[i].duration = 1f;
			}
		}
	}

	public float GetCarMoveSpeed()
	{
		return CarSpeedVal;
	}

	void OnGUI()
	{
		CarSpeedVal = CarRig.velocity.magnitude * 3.6f;
		string strA = "carSpeed: "+CarSpeedVal.ToString("f2");
		strA += ", PerVal "+XKPlayerMoveCarCameraCtrl.PerCameraFollowSpeed.ToString("f2");
//		string strB = "wheel[0]: motorTorque "+CarController.m_WheelColliders[0].motorTorque.ToString("f2")
//			+", brakeTorque "+CarController.m_WheelColliders[0].brakeTorque.ToString("f2");
//		string strC = "wheel[1]: motorTorque "+CarController.m_WheelColliders[1].motorTorque.ToString("f2")
//			+", brakeTorque "+CarController.m_WheelColliders[1].brakeTorque.ToString("f2");
//		string strD = "wheel[2]: motorTorque "+CarController.m_WheelColliders[2].motorTorque.ToString("f2")
//			+", brakeTorque "+CarController.m_WheelColliders[2].brakeTorque.ToString("f2");
//		string strE = "wheel[3]: motorTorque "+CarController.m_WheelColliders[3].motorTorque.ToString("f2")
//			+", brakeTorque "+CarController.m_WheelColliders[3].brakeTorque.ToString("f2");

		float hVal = 25f;
		float wVal = 300f;
		GUI.Box(new Rect(0f, 0f, wVal, hVal), strA);
//		GUI.Box(new Rect(0f, hVal, wVal, hVal), strB);
//		GUI.Box(new Rect(0f, hVal * 2f, wVal, hVal), strC);
//		GUI.Box(new Rect(0f, hVal * 3f, wVal, hVal), strD);
//		GUI.Box(new Rect(0f, hVal * 4f, wVal, hVal), strE);
	}
}