using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject EnemyBullet;

    private Coroutine spawnerCoroutine;

    void Start()
    {
        if (Enemy == null || EnemyBullet == null)
        {
            throw new UnassignedReferenceException();
        }

        PoolManager.WarmPool(Enemy, 5);
        PoolManager.WarmPool(EnemyBullet, 5);
    }

    void OnEnable()
    {
        spawnerCoroutine = StartCoroutine(Spawn());
    }

    void OnDisable()
    {
        StopCoroutine(spawnerCoroutine);
    }

    IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        while (gameObject.activeInHierarchy)
        {
            yield return wait;
            var position = new Vector3(Random.Range(-5, 5), gameObject.transform.position.y, 0);
            var enemy = PoolManager.SpawnObject(Enemy, position, Quaternion.identity);
        }
    }
}
