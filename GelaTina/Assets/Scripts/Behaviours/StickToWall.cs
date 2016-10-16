using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Stick To Wall")]
public class StickToWall : AbstractBehaviour
{
    public bool onWallDetected;

    protected float defaultGravityScale;
    protected float defaultDrag;

    void Start()
    {
        defaultGravityScale = _body2d.gravityScale;
        defaultDrag = _body2d.drag;
    }

    protected virtual void Update()
    {
        if (_collisionState.onWall && !_collisionState.grounded)
        {
            if (!onWallDetected)
            {
                OnStick();
                ToggleScripts(false);
                onWallDetected = true;
            }
        }
        else
        {
            if (onWallDetected)
            {
                OffWall();
                ToggleScripts(true);
                onWallDetected = false;
            }
        }
    }

    protected virtual void OnStick()
    {
        if (!_collisionState.grounded && _body2d.velocity.y > 0)
        {
            _body2d.gravityScale = 0;
            _body2d.drag = 100;
        }
    }

    protected virtual void OffWall()
    {
        if (_body2d.gravityScale != defaultGravityScale)
        {
            _body2d.gravityScale = defaultGravityScale;
            _body2d.drag = defaultDrag;
        }
    }
}
