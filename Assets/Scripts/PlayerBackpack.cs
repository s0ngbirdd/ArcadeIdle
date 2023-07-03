using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackpack : MonoBehaviour
{
    [SerializeField] private int _backpackWidth = 2;
    [SerializeField] private int _backpackLength = 2;
    [SerializeField] private int _backpackHeight = 2;
    
    private GameObject[,,] _backpack;
    private float _spawnOffsetX;
    private float _spawnOffsetZ = 1f;
    private bool _isFullBackpack;

    public bool IsFullBackpack => _isFullBackpack;

    private void Start()
    {
        _backpack = new GameObject[_backpackWidth, _backpackLength, _backpackHeight];
        
        if (_backpackWidth % 2 == 0)
        {
            _spawnOffsetX = _backpackWidth / 2 - 0.5f;
        }
        else if (_backpackWidth != 1 && _backpackWidth % 2 != 0)
        {
            _spawnOffsetX = _backpackWidth / 2;
        }
    }

    public Vector3 PickupObject(GameObject obj)
    {
        for (int x = 0; x < _backpack.GetLength(0); x++)
        {
            for (int z = 0; z < _backpack.GetLength(1); z++)
            {
                for (int y = 0; y < _backpack.GetLength(2); y++)
                {
                    if (_backpack[x, z, y] == null)
                    {
                        _isFullBackpack = false;
                        
                        _backpack[x, z, y] = obj;
                        return new Vector3(x - _spawnOffsetX, y, -z - _spawnOffsetZ);
                    }
                    else
                    {
                        _isFullBackpack = true;
                    }
                }
            }
        }
        
        return Vector3.zero;
    }
    
    private void DropObject()
    {
        for (int x = _backpack.GetLength(0) - 1; x > -1; x--)
        {
            for (int z = _backpack.GetLength(1) - 1; z > -1; z--)
            {
                for (int y = _backpack.GetLength(2) - 1; y > -1; y--)
                {
                    if (_backpack[x, z, y] != null)
                    {
                        //Destroy(_backpack[x, z, y]);
                        return;
                    }
                }
            }
        }
    }
}
