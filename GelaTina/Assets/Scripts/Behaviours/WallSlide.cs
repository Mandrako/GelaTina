using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Wall Slide")]
public class WallSlide : StickToWall
{
    public float slideVelocity = -5f;
    public float slideMultiplier = 5f;
    public GameObject dustPrefab;
    public float dustSpawnDelay = 0.5f;

    private float _timeElapsed = 0f;
    private CircleCollider2D _circleCollider;

    protected override void Awake()
    {
        base.Awake();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    protected override void Update()
    {
        base.Update();

        if (onWallDetected && !_collisionState.grounded)
        {
            var velY = slideVelocity;

            if (_inputState.GetButtonValue(inputButtons[0]))
            {
                velY *= slideMultiplier;
            }

            _body2d.velocity = new Vector2(_body2d.velocity.x, velY);

            if(_timeElapsed > dustSpawnDelay)
            {
                var dust = Instantiate(dustPrefab);
                var pos = transform.position;
                pos.y += 2;
                pos.x += _circleCollider.radius * (float)_inputState.direction;
                dust.transform.position = pos;
                dust.transform.localScale = transform.localScale;
                _timeElapsed = 0;
            }

            _timeElapsed += Time.deltaTime;
        }
    }

    protected override void OnStick()
    {
        _body2d.velocity = Vector2.zero;
    }

    protected override void OffWall()
    {
        _timeElapsed = 0;
    }

}
