using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    private Rigidbody2D Body;
    private Coroutine moveCoroutine;
    public float VerticalSpeed = 0f;
    public float HorizontalSpeed = 0f;

    public GameObject EnemyBullet;

    void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Body.velocity = new Vector2(HorizontalSpeed * (Random.value > 0.5f ? -1 : 1), -1 * VerticalSpeed);
        moveCoroutine = StartCoroutine(Move());
    }

    void OnDisable()
    {
        StopCoroutine(moveCoroutine);
    }

    void OnBecameInvisible()
    {
        PoolManager.ReleaseObject(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PoolManager.ReleaseObject(gameObject);
    }

    IEnumerator Move()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (gameObject.activeInHierarchy)
        {
            yield return wait;
            var velocity = Body.velocity;
            velocity.x = -velocity.x;
            Body.velocity = velocity;
            yield return wait;
            var position = Body.transform.position;
            position.y -= 0.5f;
            var bullet = PoolManager.SpawnObject(EnemyBullet, position, Quaternion.identity);
        }
    }
}
