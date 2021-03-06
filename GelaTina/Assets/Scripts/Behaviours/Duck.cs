﻿using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Duck")]
public class Duck : AbstractBehaviour {

    public float scale = 0.5f;
    public bool isDucking;
    public float centerOffsetY = 0;

    private CircleCollider2D _circleCollider;
    private Vector2 _originalCenter;

    override protected void Awake()
    {
        base.Awake();

        _circleCollider = GetComponent<CircleCollider2D>();
        _originalCenter = _circleCollider.offset;
    }

    protected virtual void OnDuck(bool value)
    {
        isDucking = value;

        ToggleScripts(!isDucking);

        var size = _circleCollider.radius;

        float newOffsetY;
        float sizeReciprocal;

        if (isDucking)
        {
            sizeReciprocal = scale;
            newOffsetY = _circleCollider.offset.y - size / 2 + centerOffsetY;
        }
        else
        {
            sizeReciprocal = 1 / scale;
            newOffsetY = _originalCenter.y;
        }

        size = size * sizeReciprocal;
        _circleCollider.radius = size;
        _circleCollider.offset = new Vector2(_circleCollider.offset.x, newOffsetY);
    }

	void Update () {
        var canDuck = _inputState.GetButtonValue(inputButtons[0]);

        if(canDuck && _collisionState.grounded && !isDucking)
        {
            OnDuck(true);
        }
        else if(isDucking && !canDuck)
        {
            OnDuck(false);
        }
	}
}
