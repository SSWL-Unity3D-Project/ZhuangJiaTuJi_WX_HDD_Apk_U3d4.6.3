using UnityEngine;
using System.Collections;

public class XKPlayerTiaoBanCtrl : MonoBehaviour
{
	public XKPlayerMoveCtrl PlayerMoveScript;
	public void MovePlayerOverPaoWuXianByITween()
	{
		Debug.Log("XKPlayerTiaoBanCtrl -> MovePlayerOverPaoWuXianByITween...");
		PlayerMoveScript.MovePlayerOverPaoWuXianByITween();
	}
}