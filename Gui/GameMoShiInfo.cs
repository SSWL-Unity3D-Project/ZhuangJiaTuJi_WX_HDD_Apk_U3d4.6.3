using UnityEngine;
using System.Collections;

public class GameMoShiInfo : MonoBehaviour {
	public GameMode AppModeVal = GameMode.Null;
	public static GameMoShiInfo InstanceDanJi;
	public static GameMoShiInfo InstanceLianJi;
	UISprite ModeSprite;
	void Start()
	{
		ModeSprite = GetComponent<UISprite>();
		switch (AppModeVal) {
		case GameMode.LianJi:
			InstanceLianJi = this;
			break;

		default:
			InstanceDanJi = this;
			break;
		}
	}

	public void SetTransformScale(Vector3 scaleVal)
	{
		transform.localScale = scaleVal;
		if (scaleVal != new Vector3(1f, 1f, 1f)) {
			switch (AppModeVal) {
			case GameMode.LianJi:
				InstanceDanJi.ModeSprite.spriteName = "DanRen_1";
				InstanceLianJi.ModeSprite.spriteName = "ShuangRen_2";
				break;
				
			default:
				InstanceDanJi.ModeSprite.spriteName = "DanRen_2";
				InstanceLianJi.ModeSprite.spriteName = "ShuangRen_1";
				break;
			}
		}
	}
}