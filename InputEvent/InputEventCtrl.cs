using UnityEngine;
using System.Collections;

public class InputEventCtrl : MonoBehaviour {
	public static bool IsClickFireBtOneDown;
	public static bool IsClickFireBtTwoDown;
	public static bool IsClickFireBtThreeDown;
	public static bool IsClickFireBtFourDown;
	/// <summary>
	/// 玩家方向信息.
	/// </summary>
	public static float[] PlayerFX = new float[4];
	/**
	 * PlayerFXTmp[0] == 1 -> 1P左按下.
	 * PlayerFXTmp[0] == 0 -> 1P左弹起.
	 * PlayerFXTmp[1] == 1 -> 1P右按下.
	 * PlayerFXTmp[1] == 0 -> 1P右弹起.
	 * ...
	 * ...
	 * PlayerFXTmp[6] == 1 -> 4P左按下.
	 * PlayerFXTmp[6] == 0 -> 4P左弹起.
	 * PlayerFXTmp[7] == 1 -> 4P右按下.
	 * PlayerFXTmp[7] == 0 -> 4P右弹起.
	 */
	static float[] PlayerFXTmp = new float[8];
	/// <summary>
	/// 玩家油门信息.
	/// </summary>
	public static float[] PlayerYM = new float[4];
	/**
	 * PlayerYMTmp[0] == 1 -> 1P上按下.
	 * PlayerYMTmp[0] == 0 -> 1P上弹起.
	 * PlayerYMTmp[1] == 1 -> 1P下按下.
	 * PlayerYMTmp[1] == 0 -> 1P下弹起.
	 * ...
	 * ...
	 * PlayerYMTmp[6] == 1 -> 4P上按下.
	 * PlayerYMTmp[6] == 0 -> 4P上弹起.
	 * PlayerYMTmp[7] == 1 -> 4P下按下.
	 * PlayerYMTmp[7] == 0 -> 4P下弹起.
	 */
	static float[] PlayerYMTmp = new float[8];
	/// <summary>
	/// 玩家刹车信息.
	/// </summary>
	public static float[] PlayerSC = new float[4];
	static private InputEventCtrl Instance = null;
	static public InputEventCtrl GetInstance()
	{
		if(Instance == null)
		{
			GameObject obj = new GameObject("_InputEventCtrl");
			Instance = obj.AddComponent<InputEventCtrl>();
			pcvr.GetInstance();
			XKGlobalData.GetInstance();
			SetPanelCtrl.GetInstance();
		}
		return Instance;
	}

	#region Click Button Envent
	public delegate void EventHandel(ButtonState val);
	public event EventHandel ClickStartBtOneEvent;
	public void ClickStartBtOne(ButtonState val)
	{
		if(ClickStartBtOneEvent != null)
		{
			ClickStartBtOneEvent( val );
			//pcvr.StartLightStateP1 = LedState.Mie;
		}
		pcvr.SetIsPlayerActivePcvr();
	}
	
	public event EventHandel ClickStartBtTwoEvent;
	public void ClickStartBtTwo(ButtonState val)
	{
		if(ClickStartBtTwoEvent != null)
		{
			ClickStartBtTwoEvent( val );
			//pcvr.StartLightStateP2 = LedState.Mie;
		}
		pcvr.SetIsPlayerActivePcvr();
	}
	
	public event EventHandel ClickStartBtThreeEvent;
	public void ClickStartBtThree(ButtonState val)
	{
		if(ClickStartBtThreeEvent != null)
		{
			ClickStartBtThreeEvent( val );
			//pcvr.StartLightStateP2 = LedState.Mie;
		}
		pcvr.SetIsPlayerActivePcvr();
	}
	
	public event EventHandel ClickStartBtFourEvent;
	public void ClickStartBtFour(ButtonState val)
	{
		if(ClickStartBtFourEvent != null)
		{
			ClickStartBtFourEvent( val );
			//pcvr.StartLightStateP2 = LedState.Mie;
		}
		pcvr.SetIsPlayerActivePcvr();
	}

