using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
#if UNITY_EDITOR
[CanEditMultipleObjects]
#endif
public class SFXGroup : MonoBehaviour
{
    public List<AudioClip> soundEffects;
    [HideInInspector]
    public Dictionary<string, AudioClip> effectsByName;


    void Start()
    {
        effectsByName = new Dictionary<string, AudioClip>();
        foreach(AudioClip soundEffect in soundEffects)
        {
            effectsByName.Add(soundEffect.name, soundEffect);
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(SFXGroup))]
[CanEditMultipleObjects]
public class SFXGroupEditor : Editor 
{
    private SFXGroup thisSFXGroup;
    private GUIStyle myGui;


    void OnEnable()
    {
        thisSFXGroup = (SFXGroup)target;
        myGui = new GUIStyle();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        myGui.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField(thisSFXGroup.gameObject.name, myGui);

        DrawDefaultInspector();
    }
}
#endif
