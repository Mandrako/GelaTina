using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class LaserBeam : MonoBehaviour
{
    public string targetTag = "Player";
    public Vector3 startPoint
    {
        get { return _startPoint; }
        set {
            _startPoint = value;
            CalculateBeam();
        }
    }
    public Vector3 endPoint
    {
        get { return _endPoint; }
        set
        {
            _endPoint = value;
            CalculateBeam();
        }
    }

    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private Sprite _sprite;

    void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Start()
    {
        CalculateBeam();
    }

    void CalculateBeam()
    {
        Vector3 direction = (_startPoint - _endPoint).normalized;
        Vector3 midPoint = Vector3.Lerp(_startPoint, _endPoint, 0.5f);
        float distance = Vector3.Distance(_startPoint, _endPoint);

        transform.position = midPoint;

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        transform.localScale = new Vector3(distance / _sprite.texture.width, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == targetTag)
        {
            Death dieBehaviour = target.gameObject.GetComponent<Death>();

            if(dieBehaviour != null)
            {
                dieBehaviour.Die();
            }
        }
    }
}
