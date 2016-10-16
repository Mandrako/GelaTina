using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Wall Jump")]
public class WallJump : AbstractBehaviour {

    public Vector2 jumpVelocity = new Vector2(50, 200);
    public bool isJumpingOffWall;
    public float resetDelay = .2f;

    private float _timeElapsed = 0;

	void Update () {
	    if(_collisionState.onWall && !_collisionState.grounded)
        {
            var canJump = _inputState.GetButtonValue(inputButtons[0]);

            if (canJump && !isJumpingOffWall)
            {
                _inputState.direction = _inputState.direction == Directions.Right ? Directions.Left : Directions.Right;
                _body2d.velocity = new Vector2(jumpVelocity.x * (float)_inputState.direction, jumpVelocity.y);

                ToggleScripts(false);
                isJumpingOffWall = true;
            }
        }

        if (isJumpingOffWall)
        {
            _timeElapsed += Time.deltaTime;

            if(_timeElapsed > resetDelay)
            {
                ToggleScripts(true);
                isJumpingOffWall = false;
                _timeElapsed = 0;
            }
        }
	}
}
