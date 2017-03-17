#define LINE_STEER
using UnityEngine;
using System.Collections;
using System;

public class TestTanKCom : MonoBehaviour
{
	static int TimeVal;
	static string TestReadMsgA = "";

	uint SteerValMaxP2;
	uint SteerValCenP2;
	uint SteerValMinP2;
	uint SteerValMaxP3;
	uint SteerValCenP3;
	uint SteerValMinP3;
	uint YouMenValMaxP2;
	uint YouMenValMinP2;
	uint YouMenValMaxP3;
	uint YouMenValMinP3;

	/**
	 * AnJianState[0] -> 2P射击.
	 * AnJianState[1] -> 2P导弹.
	 * AnJianState[2] -> 3P射击.
	 * AnJianState[3] -> 3P导弹.
	 */
	byte[] AnJianState =  new byte[4];
	public static float FangXiangStateP2;
	public static int YouMenStateP2;
	public static float FangXiangStateP3;
	public static int YouMenStateP3;
	public static bool IsTestTankCom;
	string[] FXAZInfo = new string[2];
	string[] YMAZInfo = new string[2];
	void Start()
	{
		IsTestTankCom = true;
		XKGlobalData.GetInstance();
		string FileName = "TestTankCom";
		HandleJson HandleJsonObj = XKGlobalData.HandleJsonObj;
		
		FXAZInfo[0] = HandleJsonObj.ReadFromFileXml(FileName, "FXAZP2");
		if(FXAZInfo[0] == null || FXAZInfo[0] == "") {
			FXAZInfo[0] = "0";
			HandleJsonObj.WriteToFileXml(FileName, "FXAZP2", FXAZInfo[0]);
		}

		FXAZInfo[1] = HandleJsonObj.ReadFromFileXml(FileName, "FXAZP3");
		if(FXAZInfo[1] == null || FXAZInfo[1] == "") {
			FXAZInfo[1] = "0";
			HandleJsonObj.WriteToFileXml(FileName, "FXAZP3", FXAZInfo[1]);
		}

		YMAZInfo[0] = HandleJsonObj.ReadFromFileXml(FileName, "YMAZP2");
		if(YMAZInfo[0] == null || YMAZInfo[0] == "") {
			YMAZInfo[0] = "0";
			HandleJsonObj.WriteToFileXml(FileName, "YMAZP2", YMAZInfo[0]);
		}
		
		YMAZInfo[1] = HandleJsonObj.ReadFromFileXml(FileName, "YMAZP3");
		if(YMAZInfo[1] == null || YMAZInfo[1] == "") {
			YMAZInfo[1] = "0";
			HandleJsonObj.WriteToFileXml(FileName, "YMAZP3", YMAZInfo[1]);
		}

		string strValMax = HandleJsonObj.ReadFromFileXml(FileName, "SteerValMaxP2");
		if(strValMax == null || strValMax == "") {
			strValMax = "1";
			HandleJsonObj.WriteToFileXml(FileName, "SteerValMaxP2", strValMax);
		}
		SteerValMaxP2 = Convert.ToUInt32( strValMax );
		
		string strValCen = HandleJsonObj.ReadFromFileXml(FileName, "SteerValCenP2");
		if(strValCen == null || strValCen == "") {
			strValCen = "1";
			HandleJsonObj.WriteToFileXml(FileName, "SteerValCenP2", strValCen);
		}
		SteerValCenP2 = Convert.ToUInt32( strValCen );

		string strValMin = HandleJsonObj.ReadFromFileXml(FileName, "SteerValMinP2");
		if(strValMin == null || strValMin == "") {
			strValMin = "1";
			HandleJsonObj.WriteToFileXml(FileName, "SteerValMinP2", strValMin);
		}
		SteerValMinP2 = Convert.ToUInt32( strValMin );
		
		strValMax = HandleJsonObj.ReadFromFileXml(FileName, "SteerValMaxP3");
		if(strValMax == null || strValMax == "") {
			strValMax = "1";
			HandleJsonObj.WriteToFileXml(FileName, "SteerValMaxP3", strValMax);
		}
		SteerValMaxP3 = Convert.ToUInt32( strValMax );

		strValCen = HandleJsonObj.ReadFromFileXml(FileName, "SteerValCenP3");
		if(strValCen == null || strValCen == "") {
			strValCen = "1";
			HandleJsonObj.WriteToFileXml(FileName, "SteerValCenP3", strValCen);
		}
		SteerValCenP3 = Convert.ToUInt32( strValCen );

		strValMin = HandleJsonObj.ReadFromFileXml(FileName, "SteerValMinP3");
		if(strValMin == null || strValMin == "") {
			strValMin = "1";
			HandleJsonObj.WriteToFileXml(FileName, "SteerValMinP3", strValMin);
		}
		SteerValMinP3 = Convert.ToUInt32( strValMin );

		strValMax = HandleJsonObj.ReadFromFileXml(FileName, "YouMenValMaxP2");
		if(strValMax == null || strValMax == "") {
			strValMax = "1";
			HandleJsonObj.WriteToFileXml(FileName, "YouMenValMaxP2", strValMax);
		}
		YouMenValMaxP2 = Convert.ToUInt32( strValMax );
		
		strValMin = HandleJsonObj.ReadFromFileXml(FileName, "YouMenValMinP2");
		if(strValMin == null || strValMin == "") {
			strValMin = "1";
			HandleJsonObj.WriteToFileXml(FileName, "YouMenValMinP2", strValMin);
		}
		YouMenValMinP2 = Convert.ToUInt32( strValMin );
		
		strValMax = HandleJsonObj.ReadFromFileXml(FileName, "YouMenValMaxP3");
		if(strValMax == null || strValMax == "") {
			strValMax = "1";
			HandleJsonObj.WriteToFileXml(FileName, "YouMenValMaxP3", strValMax);
		}
		YouMenValMaxP3 = Convert.ToUInt32( strValMax );
		
		strValMin = HandleJsonObj.ReadFromFileXml(FileName, "YouMenValMinP3");
		if(strValMin == null || strValMin == "") {
			strValMin = "1";
			HandleJsonObj.WriteToFileXml(FileName, "YouMenValMinP3", strValMin);
		}
		YouMenValMinP3 = Convert.ToUInt32( strValMin );
	}

