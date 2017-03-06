using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [Range(0f, 2f)]
    public float fadeSpeed;

    private SpriteRenderer _renderer2D;
    private Color _start;
    private Color _end;
    private float _t = 0f;

    void Start()
    {
        _renderer2D = GetComponent<SpriteRenderer>();
        _start = _renderer2D.color;
        _end = new Color(_start.r, _start.g, _start.b, 0f);
    }

    void Update()
    {
        _t += Time.deltaTime * fadeSpeed;
        _renderer2D.material.color = Color.Lerp(_start, _end, _t);

        if(_renderer2D.material.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
