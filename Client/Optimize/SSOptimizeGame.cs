using UnityEngine;

public class SSOptimizeGame : MonoBehaviour
{
    private XKPlayerMvFanWei m_FanWeiHou;
    bool IsHitFanWeiHou;

    // Use this for initialization
    void Start()
    {
        Invoke("Init", 2.5f);
	}
	
    void Init()
    {
        m_FanWeiHou = XKPlayerMvFanWei.GetInstanceHou();
    }

	// Update is called once per frame
	void Update()
    {
        if (Time.frameCount % 15 == 0 && !IsHitFanWeiHou)
        {
            if (m_FanWeiHou != null)
            {
                Vector3 posTA = m_FanWeiHou.transform.position;
                Vector3 posTB = transform.position;
                posTA.y = posTB.y = 0f;
                Vector3 vecForward = -m_FanWeiHou.transform.forward;
                Vector3 vecAB = posTB - posTA;
                vecForward.y = vecAB.y = 0f;
                if (Vector3.Dot(vecForward, vecAB) < 0f)
                {
                    float dis = Vector3.Distance(posTA, posTB);
                    if (dis > 15f && dis < 40f)
                    {
                        //Debug.LogError("=============== test remove obj name =============== " + name);
                        IsHitFanWeiHou = true;
                        Destroy(gameObject);
                        return;
                    }
                }
            }
        }
    }
}