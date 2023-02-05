using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public class Neuron : MonoBehaviour
{
    [FormerlySerializedAs("neuron")] [SerializeField] private GameObject _neuron;
    [SerializeField] private PathType _branch;

    [SerializeField] private SpriteRenderer _tendrils;

    public Neuron _first { get; private set; }
    public Neuron _second { get; private set; }

    public Neuron _previous { get; private set; }

    public int _layer { get; private set; } = 0;

    // distance and angle at which to spawn new neurons
    [SerializeField] private float spawnDistance = 3;
    [SerializeField] private float spawnAngle = 30;

    private void OnSpawn(Neuron spawner)
    {
        _previous = spawner;
        _layer = spawner._layer + 1;
        _branch = spawner._branch;
    }
    
    public void AddLayer()
    {
        if (_first != null) return;
        
        SpawnNewNeurons();
    }

    private void SpawnNewNeurons()
    {
        // Vector3 firstRotation = new Vector3(0, 0, spawnAngle);

        Quaternion firstRot = Quaternion.Euler(0, 0, spawnAngle);
        Quaternion secondRot = Quaternion.Euler(0, 0, -spawnAngle);
        
        Quaternion firstTRot = Quaternion.Euler(0, 0, spawnAngle+90);
        Quaternion secondTRot = Quaternion.Euler(0, 0, -(spawnAngle+90));
        
        Vector3 firstSpawn = (firstRot * transform.up);
        Vector3 secondSpawn = (secondRot * transform.up);

        _first = Instantiate(_neuron, transform.position + ((spawnDistance * (_layer+1)) * firstSpawn), transform.rotation * firstRot).GetComponent<Neuron>();
        var tendril1 = Instantiate(_tendrils, transform.position + ((spawnDistance * (_layer+1) * 0.5f) * firstSpawn), transform.rotation * firstTRot);
        _second = Instantiate(_neuron, transform.position + ((spawnDistance * (_layer+1)) * secondSpawn), transform.rotation * secondRot).GetComponent<Neuron>();
        var tendril2 = Instantiate(_tendrils, transform.position + ((spawnDistance * (_layer+1) * 0.5f) * secondSpawn), transform.rotation * secondTRot);

        tendril1.transform.localScale = new Vector3(0.5f * (1+_layer),tendril1.transform.localScale.y, tendril1.transform.localScale.z);
        tendril2.transform.localScale = new Vector3(0.5f * (1+_layer),tendril2.transform.localScale.y, tendril2.transform.localScale.z);
        
        _first.OnSpawn(this);
        _second.OnSpawn(this);
    }
}
