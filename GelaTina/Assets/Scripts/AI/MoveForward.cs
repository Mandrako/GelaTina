using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody2D _body2D;

    void Start()
    {
        _body2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _body2D.velocity = new Vector2(transform.localScale.x, 0) * speed;
    }
}
