using UnityEngine;
using System.Collections;

public class XKGmChangeUI : MonoBehaviour
{
	public UITexture[] UItextureGm;
	public Texture[] TextureGm;
	public MeshRenderer[] MeshRd;
	public Material[] MaterialAy;
	public int[] IndexMaterial;
	// Use this for initialization
	void Start()
	{
		XKGlobalData.GetInstance();
		if (XKGlobalData.GameVersionPlayer == 0) {
			return;
		}

		for (int i = 0; i < UItextureGm.Length; i++) {
			UItextureGm[i].mainTexture = TextureGm[i];
		}

		for (int i = 0; i < MeshRd.Length; i++) {
			//MeshRd[i].materials[IndexMaterial[i]] = MaterialAy[i];
			MeshRd[i].material = MaterialAy[i];
		}
	}
}