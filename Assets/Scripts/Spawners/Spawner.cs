using DG.Tweening;
using PickupObjects;
using Player;
using UnityEngine;

namespace Spawners
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] protected float _animationDuration = 1f;
        [SerializeField] protected int _pileWidth = 2;
        [SerializeField] protected int _pileLength = 2;
        [SerializeField] protected int _pileHeight = 2;
    
        protected PickupObject[,,] _pile;
    
        protected float _spawnOffsetX;
        protected float _spawnOffsetZ = 1f;
        protected Tween _spawnTween;
        private Tween _despawnTween;
        private Tween _rotateTween;

        protected bool _canSpawn = true;
        private bool _canDespawn = true;
        protected bool _haveSpace = true;
    
        public virtual void Start()
        {
            if (_pileWidth % 2 == 0)
            {
                _spawnOffsetX = _pileWidth / 2 - 0.5f;
            }
            else if (_pileWidth != 1 && _pileWidth % 2 != 0)
            {
                _spawnOffsetX = _pileWidth / 2;
            }
        }

        public virtual void Update()
        {
            if (_canSpawn)
            {
                SpawnObject();
            }
        }

        public abstract void SpawnObject();

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
                            _haveSpace = true;
                            _canDespawn = false;

                            Vector3 backpackPosition = playerBackpack.PickupObject(_pile[x, z, y]);

                            if (backpackPosition != Vector3.zero)
                            {
                                _pile[x, z, y].transform.SetParent(other.gameObject.transform);

                                _despawnTween = _pile[x, z, y].transform.DOLocalJump(backpackPosition, 2, 1, _animationDuration).SetEase(Ease.Linear).OnComplete(() =>
                                {
                                    _canDespawn = true;
                                    _despawnTween.Kill();
                                });
                            
                                _rotateTween = _pile[x, z, y].transform.DOLocalRotate(Vector3.zero, _animationDuration).SetEase(Ease.Linear).OnComplete(() =>
                                {
                                    _rotateTween.Kill();
                                });
                        
                                _pile[x, z, y] = null;
                            }
                            else
                            {
                                _canDespawn = true;
                            }
                        
                            return;
                        }
                    }
                }
            }
        }
    }
}
