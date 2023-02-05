using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game Events")]
public class EventData : ScriptableObject
{
    public List<GameEvent> _gameEvents;
}

[Serializable]
public class GameEvent
{
    [Serializable]
    public class StatModifier
    {
        [FormerlySerializedAs("_statType")] public ResourceType _resourceType;
        public float _amount;
    }
    public string _eventText;
    public List<StatModifier> _StatModifiers;
}

