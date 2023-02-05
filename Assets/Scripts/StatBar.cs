using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatBar : MonoBehaviour
{

    public ResourceType barType;
    public Image bar;
    public TextMeshProUGUI statAmount;

    public SFXManager sfxManager;
    public GameObject gameOverLoseMessage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //BLECH!
        switch(barType)
        {
            case ResourceType.ENERGY:
                bar.fillAmount = Stats.energy / Stats.maxEnergy;
                statAmount.text = "" + Stats.energy;
                break;

            case ResourceType.MOTIVATION:
                bar.fillAmount = Stats.motivation / Stats.maxMotivation;
                statAmount.text = "" + Stats.motivation;
                break;

            case ResourceType.TIME:
                bar.fillAmount = Stats.time / Stats.maxTime;
                statAmount.text = "" + Stats.time;
                break;

            case ResourceType.HAPPINESS:
                bar.fillAmount = Stats.happiness / Stats.maxHappiness;
                statAmount.text = "" + Stats.happiness;
                if(Stats.happiness <= 0f)
                {
                    //Gameover!
                    sfxManager.Play("AllSFX", "sx_gameOver");
                    gameOverLoseMessage.SetActive(true);
                    Time.timeScale = 0f;
                }
                else if(Stats.happiness >= 100f)
                {
                    //Win!
                    sfxManager.Play("AllSFX", "sx_neuron_unlocked");
                    Time.timeScale = 0f;
                }
                break;
            
            default:
                break;
        }
    }
}
