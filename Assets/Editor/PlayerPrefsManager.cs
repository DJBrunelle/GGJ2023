using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlayerPrefsManager : EditorWindow {

    bool setInt = false;
    bool setFloat = false;
    bool setString = false;
    bool deleteAll = false;
    bool showConfirm = false;
    bool yes = false;
    bool no = false;

    string deleteKey = "";

    string intKeyGet = "";
    string intValGet = "";
    string floatKeyGet = "";
    string floatValGet = "";
    string stringKeyGet = "";
    string stringValGet = "";
    int intValue;
    string intKeySet = "";
    int intValSet;
    string floatKeySet = "";
    float floatValSet;
    string stringKeySet = "";
    string stringValSet = "";

    bool showDeleteAll = false;
    string status = "Delete All";


    [MenuItem ("Window/Player Prefs")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlayerPrefsManager));
    }

    void OnGUI()
    {

        GUILayout.Label("Manage Player Preferences", EditorStyles.boldLabel);
        GUILayout.Label("Delete Key", EditorStyles.boldLabel);
        deleteKey = EditorGUILayout.TextField("        Key To Delete:", 
                deleteKey);
        if(deleteKey != "" && PlayerPrefs.HasKey(deleteKey))
        {
            if(GUILayout.Button("Delete Key"))
            {
                PlayerPrefs.DeleteKey(deleteKey);
                deleteKey = "";
                ClearFocus();
            }
        }


        GUILayout.Label("Get Int", EditorStyles.boldLabel);
        intKeyGet = EditorGUILayout.TextField("        Int Key:", intKeyGet);
        GUILayout.Label("        Int Value:               " + intValGet);
        if(intKeyGet != "")
        {
            if(PlayerPrefs.HasKey(intKeyGet))
            {
                intValGet = "" + PlayerPrefs.GetInt(intKeyGet);
            }
            else
            {
                intValGet = "<key does not exist>";
            }
        }
        else
        {
            intValGet = "";
        }

        GUILayout.Label("Get Float", EditorStyles.boldLabel);
        floatKeyGet = EditorGUILayout.TextField("        Float Key:", 
                floatKeyGet);
        GUILayout.Label("        Float Value:            " + floatValGet);
        if(floatKeyGet != "")
        {
            if(PlayerPrefs.HasKey(floatKeyGet))
            {
                floatValGet = "" + PlayerPrefs.GetFloat(floatKeyGet);
            }
            else
            {
                floatValGet = "<key does not exist>";
            }
        }
        else
        {
            floatValGet = "";
        }
        
        GUILayout.Label("Get String", EditorStyles.boldLabel);
        stringKeyGet = EditorGUILayout.TextField("        String Key:", 
                stringKeyGet);
        GUILayout.Label("        String Value:          " + stringValGet);
        if(stringKeyGet != "")
        {
            if(PlayerPrefs.HasKey(stringKeyGet))
            {
                stringValGet = PlayerPrefs.GetString(stringKeyGet);
            }
            else
            {
                stringValGet = "<key does not exist>";
            }
        }
        else
        {
            stringValGet = "";
        }


        //GUILayout.Label("Set Int");
        //EditorGUILayout.IntField("Int Value", intValue):
        //GUILayout.Label("Set Float");
        GUILayout.Label("Set Int", EditorStyles.boldLabel);
        intKeySet = EditorGUILayout.TextField("        Int Key:", 
                intKeySet);
        intValSet = EditorGUILayout.IntField("        Int Value:", 
                intValSet);
        if(intKeySet != "")
        {
            setInt = GUILayout.Button("Set Int");
        }
        if(setInt)
        {
            PlayerPrefs.SetInt(intKeySet, intValSet);
            intKeySet = "";
            intValSet = 0;
            setInt = false;
            ClearFocus();
        }

        GUILayout.Label("Set Float", EditorStyles.boldLabel);
        floatKeySet = EditorGUILayout.TextField("        Float Key:", 
                floatKeySet);
        floatValSet = EditorGUILayout.FloatField("        Float Value:", 
                floatValSet);
        if(floatKeySet != "")
        {
            setFloat = GUILayout.Button("Set Float");
        }
        if(setFloat)
        {
            PlayerPrefs.SetFloat(floatKeySet, floatValSet);
            floatKeySet = "";
            floatValSet = 0f;
            setFloat = false;
            ClearFocus();
        }

        GUILayout.Label("Set String", EditorStyles.boldLabel);
        stringKeySet = EditorGUILayout.TextField("        String Key:", 
                stringKeySet);
        stringValSet = EditorGUILayout.TextField("        String Value:", 
                stringValSet);
        if(stringKeySet != "")
        {
            setString = GUILayout.Button("Set String");
        }
        if(setString)
        {
            PlayerPrefs.SetString(stringKeySet, stringValSet);
            stringKeySet = stringValSet = "";
            setString = false;
            ClearFocus();
        }
        
        EditorGUILayout.Space();
        showDeleteAll = EditorGUILayout.Foldout(showDeleteAll, status, 
                EditorStyles.boldLabel);
        if(showDeleteAll)
        {
            deleteAll = GUILayout.Button("Delete All Player Prefs");
            if(deleteAll)
            {
                showConfirm = true;
            }
            if(showConfirm)
            {
                GUILayout.Label(
                        "Are you sure you want to delete all player prefs?",
                        EditorStyles.boldLabel);
                yes = GUILayout.Button("Yes");
                no = GUILayout.Button("No");
                if(yes)
                {
                    PlayerPrefs.DeleteAll();
                }
                if(yes || no)
                {
                    yes = no = showConfirm = false;
                }
            }
        }
        else
        {
            yes = no = showConfirm = false;
        }
    }

    void ClearFocus()
    {
        GUI.SetNextControlName("none");
        GUI.FocusControl("none");
    }
}
