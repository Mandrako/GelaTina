using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Equip")]
public class Equip : AbstractBehaviour {

    private int _currentItem = 0;
    private Animator _animator;
    private float _originalRadius;
    private CircleCollider2D _circleCollider;

    protected override void Awake()
    {
        base.Awake();
        _circleCollider = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _originalRadius = _circleCollider.radius;
    }

    public int currentItem
    {
        get { return _currentItem; }
        set
        {
            _currentItem = value;
            _animator.SetInteger("EquippedItem", _currentItem);
        }
    }
}
