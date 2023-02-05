using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    public AudioSource music;
    public string defaultTrack;
    public bool playOnAwake;
    public List<AudioClip> musicTracks;

    private GameObject[] allMusicManagers;
    private int myIndex;
    private Dictionary<string, AudioClip> tracksByName;
    private float volume;
    private string trackName;
    private string currTrackName;


    void Awake()
    {
        //This allows each scene to have a MusicManager for dev and testing
        //purposes, but will ensure that only 1 manager is present in any scene
        //at runtime.
        allMusicManagers = GameObject.FindGameObjectsWithTag("MusicManager");
        myIndex = allMusicManagers.Length;
        if(myIndex > 1)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () 
    {
        music.volume = PlayerPrefs.GetFloat("MusicVolume");
        DontDestroyOnLoad(gameObject);
        tracksByName = new Dictionary<string, AudioClip>();
        foreach(AudioClip musicTrack in musicTracks)
        {
            tracksByName.Add(musicTrack.name, musicTrack);
        }
        if(playOnAwake && tracksByName.ContainsKey(defaultTrack))
        {
            StartMusic(defaultTrack);
        }
	}

    public void StartMusic(string trackName)
    {
        if(tracksByName.ContainsKey(trackName))
        {
            if(!music.isPlaying || trackName != currTrackName)
            {
                currTrackName = trackName;
                volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
                music.volume = volume;
                music.clip = tracksByName[trackName];
                music.Play();
            }
        }
    }

    public void StartMusic(string trackName, bool loop)
    {
        if(tracksByName.ContainsKey(trackName))
        {
            music.loop = loop;
            StartMusic(trackName);
        }
    }

    public void StopMusic()
    {
        StopMusic(false);
    }

    public void StopMusic(bool fade)
    {
        if(!fade)
        {
            music.Stop();
        }
    }

    public void PauseMusic()
    {
        music.Pause();
    }

    public void ResumeMusic()
    {
        music.Play();
    }

    //This can be bound to the in-game volume setting, such as a volume slider in the options
    public void MusicVolumeChanged(float newVolume)
    {
        if(newVolume == 0f)
        {
            music.Pause();
        }
        music.volume = newVolume;
    }
}
