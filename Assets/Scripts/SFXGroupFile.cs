using System;
using System.IO;
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
public class SFXGroupFile : MonoBehaviour
{
    [Tooltip("Folder name under /Sound/.\nSeparate subfolders with </>.")]
    public string sourceFolder;
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
[CustomEditor(typeof(SFXGroupFile))]
[CanEditMultipleObjects]
public class SFXGroupFileEditor : Editor 
{
    private SFXGroupFile thisSFXGroupFile;
    private bool reImport = false;
    private bool clear = false;
    private string[] sfxPaths;
    private string cleanedPath;
    private AudioClip newSFX;
    private GUIStyle myGui;
    private bool showWarning;


    void OnEnable()
    {
        thisSFXGroupFile = (SFXGroupFile)target;
        myGui = new GUIStyle();
        showWarning = false;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        myGui.fontStyle = FontStyle.Bold;
        EditorGUILayout.LabelField(thisSFXGroupFile.gameObject.name, myGui);

        DrawDefaultInspector();

        GUILayout.BeginHorizontal();
        GUILayout.Space(23);
            reImport = GUILayout.Button("Re-Import");
            clear = GUILayout.Button("Clear List");
            if(clear)
            {
                Clear();
            }
            if(reImport)
            {
                EditorGUILayout.LabelField("Loading...");
                ReImport();
            }
        GUILayout.EndHorizontal();
        if(showWarning)
        {
            EditorGUILayout.Space();
            myGui.fontStyle = FontStyle.BoldAndItalic;
            EditorGUILayout.LabelField("Source Folder does not exist!", myGui);
            EditorGUILayout.Space();
        }
    }

    bool FolderExists()
    {
         return Directory.Exists( Application.dataPath + "/Sound/" + 
            thisSFXGroupFile.sourceFolder);
    }

    void Clear()
    {
        if(thisSFXGroupFile.soundEffects != null)
        {
            thisSFXGroupFile.soundEffects.Clear();
        }
        else
        {
            thisSFXGroupFile.soundEffects = new List<AudioClip>();
        }
    }

    void ReImport()
    {
        if(FolderExists())
        {
            showWarning = false;
            Clear();

             sfxPaths = Directory.GetFiles(
                Application.dataPath + "/Sound/" + 
                thisSFXGroupFile.sourceFolder, "*.ogg");
            foreach(string sfxPath in sfxPaths)
            {
                cleanedPath = "Assets" + sfxPath.Split(
                        new string[] {"Assets"}, 
                        StringSplitOptions.None)[1];
                newSFX = (AudioClip)AssetDatabase.LoadAssetAtPath(cleanedPath,
                        typeof(AudioClip));
                thisSFXGroupFile.soundEffects.Add(newSFX);
            }
        }
        else
        {
            showWarning = true;
        }
    }
}
#endif
