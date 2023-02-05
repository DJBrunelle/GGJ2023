using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Facility : MonoBehaviour
{

    public string displayName;
    public string buildSoundName;
    // public float interpersonalEnergyCost;
    // public float interpersonalMotivationCost;
    // public float interpersonalTimeCost;
    [Header("Costs")]
    public float personalEnergyCost;
    public float personalMotivationCost;
    public float personalTimeCost;
    // public float physicalEnergyCost;
    // public float physicalMotivationCost;
    // public float physicalTimeCost;

    [Header("Health")]
    public float baseHealth;
    public float levelHealthMultiplier;

    // public ResourceType generates;
    // public float levelResourceGeneratesMultiplier = 1;
    
    [Header("Resources Generated")]
    public float generatesEnergy = 0;
    public float generatesMotivation = 0;
    public float generatesTime = 0;
    public float generatesHappiness = 0;

    [Header("Other")]
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
        Stats.energy += generatesEnergy;
        if (Stats.energy > 100)
        {
            Stats.energy = 100;
        }
        Stats.happiness += generatesHappiness;
        if (Stats.happiness > 100)
        {
            Stats.happiness = 100;
        }
        Stats.motivation += generatesMotivation;
        if (Stats.motivation > 100)
        {
            Stats.motivation = 100;
        }
        Stats.time += generatesTime;
        if (Stats.time > 100)
        {
            Stats.time = 100;
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
