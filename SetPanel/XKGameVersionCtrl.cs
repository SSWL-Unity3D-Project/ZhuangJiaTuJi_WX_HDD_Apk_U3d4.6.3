using UnityEngine;
using System.Collections;

public class XKGameVersionCtrl : MonoBehaviour {
	UILabel VersionLB;
	public static string GameVersion = "Version: V1.4_COM_20170328";
	// Use this for initialization
	void Start()
	{
		VersionLB = GetComponent<UILabel>();
		VersionLB.text = GameVersion;
	}
}