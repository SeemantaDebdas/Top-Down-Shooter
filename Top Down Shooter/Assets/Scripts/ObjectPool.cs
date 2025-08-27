using System;
using System.Collections.Generic;
using UnityEngine;

namespace TDS
{
    public class ObjectPool : MonoBehaviour
    {

        [SerializeField] GameObject bulletPrefab;
        [SerializeField] int poolSize = 20;
        Dictionary<GameObject, Queue<GameObject>> poolDictionary = new();

        public static ObjectPool Instance { get; private set; }
        void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = this;
        }

        void InitializePool(GameObject prefab)
        {
            poolDictionary[prefab] = new();

            for (int i = 0; i < poolSize; i++)
            {
                CreateNewObject(prefab);
            }
        }

        private void CreateNewObject(GameObject prefab)
        {
            GameObject objectSpawn = Instantiate(prefab, transform);
            objectSpawn.AddComponent<PooledObject>().OriginalPrefab = prefab;

            objectSpawn.SetActive(false);
            poolDictionary[prefab].Enqueue(objectSpawn);
        }

        public GameObject GetObject(GameObject prefab, Vector3 position = default, Quaternion rotation = default)
        {
            if (!poolDictionary.ContainsKey(prefab))
                InitializePool(prefab);

            if (poolDictionary[prefab].Count == 0)
                CreateNewObject(prefab);

            GameObject objectToReturn = poolDictionary[prefab].Dequeue();

            objectToReturn.transform.SetPositionAndRotation(position, rotation);

            objectToReturn.SetActive(true);
            return objectToReturn;
        }

        public bool TryReturnObjectToPool(GameObject objectToReturn)
        {
            if (objectToReturn.TryGetComponent(out PooledObject _))
            {
                ReturnObjectToPool(objectToReturn);
                return true;
            }

            return false;
        }

        private void ReturnObjectToPool(GameObject objectToReturn)
        {
            GameObject originalPrefab = objectToReturn.GetComponent<PooledObject>().OriginalPrefab;

            objectToReturn.SetActive(false);
            objectToReturn.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            poolDictionary[originalPrefab].Enqueue(objectToReturn);
        }
    }
}
