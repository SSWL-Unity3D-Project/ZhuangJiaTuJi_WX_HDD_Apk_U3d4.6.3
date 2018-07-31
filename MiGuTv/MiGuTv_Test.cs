using UnityEngine;

public class MiGuTv_Test : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		MiGuTv_InterFace temp = null;
		temp = GetComponent<MiGuTv_InterFace>();
		if (temp!=null)
		{
			temp.MiGuTv_Initial("");
			//temp.MiGuTv_OnMonthPay("001");
            temp.MiGuTv_OnCountPay("001", "1234567890123456");
        }
	}
}
