using UnityEngine;
using System.Collections.Generic;

namespace GI.UnityToolkit.Utilities
{
    public class GameObjectPool<T> where T : Component
    {
        private const int MAX_POOL_SIZE = 200;

        private readonly Queue<T> _pool = new Queue<T>();
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly List<T> _used = new List<T>();

        public GameObjectPool(GameObject prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public T Get(Transform parent = null)
        {
            while (true)
            {
                if (_pool.Count == 0)
                {
                    var newObj = Object.Instantiate(_prefab, parent).GetComponent<T>();
                    newObj.gameObject.SetActive(true);
                    _used.Add(newObj);
                    return newObj;
                }

                var item = _pool.Dequeue();
                if (item == null) continue;

                item.gameObject.SetActive(true);
                item.transform.SetParent(parent);
                _used.Add(item);

                return item;
            }
        }

        public void Put(T obj)
        {
            _used.Remove(obj);
            if (_pool.Count < MAX_POOL_SIZE)
            {
                obj.transform.SetParent(_parent);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
            else
            {
                Object.Destroy(obj.gameObject);
            }
        }

        public void PutAll() => _used.ForEachBackwards(Put);
    }
}