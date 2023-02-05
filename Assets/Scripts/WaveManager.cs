using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private EventData _eventData;
    
    [SerializeField] private List<Neuron> _neurons;
    [SerializeField] private BadThought _thought;
    [SerializeField] private float _timeBetweenLevels;

    [SerializeField] private float _thoughtRate = 1;
    private float _timeSinceThought = 0;

    [SerializeField] private float _eventRate = 10;
    private float _timeSinceEvent;

    [SerializeField] private float _startMaxEnergy = 100f;
    [SerializeField] private float _startMaxMotivation = 100f;
    [SerializeField] private float _startMaxTime = 100f;

    private EventSimulator _eventSimulator;
    private float _lastLevelUp;

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
            neuron.gameObject.tag = "Node";
            neuron._first.gameObject.tag = "OuterNode";
            neuron._second.gameObject.tag = "OuterNode";

            Color newColor = neuron.GetComponent<SpriteRenderer>().color;
            newColor.r = 1f;
            newColor.g = 1f;
            newColor.b = 1f;
            neuron.GetComponent<SpriteRenderer>().color = newColor;

            newColor = neuron._first.GetComponent<SpriteRenderer>().color;
            newColor.r = 0.5f;
            newColor.g = 0.5f;
            newColor.b = 0.5f;
            neuron._first.GetComponent<SpriteRenderer>().color = newColor;
            newColor = neuron._second.GetComponent<SpriteRenderer>().color;
            newColor.r = 0.5f;
            newColor.g = 0.5f;
            newColor.b = 0.5f;
            neuron._second.GetComponent<SpriteRenderer>().color = newColor;


            _newNeurons.Add(neuron._first);
            _newNeurons.Add(neuron._second);
            Camera.main.orthographicSize += 0.8f;
        }
        _neurons.Clear();
        _neurons = _newNeurons;
        _lastLevelUp = Time.time;
    }
    
    void Start()
    {
        _input.ClickEvent += HandleClick;

        _eventSimulator = new EventSimulator();

        _lastLevelUp = Time.time - (_neurons.Count * _timeBetweenLevels);

        Stats.maxEnergy = _startMaxEnergy;
        Stats.maxMotivation = _startMaxMotivation;
        Stats.maxTime = _startMaxTime;
        Stats.energy = 0.5f * _startMaxEnergy;
        Stats.motivation = 0.5f * _startMaxMotivation;
        Stats.time = 0.5f * _startMaxTime;

    }

    private void Update()
    {
        if(Time.time - _lastLevelUp >=  _neurons.Count * _timeBetweenLevels)
        {
            LevelUp();
        }
        if (_timeSinceThought > _thoughtRate)
        {
            SpawnThought();
            _timeSinceThought = 0;
        }

        if (_timeSinceEvent > _eventRate)
        {
            var gameEvent = _eventSimulator.TriggerEvent(_eventData);
            
            // TODO implement text output on UI on random event
        }

        _timeSinceThought += Time.deltaTime;
        _timeSinceEvent += Time.deltaTime;
    }

    private void HandleClick()
    {
        //LevelUp();
        //SpawnThought();
    }
}
