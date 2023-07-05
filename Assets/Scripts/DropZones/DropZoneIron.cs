using UnityEngine;

public class DropZoneIron : DropZone
{
    public override void OnTriggerStay(Collider other)
    {
        _playerBackpack.DropIronObject(transform.parent);
    }
}
