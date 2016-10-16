using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Walk")]
public class Walk : AbstractBehaviour {

    public float speed = 50f;
    public float runMultiplier = 2f;
    public bool isRunning;

	void Update () {
        isRunning = false;
        var right = _inputState.GetButtonValue(inputButtons[0]);
        var left = _inputState.GetButtonValue(inputButtons[1]);
        var run = _inputState.GetButtonValue(inputButtons[2]);

        if (right || left)
        {
            var tmpSpeed = speed;

            if (run && runMultiplier > 0)
            {
                isRunning = true;
                tmpSpeed *= runMultiplier;
            }

            var velX = tmpSpeed * (float)_inputState.direction;

            _body2d.velocity = new Vector2(velX, _body2d.velocity.y);
        }
    }
}
