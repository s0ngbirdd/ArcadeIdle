using System;
using UnityEngine;

public abstract class DropZone : MonoBehaviour
{
    public event Action OnPlayerEnter;
    public event Action OnPlayerExit;
    
    protected PlayerBackpack _playerBackpack;
    
    private void Start()
    {
        _playerBackpack = FindObjectOfType<PlayerBackpack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerEnter?.Invoke();
    }
    
    private void OnTriggerExit(Collider other)
    {
        OnPlayerExit?.Invoke();
    }

    public abstract void OnTriggerStay(Collider other);
}
