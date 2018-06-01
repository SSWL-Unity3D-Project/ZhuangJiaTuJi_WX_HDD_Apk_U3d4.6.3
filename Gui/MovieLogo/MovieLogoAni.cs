using UnityEngine;

public class MovieLogoAni : MonoBehaviour
{
    public GameObject[] m_HiddenObjArray;
    public void OnAnimationOver()
    {
        //开始播放动画视频.
        //GameMovieCtrl.GetInstance().PlayMovie();
    }

    public void SetActiveHiddenObj(bool isActive)
    {
        for (int i = 0; i < m_HiddenObjArray.Length; i++)
        {
            m_HiddenObjArray[i].SetActive(isActive);
        }
    }
}