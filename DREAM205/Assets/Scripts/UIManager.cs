using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Image> Subtitle = new List<Image>();
    public SoundManager soundManager;

    public void u_StartPlay()
    {
        StartCoroutine(PlayerSubtitle());
    }

    IEnumerator PlayerSubtitle()
    {
        yield return null;
    }
}
