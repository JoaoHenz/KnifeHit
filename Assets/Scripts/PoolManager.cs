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

    public class PoolManager : MonoBehaviour
    {
        public ObjectPoolEntry[] ObjectPoolEntries;

        [SerializeField] private static Dictionary<string, ObjectPool> _objectPools = new Dictionary<string, ObjectPool>();


        #region singleton
        private static PoolManager _instance;
        private void SingletonAwake()
        {
            if (_instance)
                Destroy(this);
            else
                _instance = this;

            DontDestroyOnLoad(this);
        }
        #endregion

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
            objectPool.PooledObjects[objectPool.Index].SetActive(true);
            return objectPool.PooledObjects[objectPool.Index];
        }

        private void Awake()
        {
            SingletonAwake();

            foreach(ObjectPoolEntry entry in ObjectPoolEntries)
            {
                ObjectPool objectPool = new ObjectPool();
                List<GameObject> objectList = new List<GameObject>();

                for (int i = 0; i < entry.Amount; i++)
                {
                    objectList.Add(Instantiate(entry.GameObject,transform));
                }

                objectPool.PooledObjects = objectList;
                _objectPools[entry.GameObject.name] = objectPool;
            }
        }
    }
}
