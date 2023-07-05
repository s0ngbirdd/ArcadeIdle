using UnityEngine;

public class DropZoneSword : DropZone
{
    public override void OnTriggerStay(Collider other)
    {
        _playerBackpack.DropSwordObject(transform.parent);
    }
}
