using DG.Tweening;
using UnityEngine;

public class SpawnerIron : Spawner
{
    public override void Start()
    {
        base.Start();

        _pile = new PickupObjectIron[_pileWidth, _pileLength, _pileHeight];
    }
    
    public override void SpawnObject()
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

                        GameObject iron = ObjectPool.Instance.GetIronPooledObject();
                        iron.transform.position = transform.position;
                        iron.transform.rotation = Quaternion.identity;
                        iron.transform.SetParent(transform);
                        iron.SetActive(true);

                        _spawnTween = iron.transform.DOLocalJump(new Vector3(x - _spawnOffsetX, y, z + _spawnOffsetZ), 2, 1, _spawnDuration).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            _pile[x, z, y] = iron.GetComponent<PickupObjectIron>();
                            
                            _pile[x, z, y].name = $"Iron_x{x}_z{z}_y{y}";
                            _canSpawn = true;
                            _spawnTween.Kill();
                        });
                        
                        return;
                    }
                }
            }
        }
    }
}
