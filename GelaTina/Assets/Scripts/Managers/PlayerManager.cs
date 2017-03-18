using UnityEngine;
using System.Collections;
using System;

public class PlayerManager : MonoBehaviour
{
    private InputState _inputState;
    private Walk _walkBehaviour;
    private Animator _animator;
    private CollisionState _collisionState;
    private Duck _duckBehaviour;

    void Awake()
    {
        _inputState = GetComponent<InputState>();
        _walkBehaviour = GetComponent<Walk>();
        _animator = GetComponent<Animator>();
        _collisionState = GetComponent<CollisionState>();
        _duckBehaviour = GetComponent<Duck>();
    }

    void Update()
    {
        if (_collisionState.grounded)
        {
            ChangeAnimationState(0);
        }

        if (_inputState.absValX > 0)
        {
            ChangeAnimationState(1);
        }

        if (!_collisionState.grounded && _inputState.absValY > 0)
        {
            ChangeAnimationState(2);
        }

        _animator.speed = (_walkBehaviour.isRunning && _collisionState.grounded) ? _walkBehaviour.runMultiplier : 1;

        if (_duckBehaviour.isDucking)
        {
            ChangeAnimationState(3);
        }

        if (!_collisionState.grounded && _collisionState.onWall)
        {
            ChangeAnimationState(4);
        }
    }

    void ChangeAnimationState(int value)
    {
        _animator.SetInteger("AnimState", value);
    }
}
