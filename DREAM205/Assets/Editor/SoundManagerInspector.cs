using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerInspector : Editor
{
    SoundManager soundManager = null;
    private void OnEnable()
    {
        soundManager = (SoundManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        soundManager.dataIndex = EditorGUILayout.IntSlider(soundManager.dataIndex, 0, soundManager.DataList.Count);

        EditorGUILayout.BeginHorizontal();
        {
            if(GUILayout.Button("InsertDirectingData at end"))
            {
                soundManager.InsertData();
            }

            if (GUILayout.Button("Insert DirectingData at dataIndex"))
            {
                soundManager.InsertDataAtDataIndex();
            }

            if(GUILayout.Button("Remove DirectingData"))
            {
                if(EditorUtility.DisplayDialog("유저 메세지",
                    string.Concat(soundManager.dataIndex,"번째 데이터를 정말로 삭제하시겠습니까?\ndata description: ",soundManager.DataList[soundManager.dataIndex].description),
                    "예","아니오"))
                {
                    soundManager.RemoveData();
                }
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}
