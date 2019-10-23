using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DirectingData
{
    public AudioSource source;
    public Animation ani;
    public InteractableObj[] interactableObj;
    public float delayTime; // 연출을 바로하지 않고 조금 있다가 해야할 때
    public string description;
    public bool isShowTextByEnd;
}

public class SoundManager : Singletone<SoundManager>
{
    public Action<int> OnSoundStart { get; set; }
    public Action<int> OnSoundEnd { get; set; }
    public Action OnSoundPlayEnd { get; set; }

    public Animation StartFo;
    //AudioClip narr;
    public List<AudioSource> SoundList = new List<AudioSource>();
    public List<DirectingData> DataList = new List<DirectingData>();

    private void Start()
    {
        StartCoroutine(IntroSoundPlay());
    }

    public IEnumerator IntroSoundPlay()
    {
        if (StartFo != null)
        {
            StartFo.Play();
            while (StartFo.isPlaying)
                yield return null;
        }

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < DataList.Count; i++)
        {
            Debug.Log("play index: " + i);
            if (DataList[i].delayTime > 0f)
                yield return new WaitForSeconds(DataList[i].delayTime);

            if (DataList[i].source != null)
                DataList[i].source.Play();

            if (OnSoundStart != null)
                OnSoundStart(i);    // // 이미지 보임

            if (DataList[i].ani != null)
                DataList[i].ani.Play();
            
            // 사운드
            while (DataList[i].source != null && DataList[i].source.isPlaying)
                yield return null;

            while (DataList[i].ani != null && DataList[i].ani.isPlaying)
                yield return null;

            if (!DataList[i].isShowTextByEnd)   // 인터랙션까지 이미지를 보여줘야되는게 아닌 경우
            {
                if (OnSoundEnd != null)
                    OnSoundEnd(i);  // 이미지 사라짐
            }

            // 인터랙션을 위한 초기화
            for (int j = 0; j < DataList[i].interactableObj.Length; j++)
            {
                if (DataList[i].interactableObj != null && DataList[i].interactableObj.Length > 0)
                {
                    // 연출중에 상호작용해야 할 오브젝트가 있는 경우
                    if (DataList[i].interactableObj[j] != null && DataList[i].interactableObj[j].OnExecuteInteract != null)
                        DataList[i].interactableObj[j].OnExecuteInteract();
                }
            }

            if (DataList[i].interactableObj != null && DataList[i].interactableObj.Length > 0)
            {
                // 상호작용이 끝날때까지 기다림
                // 일반적으로는 interactableObj가 1개이나 특수한 경우에는 여러개일 수 있음
                // 따라서 두가지 경우를 충족하기 위해 첫번째 요소만 검사
                while (DataList[i].interactableObj[0] != null && DataList[i].interactableObj[0].IsInteractionEnd == false)
                    yield return null;

                if (DataList[i].isShowTextByEnd)
                {
                    if (OnSoundEnd != null)
                        OnSoundEnd(i);  // 이미지 사라짐
                }
            }
        }

        if (OnSoundPlayEnd != null)
            OnSoundPlayEnd();   // FO, 다음씬 로드

        Debug.Log("Directing end");
    }

    /* --------------------- 에디터 상에서 데이터 편집용 ---------------------*/
    public int dataIndex;

    /// <summary>
    /// 맨 끝에 데이터를 추가
    /// </summary>
    public void InsertData()
    {
        DataList.Add(new DirectingData());
    }
    
    /// <summary>
    /// 넣고싶은 데이터를 원하는 인덱스의 위치에 추가
    /// </summary>
    public void InsertDataAtDataIndex()
    {
        DataList.Insert(dataIndex, new DirectingData());
    }

    /// <summary>
    /// 없애고 싶은 데이터를 제거
    /// </summary>
    public void RemoveData()
    {
        if(DataList.Count == 0)
        {
            Debug.LogError("DataList is empty");
            return;
        }

        DataList.RemoveAt(dataIndex);
    }
}
