﻿using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public DoorTrigger[] doorTriggers;
    public Laser[] lasers;
    public bool isSticky;

    private bool _isDown;
    private Animator _animator;
    private Collider2D _lastTriggerer;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Deadly")
            return;

        if (_lastTriggerer == null)
        {
            _lastTriggerer = target;
        }

        _isDown = true;
        _animator.SetInteger("AnimState", 1);

        foreach (var trigger in doorTriggers)
        {
            if (trigger != null)
            {
                trigger.Toggle(true);
            }
        }

        foreach(var laser in lasers)
        {
            if(laser != null)
            {
                laser.Toggle(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if ((isSticky && _isDown) || target != _lastTriggerer)
            return;

        _isDown = false;

        _animator.SetInteger("AnimState", 2);

        foreach (var trigger in doorTriggers)
        {
            if (trigger != null)
            {
                trigger.Toggle(false);
            }
        }

        foreach (var laser in lasers)
        {
            if (laser != null)
            {
                laser.Toggle(true);
            }
        }

        _lastTriggerer = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = isSticky ? Color.red : Color.green;

        foreach(var trigger in doorTriggers)
        {
            if(trigger != null)
            {
                Gizmos.DrawLine(transform.position, trigger.door.transform.position);
            }
        }

        foreach (var laser in lasers)
        {
            if (laser != null)
            {
                Gizmos.DrawLine(transform.position, laser.transform.position);
            }
        }
    }
}
