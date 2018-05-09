using UnityEngine;
using System.Collections;

public class XkAmmoTieHuaCtrl : MonoBehaviour
{
	public Transform TieHuaTran;
	void Start()
	{
		if (TieHuaTran == null) {
			Debug.LogWarning("Unity:"+"TieHuaTran is null");
			GameObject obj = null;
			obj.name = "null";
		}
	}
}