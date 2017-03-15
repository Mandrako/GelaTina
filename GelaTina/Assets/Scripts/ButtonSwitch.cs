using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public DoorTrigger[] doorTriggers;
    public bool isSticky;

    private bool _isDown;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        _isDown = true;
        _animator.SetInteger("AnimState", 1);

        foreach (var trigger in doorTriggers)
        {
            if (trigger != null)
            {
                trigger.Toggle(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (isSticky && _isDown)
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
    }
}
