﻿#define USE_SPHERE_HIT
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAmmoCtrl : MonoBehaviour {
	[Range(0, 100)]public int AmmoIndex = 0;
	public PlayerAmmoType AmmoType = PlayerAmmoType.PuTongAmmo;
	/**
	 * 迫击炮的内核.
	 */
	public GameObject AmmoCore;
	/**
	 * 迫击炮的提示特效.
	 */
	public GameObject PaiJiPaoTiShiPrefab;
	public GameObject AmmoExplode;
	[Range(1, 1000)] public int DamageNpc = 1;
	[Range(1f, 4000f)] public float MvSpeed = 50f;
	const float MvSpeedMax = 500f;
	[Range(1f, 1000f)] public float AmmoDamageDis = 50f;
	[Range(0.001f, 100f)] public float LiveTime = 4f;
	public GameObject MetalParticle;		//金属.
	public GameObject ConcreteParticle;		//混凝土.
	public GameObject DirtParticle;			//土地.
	public GameObject WoodParticle;			//树木.
	public GameObject WaterParticle;		//水.
	public GameObject SandParticle;			//沙滩.
	public GameObject GlassParticle;		//玻璃.
	GameObject ObjAmmo;
	Transform AmmoTran;
	PlayerEnum PlayerState = PlayerEnum.Null;
	public static LayerMask PlayerAmmoHitLayer;
	public static LayerMask NpcCollisionLayer;
	//Vector3 AmmoStartPos;
	//Vector3 AmmoEndPos;
	bool IsHandleRpc;
	bool IsDestroyAmmo;
	TrailRenderer TrailScript;
	float TrailTime = 3f;
	float MaxDisVal;
	float CosABMin = Mathf.Cos(Mathf.PI*(60f/180f));
	void Awake()
	{
		TrailScript = GetComponentInChildren<TrailRenderer>();
		if (TrailScript != null) {
			TrailScript.castShadows = false;
			TrailScript.receiveShadows = false;
			TrailTime = TrailScript.time;
		}

		AmmoTran = transform;
		ObjAmmo = gameObject;
		AmmoTran.parent = XkGameCtrl.PlayerAmmoArray;
		MaxDisVal = MvSpeed * LiveTime;
	}
	
	void Update()
	{
		if (!IsHandleRpc) {
			return;
		}

		if (IsDestroyAmmo || IsChuanJiaDanHitCamColForward) {
			return;
		}
		CheckPlayerAmmoForwardHitNpc();
	}

	List<XKNpcHealthCtrl> NpcHealthList;
	bool CheckAmmoHitObj(GameObject hitObjNpc, PlayerEnum playerIndex)
	{
//		BuJiBaoCtrl buJiBaoScript = hitObjNpc.GetComponent<BuJiBaoCtrl>();
//		if (buJiBaoScript != null) {
//			buJiBaoScript.RemoveBuJiBao(playerIndex); //buJiBaoScript
//			return;
//		}

		bool isStopCheckHit = false;
		if (AmmoType != PlayerAmmoType.PaiJiPaoAmmo) {
			XKPlayerCheckCamera checkCam = hitObjNpc.GetComponent<XKPlayerCheckCamera>();
			if (checkCam != null) {
				MoveAmmoOnCompelteITween();
				return true;
			}
		}

		XKNpcHealthCtrl healthScript = hitObjNpc.GetComponent<XKNpcHealthCtrl>();
		if (healthScript != null && !healthScript.GetIsDeathNpc()) {
			/*Debug.Log("Unity:"+"CheckAmmoHitObj -> OnDamageNpc: "
			          +"AmmoType "+AmmoType
			          +", AmmoName "+AmmoTran.name
			          +", NpcName "+healthScript.GetNpcName()
			          +", AmmoDamageDis "+AmmoDamageDis);*/
			bool isHitNpc = false;
			switch (AmmoType) {
			case PlayerAmmoType.ChuanTouAmmo:
			case PlayerAmmoType.PaiJiPaoAmmo:
				if (NpcHealthList == null) {
					NpcHealthList = new List<XKNpcHealthCtrl>();
				}
				
				if (!NpcHealthList.Contains(healthScript)) {
					NpcHealthList.Add(healthScript);
					isHitNpc = true;
				}
				break;
			default:
				MoveAmmoOnCompelteITween();
				isStopCheckHit = true;
				isHitNpc = true;
				break;
			}

			if (isHitNpc) {
				healthScript.OnDamageNpc(DamageNpc, PlayerState, AmmoType);
				SpawnAmmoParticleObj();
			}
		}

		if (hitObjNpc != null) {
			NpcAmmoCtrl npcAmmoScript = hitObjNpc.GetComponent<NpcAmmoCtrl>();
			if (npcAmmoScript != null) {
				npcAmmoScript.MoveAmmoOnCompelteITween();
			}

			if (AmmoType == PlayerAmmoType.DaoDanAmmo
			    || AmmoType == PlayerAmmoType.GaoBaoAmmo
			    || AmmoType == PlayerAmmoType.PuTongAmmo
			    || AmmoType == PlayerAmmoType.SanDanAmmo) {
				if (hitObjNpc.layer != LayerMask.NameToLayer("Default")) {
					MoveAmmoOnCompelteITween();
					isStopCheckHit = true;
				}
			}
		}
		return isStopCheckHit;
	}

	public void StartMoveAmmo(Vector3 firePos, PlayerEnum playerIndex,
	                          NpcPathCtrl ammoMovePath = null, GameObject hitObjNpc = null)
	{
		float disTmp = Vector3.Distance(firePos, AmmoTran.position);
		Vector3 vecA = firePos - AmmoTran.position;
		if (disTmp < 10f
			|| disTmp > MaxDisVal
		    || AmmoType == PlayerAmmoType.PaiJiPaoAmmo) {
			firePos = AmmoTran.position + (vecA.normalized * MaxDisVal);
			//Debug.Log("Unity:"+"StartMoveAmmo::fix firePos -> "+"disTmp "+disTmp+", disMax "+MaxDisVal);
		}
		IsChuanJiaDanHitCamColForward = false;

		ObjAmmo = gameObject;
		if (!ObjAmmo.activeSelf) {
			ObjAmmo.SetActive(true);
			IsDestroyAmmo = false;
		}

		AmmoTran = transform;
		PlayerState = playerIndex;
		MoveAmmoByItween(firePos, ammoMovePath);
		IsHandleRpc = true;
	}

	void ResetTrailScriptInfo()
	{
		gameObject.SetActive(false);
		if (TrailScript == null) {
			return;
		}
		TrailScript.time = TrailTime;
	}

	Vector3 GetGenZongDanFirePos()
	{
		Vector3 vecA = AmmoTran.forward;
		Vector3 vecB = Vector3.forward;
		Vector3 posA = AmmoTran.position;
		Vector3 posB = Vector3.zero;
		Vector3 firePos = Vector3.zero;

		Transform[] npcArray = XkGameCtrl.GetInstance().GetNpcTranList().ToArray();
		int max = npcArray.Length;
		for (int i = 0; i < max; i++) {
			if (npcArray[i] == null) {
				continue;
			}
			
			posB = npcArray[i].position;
			vecA.y = posA.y = posB.y = 0f;
			vecB = posB - posA;
			if (Vector3.Dot(vecA, vecB.normalized) < CosABMin || vecB.magnitude > 50f) {
				continue;
			}
			firePos = npcArray[i].position;
			break;
		}
		return firePos;
	}

	GameObject PaiJiPaoTiShi;
	void MoveAmmoByItween(Vector3 firePos, NpcPathCtrl ammoMovePath)
	{
		if (ammoMovePath == null) {
			Vector3[] posArray = new Vector3[2];
			posArray[0] = AmmoTran.position;
			float lobTime = Vector3.Distance(firePos, posArray[0]) / MvSpeed;
			if (AmmoType == PlayerAmmoType.GenZongAmmo
			    || AmmoType == PlayerAmmoType.PaiJiPaoAmmo) {

				Vector3 posTmp = AmmoType == PlayerAmmoType.GenZongAmmo ? GetGenZongDanFirePos() : firePos;
				if (posTmp != Vector3.zero) {
					firePos = posTmp;
				}

				if (AmmoType == PlayerAmmoType.PaiJiPaoAmmo) {
					RaycastHit hit;
					Vector3 posA = firePos + Vector3.up * 50f;
					Physics.Raycast(posA, Vector3.down, out hit, 500f, XkGameCtrl.GetInstance().LandLayer);
					if (hit.collider != null) {
						firePos = hit.point;
					}
				}
				
				if (AmmoType == PlayerAmmoType.PaiJiPaoAmmo && PaiJiPaoTiShiPrefab != null) {
					PaiJiPaoTiShi = (GameObject)Instantiate(PaiJiPaoTiShiPrefab, firePos, Quaternion.identity);
					if (XkGameCtrl.NpcAmmoArray != null) {
						PaiJiPaoTiShi.transform.parent = XkGameCtrl.NpcAmmoArray;
					}
				}

				float disMV = Vector3.Distance(firePos , posArray[0]);
				lobTime = disMV / MvSpeed;
				float lobHeight = disMV * XKPlayerGlobalDt.GetInstance().KeyPaiJiPaoValPlayer;
				//lobHeight = lobHeight > 10f ? 10f : lobHeight;
				AmmoCore.transform.localPosition = Vector3.zero;
				AmmoCore.transform.localEulerAngles = Vector3.zero;
				iTween.MoveBy(AmmoCore, iTween.Hash("y", lobHeight,
				                                    "time", lobTime/2,
				                                    "easeType", iTween.EaseType.easeOutQuad));
				iTween.MoveBy(AmmoCore, iTween.Hash("y", -lobHeight,
				                                    "time", lobTime/2,
				                                    "delay", lobTime/2,
				                                    "easeType", iTween.EaseType.easeInCubic));     
				//iTween.FadeTo(gameObject, iTween.Hash("delay", 3, "time", .5, "alpha", 0, "onComplete", "CleanUp"));
			}
			posArray[1] = firePos;
			//AmmoStartPos = AmmoTran.position;
			iTween.MoveTo(gameObject, iTween.Hash("position", firePos,
			                                      "time", lobTime,
			                                      "easeType", iTween.EaseType.linear,
			                                      "oncomplete", "MoveAmmoOnCompelteITween"));
		}
		else {
			int countMark = ammoMovePath.transform.childCount;
			Transform[] tranArray = ammoMovePath.transform.GetComponentsInChildren<Transform>();
			List<Transform> nodesTran = new List<Transform>(tranArray){};
			nodesTran.Remove(ammoMovePath.transform);
			transform.position = nodesTran[0].position;
			transform.rotation = nodesTran[0].rotation;
			firePos = nodesTran[countMark-1].position;
			//AmmoStartPos = nodesTran[countMark-2].position;
			iTween.MoveTo(ObjAmmo, iTween.Hash("path", nodesTran.ToArray(),
			                                   "speed", MvSpeed,
			                                   "orienttopath", true,
			                                   "easeType", iTween.EaseType.linear,
			                                   "oncomplete", "MoveAmmoOnCompelteITween"));
		}
		//AmmoEndPos = firePos;
	}

	void SpawnAmmoParticleObj()
	{
        if (AmmoType != PlayerAmmoType.PaiJiPaoAmmo)
        {
            //处理迫击炮子弹外,其它子弹均需检测是否产生爆炸粒子.
            if (XkGameCtrl.IsNoAmmoBaoZhaLiZi)
            {
                return;
            }
        }

		#if USE_SPHERE_HIT
		GameObject objParticle = null;
		GameObject hitObj = CheckPlayerAmmoOverlapSphereHit(1);
		if (hitObj == null) {
			switch (AmmoType) {
			case PlayerAmmoType.PuTongAmmo:
				break;
			default:
				objParticle = AmmoExplode;
				break;
			}
		}
		else {
			string tagHitObj = hitObj.tag;
			switch (AmmoType) {
			case PlayerAmmoType.PuTongAmmo:
				XKAmmoParticleCtrl ammoParticleScript = hitObj.GetComponent<XKAmmoParticleCtrl>();
				if (ammoParticleScript != null && ammoParticleScript.PuTongAmmoLZ != null) {
					objParticle = ammoParticleScript.PuTongAmmoLZ;
				}
				else {
					switch (tagHitObj) {
					case "metal":
						objParticle = MetalParticle;
						break;
						
					case "concrete":
						objParticle = ConcreteParticle;
						break;
						
					case "dirt":
						objParticle = DirtParticle;
						break;
						
					case "wood":
						objParticle = WoodParticle;
						break;
						
					case "water":
						objParticle = WaterParticle;
						break;
						
					case "sand":
						objParticle = SandParticle;
						break;
						
					case "glass":
						objParticle = GlassParticle;
						break;
					default:
						objParticle = AmmoExplode;
						break;
					}
				}
				break;
			default:
				switch (tagHitObj) {
				case "dirt":
					objParticle = DirtParticle;
					break;
				case "water":
					objParticle = WaterParticle;
					break;
				default:
					objParticle = AmmoExplode;
					break;
				}
				break;
			}
		}

		if (objParticle == null) {
			return;
		}
		
		GameObject obj = (GameObject)Instantiate(objParticle, AmmoTran.position, AmmoTran.rotation);
		Transform tran = obj.transform;
		tran.parent = XkGameCtrl.PlayerAmmoArray;
		XkGameCtrl.CheckObjDestroyThisTimed(obj);
		#else
		GameObject objParticle = null;
		GameObject obj = null;
		Transform tran = null;
		Vector3 hitPos = transform.position;
		RaycastHit hit;
		Vector3 forwardVal = Vector3.Normalize(AmmoEndPos - AmmoStartPos);
		if (AmmoType == PlayerAmmoType.PuTongAmmo) {
			float disVal = Vector3.Distance(AmmoEndPos, AmmoStartPos) + 10f;
			Physics.Raycast(AmmoStartPos, forwardVal, out hit, disVal, PlayerAmmoHitLayer);
			if (hit.collider != null) {
				XKAmmoParticleCtrl ammoParticleScript = hit.collider.GetComponent<XKAmmoParticleCtrl>();
				if (ammoParticleScript != null && ammoParticleScript.PuTongAmmoLZ != null) {
					objParticle = ammoParticleScript.PuTongAmmoLZ;
				}
				else {
					string tagHitObj = hit.collider.tag;
					switch (tagHitObj) {
					case "metal":
						if (MetalParticle != null) {
							objParticle = MetalParticle;
						}
						break;
						
					case "concrete":
						if (ConcreteParticle != null) {
							objParticle = ConcreteParticle;
						}
						break;
						
					case "dirt":
						if (DirtParticle != null) {
							objParticle = DirtParticle;
						}
						break;
						
					case "wood":
						if (WoodParticle != null) {
							objParticle = WoodParticle;
						}
						break;
						
					case "water":
						if (WaterParticle != null) {
							objParticle = WaterParticle;
						}
						break;
						
					case "sand":
						if (SandParticle != null) {
							objParticle = SandParticle;
						}
						break;
						
					case "glass":
						if (GlassParticle != null) {
							objParticle = GlassParticle;
						}
						break;
					}
					
					if (objParticle == null) {
						objParticle = AmmoExplode;
					}
				}
			}
			else {
				objParticle = AmmoExplode;
			}
		}
		else {
			float disVal = Vector3.Distance(AmmoEndPos, AmmoStartPos) + 10f;
			Physics.Raycast(AmmoStartPos, forwardVal, out hit, disVal, PlayerAmmoHitLayer);
			if (hit.collider != null) {
				string tagHitObj = hit.collider.tag;
				switch (tagHitObj) {
				case "dirt":
					if (DirtParticle != null) {
						objParticle = DirtParticle;
					}
					break;
					
				case "water":
					if (WaterParticle != null) {
						objParticle = WaterParticle;
					}
					break;
				}
				
				if (objParticle == null) {
					objParticle = AmmoExplode;
				}
			}
			else {
				objParticle = AmmoExplode;
			}
		}
		
		if (objParticle == null) {
			return;
		}
		
		hitPos = explodePos;
		switch (AmmoType) {
		case PlayerAmmoType.DaoDanAmmo:
			Vector3 AmmoPos = transform.position - (transform.forward * 3f);
			Physics.Raycast(AmmoPos, forwardVal, out hit, 13f, XkGameCtrl.GetInstance().LandLayer);
			if (hit.collider != null) {
				Vector3 normalVal = hit.normal;
				Quaternion rotVal = Quaternion.LookRotation(normalVal);
				obj = (GameObject)Instantiate(objParticle, hitPos, rotVal);
				obj.transform.up = normalVal;
			}
			else {
				obj = (GameObject)Instantiate(objParticle, hitPos, transform.rotation);
			}
			break;
		case PlayerAmmoType.ChuanTouAmmo:
			obj = (GameObject)Instantiate(objParticle, explodePos, transform.rotation);
			break;
		default:
			obj = (GameObject)Instantiate(objParticle, hitPos, transform.rotation);
			break;
		}
		tran = obj.transform;
		tran.parent = XkGameCtrl.PlayerAmmoArray;
		XkGameCtrl.CheckObjDestroyThisTimed(obj);
		
		XkAmmoTieHuaCtrl tieHuaScript = obj.GetComponent<XkAmmoTieHuaCtrl>();
		if (tieHuaScript != null && tieHuaScript.TieHuaTran != null) {
			Transform tieHuaTran = tieHuaScript.TieHuaTran;
			Vector3 AmmoPos = transform.position - (transform.forward * 3f);
			Physics.Raycast(AmmoPos, forwardVal, out hit, 13f, XkGameCtrl.GetInstance().PlayerAmmoHitLayer);
			if (hit.collider != null) {
				tieHuaTran.up = hit.normal;
			}
		}
		#endif
	}
	
	void MoveAmmoOnCompelteITween()
	{
		if (IsDestroyAmmo) {
			return;
		}
		IsDestroyAmmo = true;

		if (NpcHealthList != null) {
			NpcHealthList.Clear();
			NpcHealthList = null;
		}

		if (PaiJiPaoTiShi != null) {
			Destroy(PaiJiPaoTiShi);
		}

		if (AmmoType != PlayerAmmoType.ChuanTouAmmo) {
			SpawnAmmoParticleObj();
		}

		NpcAmmoCtrl.RemoveItweenComponents(gameObject);
		NpcAmmoCtrl.RemoveItweenComponents(gameObject, 1);
		if (AmmoType == PlayerAmmoType.GenZongAmmo
		    || AmmoType == PlayerAmmoType.PaiJiPaoAmmo) {
			CheckPlayerAmmoOverlapSphereHit();
		}
		DaleyHiddenPlayerAmmo();
	}

	void DaleyHiddenPlayerAmmo()
	{
		gameObject.SetActive(false);
	}

	void CheckPlayerAmmoForwardHitNpc()
	{
		if (AmmoType == PlayerAmmoType.GenZongAmmo
		    || AmmoType == PlayerAmmoType.PaiJiPaoAmmo) {
			return;
		}
		CheckPlayerAmmoOverlapSphereHit();
	}

	
	bool IsChuanJiaDanHitCamColForward = false;
	/**
	 * key == 0 -> 检测子弹打中的物体.
	 * key == 1 -> 检测子弹打中的物体, 并且用来调用爆炸特效.
	 */
	GameObject CheckPlayerAmmoOverlapSphereHit(int key = 0)
	{
		bool isBreak = false;
		GameObject obj = null;
		float disDamage = key == 0 ? AmmoDamageDis : 0.2f;
		XKPlayerMvFanWei playerMvFanWei = null;
		Collider[] hits = Physics.OverlapSphere(AmmoTran.position, disDamage, PlayerAmmoHitLayer);
		foreach (Collider c in hits) {
			// Don't collide with triggers
			if (c.isTrigger || IsChuanJiaDanHitCamColForward) {
				continue;
			}

			if (AmmoType != PlayerAmmoType.PaiJiPaoAmmo) {
				playerMvFanWei = c.GetComponent<XKPlayerMvFanWei>();
				if (playerMvFanWei != null) {
					if (playerMvFanWei.FanWeiState == PointState.Hou) {
						continue;
					}
					
					if (playerMvFanWei.FanWeiState == PointState.Qian) {
						if (AmmoType != PlayerAmmoType.ChuanTouAmmo ) {
							isBreak = true;
							obj = c.gameObject;
							MoveAmmoOnCompelteITween();
							break;
						}
						else {
							IsChuanJiaDanHitCamColForward = true;
							break;
						}
					}
				}
			}

			switch (key) {
			case 0:
				isBreak = CheckAmmoHitObj(c.gameObject, PlayerState);
				if (isBreak) {
					break;
				}
				break;
			case 1:
				isBreak = true;
				obj = c.gameObject;
				break;
			}
		}
		return obj;
	}
}