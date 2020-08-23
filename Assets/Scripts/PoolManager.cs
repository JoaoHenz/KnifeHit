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

        /// <summary>
        /// Put object back in the pool
        /// </summary>
        /// <param name="gameObject"></param>
        public static void RePool(GameObject obj)
        {
            Transform objTransform = obj.transform;

            objTransform.parent = _instance.transform.Find("Object Pools").Find(objTransform.name);
            objTransform.localPosition = Vector3.zero;
            objTransform.localScale = Vector3.one;
            objTransform.localEulerAngles = Vector3.zero;
            objTransform.GetComponent<PoolableObject>().OnRePool();
            objTransform.gameObject.SetActive(false);
        }

        /// <summary>
        /// Put object back in the pool
        /// </summary>
        /// <param name="gameObject"></param>
        public static void RePool(Transform objTransform)
        {
            objTransform.parent = _instance.transform.Find("Object Pools").Find(objTransform.name);
            objTransform.localPosition = Vector3.zero;
            objTransform.localScale = Vector3.one;
            objTransform.localEulerAngles = Vector3.zero;
            objTransform.GetComponent<PoolableObject>().OnRePool();
            objTransform.gameObject.SetActive(false);
        }

        private void Awake()
        {
            SingletonAwake();
            GameObject objectPools = new GameObject();
            objectPools.transform.parent = transform;
            objectPools.name = "Object Pools";

            foreach(ObjectPoolEntry entry in ObjectPoolEntries)
            {
                if (!entry.GameObject.GetComponent<PoolableObject>())
                {
                    Debug.LogError(entry.GameObject.name+" must have the PoolableObject class!");
                    return;
                }
                GameObject pool = new GameObject();
                pool.transform.parent = objectPools.transform;
                pool.name = entry.GameObject.name;

                ObjectPool objectPool = new ObjectPool();
                List<GameObject> objectList = new List<GameObject>();

                for (int i = 0; i < entry.Amount; i++)
                {
                    GameObject pooledObject = Instantiate(entry.GameObject, pool.transform);
                    pooledObject.SetActive(false);
                    pooledObject.name = entry.GameObject.name;
                    objectList.Add(pooledObject);
                }

                objectPool.PooledObjects = objectList;
                _objectPools[entry.GameObject.name] = objectPool;
            }
        }
    }
}
