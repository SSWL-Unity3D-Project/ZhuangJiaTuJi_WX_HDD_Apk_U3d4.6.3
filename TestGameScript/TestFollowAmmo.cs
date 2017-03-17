using UnityEngine;
using System.Collections;

public class TestFollowAmmo : MonoBehaviour {
	public float speed = 15.0f;
	public float lifeTime = 1.5f;
//	public float damageAmount = 5f;
//	public float forceAmount = 5f;
	public float radius = 1.0f;
	public float seekPrecision = 1.3f;
	public LayerMask ignoreLayers;
	public float noise = 0.0f;
	public GameObject explosionPrefab;
	
	private Vector3 dir;
	private float spawnTime;
	public GameObject targetObject;
	private Transform tr;
	private float sideBias;
	
	void OnEnable()
	{
		tr = transform;
		dir = transform.forward;
		//targetObject = GameObject.FindWithTag ("Player");
		spawnTime = Time.realtimeSinceStartup;
		sideBias = Mathf.Sin (Time.time * 5);
	}

	// Update is called once per frame
	void Update()
	{
		AmmoUpdate();
	}
	
	void AmmoUpdate()
	{
		if (Time.realtimeSinceStartup > spawnTime + lifeTime) {
			Destroy(gameObject);
			return;
		}
		
		if (targetObject) {
			Vector3 targetPos = targetObject.transform.position;
			targetPos += transform.right * (Mathf.PingPong (Time.time, 1.0f) - 0.5f) * noise;
			Vector3 targetDir = (targetPos - tr.position);
			float targetDist = targetDir.magnitude;
			targetDir /= targetDist;
			if (Time.time - spawnTime < lifeTime * 0.2f && targetDist > 3f) {
				targetDir += transform.right * 0.5f * sideBias;
			}
			
			dir = Vector3.Slerp (dir, targetDir, Time.deltaTime * seekPrecision);
			tr.rotation = Quaternion.LookRotation(dir); 	
			tr.position += (dir * speed) * Time.deltaTime;
		}
		
		// Check if this one hits something
		Collider[] hits = Physics.OverlapSphere(tr.position, radius, ~ignoreLayers.value);
		bool collided = false;
		foreach (Collider c in hits) {
			// Don't collide with triggers
			if (c.isTrigger)
				continue;
			
//			var targetHealth : Health = c.GetComponent.<Health> ();
//			if (targetHealth) {
//				// Apply damage
//				targetHealth.OnDamage (damageAmount, -tr.forward);
//			}
			// Get the rigidbody if any
//			if (c.rigidbody) {
//				// Apply force to the target object
//				var force : Vector3 = tr.forward * forceAmount;
//				force.y = 0;
//				c.rigidbody.AddForce (force, ForceMode.Impulse);
//			}
			collided = true;
		}
		
		if (collided) {
			Destroy(gameObject);
			Debug.Log("***********");
			//Instantiate(explosionPrefab, transform.position, transform.rotation);
		}
	}
}