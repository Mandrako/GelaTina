using UnityEngine;
using System.Collections;

public class PowerUpCan : Collectable {

    public GameObject projectilePrefab;

    protected override void OnCollect(GameObject target)
    {
        var shootBehaviour = target.GetComponent<FireProjectile>();

        if(shootBehaviour != null)
        {
            shootBehaviour.projectilePrefab = projectilePrefab;
        }
    }
}
