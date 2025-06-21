using System;
using System.Collections.Generic;
using UnityEngine;

namespace TDS
{
    public class ObjectPool : MonoBehaviour
    {

        [SerializeField] GameObject bulletPrefab;
        [SerializeField] int poolSize = 20;

        Queue<GameObject> bulletPool = new();
        public static ObjectPool Instance { get; private set; }
        void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = this;
        }

        void Start()
        {
            CreateInitialPool();
        }

        void CreateInitialPool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                CreateNewBullet();
            }
        }

        private void CreateNewBullet()
        {
            GameObject bulletSpawn = Instantiate(bulletPrefab, transform);
            bulletSpawn.SetActive(false);
            bulletPool.Enqueue(bulletSpawn);
        }

        public GameObject GetBullet()
        {
            if (bulletPool.Count == 0)
                CreateNewBullet();

            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }

        public void ReturnBulletToPool(GameObject bullet)
        {
            bullet.SetActive(false);
            bullet.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            bulletPool.Enqueue(bullet);
        }
    }
}
