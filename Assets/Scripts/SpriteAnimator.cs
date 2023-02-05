using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
#if UNITY_EDITOR
[CanEditMultipleObjects]
#endif
public class SpriteAnimation
{
    public string name;
    [Tooltip("Frames per second")]
    public float speed = 24f;
    public bool loop = true;
    public int firstFrame;
    public int lastFrame;
    [Tooltip("Overrides First and Last frame.\nComma-delimited list.")]
    public string frames;

    [HideInInspector]
    public List<string> frameList;


    public void SetFrames()
    {
        frames = frames.Replace(" ", "");
        if(this.frames.Length > 0)
        {
            this.frameList = new List<string>(this.frames.Split(','));
        }
        else
        {
            this.frameList = new List<string>();
            for(int i = this.firstFrame; i <= this.lastFrame; i++)
            {
                this.frameList.Add("" + i);
            }
        }
    }
}

public class SpriteAnimator : MonoBehaviour {

    private int frameCount;
    public SpriteRenderer myRenderer;
    public Image myImage;
    public SpriteAtlas sheet;
    public int defaultAnimation;
    public string prefix;
    [Tooltip ("Which sprite atlas will be used?")]
    public int sheetIndex;
    public List<SpriteAnimation> animations;

    private Dictionary<string, SpriteAnimation> animationsByName;
    private int index;
    private float speed;
    private float overrideSpeed;
    private bool loop;
    private bool reverse;
    private bool stopped;
    private float prevFrame;
    [HideInInspector]
    public SpriteAnimation currAnim;
    private int currAnimIndex;
    private List<string> frames;
    private Sprite mySprite;


	//void Start () 
	void Awake () 
    {
        animationsByName = new Dictionary<string, SpriteAnimation>();
        foreach(SpriteAnimation anim in animations)
        {
            anim.SetFrames();
            animationsByName[anim.name] = anim;
        }

        CheckRenderer();

        if(prefix == "")
        {
            prefix = sheet.name + "_";
        }
        prevFrame = 0f;
        overrideSpeed = 0;
        loop = true;
        reverse = false;
        stopped = false;

        if(animations.Count > 0)
        {
            SetAnimation(animations[defaultAnimation].name);
        }
	}

	void FixedUpdate () 
    {
        if(currAnim != null && !stopped)
        {
            if(Time.time - prevFrame > 1f / speed)
            {
                if(!reverse)
                {
                    index++;
                    if(index >= frames.Count)
                    {
                        if(loop)
                        {
                            index = 0;
                        }
                        else
                        {
                            stopped = true;
                        }
                    }
                }
                else
                {
                    index--;
                    if(index < 0)
                    {
                        if(loop)
                        {
                            index = frames.Count - 1;
                        }
                        else
                        {
                            stopped = true;
                        }
                    }
                }
                if(!stopped)
                {
                    mySprite = sheet.GetSprite(prefix + frames[index]);
                    AssignSpriteToRenderer();
                    ////Debug.Log("Frame Length:" + (Time.time - prevFrame));
                    //Debug.Log("Frame time:" + Time.time);
                    frameCount++;
                    if(frameCount % 160 == 0)
                        Debug.Log("160 frames time:" + (Time.time / 9));
                    prevFrame = Time.time;
                }
            }
        }
	}

    public void Pause()
    {
        stopped = true;
    }

    public void Stop()
    {
        stopped = true;
        mySprite = sheet.GetSprite(prefix + frames[0]);
        AssignSpriteToRenderer();
    }

    public void Play()
    {
        stopped = false;
    }

    public void Play(string animName)
    {
        Stop();
        SetAnimation(animName);
        Play();
    }

    public void NextAnimation()
    {
        currAnimIndex = animations.IndexOf(currAnim);
        if(currAnimIndex + 1 >= animations.Count)
        {
            currAnimIndex = 0;
        }
        else
        {
            currAnimIndex++;
        }
        currAnim = animations[currAnimIndex];
        SetAnimation(currAnim.name);
    }

    public void SetAnimation(string newAnimName)
    {
        currAnim = animationsByName[newAnimName];
        if(overrideSpeed == 0)
        {
            speed = currAnim.speed;
        }

        index = 0;
        frames = currAnim.frameList;
        loop = currAnim.loop;
        stopped = false;
        mySprite = sheet.GetSprite(prefix + frames[index]);
        AssignSpriteToRenderer();
    }

    public void SetSpeed(float newSpeed)
    {
        overrideSpeed = newSpeed;
        if(overrideSpeed > 0)
        {
            speed = overrideSpeed;
        }
        else
        {
            speed = currAnim.speed;
        }
    }

    public void SetReverse(bool newReverse)
    {
        reverse = newReverse;
    }

    public void CheckRenderer()
    {
        if(myRenderer != null)
        {
            mySprite = myRenderer.sprite;
        }
        else if(myImage != null)
        {
            mySprite = myImage.sprite;
        }
    }

    public void AssignSpriteToRenderer()
    {
        if(myRenderer != null)
        {
            myRenderer.sprite = mySprite;
        }
        else if(myImage != null)
        {
            myImage.sprite = mySprite;
        }
    }
}
