using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float Velocity = 0f;
    public float Speed = 1f;

    public GameObject Bullet;

    void Start()
    {
        if (Bullet == null)
        {
            throw new UnassignedReferenceException("Bullet is unassigned");
        }
        PoolManager.WarmPool(Bullet, 8);
    }

    void OnFire()
    {
        Debug.Log("OnFire!");
        var position = this.gameObject.transform.position;
        position.y += 0.5f;
        var bullet = PoolManager.SpawnObject(Bullet, position, Quaternion.identity);
    }

    void OnMove(InputValue value)
    {
        float direction = value.Get<float>();
        Debug.LogFormat("OnMove {0}!", direction);
        Velocity = Speed * direction;
    }

    void Update()
    {
        var position = this.gameObject.transform.position;
        position.x += Velocity * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, -5, 5);
        this.gameObject.transform.position = position;
    }
}
