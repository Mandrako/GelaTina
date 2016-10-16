﻿using UnityEngine;
using System.Collections;
using System;

public enum Buttons
{
    Right,
    Left,
    Up,
    Down,
    A,
    B,
    X,
    Y
}

public enum Condition
{
    GreaterThan,
    LessThan
}

[Serializable]
public class InputAxisState
{
    public string axisName;
    public float offValue;
    public Buttons button;
    public Condition condition;

    public bool value
    {
        get
        {
            float val = Input.GetAxis(axisName);

            switch (condition)
            {
                case Condition.GreaterThan:
                    return val > offValue;
                case Condition.LessThan:
                    return val < offValue;
            }

            return false;
        }
    }
}

public class InputManager : MonoBehaviour {

    public InputAxisState[] inputs;
    public InputState inputState;

	void Update () {
	    foreach(var input in inputs)
        {
            inputState.SetButtonValue(input.button, input.value);
        }
	}
}
