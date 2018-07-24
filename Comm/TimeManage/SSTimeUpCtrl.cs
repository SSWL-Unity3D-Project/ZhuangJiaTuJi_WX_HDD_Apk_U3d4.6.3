using UnityEngine;

public class SSTimeUpCtrl : MonoBehaviour
{
    float MaxTimeVal = 0f;
    float LastTimeVal = 0f;
    bool IsInitUpTime = false;
    bool IsTimeUpOver = false;
    /// <summary>
    /// 时间上升结束事件.
    /// </summary>
    public delegate void TimeUpOverEvent();
    public event TimeUpOverEvent OnTimeUpOverEvent;
    void OnTimeUpOver()
    {
        if (OnTimeUpOverEvent != null)
        {
            OnTimeUpOverEvent();
        }
    }

    /// <summary>
    /// 初始化.
    /// </summary>
    public void Init(float maxTime)
    {
        MaxTimeVal = maxTime;
        LastTimeVal = Time.realtimeSinceStartup;
        IsInitUpTime = true;
    }

    float m_PauseTimeVal = 0f;
    void Update()
    {
        if (!IsInitUpTime)
        {
            return;
        }

        //if (SSGameDataCtrl.GetInstance().m_SSUIRoot.m_ExitGameUI != null)
        //{
        //    //退出游戏界面存在时,不累加时间.
        //    m_PauseTimeVal += Time.deltaTime;
        //    return;
        //}

        //if (SSGameDataCtrl.GetInstance().IsPauseGame)
        //{
        //    //暂停游戏时间累加.
        //    m_PauseTimeVal += Time.deltaTime;
        //    return;
        //}

        if (m_PauseTimeVal > 0f)
        {
            //消除暂停游戏的累加时间.
            LastTimeVal += m_PauseTimeVal;
            m_PauseTimeVal = 0f;
        }

        if (Time.realtimeSinceStartup - LastTimeVal >= MaxTimeVal)
        {
            if (!IsTimeUpOver)
            {
                IsTimeUpOver = true;
                OnTimeUpOver();
                Destroy(this);
                return;
            }
        }
    }
}