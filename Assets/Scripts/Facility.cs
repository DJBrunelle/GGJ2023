using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Facility : MonoBehaviour
{

    public string displayName;
    public float interpersonalEnergyCost;
    public float interpersonalMotivationCost;
    public float interpersonalTimeCost;
    public float personalEnergyCost;
    public float personalMotivationCost;
    public float personalTimeCost;
    public float physicalEnergyCost;
    public float physicalMotivationCost;
    public float physicalTimeCost;

    public float baseHealth;
    public float levelHealthMultiplier;

    public ResourceType generates;
    public float levelResourceGeneratesMultiplier = 1;

    public Collider myCollider;

    private float currentHealth;
    private int levelNumber;


    public void GenerateResource()
    {

    }

    public void Build(PathType pathType, int _levelNumber)
    {
		switch (pathType) 
		{
            case PathType.INTERPERSONAL:

                //Stats.PurchaseFacility(interpersonalEnergyCost, interpersonalMotivationCost, interpersonalTimeCost);
                //generates = 
                break;

            case PathType.PHYSICAL:

                //Stats.PurchaseFacility(physicalEnergyCost, physicalMotivationCost, physicalTimeCost);
                break;

            case PathType.PERSONAL:

                //Stats.PurchaseFacility(personalEnergyCost, personalMotivationCost, personalTimeCost);
                break;

            default:
                break;
        }
        levelNumber = _levelNumber;
        currentHealth = baseHealth * levelHealthMultiplier * levelNumber;
        myCollider.enabled = true;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Placeholder function for when we need to do stuff. e.g. play sound, play animation, etc.
        Debug.Log("Kablooie!");
    }
}
