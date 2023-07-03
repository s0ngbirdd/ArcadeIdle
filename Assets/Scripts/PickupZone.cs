using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupZone : MonoBehaviour
{
    private Spawner _spawner;
    /*private PlayerBackpack _playerBackpack;

    private void Awake()
    {
        _playerBackpack = GetComponent<PlayerBackpack>();
    }*/

    private void Start()
    {
        _spawner = GetComponentInParent<Spawner>();
    }

    private void OnTriggerStay(Collider other)
    {
        _spawner.DespawnObject(other);
        //Debug.Log("ENTER");
    }
}