	bool IsCloseOutInfo;
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.T)) {
			IsCloseOutInfo = !IsCloseOutInfo;
		}
	}

	void OnGUI()
	{
		if (MyCOMDevice.ComThreadClass.ReadByteMsg.Length < (MyCOMDevice.ComThreadClass.BufLenRead - MyCOMDevice.ComThreadClass.BufLenReadEnd)) {
//			Debug.Log("ReadBufLen: "+MyCOMDevice.ComThreadClass.ReadByteMsg.Length);
//			Debug.LogError("ReadMsgError: msg -> "+TestReadMsg);
			return;
		}

		float startX = 10f;
		float hVal = 20f;
		float wVal = Screen.width * 0.5f;
		int len = MyCOMDevice.ComThreadClass.ReadByteMsg.Length;
		uint[] buffer = new uint[len];
		if (MyCOMDevice.ComThreadClass.ReadCount > 0) {
			TestReadMsgA = "Read: ";
			for (int i = 0; i < MyCOMDevice.ComThreadClass.ReadByteMsg.Length; i++) {
				buffer[i] = MyCOMDevice.ComThreadClass.ReadByteMsg[i];
				TestReadMsgA += MyCOMDevice.ComThreadClass.ReadByteMsg[i].ToString("X2") + " ";
			}

			if (!IsCloseOutInfo) {
				GUI.Label(new Rect(startX, 10f, Screen.width, hVal), TestReadMsgA);
			}
		}
		
		TestReadMsgA = "ReadTimeOutCount: "+MyCOMDevice.ComThreadClass.ReadTimeOutCount;
		GUI.Label(new Rect(startX, 10f + hVal, wVal, hVal), TestReadMsgA);
		
		TimeVal = (int)Time.realtimeSinceStartup;
		TestReadMsgA = MyCOMDevice.ComThreadClass.ComPortName + " -> time: "+TimeVal.ToString();
		GUI.Label(new Rect(startX, 10f + 2f * hVal, wVal, hVal), TestReadMsgA);

		uint fangXiangVal = ((buffer[2] & 0x0f) << 8) + buffer[3];
		string fxInfo = "";
		float fangXiangValTmp = 0f;
		#if LINE_STEER
		if (fangXiangVal >= SteerValCenP2) {
			switch (FXAZInfo[0]) {
			case "0":
				fangXiangValTmp = ((float)fangXiangVal - SteerValCenP2) / (SteerValMaxP2 - SteerValCenP2);
				break;
			default:
				fangXiangValTmp = ((float)SteerValCenP2 - fangXiangVal) / (SteerValMaxP2 - SteerValCenP2);
				break;
			}
		}
		else {
			switch (FXAZInfo[0]) {
			case "0":
				fangXiangValTmp = ((float)fangXiangVal - SteerValCenP2) / (SteerValCenP2 - SteerValMinP2);
				break;
			default:
				fangXiangValTmp = ((float)SteerValCenP2 - fangXiangVal) / (SteerValCenP2 - SteerValMinP2);
				break;
			}
		}
		
		fangXiangValTmp = Mathf.Clamp(fangXiangValTmp, -1f, 1f);
		fangXiangValTmp = Mathf.Abs(fangXiangValTmp) <= 0.05f ? 0f : fangXiangValTmp;
		if (fangXiangValTmp > 0f) {
			fxInfo = "右";
		}
		if (fangXiangValTmp < 0f) {
			fxInfo = "左";
		}
		if (fangXiangValTmp == 0f) {
			fxInfo = "中";
		}
		fxInfo += ", steer "+fangXiangValTmp.ToString("f2");
		FangXiangStateP2 = fangXiangValTmp;
		#else
		if (fangXiangVal > SteerValMaxP2) {
			switch (FXAZInfo[0]) {
			case "0":
				fxInfo = "右";
				FangXiangStateP2 = 1;
				break;
			default:
				fxInfo = "左";
				FangXiangStateP2 = -1;
				break;
			}
		}
		if (fangXiangVal < SteerValMinP2) {
			switch (FXAZInfo[0]) {
			case "0":
				fxInfo = "左";
				FangXiangStateP2 = -1;
				break;
			default:
				fxInfo = "右";
				FangXiangStateP2 = 1;
				break;
			}
		}
		if (fangXiangVal <= SteerValMaxP2 && fangXiangVal >= SteerValMinP2) {
			fxInfo = "中";
			FangXiangStateP2 = 0;
		}
		#endif
		TestReadMsgA = "2P -> 方向: "+fangXiangVal.ToString("d5")+", "+fxInfo;
		if (!IsCloseOutInfo) {
			GUI.Label(new Rect(startX, 10f + 3f * hVal, wVal, hVal), TestReadMsgA);
		}
		
		uint youMenVal = ((buffer[4] & 0x0f) << 8) + buffer[5];
		string ymInfo = "";
		if (youMenVal > YouMenValMaxP2) {
			switch (YMAZInfo[0]) {
			case "0":
				ymInfo = "前进";
				YouMenStateP2 = 1;
				break;
			default:
				ymInfo = "后退";
				YouMenStateP2 = -1;
				break;
			}
		}
		if (youMenVal < YouMenValMinP2) {
			switch (YMAZInfo[0]) {
			case "0":
				ymInfo = "后退";
				YouMenStateP2 = -1;
				break;
			default:
				ymInfo = "前进";
				YouMenStateP2 = 1;
				break;
			}
		}
		if (youMenVal <= YouMenValMaxP2 && youMenVal >= YouMenValMinP2) {
			ymInfo = "停止";
			YouMenStateP2 = 0;
		}
		TestReadMsgA = "         油门: "+youMenVal.ToString("d5")+", "+ymInfo;
		if (!IsCloseOutInfo) {
			GUI.Label(new Rect(startX, 10f + 4f * hVal, wVal, hVal), TestReadMsgA);
		}

		if( AnJianState[0] == 0 && (buffer[19]&0x40) == 0x40 ) {
			AnJianState[0] = 1;
			InputEventCtrl.IsClickFireBtOneDown = true;
			InputEventCtrl.GetInstance().ClickFireBtTwo( ButtonState.DOWN );
		}
		else if( AnJianState[0] == 1 && (buffer[19]&0x40) == 0x00 ) {
			AnJianState[0] = 0;
			InputEventCtrl.IsClickFireBtOneDown = false;
			InputEventCtrl.GetInstance().ClickFireBtTwo( ButtonState.UP );
		}
		string anJianInfo = AnJianState[0] == 0 ? "弹起" : "按下";
		TestReadMsgA = "         发射按键: "+anJianInfo;
		if (!IsCloseOutInfo) {
			GUI.Label(new Rect(startX, 10f + 5f * hVal, wVal, hVal), TestReadMsgA);
		}

		if( AnJianState[1] == 0 && (buffer[19]&0x80) == 0x80 ) {
			AnJianState[1] = 1;
			InputEventCtrl.GetInstance().ClickDaoDanBtTwo( ButtonState.DOWN );
		}
		else if( AnJianState[1] == 1 && (buffer[19]&0x80) == 0x00 ) {
			AnJianState[1] = 0;
			InputEventCtrl.GetInstance().ClickDaoDanBtTwo( ButtonState.UP );
		}
		anJianInfo = AnJianState[1] == 0 ? "弹起" : "按下";
		TestReadMsgA = "         导弹按键: "+anJianInfo;
		if (!IsCloseOutInfo) {
			GUI.Label(new Rect(startX, 10f + 6f * hVal, wVal, hVal), TestReadMsgA);
		}

		fangXiangVal = ((buffer[6] & 0x0f) << 8) + buffer[7];
		fxInfo = "";
		#if LINE_STEER
		if (fangXiangVal >= SteerValCenP3) {
			switch (FXAZInfo[1]) {
			case "0":
				fangXiangValTmp = ((float)fangXiangVal - SteerValCenP3) / (SteerValMaxP3 - SteerValCenP3);
				break;
			default:
				fangXiangValTmp = ((float)SteerValCenP3 - fangXiangVal) / (SteerValMaxP3 - SteerValCenP3);
				break;
			}
		}
		else {
			switch (FXAZInfo[1]) {
			case "0":
				fangXiangValTmp = ((float)fangXiangVal - SteerValCenP3) / (SteerValCenP3 - SteerValMinP3);
				break;
			default:
				fangXiangValTmp = ((float)SteerValCenP3 - fangXiangVal) / (SteerValCenP3 - SteerValMinP3);
				break;
			}
		}

		fangXiangValTmp = Mathf.Clamp(fangXiangValTmp, -1f, 1f);
		fangXiangValTmp = Mathf.Abs(fangXiangValTmp) <= 0.05f ? 0f : fangXiangValTmp;
		if (fangXiangValTmp > 0f) {
			fxInfo = "右";
		}
		if (fangXiangValTmp < 0f) {
			fxInfo = "左";
		}
		if (fangXiangValTmp == 0f) {
			fxInfo = "中";
		}
		fxInfo += ", steer "+fangXiangValTmp.ToString("f2");
		FangXiangStateP3 = fangXiangValTmp;
		#else
		if (fangXiangVal > SteerValMaxP3) {
			switch (FXAZInfo[0]) {
			case "0":
				fxInfo = "右";
				FangXiangStateP3 = 1;
				break;
			default:
				fxInfo = "左";
				FangXiangStateP3 = -1;
				break;
			}
		}
		if (fangXiangVal < SteerValMinP3) {
			switch (FXAZInfo[1]) {
			case "0":
				fxInfo = "左";
				FangXiangStateP3 = -1;
				break;
			default:
				fxInfo = "右";
				FangXiangStateP3 = 1;
				break;
			}
		}
		if (fangXiangVal <= SteerValMaxP3 && fangXiangVal >= SteerValMinP3) {
			fxInfo = "中";
			FangXiangStateP3 = 0;
		}
		#endif
		TestReadMsgA = "3P -> 方向: "+fangXiangVal.ToString("d5")+", "+fxInfo;
		if (!IsCloseOutInfo) {
			GUI.Label(new Rect(startX, 10f + 7f * hVal, wVal, hVal), TestReadMsgA);
		}
		
		youMenVal = ((buffer[8] & 0x0f) << 8) + buffer[9];
		ymInfo = "";
		if (youMenVal > YouMenValMaxP3) {
			switch (YMAZInfo[1]) {
			case "0":
				ymInfo = "前进";
				YouMenStateP3 = 1;
				break;
			default:
				ymInfo = "后退";
				YouMenStateP3 = -1;
				break;
			}
		}
		if (youMenVal < YouMenValMinP3) {
			switch (YMAZInfo[1]) {
			case "0":
				ymInfo = "后退";
				YouMenStateP3 = -1;
				break;
			default:
				ymInfo = "前进";
				YouMenStateP3 = 1;
				break;
			}
		}
		if (youMenVal <= YouMenValMaxP3 && youMenVal >= YouMenValMinP3) {
			ymInfo = "停止";
			YouMenStateP3 = 0;
		}
		TestReadMsgA = "         油门: "+youMenVal.ToString("d5")+", "+ymInfo;
		if (!IsCloseOutInfo) {
			GUI.Label(new Rect(startX, 10f + 8f * hVal, wVal, hVal), TestReadMsgA);
		}
		
		if( AnJianState[2] == 0 && (buffer[20]&0x01) == 0x01 ) {
			AnJianState[2] = 1;
			InputEventCtrl.IsClickFireBtTwoDown = true;
			InputEventCtrl.GetInstance().ClickFireBtThree( ButtonState.DOWN );
		}
		else if( AnJianState[2] == 1 && (buffer[20]&0x01) == 0x00 ) {
			AnJianState[2] = 0;
			InputEventCtrl.IsClickFireBtTwoDown = false;
			InputEventCtrl.GetInstance().ClickFireBtThree( ButtonState.UP );
		}
		anJianInfo = AnJianState[2] == 0 ? "弹起" : "按下";
		TestReadMsgA = "         发射按键: "+anJianInfo;
		if (!IsCloseOutInfo) {
			GUI.Label(new Rect(startX, 10f + 9f * hVal, wVal, hVal), TestReadMsgA);
		}
		
		if( AnJianState[3] == 0 && (buffer[20]&0x02) == 0x02 ) {
			AnJianState[3] = 1;
			InputEventCtrl.GetInstance().ClickDaoDanBtThree( ButtonState.DOWN );
		}
		else if( AnJianState[3] == 1 && (buffer[20]&0x02) == 0x00 ) {
			AnJianState[3] = 0;
			InputEventCtrl.GetInstance().ClickDaoDanBtThree( ButtonState.UP );
		}
		anJianInfo = AnJianState[3] == 0 ? "弹起" : "按下";
		TestReadMsgA = "         导弹按键: "+anJianInfo;
		if (!IsCloseOutInfo) {
			GUI.Label(new Rect(startX, 10f + 10f * hVal, wVal, hVal), TestReadMsgA);
		}
	}
}