﻿using UnityEngine;
using System.Collections.Generic;

public class XKNpcSpawnListCtrl : MonoBehaviour
{
	public List<XKNpcSpawnListDt> NpcDtList = new List<XKNpcSpawnListDt>(10);
	static XKNpcSpawnListCtrl _Instance;
	public static XKNpcSpawnListCtrl GetInstance()
	{
		if (_Instance == null) {
			GameObject obj = new GameObject("_XKNpcSpawnListCtrl");
			_Instance = obj.AddComponent<XKNpcSpawnListCtrl>();
			Transform tran = obj.transform;
			tran.parent = XkGameCtrl.MissionCleanup;
            obj.SetActive(false);

        }
		return _Instance;
	}

	public GameObject GetNpcObjFromNpcDtList(XKSpawnNpcPoint spawnCom, GameObject npcPrefab, Vector3 pos, Quaternion rot)
	{
		if (npcPrefab == null)
        {
			return null;
		}

		GameObject npcObj = null;
		int max = NpcDtList.Count;
		if (max > 0) {
			for (int i = 0; i < max; i++) {
				if (NpcDtList[i] != null && NpcDtList[i].NpcPrefabName == npcPrefab.name) {
					npcObj = NpcDtList[i].FindNpcObjFromNpcList(spawnCom, npcPrefab);
					break;
				}
			}
		}

		if (npcObj == null) {
			//Debug.Log("Unity:"+"GetNpcObjFromNpcDtList -> npcPrefabName is "+npcPrefab.name);
			GameObject objNpcSpawnList = new GameObject("XKNpcSpawnListDt");
			XKNpcSpawnListDt npcSpawnList = objNpcSpawnList.AddComponent<XKNpcSpawnListDt>();
			HandleNpcDtList(npcSpawnList);
			Transform tran = objNpcSpawnList.transform;
			tran.parent = transform;
			npcObj = npcSpawnList.FindNpcObjFromNpcList(spawnCom, npcPrefab);
			if (npcObj == null) {
				HandleRemoveNpcDtList(npcSpawnList);
			}
		}

		if (npcObj != null) {
			//Transform npcTran = npcObj.transform;
			npcObj.transform.position = pos;
			npcObj.transform.rotation = rot;
			//Debug.Log("Unity:"+npcObj.name+", id "+npcObj.GetInstanceID()+", pos "+pos+", rot "+rot);
		}
		return npcObj;
	}
	
	void HandleNpcDtList(XKNpcSpawnListDt npcSpawnList)
	{
		if (npcSpawnList != null && NpcDtList.Contains(npcSpawnList)) {
			return;
		}
		NpcDtList.Add(npcSpawnList);
	}

	void HandleRemoveNpcDtList(XKNpcSpawnListDt npcSpawnList)
	{
		//Debug.Log("Unity:"+"HandleRemoveNpcDtList**************");
		if (npcSpawnList != null && !NpcDtList.Contains(npcSpawnList)) {
			return;
		}
		NpcDtList.Remove(npcSpawnList);
		Destroy(npcSpawnList.gameObject);
	}

	public void CheckNpcObjByNpcSpawnListDt(GameObject npcObj)
	{
		if (npcObj == null) {
			return;
		}

		string npcNameStr = "";
		int max = NpcDtList.Count;
		for (int i = 0; i < max; i++) {
			if (NpcDtList[i] == null) {
				continue;
			}

			npcNameStr = NpcDtList[i].NpcPrefabName + "(Clone)";
			if (npcNameStr == npcObj.name) {
				NpcDtList[i].CheckRemoveNpcSpawnListDt(npcObj);
				break;
			}
		}
	}

    /// <summary>
    /// 清理暂时不用的npc数据.
    /// </summary>
    public void CleanGameNoUsedNpcData()
    {
        int max = NpcDtList.Count;
        for (int i = 0; i < max; i++)
        {
            if (NpcDtList[i] != null)
            {
                NpcDtList[i].ClearNpcObj();
            }
        }
    }
}