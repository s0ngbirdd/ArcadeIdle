using System;
using DG.Tweening;
using UnityEngine;

public class PlayerBackpack : MonoBehaviour
{
    public static event Action OnDropObjectIron;
    public static event Action OnDropObjectSword;
    
    [SerializeField] private float _spawnDuration = 1f;
    [SerializeField] private int _backpackWidth = 2;
    [SerializeField] private int _backpackLength = 2;
    [SerializeField] private int _backpackHeight = 2;
    
    private PickupObject[,,] _backpack;
    
    private float _spawnOffsetX;
    private float _spawnOffsetZ = 1f;
    private Tween _despawnTween;
    private bool _canDespawn = true;

    private void Start()
    {
        _backpack = new PickupObject[_backpackWidth, _backpackLength, _backpackHeight];
        
        if (_backpackWidth % 2 == 0)
        {
            _spawnOffsetX = _backpackWidth / 2 - 0.5f;
        }
        else if (_backpackWidth != 1 && _backpackWidth % 2 != 0)
        {
            _spawnOffsetX = _backpackWidth / 2;
        }
    }

    public Vector3 PickupObject(PickupObject pickupObject)
    {
        for (int x = 0; x < _backpack.GetLength(0); x++)
        {
            for (int z = 0; z < _backpack.GetLength(1); z++)
            {
                for (int y = 0; y < _backpack.GetLength(2); y++)
                {
                    if (_backpack[x, z, y] == null)
                    {
                        _backpack[x, z, y] = pickupObject;
                        return new Vector3(x - _spawnOffsetX, y, -z - _spawnOffsetZ);
                    }
                }
            }
        }
        
        return Vector3.zero;
    }

    public void DropIronObject(Transform stockpile)
    {
        for (int x = _backpack.GetLength(0) - 1; x > -1; x--)
        {
            for (int z = _backpack.GetLength(1) - 1; z > -1; z--)
            {
                for (int y = _backpack.GetLength(2) - 1; y > -1; y--)
                {
                    if (_backpack[x, z, y] != null && _backpack[x, z, y] is PickupObjectIron && _canDespawn)
                    {
                        _canDespawn = false;
                        
                        _backpack[x, z, y].transform.SetParent(stockpile);
                    
                        _despawnTween = _backpack[x, z, y].transform.DOLocalJump(Vector3.zero, 2, 1, _spawnDuration).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            _canDespawn = true;
                            OnDropObjectIron?.Invoke();
                            
                            _backpack[x, z, y].gameObject.SetActive(false);
                            _backpack[x, z, y] = null;
                            
                            _despawnTween.Kill();
                        });

                        return;
                    }
                }
            }
        }
    }
    
    public void DropSwordObject(Transform stockpile)
    {
        for (int x = _backpack.GetLength(0) - 1; x > -1; x--)
        {
            for (int z = _backpack.GetLength(1) - 1; z > -1; z--)
            {
                for (int y = _backpack.GetLength(2) - 1; y > -1; y--)
                {
                    if (_backpack[x, z, y] != null && _backpack[x, z, y] is PickupObjectSword && _canDespawn)
                    {
                        _canDespawn = false;
                        
                        _backpack[x, z, y].transform.SetParent(stockpile);
                    
                        _despawnTween = _backpack[x, z, y].transform.DOLocalJump(Vector3.zero, 2, 1, _spawnDuration).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            _canDespawn = true;
                            OnDropObjectSword?.Invoke();
                            
                            _backpack[x, z, y].gameObject.SetActive(false);
                            _backpack[x, z, y] = null;
                            
                            _despawnTween.Kill();
                        });

                        return;
                    }
                }
            }
        }
    }
}
