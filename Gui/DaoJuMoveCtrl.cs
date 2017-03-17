using UnityEngine;
using System.Collections;

public class DaoJuMoveCtrl : MonoBehaviour
{
	PlayerEnum PlayerIndex;
	UITexture UITextureCom;
	TweenRotation TweenRot;
	TweenScale TweenScale;
	BuJiBaoType BuJiVal;
	public void SetDaoJuInfo(UITexture uitextureVal, TweenRotation tweenRotVal)
	{
		UITextureCom = uitextureVal;
		TweenRot = tweenRotVal;
	}

	static float ScaleKey = (0.4f - 0.2f) / (20f - 40f);
	public static GameObject SpawnDaoJuMoveObj(Transform parTr)
	{
		GameObject obj = new GameObject("daoJu");
		obj.transform.parent = parTr;
		obj.transform.localPosition = Vector3.zero;
		obj.layer = LayerMask.NameToLayer("NGUI");
		UITexture uITextureCom = obj.AddComponent<UITexture>();
		uITextureCom.depth = 7;

		TweenRotation tweenCom = obj.AddComponent<TweenRotation>();
		tweenCom.style = UITweener.Style.Loop;
		tweenCom.duration = 1f;
		tweenCom.from = Vector3.zero;
		tweenCom.to = new Vector3(0f, 0f, 360f);

		DaoJuMoveCtrl daoJuScript = obj.AddComponent<DaoJuMoveCtrl>();
		daoJuScript.SetDaoJuInfo(uITextureCom, tweenCom);
		return obj;
	}

	public void MoveDaoJuToPlayer(Texture daoJuTexture, PlayerEnum indexVal, BuJiBaoType buJiState, Vector3[] path)
	{
		if (!gameObject.activeSelf) {
			gameObject.SetActive(true);
		}
		UITextureCom.mainTexture = daoJuTexture;
		UITextureCom.width = daoJuTexture.width;
		UITextureCom.height = daoJuTexture.height;
		PlayerIndex = indexVal;
		BuJiVal = buJiState;

		TweenScale = gameObject.AddComponent<TweenScale>();
		TweenScale.duration = 1f;
		float disZ = path[0].z;
		if (disZ > 40f) {
			disZ = 40f;
		}

		if (disZ < 20f) {
			disZ = 20f;
		}
		float scaleVal = 0.2f + ScaleKey * (disZ - 40f);
		TweenScale.from = new Vector3(scaleVal, scaleVal, 1f);
		TweenScale.to = new Vector3(0.05f, 0.05f, 1f);
		TweenScale.PlayForward();

		TweenRot.ResetToBeginning();
		TweenRot.PlayForward();

		transform.localPosition = path[0];
		path[0] = transform.position;
		path[0].z = path[1].z = 0f;
		float disVal = Vector3.Distance(path[0], path[1]);
		float speedVal = disVal / 0.2f;
		iTween.MoveTo(gameObject, iTween.Hash("path", path,
		                                      "speed", speedVal,
		                                      "orienttopath", false,
		                                      "looptype", iTween.LoopType.none,
		                                      "easeType", iTween.EaseType.linear,
		                                      "oncomplete", "MoveDaoJuOnCompelte"));
	}

	void MoveDaoJuOnCompelte()
	{
		gameObject.SetActive(false);
		if (TweenScale != null) {
			Destroy(TweenScale);
		}
		DaoJuCtrl.GetInstance().ShowPlayerDaoJu(PlayerIndex, BuJiVal);
	}
}