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


    void SetEnergyCost(string cost)
    {
        energyCostText.text = cost;
    }

    void SetMotivationCost(string cost)
    {
        motivationCostText.text = cost;
    }

    void SetTimeCost(string cost)
    {
        timeCostText.text = cost;
    }
}
