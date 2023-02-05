using System.Collections;
using UnityEngine;

//FIXME:Give this a proper constructor.
public class SFXSoundEffect {

    public string sfxName = "";
    public string groupName = "";
    public float volume = -1f;
    public bool loop = false;
    public bool stop = false;
    public bool isPaused = false;
    public AudioSource source = null;
}
