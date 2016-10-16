using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Jump")]
public class Jump : AbstractBehaviour {

    public float jumpSpeed = 200f;
    public float jumpDelay = 0.1f;
    public int jumpCount = 2;
    public GameObject dustEffectPrefab;

    protected float lastJumpTime = 0;
    protected int jumpsRemaining = 0;

	void Start () {
	
	}
	
	protected virtual void Update () {
        var canJump = _inputState.GetButtonValue(inputButtons[0]);
        var holdTime = _inputState.GetButtonHoldTime(inputButtons[0]);

        if (_collisionState.grounded)
        {
            if (canJump && holdTime < 0.1f)
            {
                jumpsRemaining = jumpCount - 1;
                OnJump();
            }
        }
        else
        {
            if (canJump && holdTime < 0.1f && Time.time - lastJumpTime > jumpDelay)
            {
                if(jumpsRemaining > 0)
                {
                    OnJump();
                    jumpsRemaining--;
                    var clone = Instantiate(dustEffectPrefab);
                    clone.transform.position = transform.position;
                }
            }
        }
	}

    protected virtual void OnJump()
    {
        var vel = _body2d.velocity;

        lastJumpTime = Time.time;

        _body2d.velocity = new Vector2(vel.x, jumpSpeed);
    }
}
