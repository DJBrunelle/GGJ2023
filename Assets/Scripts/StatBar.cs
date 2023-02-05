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
            
            default:
                break;
        }
    }
}