	public event EventHandel ClickSetEnterBtEvent;
	public void ClickSetEnterBt(ButtonState val)
	{
		#if !UNITY_EDITOR
		SetEnterBtSt = val;
		#endif
		if(ClickSetEnterBtEvent != null)
		{
			ClickSetEnterBtEvent( val );
		}

		if (val == ButtonState.DOWN) {
			XKGlobalData.PlayAudioSetEnter();
			#if !UNITY_EDITOR
			TimeSetEnterMoveBt = Time.time;
			#endif
		}
		pcvr.SetIsPlayerActivePcvr();
	}
	
	public event EventHandel ClickSetMoveBtEvent;
	public void ClickSetMoveBt(ButtonState val)
	{
		if(ClickSetMoveBtEvent != null)
		{
			ClickSetMoveBtEvent( val );
		}

		if (val == ButtonState.DOWN) {
			XKGlobalData.PlayAudioSetMove();
		}
		pcvr.SetIsPlayerActivePcvr();
	}
	
	public event EventHandel ClickFireBtOneEvent;
	public void ClickFireBtOne(ButtonState val)
	{
		if(ClickFireBtOneEvent != null)
		{
			ClickFireBtOneEvent( val );
		}
		pcvr.SetIsPlayerActivePcvr();

		/*if (SetPanelUiRoot.GetInstance() == null
		    && !HardwareCheckCtrl.IsTestHardWare
		    && ClickStartBtOneEvent != null) {
			ClickStartBtOneEvent( val );
		}*/
	}

	public event EventHandel ClickFireBtTwoEvent;
	public void ClickFireBtTwo(ButtonState val)
	{
		if(ClickFireBtTwoEvent != null)
		{
			ClickFireBtTwoEvent( val );
		}
		pcvr.SetIsPlayerActivePcvr();

		/*if (SetPanelUiRoot.GetInstance() == null
		    && !HardwareCheckCtrl.IsTestHardWare
		    && ClickStartBtTwoEvent != null) {
			ClickStartBtTwoEvent( val );
		}*/
	}

	public event EventHandel ClickFireBtThreeEvent;
	public void ClickFireBtThree(ButtonState val)
	{
		if(ClickFireBtThreeEvent != null)
		{
			ClickFireBtThreeEvent( val );
		}
		pcvr.SetIsPlayerActivePcvr();
		
		/*if (SetPanelUiRoot.GetInstance() == null
		    && !HardwareCheckCtrl.IsTestHardWare
		    && ClickStartBtThreeEvent != null) {
			ClickStartBtThreeEvent( val );
		}*/
	}

	public event EventHandel ClickFireBtFourEvent;
	public void ClickFireBtFour(ButtonState val)
	{
		if(ClickFireBtFourEvent != null)
		{
			ClickFireBtFourEvent( val );
		}
		pcvr.SetIsPlayerActivePcvr();
		
		/*if (SetPanelUiRoot.GetInstance() == null
		    && !HardwareCheckCtrl.IsTestHardWare
		    && ClickStartBtFourEvent != null) {
			ClickStartBtFourEvent( val );
		}*/
	}

	public event EventHandel ClickDaoDanBtOneEvent;
	public void ClickDaoDanBtOne(ButtonState val)
	{
		if(ClickDaoDanBtOneEvent != null)
		{
			ClickDaoDanBtOneEvent( val );
		}
		pcvr.SetIsPlayerActivePcvr();

		if (val == ButtonState.DOWN) {
			pcvr.OpenZuoYiQiNang(PlayerEnum.PlayerOne);
		}

		if (SetPanelUiRoot.GetInstance() == null
		    && !HardwareCheckCtrl.IsTestHardWare
		    && ClickStartBtOneEvent != null) {
			ClickStartBtOneEvent( val );
		}
	}
	
