using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowFacilityCosts : MonoBehaviour
{
    public TextMeshProUGUI energyCostText;
    public TextMeshProUGUI motivationCostText;
    public TextMeshProUGUI timeCostText;


    void SetEnergyCost(float cost)
    {
        energyCostText.text = "" + cost;
        if(cost > Stats.energy)
        {
            energyCostText.color = Color.red;
        }
        else
        {
            energyCostText.color = Color.white;
        }
    }

    void SetMotivationCost(float cost)
    {
        motivationCostText.text = "" + cost;
        if(cost > Stats.motivation)
        {
            motivationCostText.color = Color.red;
        }
        else
        {
            motivationCostText.color = Color.white;
        }
    }

    void SetTimeCost(float cost)
    {
        timeCostText.text = "" + cost;
        if(cost > Stats.time)
        {
            timeCostText.color = Color.red;
        }
        else
        {
            timeCostText.color = Color.white;
        }
    }
}
