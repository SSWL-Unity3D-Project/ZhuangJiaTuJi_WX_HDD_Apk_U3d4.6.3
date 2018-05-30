using UnityEngine;

public class XKPlayerTiaoBanCtrl : MonoBehaviour
{
	public XKPlayerMoveCtrl PlayerMoveScript;
    void Start()
    {
        gameObject.SetActive(false);
    }

	public void MovePlayerOverPaoWuXianByITween()
	{
		Debug.Log("Unity:"+"XKPlayerTiaoBanCtrl -> MovePlayerOverPaoWuXianByITween...");
		PlayerMoveScript.MovePlayerOverPaoWuXianByITween();
	}
}