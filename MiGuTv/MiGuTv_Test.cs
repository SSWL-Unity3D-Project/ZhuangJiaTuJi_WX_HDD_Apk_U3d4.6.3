using UnityEngine;

public class MiGuTv_Test : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		MiGuTv_InterFace temp = null;
		temp = GetComponent<MiGuTv_InterFace>();
		if (temp!=null)
		{
			temp.MiGuTv_Initial();
			temp.MiGuTv_OnMonthPay("001");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
