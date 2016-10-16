using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Death")]
public class Death : AbstractBehaviour {

    public void Die()
    {
        OnDeath();
    }

	protected virtual void OnDeath () {
        Destroy(gameObject);
	}
}
