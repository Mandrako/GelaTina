using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Fire Projectile")]
public class FireProjectile : AbstractBehaviour {

    public float shootDelay = .5f;
    public GameObject projectilePrefab;
    public Vector2 firePosition = Vector2.zero;
    public Color debugColor = Color.yellow;
    public float debugRadius = 3f;

    private float _timeElapsed = 0;

	void Update () {
	    if(projectilePrefab != null)
        {
            var canFire = _inputState.GetButtonValue(inputButtons[0]);

            if(canFire && _timeElapsed > shootDelay)
            {
                CreateProjectile(CalculateFirePosition());
                _timeElapsed = 0;
            }

            _timeElapsed += Time.deltaTime;
        }
	}

    Vector2 CalculateFirePosition()
    {
        var pos = firePosition;
        pos.x *= (float)_inputState.direction;
        pos.x += transform.position.x;
        pos.y += transform.position.y;
        return pos;
    }

    public void CreateProjectile(Vector2 pos)
    {
        var clone = Instantiate(projectilePrefab, pos, Quaternion.identity) as GameObject;
        clone.transform.localScale = transform.localScale;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = debugColor;

        var pos = firePosition;

        if (_inputState != null)
        {
            pos.x *= (float)_inputState.direction;

        }

        pos.x += transform.position.x;
        pos.y += transform.position.y;

        Gizmos.DrawWireSphere(pos, debugRadius);
    }
}
