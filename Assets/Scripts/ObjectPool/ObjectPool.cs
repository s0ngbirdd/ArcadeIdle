using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    
    [Header("Iron")]
    [SerializeField] private GameObject _ironPrefab; // object to pool
    [SerializeField] private int _ironAmount; // amount to pool
    
    [Header("Sword")]
    [SerializeField] private GameObject _swordPrefab; // object to pool
    [SerializeField] private int _swordAmount; // amount to pool

    private List<GameObject> _ironPooledObjects; // pooled objects
    private List<GameObject> _swordPooledObjects; // pooled objects

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        _ironPooledObjects = new List<GameObject>();
        _swordPooledObjects = new List<GameObject>();
        GameObject temp;
    
        for (int i = 0; i < _ironAmount; i++)
        {
            temp = Instantiate(_ironPrefab, transform);
            temp.SetActive(false);
            _ironPooledObjects.Add(temp);
        }
    
        for (int i = 0; i < _swordAmount; i++)
        {
            temp = Instantiate(_swordPrefab, transform);
            temp.SetActive(false);
            _swordPooledObjects.Add(temp);
        }
    }

    // get pooled object
    public GameObject GetIronPooledObject()
    {
        for (int i = 0; i < _ironPooledObjects.Count; i++)
        {
            if (!_ironPooledObjects[i].activeInHierarchy)
            {
                return _ironPooledObjects[i];
            }
        }
        
        GameObject temp;
        temp = Instantiate(_ironPrefab, transform);
        temp.SetActive(false);
        _ironPooledObjects.Add(temp);
    
        return temp;
    }

    // get pooled object
    public GameObject GetSwordPooledObject()
    {
        for (int i = 0; i < _swordPooledObjects.Count; i++)
        {
            if (!_swordPooledObjects[i].activeInHierarchy)
            {
                return _swordPooledObjects[i];
            }
        }
    
        GameObject temp;
        temp = Instantiate(_swordPrefab, transform);
        temp.SetActive(false);
        _swordPooledObjects.Add(temp);
    
        return temp;
    }
}
