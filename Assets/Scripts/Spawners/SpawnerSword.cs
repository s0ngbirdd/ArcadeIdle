using DG.Tweening;
using UnityEngine;

public class SpawnerSword : Spawner
{
    private StockDisplayerIron _stockDisplayerIron;

    public override void Start()
    {
        base.Start();

        _pile = new PickupObjectSword[_pileWidth, _pileLength, _pileHeight];

        _stockDisplayerIron = FindObjectOfType<StockDisplayerIron>();
    }

    public override void Update()
    {
        if (_canSpawn && _haveSpace && _stockDisplayerIron.StockObjectNumber > 0)
        {
            SpawnObject();

            if (_haveSpace)
            {
                _stockDisplayerIron.DecreseStockObjectNumber();
            }
        }
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
                        _haveSpace = true;
                        _canSpawn = false;

                        GameObject sword = ObjectPool.Instance.GetSwordPooledObject();
                        sword.transform.position = transform.position;
                        sword.transform.rotation = Quaternion.identity;
                        sword.transform.SetParent(transform);
                        sword.SetActive(true);

                        _spawnTween = sword.transform.DOLocalJump(new Vector3(x - _spawnOffsetX, y, z + _spawnOffsetZ), 2, 1, _spawnDuration).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            _pile[x, z, y] = sword.GetComponent<PickupObjectSword>();
                            
                            _pile[x, z, y].name = $"Sword_x{x}_z{z}_y{y}";
                            _canSpawn = true;
                            _spawnTween.Kill();
                        });
                        
                        return;
                    }
                    else
                    {
                        _haveSpace = false;
                    }
                }
            }
        }
    }
}
