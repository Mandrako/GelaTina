using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Long Jump")]
public class LongJump : Jump {
    public float longJumpDelay = 0.15f;
    public float longJumpMultiplier = 1.5f;
    public bool canLongJump;
    public bool isLongJumping;

    protected override void Update()
    {
        var canJump = _inputState.GetButtonValue(inputButtons[0]);
        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);

        if (!canJump)
        {
            canLongJump = false;
        }

        if(_collisionState.grounded && isLongJumping)
        {
            isLongJumping = false;
        }

        base.Update();

        if(canLongJump && !_collisionState.grounded && holdTime > longJumpDelay)
        {
            var vel = _body2d.velocity;
            _body2d.velocity = new Vector2(vel.x, jumpSpeed * longJumpMultiplier);
            canLongJump = false;
            isLongJumping = true;
        }
    }

    protected override void OnJump()
    {
        base.OnJump();

        canLongJump = true;
    }
}
