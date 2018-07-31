using UnityEngine;

/// <summary>
/// 游戏测试信息.
/// </summary>
public class SSGameDebugMsg : MonoBehaviour
{
    public void AddMsg(string msg)
    {
        DebugMsg += " * " + msg + " *";
    }

    string DebugMsg = "DebugMsg:";
    void OnGUI()
    {
        Rect rt = new Rect(5f, 5f, Screen.width - 10f, 350f);
        GUI.Box(rt, "");
        GUI.Label(rt, DebugMsg);
    }
}