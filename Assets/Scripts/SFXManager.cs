/*
***
This script works with SFXGroup.cs, SFXGroupFile.cs, and SFXSoundEffect.cs
There are two configurations that need to be made to a Unity project before
using this SFXManager. They are:

    1)  Make sure there is a tag in the tag manager called "SFXManager".
    2)  Set the following order in the Script Execution Order settings.
        (It shouldn't matter if they go before or after "Default Time". 
        If unsure, put them after.)
            a) SFXGroup
            b) SFXGroupFile
            c) SFXManager

The manager should now be good to go!
***
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SFXManager : MonoBehaviour {

    public AudioSource sfx;

    private GameObject[] allSFXManagers;
    private int myIndex;
    private Dictionary<string, Dictionary<string, AudioClip>> groupsByName;
    private List<string> sfxNames;
    private string sfxName;
    private int numSfx;
    private float volume;
    private Dictionary<string, SFXSoundEffect> activeSounds;
    private AudioSource[] audioSources;


    void Awake()
    {
        //This allows each scene to have an SFXManager for dev and testing
        //purposes, but will ensure that only 1 manager is present in any scene
        //at runtime.
        allSFXManagers = GameObject.FindGameObjectsWithTag("SFXManager");
        myIndex = allSFXManagers.Length;
        if(myIndex > 1)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(gameObject); //So SFX can carry over between scenes.
        groupsByName = new Dictionary<string, Dictionary<string, AudioClip>>();
        activeSounds = new Dictionary<string, SFXSoundEffect>();
        GameObject child;
        SFXGroup regGroup;
        SFXGroupFile fileGroup;
        for(int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            regGroup = child.GetComponent(typeof(SFXGroup)) as SFXGroup;
            fileGroup = child.GetComponent(typeof(SFXGroupFile)) as SFXGroupFile;
            if(regGroup)
            {
                groupsByName.Add(child.name, regGroup.effectsByName);
            }
            else if(fileGroup)
            {
                groupsByName.Add(child.name, fileGroup.effectsByName);
            }
        }
	}

    public void PlayRandomSFX(string groupName)
    {
        sfxNames = new List<string>(groupsByName[groupName].Keys);
        numSfx = sfxNames.Count;
        sfxName = sfxNames[URandom.Range(0, numSfx)];
        Play(groupName, sfxName);
    }

    public void Play(string groupName, string sfxName)
    {
        if(groupsByName[groupName].ContainsKey(sfxName))
        {
            volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
            sfx.PlayOneShot(groupsByName[groupName][sfxName], 
                    volume);
        }
    }

    public void Play(SFXSoundEffect sound, double delay = 0.0)
    {
        if(sound.volume < 0f)
        {
            volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }
        else
        {
            volume = sound.volume;
        }

        if(sound.source == null)
        {
            AudioSource newSource = (AudioSource)gameObject.AddComponent<
                AudioSource>();
            sound.source = newSource;
            sound.source.playOnAwake = false;
        }
        sound.isPaused = false;
        sound.source.volume = volume;
        sound.source.loop = sound.loop;
        sound.source.clip = groupsByName[sound.groupName][sound.sfxName];
        //sound.source.Play();
        sound.source.PlayScheduled(delay);
        activeSounds[sound.sfxName + "," + sound.groupName] = sound;
    }

    public void Stop(SFXSoundEffect sound)
    {
        sound.isPaused = false;
        sound.stop = true;
        if(sound.source != null)
        {
            sound.source.Stop();
            if(sound.source != sfx)
            {
                Destroy(sound.source);
            }
        }
    }

    public void StopAll()
    {
        foreach(AudioSource audioSource in audioSources)
        {
           if(audioSource.isPlaying)
           {
               audioSource.Stop();
           }
        }
    }

    public void Pause(SFXSoundEffect sound)
    {
        if(sound.source != null)
        {
            sound.isPaused = true;
            sound.source.Pause();
        }
    }

}


#if UNITY_EDITOR
[CustomEditor(typeof(SFXManager))]
[CanEditMultipleObjects]
public class SFXManagerEditor : Editor 
{
    private SFXManager thisSFXManager;


    void OnEnable()
    {
        thisSFXManager = (SFXManager)target;
        thisSFXManager.gameObject.tag = "SFXManager";
    }
}
#endif
