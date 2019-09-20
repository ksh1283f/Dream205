using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*--------------------- 수정된 부분 -------------------------*/

    public SoundManager soundManager;
    [SerializeField] List<Image> imageList = new List<Image>();


    private void Awake()
    {
        if (soundManager == null)
        {
            Debug.LogError("soundManager is null");
            return;
        }
        soundManager.OnSoundStart += ShowImage;
        soundManager.OnSoundEnd += HideImage;

        for (int i = 0; i < imageList.Count; i++)   // 처음엔 안보이게 초기화
            imageList[i].gameObject.SetActive(false);
    }

    void ShowImage(int index)
    {
        if (imageList.Count <= index)  // 인덱스가 이미지리스트의 사이즈를 벗어날 경우
        {
            Debug.LogError("out of index, index: " + index + " ," + "size: " + imageList.Count);
            return;
        }

        if (imageList[index] == null)
        {
            Debug.LogError("imageList " + index + "is null");
            return;
        }

        imageList[index].gameObject.SetActive(true);
    }

    void HideImage(int index)
    {
        if (imageList.Count <= index)  // 인덱스가 이미지리스트의 사이즈를 벗어날 경우
        {
            Debug.LogError("out of index, index: " + index + " ," + "size: " + imageList.Count);
            return;
        }

        if (imageList[index] == null)
        {
            Debug.LogError("imageList " + index + "is null");
            return;
        }

        imageList[index].gameObject.SetActive(false);
    }

    /*--------------------- 수정된 부분 -------------------------*/

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
