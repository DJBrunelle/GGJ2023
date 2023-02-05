using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventSimulator : MonoBehaviour
{
    [SerializeField] private EventData _eventData;

    public GameEvent TriggerEvent()
    {
        int i = Random.Range(0, _eventData._gameEvents.Count);

        var gameEvent = _eventData._gameEvents[i];
        
        TriggerEventEffects(gameEvent);

        return gameEvent;
    }

    private void TriggerEventEffects(GameEvent gameEvent)
    {
        foreach (var modifier in gameEvent._StatModifiers)  
        {
            switch (modifier._resourceType)
            {
                case ResourceType.ENERGY:
                    // TODO Modify energy
                    break;
            
                case ResourceType.HAPPINESS:
                    // TODO Modify Happiness
                    break;
            
                case ResourceType.MOTIVATION:
                    // TODO Modify Motivation
                    break;
                
                case ResourceType.TIME:
                    // TODO Modify Time
                    break;
            
            }
        }
    }
}
