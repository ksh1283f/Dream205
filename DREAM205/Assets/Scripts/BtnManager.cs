using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    [SerializeField] List<ElevatorInterac> BtnList = new List<ElevatorInterac>();
    Dictionary<E_ElevatorBtnType, ElevatorInterac> BtnDic = new Dictionary<E_ElevatorBtnType, ElevatorInterac>();
    [SerializeField] AudioSource LaserFx;
    bool isSelected = false;
    public AudioSource plant;
    public AudioSource Arrive;

    void Start()
    {
        for (int i = 0; i < BtnList.Count; i++)
            BtnList[i].OnClickedBtn += OnClickedBtn;

        for (int i = 0; i < BtnList.Count; i++)
            BtnDic.Add(BtnList[i].BtnType, BtnList[i]);

    }

    void OnClickedBtn(E_ElevatorBtnType type)
    {

        if (isSelected && type != E_ElevatorBtnType.Close)
            return;

        if (type != E_ElevatorBtnType.Close)
            isSelected = true;

        switch (type)
        {
            case E_ElevatorBtnType.Close:
                if (BtnDic.ContainsKey(E_ElevatorBtnType.Close) && !BtnDic[E_ElevatorBtnType.Close].IsInteractionEnd)
                    LaserFx.Play();
                //Invoke("AudioPlay", 1f);
                break;

            case E_ElevatorBtnType.Second:
            case E_ElevatorBtnType.Fifth:
            case E_ElevatorBtnType.Fourteenth:
                LaserFx.Play();
                //Invoke("AudioPlay", 2f);
                break;
        }

        if (BtnDic.ContainsKey(type))
            BtnDic[type].ActivateColor();

    }

    public void AudioPlay()
    {

        plant.Play();
        Arrive.Play();

    }

}
