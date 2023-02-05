using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{

    public ResourceType barType;
    public Image bar;


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
                break;

            case ResourceType.MOTIVATION:
                bar.fillAmount = Stats.motivation / Stats.maxMotivation;
                break;

            case ResourceType.TIME:
                bar.fillAmount = Stats.time / Stats.maxTime;
                break;
            
            default:
                break;
        }
    }
}
