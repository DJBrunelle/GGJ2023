using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public StatType _statType;
        public float _amount;
    }
    public string _eventText;
    public List<StatModifier> _StatModifiers;
}

public enum StatType
{
    Happiness,
    Motivation,
    Energy,
}