	public event EventHandel ClickDaoDanBtTwoEvent;
	public void ClickDaoDanBtTwo(ButtonState val)
	{
		if(ClickDaoDanBtTwoEvent != null)
		{
			ClickDaoDanBtTwoEvent( val );
		}
		pcvr.SetIsPlayerActivePcvr();
		
		if (val == ButtonState.DOWN) {
			pcvr.OpenZuoYiQiNang(PlayerEnum.PlayerTwo);
		}
		
		if (SetPanelUiRoot.GetInstance() == null
		    && !HardwareCheckCtrl.IsTestHardWare
		    && ClickStartBtTwoEvent != null) {
			ClickStartBtTwoEvent( val );
		}
	}
	
	public event EventHandel ClickDaoDanBtThreeEvent;
	public void ClickDaoDanBtThree(ButtonState val)
	{
		if(ClickDaoDanBtThreeEvent != null)
		{
			ClickDaoDanBtThreeEvent( val );
		}
		pcvr.SetIsPlayerActivePcvr();
		
		if (val == ButtonState.DOWN) {
			pcvr.OpenZuoYiQiNang(PlayerEnum.PlayerThree);
		}
		
		if (SetPanelUiRoot.GetInstance() == null
		    && !HardwareCheckCtrl.IsTestHardWare
		    && ClickStartBtThreeEvent != null) {
			ClickStartBtThreeEvent( val );
		}
	}
	
	public event EventHandel ClickDaoDanBtFourEvent;
	public void ClickDaoDanBtFour(ButtonState val)
	{
		if(ClickDaoDanBtFourEvent != null)
		{
			ClickDaoDanBtFourEvent( val );
		}
		pcvr.SetIsPlayerActivePcvr();
		
		if (val == ButtonState.DOWN) {
			pcvr.OpenZuoYiQiNang(PlayerEnum.PlayerFour);
		}
		
		if (SetPanelUiRoot.GetInstance() == null
		    && !HardwareCheckCtrl.IsTestHardWare
		    && ClickStartBtFourEvent != null) {
			ClickStartBtFourEvent( val );
		}
	}

	public event EventHandel ClickStopDongGanBtOneEvent;
	public void ClickStopDongGanBtOne(ButtonState val)
	{
		if(ClickStopDongGanBtOneEvent != null)
		{
			ClickStopDongGanBtOneEvent( val );
		}

		if (val == ButtonState.DOWN) {
			DongGanUICtrl.ShowDongGanInfo(PlayerEnum.PlayerOne);
		}
		pcvr.SetIsPlayerActivePcvr();
	}
	
	public event EventHandel ClickStopDongGanBtTwoEvent;
	public void ClickStopDongGanBtTwo(ButtonState val)
	{
		if(ClickStopDongGanBtTwoEvent != null)
		{
			ClickStopDongGanBtTwoEvent( val );
		}
		
		if (val == ButtonState.DOWN) {
			DongGanUICtrl.ShowDongGanInfo(PlayerEnum.PlayerTwo);
		}
		pcvr.SetIsPlayerActivePcvr();
	}
	
	public event EventHandel ClickStopDongGanBtThreeEvent;
	public void ClickStopDongGanBtThree(ButtonState val)
	{
		if(ClickStopDongGanBtThreeEvent != null)
		{
			ClickStopDongGanBtThreeEvent( val );
		}
		
		if (val == ButtonState.DOWN) {
			DongGanUICtrl.ShowDongGanInfo(PlayerEnum.PlayerThree);
		}
		pcvr.SetIsPlayerActivePcvr();
	}
	
