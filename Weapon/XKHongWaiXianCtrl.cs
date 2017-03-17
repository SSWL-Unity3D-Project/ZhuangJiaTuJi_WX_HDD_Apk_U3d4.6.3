using UnityEngine;
using System.Collections;

public class XKHongWaiXianCtrl : MonoBehaviour
{
	public float scrollSpeed = 0.5f;
	public float pulseSpeed = 1.5f;
	public float noiseSize = 1.0f;
	
	public float maxWidth = 0.5f;
	public float minWidth = 0.2f;
	
	private LineRenderer lRenderer;
	private float aniDir = 1.0f;
	
	void Start()
	{
		lRenderer = gameObject.GetComponent<LineRenderer>();
		// Change some animation values here and there
		StartCoroutine(ChoseNewAnimationTargetCoroutine());
	}
	
	IEnumerator ChoseNewAnimationTargetCoroutine()
	{
		while (true) {
			aniDir = aniDir * 0.9f + Random.Range (0.5f, 1.5f) * 0.1f;
			minWidth = minWidth * 0.8f + Random.Range (0.1f, 1.0f) * 0.2f;
			yield return new WaitForSeconds (1.0f + Random.value * 2.0f - 1.0f);	
		}	
	}

	void Update()
	{
		Vector2 veOffset = renderer.material.mainTextureOffset;
		veOffset.x += Time.deltaTime * aniDir * scrollSpeed;
		renderer.material.mainTextureOffset = veOffset;
		renderer.material.SetTextureOffset("_NoiseTex", new Vector2(-Time.time * aniDir * scrollSpeed, 0.0f));
		
		float aniFactor = Mathf.PingPong (Time.time * pulseSpeed, 1.0f);
		aniFactor = Mathf.Max (minWidth, aniFactor) * maxWidth;
		lRenderer.SetWidth (aniFactor, aniFactor);
	}
}