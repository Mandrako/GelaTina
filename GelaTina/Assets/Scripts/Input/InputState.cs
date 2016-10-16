using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonState
{
    public bool value;
    public float holdTime = 0;
}

public enum Directions
{
    Right = 1,
    Left = -1
}

public class InputState : MonoBehaviour {

    public Directions direction = Directions.Right;
    public float absValX = 0f;
    public float absValY = 0f;

    private Rigidbody2D _body2d;
    private Dictionary<Buttons, ButtonState> _buttonStates = new Dictionary<Buttons, ButtonState>();

    void Awake()
    {
        _body2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        absValX = Mathf.Abs(_body2d.velocity.x);
        absValY = Mathf.Abs(_body2d.velocity.y);
    }

    public void SetButtonValue(Buttons key, bool value)
    {
        if (!_buttonStates.ContainsKey(key))
        {
            _buttonStates.Add(key, new ButtonState());
        }

        var state = _buttonStates[key];

        if(state.value && !value)
        {
            state.holdTime = 0;
        }
        else if(state.value && value)
        {
            state.holdTime += Time.deltaTime;
        }

        state.value = value;
    }

    public bool GetButtonValue(Buttons key)
    {
        if (_buttonStates.ContainsKey(key))
        {
            return _buttonStates[key].value;
        }
        else
        {
            return false;
        }
    }

    public float GetButtonHoldTime(Buttons key)
    {
        if (_buttonStates.ContainsKey(key))
        {
            return _buttonStates[key].holdTime;
        }
        else
        {
            return 0;
        }
    }
}
