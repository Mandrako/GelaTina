using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForward : MonoBehaviour
{
    public Transform sightStart, sightEnd;
    public string layer = "Solid";
    public bool needsCollision = true;

    private bool _collison;

    void Update()
    {
        _collison = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer(layer));

        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);

        if (_collison == needsCollision)
        {
            transform.localScale = new Vector3(transform.localScale.x == 1 ? -1 : 1, 1, 1);
        }
    }
}
