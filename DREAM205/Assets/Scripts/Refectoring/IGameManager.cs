using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    void CreateMaggot(E_RoomInteractObjType type);
    void RemoveSound(AudioSource audio);
}