	public event EventHandel ClickStopDongGanBtFourEvent;
	public void ClickStopDongGanBtFour(ButtonState val)
	{
		if(ClickStopDongGanBtFourEvent != null)
		{
			ClickStopDongGanBtFourEvent( val );
		}
		
		if (val == ButtonState.DOWN) {
			DongGanUICtrl.ShowDongGanInfo(PlayerEnum.PlayerFour);
		}
		pcvr.SetIsPlayerActivePcvr();
	}
	/**
	 * 方向左响应P1.
	 */
	public void ClickFangXiangLBtP1(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerFXTmp[0] = 1f;
			PlayerFX[0] = -1f;
			break;
		case ButtonState.UP:
			PlayerFXTmp[0] = 0f;
			PlayerFX[0] = PlayerFXTmp[1] == 0f ? 0f : 1f;
			break;
		}
	}
	/**
	 * 方向右响应P1.
	 */
	public void ClickFangXiangRBtP1(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerFXTmp[1] = 1f;
			PlayerFX[0] = 1f;
			break;
		case ButtonState.UP:
			PlayerFXTmp[1] = 0f;
			PlayerFX[0] = PlayerFXTmp[0] == 0f ? 0f : -1f;
			break;
		}
	}
	/**
	 * 方向上响应P1.
	 */
	public void ClickFangXiangUBtP1(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerYMTmp[0] = 1f;
			PlayerYM[0] = 1f;
			break;
		case ButtonState.UP:
			PlayerYMTmp[0] = 0f;
			PlayerYM[0] = PlayerYMTmp[1] == 0f ? 0f : -1f;
			break;
		}
	}
	/**
	 * 方向下响应P1.
	 */
	public void ClickFangXiangDBtP1(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerYMTmp[1] = 1f;
			PlayerYM[0] = -1f;
			break;
		case ButtonState.UP:
			PlayerYMTmp[1] = 0f;
			PlayerYM[0] = PlayerYMTmp[0] == 0f ? 0f : 1f;
			break;
		}
	}
	/**
	 * 方向左响应P2.
	 */
	public void ClickFangXiangLBtP2(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerFXTmp[2] = 1f;
			PlayerFX[1] = -1f;
			break;
		case ButtonState.UP:
			PlayerFXTmp[2] = 0f;
			PlayerFX[1] = PlayerFXTmp[3] == 0f ? 0f : 1f;
			break;
		}
	}
	/**
	 * 方向右响应P2.
	 */
	public void ClickFangXiangRBtP2(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerFXTmp[3] = 1f;
			PlayerFX[1] = 1f;
			break;
		case ButtonState.UP:
			PlayerFXTmp[3] = 0f;
			PlayerFX[1] = PlayerFXTmp[2] == 0f ? 0f : -1f;
			break;
		}
	}
	/**
	 * 方向上响应P2.
	 */
	public void ClickFangXiangUBtP2(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerYMTmp[2] = 1f;
			PlayerYM[1] = 1f;
			break;
		case ButtonState.UP:
			PlayerYMTmp[2] = 0f;
			PlayerYM[1] = PlayerYMTmp[3] == 0f ? 0f : -1f;
			break;
		}
	}
	/**
	 * 方向下响应P2.
	 */
	public void ClickFangXiangDBtP2(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerYMTmp[3] = 1f;
			PlayerYM[1] = -1f;
			break;
		case ButtonState.UP:
			PlayerYMTmp[3] = 0f;
			PlayerYM[1] = PlayerYMTmp[2] == 0f ? 0f : 1f;
			break;
		}
	}
	/**
	 * 方向左响应P3.
	 */
	public void ClickFangXiangLBtP3(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerFXTmp[4] = 1f;
			PlayerFX[2] = -1f;
			break;
		case ButtonState.UP:
			PlayerFXTmp[4] = 0f;
			PlayerFX[2] = PlayerFXTmp[5] == 0f ? 0f : 1f;
			break;
		}
	}
	/**
	 * 方向右响应P3.
	 */
	public void ClickFangXiangRBtP3(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerFXTmp[5] = 1f;
			PlayerFX[2] = 1f;
			break;
		case ButtonState.UP:
			PlayerFXTmp[5] = 0f;
			PlayerFX[2] = PlayerFXTmp[4] == 0f ? 0f : -1f;
			break;
		}
	}
	/**
	 * 方向上响应P3.
	 */
	public void ClickFangXiangUBtP3(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerYMTmp[4] = 1f;
			PlayerYM[2] = 1f;
			break;
		case ButtonState.UP:
			PlayerYMTmp[4] = 0f;
			PlayerYM[2] = PlayerYMTmp[5] == 0f ? 0f : -1f;
			break;
		}
	}
	/**
	 * 方向下响应P3.
	 */
	public void ClickFangXiangDBtP3(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerYMTmp[5] = 1f;
			PlayerYM[2] = -1f;
			break;
		case ButtonState.UP:
			PlayerYMTmp[5] = 0f;
			PlayerYM[2] = PlayerYMTmp[4] == 0f ? 0f : 1f;
			break;
		}
	}
	/**
	 * 方向左响应P4.
	 */
	public void ClickFangXiangLBtP4(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerFXTmp[6] = 1;
			PlayerFX[3] = -1f;
			break;
		case ButtonState.UP:
			PlayerFXTmp[6] = 0f;
			PlayerFX[3] = PlayerFXTmp[7] == 0f ? 0f : 1f;
			break;
		}
	}
	/**
	 * 方向右响应P4.
	 */
	public void ClickFangXiangRBtP4(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerFXTmp[7] = 1f;
			PlayerFX[3] = 1f;
			break;
		case ButtonState.UP:
			PlayerFXTmp[7] = 0f;
			PlayerFX[3] = PlayerFXTmp[6] == 0f ? 0f : -1f;
			break;
		}
	}
	/**
	 * 方向上响应P4.
	 */
	public void ClickFangXiangUBtP4(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerYMTmp[6] = 1f;
			PlayerYM[3] = 1f;
			break;
		case ButtonState.UP:
			PlayerYMTmp[6] = 0f;
			PlayerYM[3] = PlayerYMTmp[7] == 0f ? 0f : -1f;
			break;
		}
	}
	/**
	 * 方向下响应P4.
	 */
	public void ClickFangXiangDBtP4(ButtonState val)
	{
		switch (val) {
		case ButtonState.DOWN:
			PlayerYMTmp[7] = 1f;
			PlayerYM[3] = -1f;
			break;
		case ButtonState.UP:
			PlayerYMTmp[7] = 0f;
			PlayerYM[3] = PlayerYMTmp[6] == 0f ? 0f : 1f;
			break;
		}
	}
	#endregion
	
	#if !UNITY_EDITOR
	float TimeSetEnterMoveBt;
	ButtonState SetEnterBtSt = ButtonState.UP;
	#endif

	void Update()
	{
		#if !UNITY_EDITOR
		if (SetEnterBtSt == ButtonState.DOWN && Time.time - TimeSetEnterMoveBt > 2f) {
			HardwareCheckCtrl.OnRestartGame();
		}
		#endif

		if (pcvr.bIsHardWare && !TestTanKCom.IsTestTankCom && !pcvr.IsTestInput) {
			return;
		}

		if (Input.GetKeyUp(KeyCode.T)) {
			int coinVal = XKGlobalData.CoinPlayerOne + 1;
			XKGlobalData.SetCoinPlayerOne(coinVal);
		}

		if (Input.GetKeyUp(KeyCode.Y)) {
			int coinVal = XKGlobalData.CoinPlayerTwo + 1;
			XKGlobalData.SetCoinPlayerTwo(coinVal);
		}
		
		if (Input.GetKeyUp(KeyCode.U)) {
			int coinVal = XKGlobalData.CoinPlayerThree + 1;
			XKGlobalData.SetCoinPlayerThree(coinVal);
		}
		
		if (Input.GetKeyUp(KeyCode.I)) {
			int coinVal = XKGlobalData.CoinPlayerFour + 1;
			XKGlobalData.SetCoinPlayerFour(coinVal);
		}

		//StartBt PlayerOne
		if (Input.GetKeyUp(KeyCode.G)) {
			ClickStartBtOne( ButtonState.UP );
		}
		
		if (Input.GetKeyDown(KeyCode.G)) {
			ClickStartBtOne( ButtonState.DOWN );
		}
		
		//StartBt PlayerTwo
		if (Input.GetKeyUp(KeyCode.H)) {
			ClickStartBtTwo( ButtonState.UP );
		}
		
		if (Input.GetKeyDown(KeyCode.H)) {
			ClickStartBtTwo( ButtonState.DOWN );
		}
		
		//StartBt PlayerThree
		if (Input.GetKeyUp(KeyCode.J)) {
			ClickStartBtThree( ButtonState.UP );
		}
		
		if (Input.GetKeyDown(KeyCode.J)) {
			ClickStartBtThree( ButtonState.DOWN );
		}
		
		//StartBt PlayerFour
		if (Input.GetKeyUp(KeyCode.K)) {
			ClickStartBtFour( ButtonState.UP );
		}
		
		if (Input.GetKeyDown(KeyCode.K)) {
			ClickStartBtFour( ButtonState.DOWN );
		}

		//player_1.
		if (Input.GetKeyDown(KeyCode.A)) {
			ClickFangXiangLBtP1(ButtonState.DOWN);
		}
		
		if (Input.GetKeyUp(KeyCode.A)) {
			ClickFangXiangLBtP1(ButtonState.UP);
		}
		
		if (Input.GetKeyDown(KeyCode.D)) {
			ClickFangXiangRBtP1(ButtonState.DOWN);
		}
		
		if (Input.GetKeyUp(KeyCode.D)) {
			ClickFangXiangRBtP1(ButtonState.UP);
		}

		if (Input.GetKeyDown(KeyCode.W)) {
			ClickFangXiangUBtP1(ButtonState.DOWN);
		}
		
		if (Input.GetKeyUp(KeyCode.W)) {
			ClickFangXiangUBtP1(ButtonState.UP);
		}
		
		if (Input.GetKeyDown(KeyCode.S)) {
			ClickFangXiangDBtP1(ButtonState.DOWN);
		}
		
		if (Input.GetKeyUp(KeyCode.S)) {
			ClickFangXiangDBtP1(ButtonState.UP);
		}

		//player_2.
		if (!TestTanKCom.IsTestTankCom) {
			if (Input.GetKeyDown(KeyCode.F)) {
				ClickFangXiangLBtP2(ButtonState.DOWN);
			}
			
			if (Input.GetKeyUp(KeyCode.F)) {
				ClickFangXiangLBtP2(ButtonState.UP);
			}
			
			if (Input.GetKeyDown(KeyCode.H)) {
				ClickFangXiangRBtP2(ButtonState.DOWN);
			}
			
			if (Input.GetKeyUp(KeyCode.H)) {
				ClickFangXiangRBtP2(ButtonState.UP);
			}
			
			if (Input.GetKeyDown(KeyCode.T)) {
				ClickFangXiangUBtP2(ButtonState.DOWN);
			}
			
			if (Input.GetKeyUp(KeyCode.T)) {
				ClickFangXiangUBtP2(ButtonState.UP);
			}
			
			if (Input.GetKeyDown(KeyCode.G)) {
				ClickFangXiangDBtP2(ButtonState.DOWN);
			}
			
			if (Input.GetKeyUp(KeyCode.G)) {
				ClickFangXiangDBtP2(ButtonState.UP);
			}
		}
		else {
			PlayerYM[1] = TestTanKCom.YouMenStateP2;
			PlayerFX[1] = TestTanKCom.FangXiangStateP2;
		}
		
		//player_3.
		if (!TestTanKCom.IsTestTankCom) {
			if (Input.GetKeyDown(KeyCode.J)) {
				ClickFangXiangLBtP3(ButtonState.DOWN);
			}
			
			if (Input.GetKeyUp(KeyCode.J)) {
				ClickFangXiangLBtP3(ButtonState.UP);
			}
			
			if (Input.GetKeyDown(KeyCode.L)) {
				ClickFangXiangRBtP3(ButtonState.DOWN);
			}
			
			if (Input.GetKeyUp(KeyCode.L)) {
				ClickFangXiangRBtP3(ButtonState.UP);
			}
			
			if (Input.GetKeyDown(KeyCode.I)) {
				ClickFangXiangUBtP3(ButtonState.DOWN);
			}
			
			if (Input.GetKeyUp(KeyCode.I)) {
				ClickFangXiangUBtP3(ButtonState.UP);
			}
			
			if (Input.GetKeyDown(KeyCode.K)) {
				ClickFangXiangDBtP3(ButtonState.DOWN);
			}
			
			if (Input.GetKeyUp(KeyCode.K)) {
				ClickFangXiangDBtP3(ButtonState.UP);
			}
		}
		else {
			PlayerFX[2] = TestTanKCom.FangXiangStateP3;
			PlayerYM[2] = TestTanKCom.YouMenStateP3;
		}
		
		//player_4.
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			ClickFangXiangLBtP4(ButtonState.DOWN);
		}
		
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			ClickFangXiangLBtP4(ButtonState.UP);
		}
		
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			ClickFangXiangRBtP4(ButtonState.DOWN);
		}
		
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			ClickFangXiangRBtP4(ButtonState.UP);
		}
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			ClickFangXiangUBtP4(ButtonState.DOWN);
		}
		
		if (Input.GetKeyUp(KeyCode.UpArrow)) {
			ClickFangXiangUBtP4(ButtonState.UP);
		}

		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			ClickFangXiangDBtP4(ButtonState.DOWN);
		}
		
		if (Input.GetKeyUp(KeyCode.DownArrow)) {
			ClickFangXiangDBtP4(ButtonState.UP);
		}

		//setPanel enter button
		if (Input.GetKeyUp(KeyCode.F4)) {
			ClickSetEnterBt( ButtonState.UP );
		}
		
		if (Input.GetKeyDown(KeyCode.F4)) {
			ClickSetEnterBt( ButtonState.DOWN );
		}
		
		//setPanel move button
		if (Input.GetKeyUp(KeyCode.F5)) {
			ClickSetMoveBt( ButtonState.UP );
		}
		
		if (Input.GetKeyDown(KeyCode.F5)) {
			ClickSetMoveBt( ButtonState.DOWN );
		}

		//Fire button
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			IsClickFireBtOneDown = false;
			IsClickFireBtTwoDown = false;
			IsClickFireBtThreeDown = false;
			IsClickFireBtFourDown = false;
			ClickFireBtOne( ButtonState.UP );
			if (!TestTanKCom.IsTestTankCom) {
				ClickFireBtTwo( ButtonState.UP );
				ClickFireBtThree( ButtonState.UP );
			}
			ClickFireBtFour( ButtonState.UP );
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			IsClickFireBtOneDown = true;
			IsClickFireBtTwoDown = true;
			IsClickFireBtThreeDown = true;
			IsClickFireBtFourDown = true;
			ClickFireBtOne( ButtonState.DOWN );
			if (!TestTanKCom.IsTestTankCom) {
				ClickFireBtTwo( ButtonState.DOWN );
				ClickFireBtThree( ButtonState.DOWN );
			}
			ClickFireBtFour( ButtonState.DOWN );
		}

		if (Input.GetKeyUp(KeyCode.Mouse1)) {
			ClickDaoDanBtOne( ButtonState.UP );
			if (!TestTanKCom.IsTestTankCom) {
				ClickDaoDanBtTwo( ButtonState.UP );
				ClickDaoDanBtThree( ButtonState.UP );
			}
			ClickDaoDanBtFour( ButtonState.UP );
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse1)) {
			ClickDaoDanBtOne( ButtonState.DOWN );
			if (!TestTanKCom.IsTestTankCom) {
				ClickDaoDanBtTwo( ButtonState.DOWN );
				ClickDaoDanBtThree( ButtonState.DOWN );
			}
			ClickDaoDanBtFour( ButtonState.DOWN );
		}

		if (Input.GetKeyDown(KeyCode.C)) {
			ClickStopDongGanBtOne(ButtonState.DOWN);
			ClickStopDongGanBtTwo(ButtonState.DOWN);
			ClickStopDongGanBtThree(ButtonState.DOWN);
			ClickStopDongGanBtFour(ButtonState.DOWN);
		}
		
		if (Input.GetKeyUp(KeyCode.C)) {
			ClickStopDongGanBtOne(ButtonState.UP);
			ClickStopDongGanBtTwo(ButtonState.UP);
			ClickStopDongGanBtThree(ButtonState.UP);
			ClickStopDongGanBtFour(ButtonState.UP);
		}
	}
}

public enum ButtonState : int
{
	UP = 1,
	DOWN = -1
}