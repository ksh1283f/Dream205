using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSingletoneObj : Singletone<TestSingletoneObj>
{


    public void Test()
    {
        Debug.Log("I'm TestSingletoneObj");
    }
}
