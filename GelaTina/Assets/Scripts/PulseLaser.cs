using UnityEngine;
using System.Collections;

public class PulseLaser : Laser
{
    public float IntervalLength = 5f;

    private float _pulseTimer = 0f;

    protected override void Update()
    {
        base.Update();

        _pulseTimer -= Time.deltaTime;
        if (_pulseTimer <= 0)
        {
            _isActive = !_isActive;
            _pulseTimer = IntervalLength;
        }
    }
}
