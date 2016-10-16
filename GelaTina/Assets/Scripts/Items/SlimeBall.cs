using UnityEngine;
using System.Collections;

public class SlimeBall : MonoBehaviour {

    public Vector2 initialVelocity = new Vector2(100, -100);
    public int bounces = 3;

    private Rigidbody2D _body2d;

    void Awake()
    {
        _body2d = GetComponent<Rigidbody2D>();
    }

	void Start () {
        var startVelX = initialVelocity.x * transform.localScale.x;

        _body2d.velocity = new Vector2(startVelX, initialVelocity.y);
	}

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.transform.position.y < transform.position.y)
        {
            bounces--;
        }

        if(bounces <= 0)
        {
            Destroy(gameObject);
        }
    }
}
