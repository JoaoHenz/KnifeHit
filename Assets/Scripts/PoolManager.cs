using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    [System.Serializable]
    public class ObjectPoolEntry
    {
        public GameObject GameObject;
        public int Amount;
    }

    public class ObjectPool
    {
        public List<GameObject> PooledObjects = new List<GameObject>();
        public int Index = 0;
    }

    [System.Serializable]
    public class PoolManager
    {
        public ObjectPoolEntry[] ObjectPoolEntries;

        [SerializeField] private static Dictionary<string, ObjectPool> _objectPools = new Dictionary<string, ObjectPool>();

        /// <summary>
        /// Gets an object from a pool, if there is a pool of this object
        /// </summary>
        /// <param name="name">Name of the pooled object prefab</param>
        /// <returns></returns>
        public static GameObject Instantiate(string name)
        {
            if (!_objectPools.ContainsKey(name))
            {
                Debug.LogError(name + " is not a pooled object!");
                return null;
            }

            ObjectPool objectPool = _objectPools[name];
            objectPool.Index = (objectPool.Index + 1 < objectPool.PooledObjects.Count) ? objectPool.Index + 1 : 0;
            return objectPool.PooledObjects[objectPool.Index];
        }

        public void Awake()
        {
            foreach(ObjectPoolEntry entry in ObjectPoolEntries)
            {
                ObjectPool objectPool = new ObjectPool();
                List<GameObject> objectList = new List<GameObject>();
                int i;
                for (i = 0; i < entry.Amount; i++)
                {
                    objectList.Add(entry.GameObject);
                }
                objectPool.PooledObjects = objectList;
                _objectPools[entry.GameObject.name] = objectPool;
            }
        }
    }
}
