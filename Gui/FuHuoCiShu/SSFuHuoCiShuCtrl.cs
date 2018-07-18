using UnityEngine;

public class SSFuHuoCiShuCtrl : MonoBehaviour
{
    /// <summary>
    /// 复活次数.
    /// </summary>
    public int m_FuHuoCiShu = 5;
    public UISprite[] m_FuHuoCiShuSprite = new UISprite[1];

    /// <summary>
    /// 显示游戏中玩家的复活次数UI.
    /// </summary>
    public void ShowPlayerFuHuoCiShu(int numVal)
    {
        int max = m_FuHuoCiShuSprite.Length;
        if (max <= 0)
        {
            Debug.LogWarning("Unity: ShowPlayerFuHuoCiShu -> max was wrong! max ===== " + max);
            return;
        }

        int valTmp = 0;
        int powVal = 0;
        for (int i = 0; i < max; i++)
        {
            powVal = (int)Mathf.Pow(10, max - i - 1);
            valTmp = numVal / powVal;
            Debug.Log("Unity: valTmp ====== "+valTmp);
            if (m_FuHuoCiShuSprite[i] != null)
			{
				Debug.Log("Unity: valTmp *****====== "+valTmp);
                m_FuHuoCiShuSprite[i].spriteName = valTmp.ToString();
            }
            numVal -= valTmp * powVal;
        }
    }
}