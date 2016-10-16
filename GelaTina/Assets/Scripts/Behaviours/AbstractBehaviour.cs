using UnityEngine;
using System.Collections;

public abstract class AbstractBehaviour : MonoBehaviour {

    public Buttons[] inputButtons;
    public MonoBehaviour[] disableScripts;

    protected InputState _inputState;
    protected Rigidbody2D _body2d;
    protected CollisionState _collisionState;

    protected virtual void Awake()
    {
        _inputState = GetComponent<InputState>();
        _body2d = GetComponent<Rigidbody2D>();
        _collisionState = GetComponent<CollisionState>();
    }

    protected virtual void ToggleScripts(bool value)
    {
        foreach(var script in disableScripts)
        {
            script.enabled = value;
        }
    }
}
