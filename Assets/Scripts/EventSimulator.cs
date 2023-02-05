using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventSimulator
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
                    Stats.energy += modifier._amount;
                    break;
            
                case ResourceType.HAPPINESS:
                    Stats.happiness += modifier._amount;
                    break;
            
                case ResourceType.MOTIVATION:
                    Stats.motivation += modifier._amount;
                    break;
                
                case ResourceType.TIME:
                    Stats.time += modifier._amount;
                    break;
            
            }
        }
    }
}
