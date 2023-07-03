using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnedObjectPrefab;
    [SerializeField] private float _spawnDuration = 1f;
    [SerializeField] private int _pileWidth = 2;
    [SerializeField] private int _pileLength = 2;
    [SerializeField] private int _pileHeight = 2;
    
    private GameObject[,,] _pile;
    private float _spawnOffsetX;
    private Tween _spawnTween;
    private Tween _despawnTween;

    private bool _canSpawn = true;
    private bool _canDespawn = true;
    
    private void Start()
    {
        _pile = new GameObject[_pileWidth, _pileLength, _pileHeight];

        if (_pileWidth % 2 == 0)
        {
            _spawnOffsetX = _pileWidth / 2 - 0.5f;
        }
        else if (_pileWidth != 1 && _pileWidth % 2 != 0)
        {
            _spawnOffsetX = _pileWidth / 2;
        }
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject();
        }*/

        if (_canSpawn)
        {
            SpawnObject();
        }
        
        /*if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DespawnObject();
        }*/
    }

    private void SpawnObject()
    {
        for (int x = 0; x < _pile.GetLength(0); x++)
        {
            for (int z = 0; z < _pile.GetLength(1); z++)
            {
                for (int y = 0; y < _pile.GetLength(2); y++)
                {
                    if (_pile[x, z, y] == null && _canSpawn)
                    {
                        _canSpawn = false;
                        
                        _pile[x, z, y] = Instantiate(_spawnedObjectPrefab, transform.position, Quaternion.identity, transform);

                        _spawnTween = _pile[x, z, y].transform.DOMove(new Vector3(x - _spawnOffsetX, y, -z), _spawnDuration).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            _canSpawn = true;
                            _spawnTween.Kill();
                        });
                        
                        _pile[x, z, y].name = $"Pile_x{x}_z{z}_y{y}";
                        return;
                    }
                }
            }
        }
    }

    public void DespawnObject(Collider other)
    {
        PlayerBackpack playerBackpack = other.gameObject.GetComponent<PlayerBackpack>();
        
        for (int x = _pile.GetLength(0) - 1; x > -1; x--)
        {
            for (int z = _pile.GetLength(1) - 1; z > -1; z--)
            {
                for (int y = _pile.GetLength(2) - 1; y > -1; y--)
                {
                    if (_pile[x, z, y] != null && _canDespawn)
                    {
                        _canDespawn = false;
                        
                        _spawnTween.Kill();

                        Vector3 backpackPosition = playerBackpack.PickupObject(_pile[x, z, y]);

                        if (!playerBackpack.IsFullBackpack)
                        {
                            _pile[x, z, y].transform.SetParent(other.gameObject.transform);
                        
                            _despawnTween = _pile[x, z, y].transform.DOLocalMove(backpackPosition, _spawnDuration).SetEase(Ease.Linear).OnComplete(() =>
                            {
                                _canDespawn = true;
                                _despawnTween.Kill();
                            });
                        
                            _pile[x, z, y] = null;
                        }
                        
                        _canSpawn = true;
                        return;
                    }
                }
            }
        }
    }
}
