using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Player Movement")]
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    public Buttons[] input;

    private Rigidbody2D _body2d;
    private InputState _inputState;

    void Start()
    {
        _body2d = GetComponent<Rigidbody2D>();
        _inputState = GetComponent<InputState>();
    }

    void Update()
    {
        var right = _inputState.GetButtonValue(input[0]);
        var left = _inputState.GetButtonValue(input[1]);

        var velX = moveSpeed;

        if(right || left)
        {
            velX *= left ? -1 : 1;
        }
        else
        {
            velX = 0;
        }

        _body2d.velocity = new Vector2(velX, _body2d.velocity.y);
    }
}
