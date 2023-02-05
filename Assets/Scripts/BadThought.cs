using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadThought : MonoBehaviour
{
    private Neuron _target;

    [SerializeField] private float _speed;
    [SerializeField] private GameObject _heart;

    public void OnSpawn(Neuron spawn)
    {
        if (spawn._layer == 0) return;
        _target = spawn._previous;
    }

    private bool AtTarget(Transform target)
    {
        return Vector3.Distance(target.position, transform.position) < 0.1;
    }

    private void GetNextTarget()
    {
        _target = _target._previous;
    }

    void Update()
    {
        if (_target != null && AtTarget(_target.transform))
        {
            // TODO  Check if target is heart - otherwise if facility on node, damage node and die, else:
            GetNextTarget();
        }
       
        if (_target == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _heart.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Facility"))
        {
            col.GetComponent<Facility>().Damage(1);
            Destroy(gameObject);
        }

        if (col.CompareTag("Heart"))
        {
            Stats.happiness -= 10;
            Destroy(gameObject);
        }
    }
}
