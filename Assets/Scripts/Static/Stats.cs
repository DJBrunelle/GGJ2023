using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Stats
{

    private static float _happiness = 50;
    private static float _energy = 50;
    private static float _maxEnergy = 100;
    private static float _motivation = 50;
    private static float _maxMotivation = 100;
    private static float _time = 24;
    private static float _maxTime = 24;


    public static float happiness
    {
        get { return _happiness; }
        set { _happiness = value; }
    }

    public static float energy
    {
        get { return _energy; }
        set { _energy = value; }
    }

    public static float maxEnergy
    {
        get { return _maxEnergy; }
        set { _maxEnergy = value; }
    }

    public static float motivation
    {
        get { return _motivation; }
        set { _motivation = value; }
    }

    public static float maxMotivation
    {
        get { return _maxMotivation; }
        set { _maxMotivation = value; }
    }

    public static float time
    {
        get { return _time; }
        set { _time = value; }
    }

    public static float maxTime
    {
        get { return _maxTime; }
        set { _maxTime = value; }
    }

    public static bool PurchaseFacility(float energy, float motivation, float time)
    {
        if(energy > _energy)
        {
            Debug.Log("not enough energy");
            Debug.Log("needed energy:" + energy);
            Debug.Log("available energy:" + _energy);
            return false;
        }
        if(motivation > _motivation)
        {
            Debug.Log("not enough motivation");
            return false;
        }
        if(time > _time)
        {
            Debug.Log("not enough time");
            return false;
        }
        _energy -= energy;
        _motivation -= motivation;
        _time -= time;
        return true;
    }

    public static void ReturnFacility(float energy, float motivation, float time)
    {
        _energy += energy;
        _motivation += motivation;
        _time += time;
    }


}
