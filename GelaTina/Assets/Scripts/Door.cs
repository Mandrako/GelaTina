using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float closeDelay = .5f;

    Animator _animator;
    BoxCollider2D _collider2D;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {

    }

    void EnableCollider2D()
    {
        _collider2D.enabled = true;
    }

    void DisableCollider2D()
    {
        _collider2D.enabled = false;
    }

    public void Open()
    {
        _animator.SetInteger("AnimState", 1);
    }

    public void Close()
    {
        StartCoroutine(CloseNow());
    }

    IEnumerator CloseNow()
    {
        yield return new WaitForSeconds(closeDelay);
        _animator.SetInteger("AnimState", 2);
    }
}
