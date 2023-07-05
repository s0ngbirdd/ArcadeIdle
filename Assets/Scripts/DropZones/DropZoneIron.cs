using UnityEngine;

namespace DropZones
{
    public class DropZoneIron : DropZone
    {
        public override void OnTriggerStay(Collider other)
        {
            _playerBackpack.DropIronObject(transform.parent);
        }
    }
}
