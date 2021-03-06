﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XKNpcSpawnDaoJu : MonoBehaviour
{
	public bool IsSpawnDJ = true;
	public GameObject[] DaoJuArray;
	public int[] DaoJuGaiLv;
	public void SpawnAllDaoJu()
	{
		if (!IsSpawnDJ) {
			return;
		}
		PointList = new List<Transform>();
		CheckDaoJuSpawnPointList();

		int randVal = 0;
		int max = DaoJuArray.Length;
		Transform trEndPoint = null;
		for(int i = 0; i < max; i++) {
			randVal = Random.Range(0, 10000) % 100;
			if (randVal >= DaoJuGaiLv[i]) {
				continue;
			}

			trEndPoint = GetDaoJuSpawnPoint(i);
			if (trEndPoint == null) {
				continue;
			}
			GameObject daoJuObj = (GameObject)Instantiate(DaoJuArray[i], transform.position, transform.rotation);
			BuJiBaoCtrl buJiScript = daoJuObj.GetComponent<BuJiBaoCtrl>();
			buJiScript.SetIsSpawnDaoJu();
			buJiScript.MoveDaoJuToPoint(trEndPoint);
		}
	}

	List<Transform> PointList;
	Transform GetDaoJuSpawnPoint(int indexVal)
	{
		int max = PointList.Count;
		if (max <= 0) {
			return null;
		}

		int indexValTmp = indexVal % max;
		return PointList[indexValTmp];
	}

	[Range(0.01f, 100f)]public float DisDaoJuVal = 15f;
	void CheckDaoJuSpawnPointList()
	{
		Transform[] trPointArray = XKPlayerCamera.GetInstanceFeiJi().GetDaoJuSpawnPoint();
		int max = trPointArray.Length;
		if (max <= 0) {
			return;
		}

		float disVal = DisDaoJuVal;
		Vector3 posA = transform.position;
		Vector3 posB = Vector3.zero;
		posA.y = 0f;
		for (int i = 0; i < max; i++) {
			posB = trPointArray[i].position;
			posB.y = 0f;
			if (Vector3.Distance(posA, posB) <= disVal && Vector3.Distance(posA, posB) > 1f) {
				PointList.Add(trPointArray[i]);
			}
		}
	}
}