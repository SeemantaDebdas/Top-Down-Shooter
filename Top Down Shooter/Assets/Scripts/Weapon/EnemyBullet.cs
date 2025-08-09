using TDS;
using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void OnCollisionEnter(Collision collision)
    {
        CreateImpactVFX(collision);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Shooting Player");
        }

        ObjectPool.Instance.TryReturnObjectToPool(gameObject);
    }
}