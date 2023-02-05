using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    
    [SerializeField] private List<Neuron> _neurons;
    [SerializeField] private BadThought _thought;

    [SerializeField] private float _thoughtRate = 1;
    private float _timeSinceThought = 0;

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
    }

    private void Update()
    {
        if (_timeSinceThought > _thoughtRate)
        {
            SpawnThought();
            _timeSinceThought = 0;
        }

        _timeSinceThought += Time.deltaTime;
    }

    private void HandleClick()
    {
        LevelUp();
        SpawnThought();
    }
}
