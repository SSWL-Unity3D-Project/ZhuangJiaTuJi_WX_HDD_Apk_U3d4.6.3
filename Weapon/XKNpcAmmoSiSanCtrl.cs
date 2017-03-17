using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XKNpcAmmoSiSanCtrl : MonoBehaviour
{
	public GameObject AmmoSanDan;
	[Range(0f, 100f)]public float OffsetPY = 0f;
	Vector3[] DirVecArray = new Vector3[4]{
		Vector3.right,
		-Vector3.right,
		Vector3.forward,
		-Vector3.forward
	};

	public void SpawnNpcAmmo()
	{
		for (int i = 0; i < 4; i++) {
			GameObject obj = GetNpcAmmoFromList(transform);
			NpcAmmoCtrl AmmoScript = obj.GetComponent<NpcAmmoCtrl>();
			Transform tran = obj.transform;
			tran.parent = XkGameCtrl.NpcAmmoArray;
			tran.forward = DirVecArray[i];
			tran.localPosition += new Vector3(0f, OffsetPY, 0f);
			if (AmmoScript != null) {
				AmmoScript.SetIsDestoryNpcAmmo();
				AmmoScript.SetIsAimFeiJiPlayer(false);
			}
		}
	}

	GameObject GetNpcAmmoFromList(Transform spawnPoint)
	{
		if (spawnPoint == null) {
			return null;
		}
		
		GameObject objAmmo = (GameObject)Instantiate(AmmoSanDan, spawnPoint.position, spawnPoint.rotation);
		if (objAmmo != null) {
			Transform tranAmmo = objAmmo.transform;
			tranAmmo.position = spawnPoint.position;
			tranAmmo.rotation = spawnPoint.rotation;
		}
		return objAmmo;
	}
}