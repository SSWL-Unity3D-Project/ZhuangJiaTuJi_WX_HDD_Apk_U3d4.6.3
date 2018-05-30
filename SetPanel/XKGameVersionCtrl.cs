using UnityEngine;

public class XKGameVersionCtrl : MonoBehaviour
{
	UILabel VersionLB;
	public static string GameVersion = "Version: V1.0_Apk_20180529";
	// Use this for initialization
	void Start()
	{
		VersionLB = GetComponent<UILabel>();
		VersionLB.text = GameVersion;
	}
}