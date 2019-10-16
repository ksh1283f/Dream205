using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManagerInteract
{
    void CreateMaggot(E_RoomInteractObjType type);
    void RemoveSound(AudioSource audio);
}
