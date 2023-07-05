using UnityEngine;

public class PickupZone : MonoBehaviour
{
    private Spawner _spawner;
    
    private void Start()
    {
        _spawner = GetComponentInParent<Spawner>();
    }

    private void OnTriggerStay(Collider other)
    {
        _spawner.DespawnObject(other);
    }
}
