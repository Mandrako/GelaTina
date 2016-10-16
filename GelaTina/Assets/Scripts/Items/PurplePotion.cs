using UnityEngine;
using System.Collections;

public class PurplePotion : Collectable {

    public int itemId = 1;
    public float scaleToRadius = 8;

    protected override void OnCollect(GameObject target)
    {
        var equipBehaviour = target.GetComponent<Equip>();
        
        if(equipBehaviour != null)
        {
            equipBehaviour.currentItem = itemId;
        }

        var circleCollider = target.gameObject.GetComponent<CircleCollider2D>();

        if(circleCollider != null)
        {
            circleCollider.radius = scaleToRadius;
        }
    }

}
