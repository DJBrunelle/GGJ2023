using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [Header("Scriptables")]
    [SerializeField] private InputReader _input;
    [SerializeField] private EventData _eventData;
    
    [Header("Prefabs")]
    [SerializeField] private List<Neuron> _neurons;
    [SerializeField] private BadThought _thought;
    
    
    [Header("Wave Variables")]
    [FormerlySerializedAs("_thoughtRate")] [SerializeField] private float _thoughtInterval = 1;
    private float _timeSinceThought = 0;

    [FormerlySerializedAs("_eventRate")] [SerializeField] private float _eventInterval = 10;
    private float _timeSinceEvent;

    private EventSimulator _eventSimulator;

    private void SpawnThought()
    {
        var i = Random.Range(0, _neurons.Count - 1);

        BadThought badThought = Instantiate(_thought, _neurons[i].transform.position, Quaternion.identity);
        badThought.OnSpawn(_neurons[i]);
    }

    private void LevelUp()
    {
        List<Neuron> _newNeurons = new List<Neuron>();
        foreach (var neuron in _neurons)
        {
            neuron.AddLayer();

            _newNeurons.Add(neuron._first);
            _newNeurons.Add(neuron._second);
        }
        _neurons.Clear();
        _neurons = _newNeurons;
    }
    
    void Start()
    {
        _input.ClickEvent += HandleClick;

        _eventSimulator = new EventSimulator();
    }

    private void Update()
    {
        if (_timeSinceThought > _thoughtInterval)
        {
            SpawnThought();
            _timeSinceThought = 0;
        }

        if (_timeSinceEvent > _eventInterval)
        {
            var gameEvent = _eventSimulator.TriggerEvent(_eventData);
            
            // TODO implement text output on UI on random event
        }

        _timeSinceThought += Time.deltaTime;
        _timeSinceEvent += Time.deltaTime;
    }

    private void HandleClick()
    {
        LevelUp();
        SpawnThought();
    }
}
