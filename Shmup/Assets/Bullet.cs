using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D Body;

    public Vector2 Direction = new Vector2(0, 0);
    public float Speed = 0f;

    void Awake()
    {
        Body = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Direction.Normalize();
        Body.velocity = Direction * Speed;
        Body.rotation = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
    }

    void OnBecameInvisible()
    {
        PoolManager.ReleaseObject(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PoolManager.ReleaseObject(gameObject);
    }
}
