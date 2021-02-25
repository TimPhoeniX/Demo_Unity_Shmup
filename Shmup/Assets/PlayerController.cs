using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1f;
    private float Velocity = 0f;

    void Start()
    {
    }

    void OnFire()
    {
        Debug.Log("OnFire!");
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
