using UnityEngine;
using System.Collections;

public class TestNpcFollowPlayer : MonoBehaviour
{
	public Transform PlayerTr;
	public float flyingSpeed = 5.0f;
	public float zigZagness = 3.0f;
	public float zigZagSpeed = 2.5f;
	public float oriantationMultiplier = 2.5f;
	public float backtrackIntensity = 0.5f;
	private Vector3 smoothedDirection = Vector3.zero;

	void Update()
	{
		CheckNpcMove();
	}

	void CheckNpcMove()
	{
		Vector3 dir = PlayerTr.position - transform.position;
		if (dir.magnitude < 5f) {
			dir = -transform.position;
		}
		
		Vector3 zigzag = transform.right * (Mathf.PingPong (Time.time * zigZagSpeed, 2.0f) - 1.0f) * zigZagness;
		dir.Normalize ();
		
		smoothedDirection = Vector3.Slerp (smoothedDirection, dir, Time.deltaTime * 3.0f);
		var orientationSpeed = 1.0f;
		
		Vector3 deltaVelocity = (smoothedDirection * flyingSpeed + zigzag) - rigidbody.velocity;		
		if (Vector3.Dot (dir, transform.forward) > 0.8f)
			rigidbody.AddForce (deltaVelocity, ForceMode.Force);
		else {
			rigidbody.AddForce (-deltaVelocity * backtrackIntensity, ForceMode.Force);	
			orientationSpeed = oriantationMultiplier;
		}
		
		// Make the character rotate towards the target rotation
		Vector3 faceDir = smoothedDirection;
		if (faceDir == Vector3.zero) {
			rigidbody.angularVelocity = Vector3.zero;
		}
		else {
			float rotationAngle = AngleAroundAxis (transform.forward, faceDir, Vector3.up);
			rigidbody.angularVelocity = (Vector3.up * rotationAngle * 0.2f * orientationSpeed);
		}		
	}

	// The angle between dirA and dirB around axis
	static float AngleAroundAxis (Vector3 dirA, Vector3 dirB, Vector3 axis)
	{
		// Project A and B onto the plane orthogonal target axis
		dirA = dirA - Vector3.Project (dirA, axis);
		dirB = dirB - Vector3.Project (dirB, axis);
		
		// Find (positive) angle between A and B
		float angle = Vector3.Angle (dirA, dirB);
		
		// Return angle multiplied with 1 or -1
		return angle * (Vector3.Dot (axis, Vector3.Cross (dirA, dirB)) < 0 ? -1 : 1);
	}	
}