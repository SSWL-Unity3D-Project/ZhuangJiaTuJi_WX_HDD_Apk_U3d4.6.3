using UnityEngine;

public class XKGameVersionCtrl : MonoBehaviour
{
    UILabel VersionLB;
    static string _GameVersion = "Version: V1.0_20180605";
    public static string GameVersion
    {
        get
        {
            string val = "";
#if UNITY_ANDROID
            val = _GameVersion + "_Apk";
#endif
#if UNITY_STANDALONE_WIN
            val = _GameVersion + "_Win";
#endif
            return val;
        }
    }

	// Use this for initialization
	void Start()
	{
        VersionLB = GetComponent<UILabel>();
        if (VersionLB != null)
        {
            VersionLB.text = GameVersion;
        }
    }
}