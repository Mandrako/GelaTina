using UnityEngine;
using System.Collections;

public class PurplePotion : Collectable {

    public int itemId = 1;
    public float scaleToRadius = 8;

    protected override void OnCollect(GameObject target)
    {
        var equipBehaviour = target.GetComponent<Equip>();
        var duckBehaviour = target.GetComponent<Duck>();
        var wallslideBehaviour = target.GetComponent<WallSlide>();
        
        if(equipBehaviour != null)
        {
            equipBehaviour.currentItem = itemId;
        }

        if(duckBehaviour != null)
        {
            duckBehaviour.enabled = true;
        }

        if(wallslideBehaviour != null)
        {
            wallslideBehaviour.enabled = false;
        }

        var circleCollider = target.gameObject.GetComponent<CircleCollider2D>();

        if(circleCollider != null)
        {
            circleCollider.radius = scaleToRadius;
        }
    }

}
