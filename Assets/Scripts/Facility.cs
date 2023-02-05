using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Facility : MonoBehaviour
{

    public string displayName;
    public string buildSoundName;
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
    private SFXManager sfxManager;


    void Start()
    {
        sfxManager = GameObject.FindWithTag("SFXManager").GetComponent<SFXManager>();
    }


    public void GenerateResource()
    {
        switch (generates)
        {
            case ResourceType.ENERGY:
                Stats.energy += levelResourceGeneratesMultiplier;
                break;
            case ResourceType.HAPPINESS:
                Stats.happiness += levelResourceGeneratesMultiplier;
                break;
            case ResourceType.MOTIVATION:
                Stats.motivation += levelResourceGeneratesMultiplier;
                break;
            case ResourceType.TIME:
                Stats.time += levelResourceGeneratesMultiplier;
                break;
        }
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
        GenerateResource();
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Placeholder function for when we need to do stuff. e.g. play sound, play animation, etc.
        
        sfxManager.Play("AllSFX", "sx_facility_destroyed");
        Destroy(gameObject);
    }
}
